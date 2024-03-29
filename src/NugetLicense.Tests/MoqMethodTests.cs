using System.Diagnostics.CodeAnalysis;
using Moq;
using Moq.Protected;
using NugetLicense.Toolkit.Model;

namespace NugetLicense.Toolkit.Tests

{

    [ExcludeFromCodeCoverage]

    [TestFixture]

    public class MoqMethodTests

    {

        private string _projectPath = @"../../../";

        [TestCase("BenchmarkDotNet", "0.12.1", "https://dummy.com/test", "MIT")]

        [Test]

        public async Task ExportLicenseTextMoqHttpResponse_Timeout(string packageName, string packageVersion, string licenseUrl, string licenseType)

        {

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ThrowsAsync(new TaskCanceledException("Timeout Exception"));




            var methods = new Methods(new PackageOptions

            {

                ProjectDirectory = _projectPath,

                Timeout = 2,

                ExportLicenseTexts = true,

            }, new HttpClient(mockMessageHandler.Object));




            List<LibraryInfo> infos = new List<LibraryInfo>();

            infos.Add(new LibraryInfo()

            {

                PackageName = packageName,

                PackageVersion = packageVersion,

                LicenseUrl = licenseUrl,

                LicenseType = licenseType,

            });

            await methods.ExportLicenseTexts(infos);

            var directory = methods.GetExportDirectory();

            var outpath = Path.Combine(directory, packageName + "_" + packageVersion + ".txt");

            var outpathhtml = Path.Combine(directory, packageName + "_" + packageVersion + ".html");

            // file not generated

            Assert.That(!File.Exists(outpath) || !File.Exists(outpathhtml));

        }

    }

}