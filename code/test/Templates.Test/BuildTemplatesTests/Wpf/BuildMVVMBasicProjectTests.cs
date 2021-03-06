﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;

using Microsoft.TemplateEngine.Abstractions;
using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Extensions;
using Xunit;

namespace Microsoft.Templates.Test.Build.Wpf
{
    [Collection("BuildTemplateTestCollection")]
    public class BuildMVVMBasicProjectTests : BaseGenAndBuildTests
    {
        public BuildMVVMBasicProjectTests(BuildTemplatesTestFixture fixture)
            : base(fixture, null, "MVVMBasic")
        {
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMBasic", "", Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMBasicWpf")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildProjects")]
        public async Task Build_EmptyProject_Wpf(string projectType, string framework, string platform, string language)
        {
            var (projectName, projectPath) = await GenerateEmptyProjectAsync(projectType, framework, platform, language);

            AssertBuildProjectAsync(projectPath, projectName, platform, true);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMBasic", "", Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMBasicWpf")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildAllPagesAndFeaturesWpf")]
        [Trait("Type", "BuildRandomNamesWpf")]
        public async Task Build_All_ProjectNameValidation_G1_Wpf(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group2.Contains(t.GroupIdentity)
                && t.Identity != "wts.Wpf.Feat.MSIXPackaging"
                && !t.GetIsHidden();

            var projectName = $"{ShortProjectType(projectType)}{CharactersThatMayCauseProjectNameIssues()}G1{ShortLanguageName(language)}";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetRandomName);

            AssertBuildProjectAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMBasic", "", Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMBasicWpf")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildAllPagesAndFeaturesWpf")]
        [Trait("Type", "BuildRandomNamesWpf")]
        public async Task Build_All_ProjectNameValidation_G2_Wpf(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                 && !excludedTemplates_Wpf_Group1.Contains(t.GroupIdentity)
                && t.Identity != "wts.Wpf.Feat.MSIXPackaging"
                && !t.GetIsHidden();

            var projectName = $"{ShortProjectType(projectType)}{CharactersThatMayCauseProjectNameIssues()}G1{ShortLanguageName(language)}";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetRandomName);

            AssertBuildProjectAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMBasic", ProgrammingLanguages.CSharp, Platforms.Wpf)]
        [Trait("ExecutionSet", "MinimumWpf")]
        [Trait("ExecutionSet", "MVVMBasicWpf")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "CodeStyleWpf")]
        public async Task Build_All_CheckWithStyleCop_G1_Wpf(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group2.Contains(t.GroupIdentity)
                && !t.GetIsHidden()
                && t.Identity != "wts.Wpf.Feat.MSIXPackaging"
                || t.Identity == "wts.Wpf.Feat.StyleCop";

            var projectName = $"{projectType}{framework}All";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName);

            AssertBuildProjectAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMBasic", ProgrammingLanguages.CSharp, Platforms.Wpf)]
        [Trait("ExecutionSet", "MinimumWpf")]
        [Trait("ExecutionSet", "MinimumMVVMBasicWpf")]
        [Trait("ExecutionSet", "_CIBuild")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "CodeStyleWpf")]
        public async Task Build_All_CheckWithStyleCop_G2_Wpf(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group1.Contains(t.GroupIdentity)
                && !t.GetIsHidden()
                && t.Identity != "wts.Wpf.Feat.MSIXPackaging"
                || t.Identity == "wts.Wpf.Feat.StyleCop";

            var projectName = $"{projectType}{framework}All";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName);

            AssertBuildProjectAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMBasic", ProgrammingLanguages.CSharp, Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMBasicWpf")]
        [Trait("ExecutionSet", "_Full")]
        public async Task Build_AllWithMsix_G1_Wpf(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group2.Contains(t.GroupIdentity)
                && !t.GetIsHidden()
                || t.Identity == "wts.Wpf.Feat.StyleCop";

            var projectName = $"{projectType}{framework}AllMsix";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName);

            AssertBuildProjectWpfWithMsixAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMBasic", ProgrammingLanguages.CSharp, Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMBasicWpf")]
        [Trait("ExecutionSet", "_Full")]
        public async Task Build_AllWithMsix_G2_Wpf(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group1.Contains(t.GroupIdentity)
                && !t.GetIsHidden()
                || t.Identity == "wts.Wpf.Feat.StyleCop";

            var projectName = $"{projectType}{framework}AllMsix";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName);

            AssertBuildProjectWpfWithMsixAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetPageAndFeatureTemplatesForBuild), "MVVMBasic", ProgrammingLanguages.CSharp, Platforms.Wpf, "wts.Wpf.Feat.MSIXPackaging")]
        [Trait("ExecutionSet", "BuildOneByOneMVVMBasicWpf")]
        [Trait("ExecutionSet", "_OneByOne")]
        [Trait("Type", "BuildOneByOneMVVMBasicWpf")]
        public async Task Build_MVVMBasic_OneByOneItems_Wpf(string itemName, string projectType, string framework, string platform, string itemId, string language)
        {
            var result = await AssertGenerationOneByOneAsync(itemName, projectType, framework, platform, itemId, language, false);

            AssertBuildProjectAsync(result.ProjectPath, result.ProjecName, platform);
        }
    }
}
