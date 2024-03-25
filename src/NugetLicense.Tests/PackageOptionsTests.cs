using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace NugetLicense.Toolkit.Tests
{
    [TestFixture]
    public class PackageOptionsTests
    {
        [Test]
        public void LicenseToUrlMappingsOption_When_Set_Should_Replace_Overridden_Default_Mappings()
        {
            var defaultKey1 = LicenseToUrlMappings.Default.Keys.ElementAt(0);
            var defaultKey2 = LicenseToUrlMappings.Default.Keys.ElementAt(1);
            var testMappings = new Dictionary<string, string>
            {
                {"url1", "license1"},
                {"url2", "license1"},
                {defaultKey2, "license2"}
            };
            var testFile = "test-mappings.json";
            File.WriteAllText(testFile, JsonConvert.SerializeObject(testMappings));

            var options = new CommandPackageOptions {LicenseToUrlMappingsOption = testFile};

            options.LicenseToUrlMappingsDictionary.Should().HaveCount(LicenseToUrlMappings.Default.Count + 2)
                .And.ContainKey("url1").And.ContainKey(defaultKey1).And.Contain(defaultKey2, "license2");
        }


        [Test]
        public void UniqueMappingsOption_When_Set_Should_Replace_Default_Mappings()
        {
            var options = new CommandPackageOptions {UniqueOnly = true};

            options.UniqueOnly.Should().BeTrue();
        }

        [Test]
        public void ProxyOption_ProxyURL_Included()
        {
            var options = new CommandPackageOptions {ProxyURL = "http://proxy:8080"};
            options.ProxyURL.Should().Be("http://proxy:8080");
        }

        [Test]
        public void ProxyOption_ProxySystemAuth_Should_Be_True()
        {
            var options = new CommandPackageOptions {ProxySystemAuth = true};
            options.ProxySystemAuth.Should().Be(true);
        }

        [Test]
        [TestCase("/.*/")]
        [TestCase(@"#System\..*#")]
        public void PackagesFilterOption_RegexPackagesFilter_Should_Support_Hashes_And_Slashes(string option)
        {
            var options = new CommandPackageOptions
            {
                PackagesFilterOption = option,
            };

            var regex = options.PackageRegex;
            regex.Should().NotBeNull();
        }

        [Test]
        public void PackagesFilterOption_IncorrectRegexPackagesFilter_Should_Throw_ArgumentException()
        {
            var options = new CommandPackageOptions
            {
                PackagesFilterOption = "/(?/",
            };

            Assert.Throws(typeof(ArgumentException), () =>
            {
                var regex = options.PackageRegex;
            });
        }

        [Test]
        [TestCase(@"../../../DoesNotExist.json")]
        [TestCase("/.*validregexinvalidpath#")]
        [TestCase("#invalidpath/")]
        public void PackagesFilterOption_IncorrectPackagesFilterPath_Should_Throw_FileNotFoundException(string option)
        {
            var options = new CommandPackageOptions
            {
                PackagesFilterOption = option,
            };

            Assert.Throws(typeof(FileNotFoundException), () =>
            {
                var regex = options.PackageFilter;
            });
        }
    }
}