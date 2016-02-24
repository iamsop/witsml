﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PDS.Witsml.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PDS.Witsml.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In WMLS_AddToStore, the objectType being added in WMLtypeIn must be an objectType supported by the server. The server does not support the object type trying to be added..
        /// </summary>
        internal static string DataObjectTypeNotSupported {
            get {
                return ResourceManager.GetString("DataObjectTypeNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In WMLS_AddToStore, the WMLtypeIn objectType must match the XMLin objectType. Currently, they do not match..
        /// </summary>
        internal static string DataObjectTypesDontMatch {
            get {
                return ResourceManager.GetString("DataObjectTypesDontMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to For WMLS_AddToStore, a data-object with the same type and unique identifier(s) must NOT already exist in the persistent store.
        /// </summary>
        internal static string DataObjectUidAlreadyExists {
            get {
                return ResourceManager.GetString("DataObjectUidAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to For WMLS_GetCap, the OptionsIn keyword ‘dataVersion’ must specify a Data Schema Version that is supported by the server as defined by WMLS_GetVersion..
        /// </summary>
        internal static string DataVersionNotSupported {
            get {
                return ResourceManager.GetString("DataVersionNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error adding to data store.
        /// </summary>
        internal static string ErrorAddingToDataStore {
            get {
                return ResourceManager.GetString("ErrorAddingToDataStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error deleting from data store.
        /// </summary>
        internal static string ErrorDeletingFromDataStore {
            get {
                return ResourceManager.GetString("ErrorDeletingFromDataStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error reading from data store.
        /// </summary>
        internal static string ErrorReadingFromDataStore {
            get {
                return ResourceManager.GetString("ErrorReadingFromDataStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error updating in data store.
        /// </summary>
        internal static string ErrorUpdatingInDataStore {
            get {
                return ResourceManager.GetString("ErrorUpdatingInDataStore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value specified with an OptionsIn keyword must be a recognized value for that keyword..
        /// </summary>
        internal static string InvalidKeywordValue {
            get {
                return ResourceManager.GetString("InvalidKeywordValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The OptionsIn value must be a recognized keyword for that function..
        /// </summary>
        internal static string KeywordNotSupportedByFunction {
            get {
                return ResourceManager.GetString("KeywordNotSupportedByFunction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A client must not specify an OptionsIn keyword that is not supported by the server..
        /// </summary>
        internal static string KeywordNotSupportedByServer {
            get {
                return ResourceManager.GetString("KeywordNotSupportedByServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to For WMLS_GetCap, the OptionsIn keyword ‘dataVersion’ must be specified..
        /// </summary>
        internal static string MissingDataVersion {
            get {
                return ResourceManager.GetString("MissingDataVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-empty value must be defined for the input template..
        /// </summary>
        internal static string MissingInputTemplate {
            get {
                return ResourceManager.GetString("MissingInputTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parent does not exist.
        /// </summary>
        internal static string MissingParentDataObject {
            get {
                return ResourceManager.GetString("MissingParentDataObject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The input template MUST contain a plural root element..
        /// </summary>
        internal static string MissingPluralRootElement {
            get {
                return ResourceManager.GetString("MissingPluralRootElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-empty value must be defined for WMLtypeIn..
        /// </summary>
        internal static string MissingWMLtypeIn {
            get {
                return ResourceManager.GetString("MissingWMLtypeIn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The OptionsIn parameter string must be encoded utilizing a subset (semicolon separators and no
        /// whitespace) of the encoding rules for HTML form content type application/x-www-form-urlencoded..
        /// </summary>
        internal static string ParametersNotEncodedByRules {
            get {
                return ResourceManager.GetString("ParametersNotEncodedByRules", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Partial success: Function completed successfully but some growing data-object data-nodes were not returned..
        /// </summary>
        internal static string ParialSuccess {
            get {
                return ResourceManager.GetString("ParialSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Function completed successfully.
        /// </summary>
        internal static string Success {
            get {
                return ResourceManager.GetString("Success", resourceCulture);
            }
        }
    }
}
