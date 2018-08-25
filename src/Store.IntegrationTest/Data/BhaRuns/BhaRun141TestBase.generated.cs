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
using System;
using System.Collections.Generic;
using System.Linq;
using Energistics.DataAccess;
using Energistics.DataAccess.WITSML141;
using Energistics.DataAccess.WITSML141.ComponentSchemas;
using Energistics.DataAccess.WITSML141.ReferenceData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PDS.WITSMLstudio.Store.Data.BhaRuns
{
    public abstract partial class BhaRun141TestBase : IntegrationTestBase
    {

        public const string QueryMissingNamespace = "<bhaRuns version=\"1.4.1.1\"><bhaRun /></bhaRuns>";
        public const string QueryInvalidNamespace = "<bhaRuns xmlns=\"www.witsml.org/schemas/123\" version=\"1.4.1.1\"></bhaRuns>";
        public const string QueryMissingVersion = "<bhaRuns xmlns=\"http://www.witsml.org/schemas/1series\"></bhaRuns>";
        public const string QueryEmptyRoot = "<bhaRuns xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"></bhaRuns>";
        public const string QueryEmptyObject = "<bhaRuns xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><bhaRun /></bhaRuns>";

        public const string BasicXMLTemplate = "<bhaRuns xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><bhaRun uidWell=\"{0}\" uidWellbore=\"{1}\" uid=\"{2}\">{3}</bhaRun></bhaRuns>";

        public Well Well { get; set; }
        public Wellbore Wellbore { get; set; }
        public BhaRun BhaRun { get; set; }

        public DevKit141Aspect DevKit { get; set; }

        public List<BhaRun> QueryEmptyList { get; set; }

        [TestInitialize]
        public void TestSetUp()
        {
            Logger.Debug($"Executing {TestContext.TestName}");
            DevKit = new DevKit141Aspect(TestContext);

            DevKit.Store.CapServerProviders = DevKit.Store.CapServerProviders
                .Where(x => x.DataSchemaVersion == OptionsIn.DataVersion.Version141.Value)
                .ToArray();

            Well = new Well
            {
                Uid = DevKit.Uid(),
                Name = DevKit.Name("Well"),

                TimeZone = DevKit.TimeZone
            };
            Wellbore = new Wellbore
            {
                Uid = DevKit.Uid(),
                Name = DevKit.Name("Wellbore"),

                UidWell = Well.Uid,
                NameWell = Well.Name,
                MD = new MeasuredDepthCoord(0, MeasuredDepthUom.ft)

            };
            BhaRun = new BhaRun
            {
                Uid = DevKit.Uid(),
                Name = DevKit.Name("BhaRun"),

                UidWell = Well.Uid,
                NameWell = Well.Name,
                UidWellbore = Wellbore.Uid,
                NameWellbore = Wellbore.Name

            };

            QueryEmptyList = DevKit.List(new BhaRun());

            BeforeEachTest();
            OnTestSetUp();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            AfterEachTest();
            OnTestCleanUp();
            DevKit.Container.Dispose();
            DevKit = null;
        }

        partial void BeforeEachTest();

        partial void AfterEachTest();

        protected virtual void OnTestSetUp() { }

        protected virtual void OnTestCleanUp() { }

        protected virtual void AddParents()
        {

            DevKit.AddAndAssert<WellList, Well>(Well);
            DevKit.AddAndAssert<WellboreList, Wellbore>(Wellbore);

        }
    }
}