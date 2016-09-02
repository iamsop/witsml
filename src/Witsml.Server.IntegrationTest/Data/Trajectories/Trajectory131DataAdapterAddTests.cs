﻿//----------------------------------------------------------------------- 
// PDS.Witsml.Server, 2016.1
//
// Copyright 2016 Petrotechnical Data Systems
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PDS.Witsml.Server.Data.Trajectories
{
    public partial class Trajectory131DataAdapterAddTests
    {
        [TestMethod]
        public void Trajectory141DataAdapter_AddToStore_AddTrajectory_Header()
        {
            // Add well and wellbore
            AddParents();

            // Add trajectory without stations
            DevKit.AddAndAssert(Trajectory);

            // Get trajectory
            DevKit.GetAndAssert(Trajectory);
        }

        [TestMethod]
        public void Trajectory141DataAdapter_AddToStore_AddTrajectory_With_Stations()
        {
            // Add well and wellbore
            AddParents();

            // Add trajectory without stations         
            Trajectory.TrajectoryStation = DevKit.TrajectoryStations(4, 0);
            DevKit.AddAndAssert(Trajectory);

            // Get trajectory
            var result = DevKit.GetAndAssert(Trajectory);
            Assert.AreEqual(Trajectory.TrajectoryStation.Count, result.TrajectoryStation.Count);
        }
    }
}