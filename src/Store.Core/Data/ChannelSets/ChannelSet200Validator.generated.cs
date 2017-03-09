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
using System.ComponentModel.Composition;
using Energistics.DataAccess.WITSML200;
using PDS.WITSMLstudio.Framework;

namespace PDS.WITSMLstudio.Store.Data.ChannelSets
{
    /// <summary>
    /// Provides validation for <see cref="ChannelSet" /> data objects.
    /// </summary>
    /// <seealso cref="PDS.WITSMLstudio.Store.Data.DataObjectValidator{ChannelSet}" />
    [Export(typeof(IDataObjectValidator<ChannelSet>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ChannelSet200Validator : DataObjectValidator<ChannelSet>
    {
        private readonly IWitsmlDataAdapter<ChannelSet> _channelSetDataAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelSet200Validator" /> class.
        /// </summary>
        /// <param name="container">The composition container.</param>
        /// <param name="channelSetDataAdapter">The channelSet data adapter.</param>
        [ImportingConstructor]
        public ChannelSet200Validator(
            IContainer container,
            IWitsmlDataAdapter<ChannelSet> channelSetDataAdapter)
            : base(container)
        {
            _channelSetDataAdapter = channelSetDataAdapter;
        }
    }
}
