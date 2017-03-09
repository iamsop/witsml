﻿//----------------------------------------------------------------------- 
// PDS WITSMLstudio Store, 2017.1
//
// Copyright 2017 Petrotechnical Data Systems
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//     http://www.apache.org/licenses/LICENSE-2.0
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
using Energistics.DataAccess.WITSML131;
using Energistics.DataAccess.WITSML131.ComponentSchemas;
using Energistics.DataAccess.WITSML131.ReferenceData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WbGeometry = Energistics.DataAccess.WITSML131.StandAloneWellboreGeometry;
using WbGeometryList = Energistics.DataAccess.WITSML131.WellboreGeometryList;

namespace PDS.WITSMLstudio.Store.Data.WbGeometries
{
    [TestClass]
    public partial class WbGeometry131StoreTests : WbGeometry131TestBase
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
        public void WbGeometry131DataAdapter_GetFromStore_Can_Get_WbGeometry()
        {
            AddParents();
            DevKit.AddAndAssert<WbGeometryList, WbGeometry>(WbGeometry);
            DevKit.GetAndAssert<WbGeometryList, WbGeometry>(WbGeometry);
       }

        [TestMethod]
        public void WbGeometry131DataAdapter_AddToStore_Can_Add_WbGeometry()
        {
            AddParents();
            DevKit.AddAndAssert<WbGeometryList, WbGeometry>(WbGeometry);
        }

        [TestMethod]
        public void WbGeometry131DataAdapter_UpdateInStore_Can_Update_WbGeometry()
        {
            AddParents();
            DevKit.AddAndAssert<WbGeometryList, WbGeometry>(WbGeometry);
            DevKit.UpdateAndAssert<WbGeometryList, WbGeometry>(WbGeometry);
            DevKit.GetAndAssert<WbGeometryList, WbGeometry>(WbGeometry);
        }

        [TestMethod]
        public void WbGeometry131DataAdapter_DeleteFromStore_Can_Delete_WbGeometry()
        {
            AddParents();
            DevKit.AddAndAssert<WbGeometryList, WbGeometry>(WbGeometry);
            DevKit.DeleteAndAssert<WbGeometryList, WbGeometry>(WbGeometry);
            DevKit.GetAndAssert<WbGeometryList, WbGeometry>(WbGeometry, isNotNull: false);
        }

        [TestMethod]
        public void WbGeometry131WitsmlStore_GetFromStore_Can_Transform_WbGeometry()
        {
            AddParents();
            DevKit.AddAndAssert<WbGeometryList, WbGeometry>(WbGeometry);

            // Re-initialize all capServer providers
            DevKit.Store.CapServerProviders = null;
            DevKit.Container.BuildUp(DevKit.Store);

            string typeIn, queryIn;
            var query = DevKit.List(DevKit.CreateQuery(WbGeometry));
            DevKit.SetupParameters<WbGeometryList, WbGeometry>(query, ObjectTypes.WbGeometry, out typeIn, out queryIn);

            var options = OptionsIn.Join(OptionsIn.ReturnElements.All, OptionsIn.DataVersion.Version141);
            var request = new WMLS_GetFromStoreRequest(typeIn, queryIn, options, null);
            var response = DevKit.Store.WMLS_GetFromStore(request);

            Assert.IsFalse(string.IsNullOrWhiteSpace(response.XMLout));
            Assert.AreEqual((short)ErrorCodes.Success, response.Result);

            var result = WitsmlParser.Parse(response.XMLout);
            var version = ObjectTypes.GetVersion(result.Root);
            Assert.AreEqual(OptionsIn.DataVersion.Version141.Value, version);
        }
    }
}
