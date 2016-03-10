﻿using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Xml.Linq;
using PDS.Framework;

namespace PDS.Witsml.Server.Configuration
{
    /// <summary>
    /// Provides common validation for the main WITSML Store API methods.
    /// </summary>
    public abstract class WitsmlValidator
    {
        /// <summary>
        /// Validates the WITSML Store API request.
        /// </summary>
        /// <param name="providers">The capServer providers.</param>
        /// <param name="context">The request context.</param>
        /// <param name="version">The data schema version.</param>
        /// <exception cref="WitsmlException"></exception>
        public static void ValidateRequest(IEnumerable<ICapServerProvider> providers, RequestContext context, out string version)
        {
            ValidateUserAgent(WebOperationContext.Current);
            var document = ValidateInputTemplate(context.Xml);

            var dataSchemaVersion = ObjectTypes.GetVersion(document);
            ValidateDataSchemaVersion(dataSchemaVersion);

            var capServerProvider = providers.FirstOrDefault(x => x.DataSchemaVersion == dataSchemaVersion);
            if (capServerProvider == null)
            {
                throw new WitsmlException(ErrorCodes.DataObjectNotSupported,
                    "WITSML object type not supported: " + context.ObjectType + "; Version: " + dataSchemaVersion);
            }

            capServerProvider.ValidateRequest(context, document);
            version = dataSchemaVersion;
        }

        /// <summary>
        /// Determines whether the specified function is supported for the object type.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// true if the WITSML Store supports the function for the specified object type, otherwise, false
        /// </returns>
        public abstract bool IsSupported(Functions function, string objectType);

        /// <summary>
        /// Performs validation for the specified function and supplied parameters.
        /// </summary>
        /// <param name="context">The request context.</param>
        /// <param name="document">The XML document.</param>
        public virtual void ValidateRequest(RequestContext context, XDocument document)
        {
            ValidateNamespace(document);
            ValidateObjectType(context.Function, context.ObjectType, ObjectTypes.GetObjectType(document));
            ValidatePluralRootElement(context.ObjectType, document);
        }

        /// <summary>
        /// Validates the required User-Agent header is supplied by the client.
        /// </summary>
        /// <param name="context">The web operation context.</param>
        /// <exception cref="WitsmlException">Thrown if the User-Agent header is missing.</exception>
        public static void ValidateUserAgent(WebOperationContext context)
        {
            if (context != null && context.IncomingRequest != null && string.IsNullOrWhiteSpace(context.IncomingRequest.UserAgent))
            {
                throw new WitsmlException(ErrorCodes.MissingClientUserAgent);
            }
        }

        /// <summary>
        /// Validates the required WITSML input template.
        /// </summary>
        /// <param name="xml">The XML input template.</param>
        /// <exception cref="WitsmlException"></exception>
        public static XDocument ValidateInputTemplate(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new WitsmlException(ErrorCodes.MissingInputTemplate);
            }

            XDocument doc = WitsmlParser.Parse(xml);

            if (string.IsNullOrEmpty(GetNamespace(doc)))
            {
                throw new WitsmlException(ErrorCodes.MissingDefaultWitsmlNamespace);
            }

            return doc;
        }

        /// <summary>
        /// Validates the required data schema version has been specified.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <exception cref="WitsmlException"></exception>
        public static void ValidateDataSchemaVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                throw new WitsmlException(ErrorCodes.MissingDataSchemaVersion);
            }
        }

        /// <summary>
        /// Validates the required plural root element.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="document">The XML document.</param>
        /// <exception cref="WitsmlException"></exception>
        public static void ValidatePluralRootElement(string objectType, XDocument document)
        {
            var objectGroupType = ObjectTypes.GetObjectGroupType(document);
            var pluralObjectType = objectType + "s";

            if (objectGroupType != pluralObjectType)
            {
                throw new WitsmlException(ErrorCodes.MissingPluralRootElement);
            }
        }

        /// <summary>
        /// Validates the non-empty root element.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="document">The XML document.</param>
        /// <exception cref="WitsmlException"></exception>
        public static void ValidateEmptyRootElement(string objectType, XDocument document)
        {
            if (!document.Root.Elements().Any())
            {
                throw new WitsmlException(ErrorCodes.InputTemplateNonConforming);
            }
        }

        /// <summary>
        /// Validates the required singular root element.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="document">The XML document.</param>
        /// <exception cref="WitsmlException"></exception>
        public static void ValidateSingleChildElement(string objectType, XDocument document)
        {
            if (document.Root.Elements().Count() > 1)
            {
                throw new WitsmlException(ErrorCodes.InputTemplateMultipleDataObjects);
            }
        }

        /// <summary>
        /// Validates the options are supported.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="keywords">The supported keywords.</param>
        /// <exception cref="WitsmlException"></exception>
        public static void ValidateKeywords(Dictionary<string, string> options, params string[] keywords)
        {
            foreach (var option in options.Where(x => !keywords.Contains(x.Key)))
            {
                throw new WitsmlException(ErrorCodes.KeywordNotSupportedByFunction, "Option not supported: " + option.Key);
            }
        }

        /// <summary>
        /// Validates the supported compressionMethod configuration option.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="compressionMethod">The compression method.</param>
        /// <exception cref="WitsmlException"></exception>
        public static void ValidateCompressionMethod(Dictionary<string, string> options, string compressionMethod)
        {
            string optionValue;
            if (!options.TryGetValue(OptionsIn.CompressionMethod.Keyword, out optionValue))
            {
                return;
            }

            // Validate compression method value
            if (!OptionsIn.CompressionMethod.None.Equals(optionValue) &&
                !OptionsIn.CompressionMethod.Gzip.Equals(optionValue))
            {
                throw new WitsmlException(ErrorCodes.InvalidKeywordValue);
            }

            // Validate compression method is supported
            if (!OptionsIn.CompressionMethod.None.Equals(optionValue) &&
                !optionValue.EqualsIgnoreCase(compressionMethod))
            {
                throw new WitsmlException(ErrorCodes.KeywordNotSupportedByServer);
            }
        }

        /// <summary>
        /// Validates the requestObjectSelectionCapability option.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="document">The document.</param>
        /// <exception cref="WitsmlException">
        /// </exception>
        public void ValidateRequestObjectSelectionCapability(Dictionary<string, string> options, string objectType, XDocument document)
        {
            string optionValue;
            if (!options.TryGetValue(OptionsIn.RequestObjectSelectionCapability.Keyword, out optionValue))
            {
                return;
            }

            // Validate value 
            if (!OptionsIn.RequestObjectSelectionCapability.None.Equals(optionValue) &&
                !OptionsIn.RequestObjectSelectionCapability.True.Equals(optionValue))
            {
                throw new WitsmlException(ErrorCodes.InvalidKeywordValue);
            }

            // No other options should be specified if value is true
            if (OptionsIn.RequestObjectSelectionCapability.True.Equals(optionValue))              
            {
                if (options.Count != 1)
                {
                    throw new WitsmlException(ErrorCodes.InvalidOptionsInCombination);
                }

                if (!document.Root.Elements().Any())
                {
                    throw new WitsmlException(ErrorCodes.InvalidMinimumQueryTemplate);
                }
            }
        }

        /// <summary>
        /// Validates the returnElements option.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <exception cref="WitsmlException">
        /// </exception>
        public void ValidateReturnElements(Dictionary<string, string> options, string objectType)
        {
            string optionValue;
            if (!options.TryGetValue(OptionsIn.ReturnElements.Keyword, out optionValue))
            {
                return;
            }

            // Validate value 
            if (!OptionsIn.ReturnElements.GetValues().Any(x => x.Equals(optionValue)))
            {
                throw new WitsmlException(ErrorCodes.InvalidKeywordValue);
            }

            // HeaderOnly and DataOnly options are for growing data object only
            if ((OptionsIn.ReturnElements.HeaderOnly.Equals(optionValue) || OptionsIn.ReturnElements.DataOnly.Equals(optionValue))
                && !ObjectTypes.IsGrowingDataObject(objectType))
            {
                throw new WitsmlException(ErrorCodes.InvalidOptionForGrowingObjectOnly);
            }

            // Latest-Change-Only option is for ChangeLog only
            if ((OptionsIn.ReturnElements.LatestChangeOnly.Equals(optionValue) && !objectType.Equals(ObjectTypes.ChangeLog)))
            {
                throw new WitsmlException(ErrorCodes.InvalidOptionForChangeLogOnly);
            }
        }

        /// <summary>
        /// Validates the required WITSML object type parameter for the WMLS_AddToStore method.
        /// </summary>
        /// <param name="function">The WITSML Store API function.</param>
        /// <param name="objectType">The type of data object.</param>
        /// <param name="xmlType">The type of data object in the XML.</param>
        /// <exception cref="WitsmlException"></exception>
        protected void ValidateObjectType(Functions function, string objectType, string xmlType)
        {
            if (string.IsNullOrWhiteSpace(objectType))
            {
                throw new WitsmlException(ErrorCodes.MissingWMLtypeIn);
            }

            // Not sure why these are only checked for AddToStore?
            if (function == Functions.AddToStore)
            {
                if (!objectType.Equals(xmlType))
                {
                    throw new WitsmlException(ErrorCodes.DataObjectTypesDontMatch);
                }

                if (!IsSupported(function, objectType))
                {
                    throw new WitsmlException(ErrorCodes.DataObjectTypeNotSupported);
                }
            }
        }

        /// <summary>
        /// Validates the namespace for a specific WITSML data schema version.
        /// </summary>
        /// <param name="document">The document.</param>
        protected virtual void ValidateNamespace(XDocument document)
        {
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <param name="xml">The XML document.</param>
        /// <returns>The namespace.</returns>
        protected static string GetNamespace(XDocument document)
        {
            return document.Root.GetDefaultNamespace().NamespaceName;
        }

    }
}
