﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Energistics.DataAccess;
using Energistics.DataAccess.WITSML141;
using Energistics.DataAccess.WITSML141.ComponentSchemas;
using Energistics.DataAccess.WITSML141.ReferenceData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDS.Witsml.Server.Configuration;

namespace PDS.Witsml.Server.Data.Wells
{
    [TestClass]
    public class Well141QueryTests
    {
        private DevKit141Aspect DevKit;

        [TestInitialize]
        public void TestSetUp()
        {
            DevKit = new DevKit141Aspect();

            DevKit.Store.CapServerProviders = DevKit.Store.CapServerProviders
                .Where(x => x.DataSchemaVersion == OptionsIn.DataVersion.Version141.Value)
                .ToArray();
        }

        [TestMethod]
        public void Test_return_element_all()
        {
            var well = CreateTestWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var query = new Well { Uid = uid };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.AreEqual(1, result.Count);
            var returnWell = result.FirstOrDefault();
            AssertTestWell(well, returnWell);
        }

        [TestMethod]
        public void Test_return_element_id_only()
        {
            var well = CreateTestWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var query = new Well { Uid = uid };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.AreEqual(1, result.Count);
            var returnWell = result.FirstOrDefault();
            AssertTestWell(well, returnWell);

            query = new Well { Uid = uid };
            var queryIn = EnergisticsConverter.ObjectToXml(new WellList { Well = new List<Well> { query } });
            var xmlOut = DevKit.GetFromStore(ObjectTypes.Well, queryIn, null, optionsIn: OptionsIn.ReturnElements.IdOnly).XMLout;
            var context = new RequestContext(Functions.GetFromStore, ObjectTypes.Well, xmlOut, null, null);
            var parser = new WitsmlQueryParser(context);
            Assert.IsFalse(parser.HasElements("wellDatum"));

            var wellList = EnergisticsConverter.XmlToObject<WellList>(xmlOut);
            Assert.AreEqual(1, wellList.Well.Count);
            returnWell = wellList.Well.FirstOrDefault();

            Assert.AreEqual(well.Name, returnWell.Name);
            Assert.IsNull(returnWell.DateTimeSpud);
            Assert.IsNull(returnWell.GroundElevation);
        }

        [TestMethod]
        public void Test_return_element_id_only_with_additional_elements()
        {
            var well = CreateTestWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var query = new Well { Uid = uid };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.AreEqual(1, result.Count);
            var returnWell = result.FirstOrDefault();
            AssertTestWell(well, returnWell);

            query = new Well { Uid = uid, Country = string.Empty, CommonData = new CommonData() };
            var queryIn = EnergisticsConverter.ObjectToXml(new WellList { Well = new List<Well> { query } });
            var xmlOut = DevKit.GetFromStore(ObjectTypes.Well, queryIn, null, optionsIn: OptionsIn.ReturnElements.IdOnly).XMLout;
            var context = new RequestContext(Functions.GetFromStore, ObjectTypes.Well, xmlOut, null, null);
            var parser = new WitsmlQueryParser(context);

            Assert.IsFalse(parser.HasElements("country"));
            Assert.IsFalse(parser.HasElements("wellDatum"));
            Assert.IsFalse(parser.HasElements("commonData"));

            var wellList = EnergisticsConverter.XmlToObject<WellList>(xmlOut);
            Assert.AreEqual(1, wellList.Well.Count);
            returnWell = wellList.Well.FirstOrDefault();

            Assert.AreEqual(well.Name, returnWell.Name);
            Assert.IsNull(returnWell.DateTimeSpud);
            Assert.IsNull(returnWell.GroundElevation);
        }

        [TestMethod]
        public void Test_return_element_default()
        {
            var well = CreateTestWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var query = new Well { Uid = uid };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.AreEqual(1, result.Count);
            var returnWell = result.FirstOrDefault();
            AssertTestWell(well, returnWell);

            query = new Well { Uid = uid, WellDatum = new List<WellDatum> { new WellDatum() } };
            var queryIn = EnergisticsConverter.ObjectToXml(new WellList { Well = new List<Well> { query } });
            var xmlOut = DevKit.GetFromStore(ObjectTypes.Well, queryIn, null, null).XMLout;
            var context = new RequestContext(Functions.GetFromStore, ObjectTypes.Well, xmlOut, null, null);
            var parser = new WitsmlQueryParser(context);
            Assert.IsFalse(parser.HasElements("name"));

            var wellList = EnergisticsConverter.XmlToObject<WellList>(xmlOut);
            Assert.AreEqual(1, wellList.Well.Count);
            returnWell = wellList.Well.FirstOrDefault();

            Assert.IsNull(returnWell.DateTimeSpud);
            Assert.IsNull(returnWell.GroundElevation);
            Assert.IsNull(returnWell.CommonData);

            foreach (var datum in well.WellDatum)
            {
                var returnDatum = returnWell.WellDatum.FirstOrDefault(d => d.Uid == datum.Uid);
                Assert.IsNotNull(returnDatum);
                Assert.AreEqual(datum.Code, returnDatum.Code);
            }
        }

        [TestMethod]
        public void Test_return_element_requested()
        {
            var well = CreateTestWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var query = new Well { Uid = uid };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.AreEqual(1, result.Count);
            var returnWell = result.FirstOrDefault();
            AssertTestWell(well, returnWell);

            query = new Well { Uid = uid, CommonData = new CommonData { Comments = string.Empty } };
            var queryIn = EnergisticsConverter.ObjectToXml(new WellList { Well = new List<Well> { query } });
            var xmlOut = DevKit.GetFromStore(ObjectTypes.Well, queryIn, null, optionsIn: OptionsIn.ReturnElements.Requested).XMLout;
            var context = new RequestContext(Functions.GetFromStore, ObjectTypes.Well, xmlOut, null, null);
            var parser = new WitsmlQueryParser(context);

            Assert.IsFalse(parser.HasElements("name"));
            Assert.IsFalse(parser.HasElements("wellDatum"));

            var wellList = EnergisticsConverter.XmlToObject<WellList>(xmlOut);
            Assert.AreEqual(1, wellList.Well.Count);
            returnWell = wellList.Well.FirstOrDefault();

            Assert.IsNull(returnWell.DateTimeSpud);
            Assert.IsNull(returnWell.GroundElevation);

            var commonData = returnWell.CommonData;

            Assert.IsNotNull(commonData);
            Assert.IsFalse(string.IsNullOrEmpty(commonData.Comments));
            Assert.IsNull(commonData.DateTimeLastChange);
        }

        [TestMethod]
        public void Test_Well_Selection_Uid_ReturnElement_All()
        {
            var well = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;
            var query = new Well { Uid = uid };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.AreEqual(1, result.Count);
            var returnWell = result.FirstOrDefault();
           
            well.Uid = uid;
            well.CommonData.DateTimeLastChange = returnWell.CommonData.DateTimeLastChange;                        
            string wellXml = EnergisticsConverter.ObjectToXml(well);
            string returnXml = EnergisticsConverter.ObjectToXml(returnWell);

            Assert.AreEqual(wellXml, returnXml);
        }

        [TestMethod]
        public void Test_Well_Selection_Uid_Caseless_Compare()
        {
            var testUid = "test well for Test_Well_Selection_Uid_Caseless_Compare";
            var query = new Well { Uid = testUid };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.IdOnly);

            if (result.Count == 0)
            {
                var well = CreateFullWell();
                well.Uid = testUid;
                var response = DevKit.Add<WellList, Well>(well);

                Assert.IsNotNull(response);
                Assert.AreEqual((short)ErrorCodes.Success, response.Result);
            }

            query = new Well { Uid = testUid.ToUpper()};
            result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.IsTrue(result.Where(x => x.Uid == testUid).Any());
        }

        [TestMethod]
        public void Test_Well_Selection_Different_Case()
        {
            var well = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;
            var query = new Well { Uid = "", Name = well.Name.ToLower(), NameLegal = well.NameLegal.ToUpper() };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.IsTrue(result.Where(x => x.Uid == uid).Any());
        }

        [TestMethod]
        public void Test_Well_Selection_Criteria_Not_Satisfied()
        {
            var dummy = "Dummy";
            var datumKB = DevKit.WellDatum(dummy);
            var query = new Well { Uid = dummy, Name = dummy, NameLegal = dummy, Country=dummy, County=dummy, WellDatum = DevKit.List(datumKB) };
            var result = DevKit.Get<WellList, Well>(DevKit.List(query), ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);
            Assert.IsNotNull(result);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result.XMLout);
            XmlElement wells = doc.DocumentElement;
            Assert.IsNotNull(wells);

            // Section 6.6.4
            Assert.AreEqual(ObjectTypes.SingleToPlural(ObjectTypes.Well), wells.Name);
            Assert.IsFalse(DevKit.HasChildNodes(wells));
        }

        [TestMethod]
        public void Test_Well_Selection_MultiQueries_Same_Object_Returned()
        {
            var well = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var datumKB = DevKit.WellDatum("Kelly Bushing");       
            var query1 = new Well { Uid = "", WellDatum = DevKit.List(datumKB) };
            var query2 = new Well { Uid = uid };
            var result = DevKit.Get<WellList, Well>(DevKit.List(query1, query2), ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.IsNotNull(result.XMLout);
            var resultWellList = EnergisticsConverter.XmlToObject<WellList>(result.XMLout);

            Assert.IsNotNull(resultWellList);
            var sameWellList = resultWellList.Items.Cast<Well>().Where(x => x.Uid == uid);

            // Section 6.6.4.1
            Assert.IsTrue(sameWellList.Count() > 1);
        }

        [TestMethod]
        public void Test_Well_Selection_MultiQueries_One_Query_Fails()
        {
            var well = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var datumKB = DevKit.WellDatum("Kelly Bushing", ElevCodeEnum.KB);
            var datumSL = DevKit.WellDatum(null, ElevCodeEnum.SL);

            var badWellQuery = new Well { Uid = "", WellDatum = DevKit.List(datumKB, datumSL) };
            var goodWellQuery = new Well { Uid = uid };

            var result = DevKit.Get<WellList, Well>(DevKit.List(goodWellQuery, badWellQuery), ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            // Section 6.6.4 
            Assert.AreEqual((short)ErrorCodes.RecurringItemsInconsistentSelection, result.Result);
        }

        [TestMethod]
        public void Test_Well_Selection_Not_Equal_Comparison_dTimCreation()
        {
            var well_01 = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well_01);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid_01 = response.SuppMsgOut;

            var well_02 = CreateFullWell();
            well_02.CommonData.DateTimeCreation = DateTime.UtcNow;
            response = DevKit.Add<WellList, Well>(well_02);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid_02 = response.SuppMsgOut;

            var query = new Well { CommonData = new CommonData() };
            query.CommonData.DateTimeCreation = well_01.CommonData.DateTimeCreation;
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            // Section 6.6.4
            Assert.IsTrue(result.Where(x => x.Uid == uid_02).Any());
            Assert.IsFalse(result.Where(x => x.Uid == uid_01).Any());
        }

        [TestMethod]
        public void Test_Well_Selection_Not_Equal_Comparison_dTimLastChange()
        {
            var well_01 = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well_01);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);
            var uid_01 = response.SuppMsgOut;

            var query = new Well { Uid = uid_01 };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(uid_01, result[0].Uid);

            var wellLastChangeTime = result[0].CommonData.DateTimeLastChange;

            var well_02 = CreateFullWell();
            well_02.CommonData.DateTimeCreation = DateTime.UtcNow;
            response = DevKit.Add<WellList, Well>(well_02);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);
            var uid_02 = response.SuppMsgOut;

            query = new Well { CommonData = new CommonData() };
            query.CommonData.DateTimeLastChange = wellLastChangeTime;
            result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);
            
            // Section 6.6.4
            Assert.IsTrue(result.Where(x => x.Uid == uid_02).Any());
            Assert.IsFalse(result.Where(x => x.Uid == uid_01).Any());
        }

        [TestMethod]
        public void Test_Well_Selection_Do_Not_Return_Empty_Values()
        {
            var well = CreateTestWell();
            Assert.IsNull(well.WaterDepth);
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var query = new Well { Uid = uid };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);
            Assert.AreEqual(1, result.Count);

            // Section 6.6.4.1 
            Assert.IsNull(result[0].WaterDepth);
        }

        [TestMethod]
        public void Test_Well_Selection_Recurring_Items()
        {
            var well = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;

            var datumKB = DevKit.WellDatum("Kelly Bushing");
            var datumSL = DevKit.WellDatum("Sea Level");
            var query = new Well { Uid = "", WellDatum = DevKit.List(datumKB,  datumSL) };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            Assert.IsTrue(result.Where(x => x.Uid == uid).Any());
        }

        [TestMethod]
        public void Test_Well_Selection_Recurring_Items_Criteria_OR()
        {
            var well_01 = CreateFullWell();
            well_01.WellDatum.RemoveAt(0);            
            var response = DevKit.Add<WellList, Well>(well_01);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);
            var uid_01 = response.SuppMsgOut;

            var well_02 = CreateFullWell();
            well_02.WellDatum.RemoveAt(1);
            response = DevKit.Add<WellList, Well>(well_02);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);
            var uid_02 = response.SuppMsgOut;

            var datumKB = DevKit.WellDatum("Kelly Bushing");
            var datumSL = DevKit.WellDatum("Sea Level");
            var query = new Well { WellDatum = DevKit.List(datumKB, datumSL) };
            var result = DevKit.Query<WellList, Well>(query, ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            // Section 4.1.5
            Assert.IsTrue(result.Where(x => x.Uid == uid_01).Any());
            Assert.IsTrue(result.Where(x => x.Uid == uid_02).Any());
        }

        [TestMethod]
        public void Test_Well_Selection_Recurring_Items_InconsistentSelection()
        {
            var well = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;
            var datumKB = DevKit.WellDatum("Kelly Bushing");
            var datumSL = DevKit.WellDatum(null, ElevCodeEnum.SL);
            var query = new Well { Uid = "", WellDatum = DevKit.List(datumKB, datumSL) };
            var result = DevKit.Get<WellList, Well>(DevKit.List(query), ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            // Section 4.1.5
            Assert.AreEqual((short)ErrorCodes.RecurringItemsInconsistentSelection, result.Result);
        }

        [TestMethod]
        public void Test_Well_Selection_Recurring_Items_EmptValue()
        {
            var well = CreateFullWell();
            var response = DevKit.Add<WellList, Well>(well);

            Assert.IsNotNull(response);
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var uid = response.SuppMsgOut;
            var datumKB = DevKit.WellDatum("Kelly Bushing");
            var datumSL = DevKit.WellDatum("");
            var query = new Well { Uid = "", WellDatum = DevKit.List(datumKB, datumSL) };
            var result = DevKit.Get<WellList, Well>(DevKit.List(query), ObjectTypes.Well, null, optionsIn: OptionsIn.ReturnElements.All);

            // Section 4.1.5
            Assert.AreEqual((short)ErrorCodes.RecurringItemsEmptySelection, result.Result);
        }

        private Well CreateFullWell()
        {
            string wellXml = "<wells xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\">" + Environment.NewLine +
            "<well>" + Environment.NewLine +
            "<name>PDS Full Test Well</name>" + Environment.NewLine +
            "<nameLegal>Company Legal Name</nameLegal>" + Environment.NewLine +
            "<numLicense>Company License Number</numLicense>" + Environment.NewLine +
            "<numGovt>Govt-Number</numGovt>" + Environment.NewLine +
            "<dTimLicense>2001-05-15T13:20:00Z</dTimLicense>" + Environment.NewLine +
            "<field>Big Field</field>" + Environment.NewLine +
            "<country>US</country>" + Environment.NewLine +
            "<state>TX</state>" + Environment.NewLine +
            "<county>Montgomery</county>" + Environment.NewLine +
            "<region>Region Name</region>" + Environment.NewLine +
            "<district>District Name</district>" + Environment.NewLine +
            "<block>Block Name</block>" + Environment.NewLine +
            "<timeZone>-06:00</timeZone>" + Environment.NewLine +
            "<operator>Operating Company</operator>" + Environment.NewLine +
            "<operatorDiv>Division Name</operatorDiv>" + Environment.NewLine +
            "<pcInterest uom=\"%\">65</pcInterest>" + Environment.NewLine +
            "<numAPI>123-543-987AZ</numAPI>" + Environment.NewLine +
            "<statusWell>drilling</statusWell>" + Environment.NewLine +
            "<purposeWell>exploration</purposeWell>" + Environment.NewLine +
            "<fluidWell>water</fluidWell>" + Environment.NewLine +
            "<dTimSpud>2001-05-31T08:15:00Z</dTimSpud>" + Environment.NewLine +
            "<dTimPa>2001-07-15T15:30:00Z</dTimPa>" + Environment.NewLine +
            "<wellheadElevation uom=\"ft\">500</wellheadElevation>" + Environment.NewLine +
            "<wellDatum uid=\"KB\">" + Environment.NewLine +
            "<name>Kelly Bushing</name>" + Environment.NewLine +
            "<code>KB</code>" + Environment.NewLine +
            "<elevation uom=\"ft\" datum=\"SL\">78.5</elevation>" + Environment.NewLine +
            "</wellDatum>" + Environment.NewLine +
            "<wellDatum uid=\"SL\">" + Environment.NewLine +
            "<name>Sea Level</name>" + Environment.NewLine +
            "<code>SL</code>" + Environment.NewLine +
            "<datumName namingSystem=\"EPSG\" code=\"5106\">Caspian Sea</datumName>" + Environment.NewLine +
            "</wellDatum>" + Environment.NewLine +
            "<groundElevation uom=\"ft\">250</groundElevation>" + Environment.NewLine +
            "<waterDepth uom=\"ft\">520</waterDepth>" + Environment.NewLine +
            "<wellLocation uid=\"loc-1\">" + Environment.NewLine +
            "<wellCRS uidRef=\"proj1\">ED50 / UTM Zone 31N</wellCRS>" + Environment.NewLine +
            "<easting uom=\"m\">425353.84</easting>" + Environment.NewLine +
            "<northing uom=\"m\">6623785.69</northing>" + Environment.NewLine +
            "<description>Location of well surface point in projected system.</description>" + Environment.NewLine +
            "</wellLocation>" + Environment.NewLine +
            "<referencePoint uid=\"SRP1\">" + Environment.NewLine +
            "<name>Slot Bay Centre</name>" + Environment.NewLine +
            "<type>Site Reference Point</type>" + Environment.NewLine +
            "<location uid=\"loc-1\">" + Environment.NewLine +
            "<wellCRS uidRef=\"proj1\">ED50 / UTM Zone 31N</wellCRS>" + Environment.NewLine +
            "<easting uom=\"m\">425366.47</easting>" + Environment.NewLine +
            "<northing uom=\"m\">6623781.95</northing>" + Environment.NewLine +
            "</location>" + Environment.NewLine +
            "<location uid=\"loc-2\">" + Environment.NewLine +
            "<wellCRS uidRef=\"localWell1\">WellOneWSP</wellCRS>" + Environment.NewLine +
            "<localX uom=\"m\">12.63</localX>" + Environment.NewLine +
            "<localY uom=\"m\">-3.74</localY>" + Environment.NewLine +
            "<description>Location of the Site Reference Point with respect to the well surface point</description>" + Environment.NewLine +
            "</location>" + Environment.NewLine +
            "</referencePoint>" + Environment.NewLine +
            "<referencePoint uid=\"WRP2\">" + Environment.NewLine +
            "<name>Sea Bed</name>" + Environment.NewLine +
            "<type>Well Reference Point</type>" + Environment.NewLine +
            "<elevation uom=\"ft\" datum=\"SL\">-118.4</elevation>" + Environment.NewLine +
            "<measuredDepth uom=\"ft\" datum=\"KB\">173.09</measuredDepth>" + Environment.NewLine +
            "<location uid=\"loc-1\">" + Environment.NewLine +
            "<wellCRS uidRef=\"proj1\">ED50 / UTM Zone 31N</wellCRS>" + Environment.NewLine +
            "<easting uom=\"m\">425353.84</easting>" + Environment.NewLine +
            "<northing uom=\"m\">6623785.69</northing>" + Environment.NewLine +
            "</location>" + Environment.NewLine +
            "<location uid=\"loc-2\">" + Environment.NewLine +
            "<wellCRS uidRef=\"geog1\">ED50</wellCRS>" + Environment.NewLine +
            "<latitude uom=\"dega\">59.743844</latitude>" + Environment.NewLine +
            "<longitude uom=\"dega\">1.67198083</longitude>" + Environment.NewLine +
            "</location>" + Environment.NewLine +
            "</referencePoint>" + Environment.NewLine +
            "<wellCRS uid=\"geog1\">" + Environment.NewLine +
            "<name>ED50</name>" + Environment.NewLine +
            "<geodeticCRS uidRef=\"4230\">4230</geodeticCRS>" + Environment.NewLine +
            "<description>ED50 system with EPSG code 4230.</description>" + Environment.NewLine +
            "</wellCRS>" + Environment.NewLine +
            "<wellCRS uid=\"proj1\">" + Environment.NewLine +
            "<name>ED50 / UTM Zone 31N</name>" + Environment.NewLine +
            "<mapProjectionCRS uidRef=\"23031\">ED50 / UTM Zone 31N</mapProjectionCRS>" + Environment.NewLine +
            "</wellCRS>" + Environment.NewLine +
            "<wellCRS uid=\"localWell1\">" + Environment.NewLine +
            "<name>WellOneWSP</name>" + Environment.NewLine +
            "<localCRS>" + Environment.NewLine +
            "<usesWellAsOrigin>true</usesWellAsOrigin>" + Environment.NewLine +
            "<yAxisAzimuth uom=\"dega\" northDirection=\"grid north\">0</yAxisAzimuth>" + Environment.NewLine +
            "<xRotationCounterClockwise>false</xRotationCounterClockwise>" + Environment.NewLine +
            "</localCRS>" + Environment.NewLine +
            "</wellCRS>" + Environment.NewLine +
            "<commonData>" + Environment.NewLine +
            "<dTimCreation>2016-03-07T22:53:59.249Z</dTimCreation>" + Environment.NewLine +
            "<dTimLastChange>2016-03-07T22:53:59.249Z</dTimLastChange > " + Environment.NewLine +
            "<itemState>plan</itemState>" + Environment.NewLine +
            "<comments>These are the comments associated with the Well data object.</comments>" + Environment.NewLine +
            "<defaultDatum uidRef=\"KB\">Kelly Bushing</defaultDatum>" + Environment.NewLine +           
            "</commonData>" + Environment.NewLine +
            "</well>" + Environment.NewLine +
            "</wells>";

            WellList wells = EnergisticsConverter.XmlToObject<WellList>(wellXml);
            return wells.Items[0] as Well;
        }

        private Well CreateTestWell()
        {
            var dateTimeSpud = DateTime.UtcNow;
            var groundElevation = new WellElevationCoord
            {
                Uom = WellVerticalCoordinateUom.m,
                Value = 40.0
            };

            var datum1 = DevKit.WellDatum(null, code: ElevCodeEnum.KB, uid: ElevCodeEnum.KB.ToString());
            var datum2 = DevKit.WellDatum(null, code: ElevCodeEnum.SL, uid: ElevCodeEnum.SL.ToString());

            var commonData = new CommonData
            {
                ItemState = ItemState.plan,
                Comments = "well in plan"
            };

            var well = new Well
            {
                Name = "Well-return-elements-all",
                Country = "US",
                DateTimeSpud = dateTimeSpud,
                DirectionWell = WellDirection.unknown,
                GroundElevation = groundElevation,
                TimeZone = DevKit.TimeZone,
                WellDatum = DevKit.List(datum1, datum2),
                CommonData = commonData
            };

            return well;
        }

        private void AssertTestWell(Well expected, Well actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.DateTimeSpud.ToString(), actual.DateTimeSpud.ToString());
            Assert.AreEqual(expected.GroundElevation.Value, actual.GroundElevation.Value);
            Assert.AreEqual(expected.WellDatum.Count, actual.WellDatum.Count);

            foreach (var datum in expected.WellDatum)
            {
                var returnDatum = actual.WellDatum.FirstOrDefault(d => d.Uid == datum.Uid);
                Assert.IsNotNull(returnDatum);
                Assert.AreEqual(datum.Code, returnDatum.Code);
            }

            Assert.IsNotNull(actual.CommonData);
            Assert.IsNotNull(actual.CommonData.DateTimeLastChange);
            Assert.AreEqual(expected.CommonData.ItemState, actual.CommonData.ItemState);
            Assert.AreEqual(expected.CommonData.Comments, actual.CommonData.Comments);
        }
    }
}
