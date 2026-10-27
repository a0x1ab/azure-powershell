// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Common;
using Microsoft.Azure.Commands.ScenarioTest;
using Microsoft.WindowsAzure.Commands.Common;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using System.Management.Automation;
using Xunit;

namespace Microsoft.Azure.Commands.Profile.Test
{
    /// <summary>
    /// Unit tests for the Change Safety dynamic parameters exposed to AutoRest modules through
    /// <see cref="ContextAdapter.GetDynamicParametersValue" />.
    ///
    /// Note: constructing a real <see cref="InvocationInfo" /> with a controllable MyCommand.Name is awkward,
    /// so the read-verb decision and dictionary building are exercised through the internal
    /// <see cref="ContextAdapter.IsReadVerbCommand" /> / <see cref="ContextAdapter.BuildChangeSafetyDynamicParameters" />
    /// helpers, plus the null-invocationInfo path of the public method.
    /// </summary>
    public class ChangeSafetyDynamicParametersTests
    {
        [Theory]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        [InlineData("New-AzFoo")]
        [InlineData("Set-AzFoo")]
        [InlineData("Update-AzFoo")]
        [InlineData("Remove-AzFoo")]
        public void WriteVerbReturnsBothChangeSafetyParameters(string commandName)
        {
            var dict = ContextAdapter.BuildChangeSafetyDynamicParameters(commandName);

            Assert.NotNull(dict);
            Assert.True(dict.ContainsKey(ChangeSafetyParameters.AcquirePolicyTokenParamName));
            Assert.True(dict.ContainsKey(ChangeSafetyParameters.ChangeReferenceParamName));
            Assert.Equal(typeof(SwitchParameter), dict[ChangeSafetyParameters.AcquirePolicyTokenParamName].ParameterType);
            Assert.Equal(typeof(string), dict[ChangeSafetyParameters.ChangeReferenceParamName].ParameterType);
        }

        [Theory]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        [InlineData("Get-AzFoo")]
        [InlineData("Test-AzFoo")]
        [InlineData("List-AzFoo")]
        [InlineData("Show-AzFoo")]
        public void ReadVerbReturnsEmptyDictionary(string commandName)
        {
            var dict = ContextAdapter.BuildChangeSafetyDynamicParameters(commandName);

            Assert.NotNull(dict);
            Assert.Empty(dict);
        }

        [Theory]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        [InlineData("Get-AzFoo", true)]
        [InlineData("Test-AzFoo", true)]
        [InlineData("List-AzFoo", true)]
        [InlineData("Show-AzFoo", true)]
        [InlineData("get-azfoo", true)]
        [InlineData("New-AzFoo", false)]
        [InlineData("Set-AzFoo", false)]
        [InlineData("Remove-AzFoo", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsReadVerbCommandClassifiesVerbs(string commandName, bool expected)
        {
            Assert.Equal(expected, ContextAdapter.IsReadVerbCommand(commandName));
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void NullInvocationInfoIsTreatedAsWriteAndDoesNotThrow()
        {
            TestExecutionHelpers.SetUpSessionAndProfile();

            // A null InvocationInfo yields an empty command name, which is treated as a write verb.
            var result = ContextAdapter.Instance.GetDynamicParametersValue("Az.Foo", "Az.Foo", null, "correlation-id");

            var dict = Assert.IsType<RuntimeDefinedParameterDictionary>(result);
            Assert.True(dict.ContainsKey(ChangeSafetyParameters.AcquirePolicyTokenParamName));
            Assert.True(dict.ContainsKey(ChangeSafetyParameters.ChangeReferenceParamName));
            Assert.Equal(typeof(SwitchParameter), dict[ChangeSafetyParameters.AcquirePolicyTokenParamName].ParameterType);
            Assert.Equal(typeof(string), dict[ChangeSafetyParameters.ChangeReferenceParamName].ParameterType);
        }
    }
}
