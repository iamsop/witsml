//----------------------------------------------------------------------- 
// PDS WITSMLstudio Store, 2018.3
//
// Copyright 2018 PDS Americas LLC
// 
// Licensed under the PDS Open Source WITSML Product License Agreement (the
// "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//     http://www.pds.group/WITSMLstudio/OpenSource/ProductLicenseAgreement
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

// ----------------------------------------------------------------------
// <auto-generated>
//     Changes to this file may cause incorrect behavior and will be lost
//     if the code is regenerated.
// </auto-generated>
// ----------------------------------------------------------------------

using Energistics.DataAccess;
using Energistics.DataAccess.WITSML141;
using Energistics.DataAccess.WITSML141.ComponentSchemas;
using Energistics.DataAccess.WITSML141.ReferenceData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PDS.WITSMLstudio.Store.Data.ToolErrorTermSets
{
    [TestClass]
    public partial class ToolErrorTermSet141StoreTests : ToolErrorTermSet141TestBase
    {
        partial void BeforeEachTest();

        partial void AfterEachTest();

        protected override void OnTestSetUp()
        {
            BeforeEachTest();
        }

        protected override void OnTestCleanUp()
        {
            AfterEachTest();
        }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_GetFromStore_Can_Get_ToolErrorTermSet()
        {
            AddParents();
            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            DevKit.GetAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
       }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_AddToStore_Can_Add_ToolErrorTermSet()
        {
            AddParents();
            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
        }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_UpdateInStore_Can_Update_ToolErrorTermSet()
        {
            AddParents();
            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            DevKit.UpdateAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            DevKit.GetAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
        }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_DeleteFromStore_Can_Delete_ToolErrorTermSet()
        {
            AddParents();
            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            DevKit.DeleteAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            DevKit.GetAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet, isNotNull: false);
        }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_AddToStore_Creates_ChangeLog()
        {
            AddParents();

            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);

            var result = DevKit.GetAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            var expectedHistoryCount = 1;
            var expectedChangeType = ChangeInfoType.add;
            DevKit.AssertChangeLog(result, expectedHistoryCount, expectedChangeType);
        }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_UpdateInStore_Updates_ChangeLog()
        {
            AddParents();

            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);

            // Update the ToolErrorTermSet141
            ToolErrorTermSet.Name = "Change";
            DevKit.UpdateAndAssert(ToolErrorTermSet);

            var result = DevKit.GetAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            var expectedHistoryCount = 2;
            var expectedChangeType = ChangeInfoType.update;
            DevKit.AssertChangeLog(result, expectedHistoryCount, expectedChangeType);
        }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_DeleteFromStore_Updates_ChangeLog()
        {
            AddParents();

            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);

            // Delete the ToolErrorTermSet141
            DevKit.DeleteAndAssert(ToolErrorTermSet);

            var expectedHistoryCount = 2;
            var expectedChangeType = ChangeInfoType.delete;
            DevKit.AssertChangeLog(ToolErrorTermSet, expectedHistoryCount, expectedChangeType);
        }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_ChangeLog_Tracks_ChangeHistory_For_Add_Update_Delete()
        {
            AddParents();

            // Add the ToolErrorTermSet141
            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);

            // Verify ChangeLog for Add
            var result = DevKit.GetAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            var expectedHistoryCount = 1;
            var expectedChangeType = ChangeInfoType.add;
            DevKit.AssertChangeLog(result, expectedHistoryCount, expectedChangeType);

            // Update the ToolErrorTermSet141
            ToolErrorTermSet.Name = "Change";
            DevKit.UpdateAndAssert(ToolErrorTermSet);

            result = DevKit.GetAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            expectedHistoryCount = 2;
            expectedChangeType = ChangeInfoType.update;
            DevKit.AssertChangeLog(result, expectedHistoryCount, expectedChangeType);

            // Delete the ToolErrorTermSet141
            DevKit.DeleteAndAssert(ToolErrorTermSet);

            expectedHistoryCount = 3;
            expectedChangeType = ChangeInfoType.delete;
            DevKit.AssertChangeLog(ToolErrorTermSet, expectedHistoryCount, expectedChangeType);

            // Re-add the same ToolErrorTermSet141...
            DevKit.AddAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);

            //... the same changeLog should be reused.
            result = DevKit.GetAndAssert<ToolErrorTermSetList, ToolErrorTermSet>(ToolErrorTermSet);
            expectedHistoryCount = 4;
            expectedChangeType = ChangeInfoType.add;
            DevKit.AssertChangeLog(result, expectedHistoryCount, expectedChangeType);

            DevKit.AssertChangeHistoryTimesUnique(result);
        }

        [TestMethod]
        public void ToolErrorTermSet141DataAdapter_GetFromStore_Filter_ExtensionNameValue()
        {
            AddParents();

            var extensionName1 = DevKit.ExtensionNameValue("Ext-1", "1.0", "m");
            var extensionName2 = DevKit.ExtensionNameValue("Ext-2", "2.0", "cm", PrimitiveType.@float);
            extensionName2.MeasureClass = MeasureClass.Length;
            var extensionName3 = DevKit.ExtensionNameValue("Ext-3", "3.0", "cm", PrimitiveType.unknown);

            ToolErrorTermSet.CommonData = new CommonData()
            {
                ExtensionNameValue = new List<ExtensionNameValue>()
                {
                    extensionName1, extensionName2, extensionName3
                }
            };

            // Add the ToolErrorTermSet141
            DevKit.AddAndAssert(ToolErrorTermSet);

            // Query for first extension
            var commonDataXml = "<commonData>" + Environment.NewLine +
                                "<extensionNameValue uid=\"\">" + Environment.NewLine +
                                "<name />{0}" + Environment.NewLine +
                                "</extensionNameValue>" + Environment.NewLine +
                                "</commonData>";

            var extValueQuery = string.Format(commonDataXml, "<dataType>double</dataType>");
            var queryXml = string.Format(BasicXMLTemplate, ToolErrorTermSet.Uid, extValueQuery);
            var result = DevKit.Query<ToolErrorTermSetList, ToolErrorTermSet>(ObjectTypes.ToolErrorTermSet, queryXml, null, OptionsIn.ReturnElements.Requested);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var resultToolErrorTermSet = result[0];
            Assert.IsNotNull(resultToolErrorTermSet);

            var commonData = resultToolErrorTermSet.CommonData;
            Assert.IsNotNull(commonData);
            Assert.AreEqual(1, commonData.ExtensionNameValue.Count);

            var env = commonData.ExtensionNameValue[0];
            Assert.IsNotNull(env);
            Assert.AreEqual(extensionName1.Uid, env.Uid);
            Assert.AreEqual(extensionName1.Name, env.Name);

            // Query for second extension
            extValueQuery = string.Format(commonDataXml, "<measureClass>length</measureClass>");
            queryXml = string.Format(BasicXMLTemplate, ToolErrorTermSet.Uid, extValueQuery);
            result = DevKit.Query<ToolErrorTermSetList, ToolErrorTermSet>(ObjectTypes.ToolErrorTermSet, queryXml, null, OptionsIn.ReturnElements.Requested);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            resultToolErrorTermSet = result[0];
            Assert.IsNotNull(resultToolErrorTermSet);

            commonData = resultToolErrorTermSet.CommonData;
            Assert.IsNotNull(commonData);
            Assert.AreEqual(1, commonData.ExtensionNameValue.Count);

            env = commonData.ExtensionNameValue[0];
            Assert.IsNotNull(env);
            Assert.AreEqual(extensionName2.Uid, env.Uid);
            Assert.AreEqual(extensionName2.Name, env.Name);

            // Query for third extension
            extValueQuery = string.Format(commonDataXml, "<dataType>unknown</dataType>");
            queryXml = string.Format(BasicXMLTemplate, ToolErrorTermSet.Uid, extValueQuery);
            result = DevKit.Query<ToolErrorTermSetList, ToolErrorTermSet>(ObjectTypes.ToolErrorTermSet, queryXml, null, OptionsIn.ReturnElements.Requested);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            resultToolErrorTermSet = result[0];
            Assert.IsNotNull(resultToolErrorTermSet);

            commonData = resultToolErrorTermSet.CommonData;
            Assert.IsNotNull(commonData);
            Assert.AreEqual(1, commonData.ExtensionNameValue.Count);

            env = commonData.ExtensionNameValue[0];
            Assert.IsNotNull(env);
            Assert.AreEqual(extensionName3.Uid, env.Uid);
            Assert.AreEqual(extensionName3.Name, env.Name);
        }
    }
}