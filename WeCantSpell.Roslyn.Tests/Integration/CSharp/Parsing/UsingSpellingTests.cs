﻿using System.Threading.Tasks;
using FluentAssertions;
using WeCantSpell.Roslyn.Tests.Utilities;
using Xunit;

namespace WeCantSpell.Roslyn.Tests.Integration.CSharp.Parsing
{
    public class UsingSpellingTests : CSharpParsingTestBase
    {
        public static object[][] can_find_spelling_mistakes_in_usings_data => new[]
        {
            new object[] { "Nope", 52 },
            new object[] { "Not", 96 },
            new object[] { "Done", 99 },
            new object[] { "bytes", 397 }
        };

        [Theory, MemberData(nameof(can_find_spelling_mistakes_in_usings_data))]
        public async Task can_find_spelling_mistakes_in_usings(string expectedWord, int expectedStart)
        {
            var expectedEnd = expectedStart + expectedWord.Length;

            var analyzer = new SpellingAnalyzerCSharp(new WrongWordChecker(expectedWord));
            var project = await ReadCodeFileAsProjectAsync("Using.SimpleExamples.csx");

            var diagnostics = await GetDiagnosticsAsync(project, analyzer);

            diagnostics.Should().ContainSingle()
                .Subject.Should()
                .HaveId("SP3110")
                .And.HaveLocation(expectedStart, expectedEnd, "Using.SimpleExamples.csx")
                .And.HaveMessageContaining(expectedWord);
        }
    }
}
