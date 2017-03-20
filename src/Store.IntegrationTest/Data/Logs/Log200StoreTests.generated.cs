//----------------------------------------------------------------------- 
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
using Energistics.DataAccess.WITSML200;
using Energistics.DataAccess.WITSML200.ComponentSchemas;
using Energistics.DataAccess.WITSML200.ReferenceData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PDS.WITSMLstudio.Store.Data.Logs
{
    [TestClass]
    public partial class Log200StoreTests : Log200TestBase
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
        public void Log200DataAdapter_GetFromStore_Can_Get_Log()
        {
            AddParents();
            DevKit.AddAndAssert(Log);
            DevKit.GetAndAssert(Log);
       }

        [TestMethod]
        public void Log200DataAdapter_AddToStore_Can_Add_Log()
        {
            AddParents();
            DevKit.AddAndAssert(Log);
        }

        [TestMethod]
        public void Log200DataAdapter_UpdateInStore_Can_Update_Log()
        {
            AddParents();
            DevKit.AddAndAssert(Log);
            DevKit.UpdateAndAssert(Log);
            DevKit.GetAndAssert(Log);
        }

        [TestMethod]
        public void Log200DataAdapter_DeleteFromStore_Can_Delete_Log()
        {
            AddParents();
            DevKit.AddAndAssert(Log);
            DevKit.DeleteAndAssert(Log);
            DevKit.GetAndAssert(Log, isNotNull: false);
        }
    }
}