using System.Text.RegularExpressions;

namespace NugetLicense.Toolkit
{
    public interface IPackageOptions
    {
        ICollection<string> AllowedLicenseType { get; }
        /// <summary>
        /// Simple json file of a text array of allowable licenses, if no file is given, all are assumed allowed. Cannot be used alongside 'forbidden-license-types'.
        /// </summary>
        string AllowedLicenseTypesOption { get; set; }
        /// <summary>
        /// Convert html licenses to plain text.
        /// </summary>
        bool ConvertHtmlToText { get; set; }
        /// <summary>
        /// Exports the raw license textsp
        /// </summary>
        bool ExportLicenseTexts { get; set; }
        ICollection<string> ForbiddenLicenseType { get; }
        /// <summary>
        /// Simple json file of a text array of forbidden licenses, if no file is given, none are assumed forbidden. Cannot be used alongside 'allowed-license-types'.
        /// </summary>
        string ForbiddenLicenseTypesOption { get; set; }
        /// <summary>
        /// Ignore SSL certificate errors in HttpClient.
        /// </summary>
        bool IgnoreSslCertificateErrors { get; set; }
        /// <summary>
        /// Adds project file path to information when enabled
        /// </summary>
        bool IncludeProjectFile { get; set; }
        /// <summary>
        /// Include distinct transitive package licenses per project file.
        /// </summary>
        bool IncludeTransitive { get; set; }
        /// <summary>
        /// Saves licenses list in a json file (licenses.json)
        /// </summary>
        bool JsonOutput { get; set; }
        IReadOnlyDictionary<string, string> LicenseToUrlMappingsDictionary { get; }
        /// <summary>
        /// Simple json file of Dictionary<string,string> to override default mappings
        /// </summary>
        string LicenseToUrlMappingsOption { get; set; }
        /// <summary>
        /// Sets log level for output display. Options: Error|Warning|Information|Verbose.
        /// </summary>
        LogLevel LogLevelThreshold { get; set; }
        ICollection<LibraryInfo> ManualInformation { get; }
        /// <summary>
        /// Simple json file of an array of LibraryInfo objects for manually determined packages.
        /// </summary>
        string ManualInformationOption { get; set; }
        /// <summary>
        /// Saves the licenses list to a markdown file (licenses.md)
        /// </summary>
        bool MarkDownOutput { get; set; }
        /// <summary>
        /// Output Directory
        /// </summary>
        string OutputDirectory { get; set; }
        /// <summary>
        /// Output filename
        /// </summary>
        string OutputFileName { get; set; }
        ICollection<string> PackageFilter { get; }
        Regex? PackageRegex { get; }
        /// <summary>
        /// Simple json file of a text array of packages to skip, or a regular expression defined between two forward slashes or two hashes.
        /// </summary>
        string PackagesFilterOption { get; set; }
        /// <summary>
        /// The page width, in characters, to use for HTML to text conversion.
        /// </summary>
        int PageWidth { get; set; }
        /// <summary>
        /// Print licenses.
        /// </summary>
        bool? Print { get; set; }
        /// <summary>
        /// The projects in which to search for used nuget packages. This can either be a folder, a project file, a solution file or a json file containing a list of projects.
        /// </summary>
        string ProjectDirectory { get; set; }
        ICollection<string> ProjectFilter { get; }
        /// <summary>
        /// Simple json file of a text array of projects to skip. Supports Ends with matching such as 'Tests.csproj'
        /// </summary>
        string ProjectsFilterOption { get; set; }
        /// <summary>
        /// Use the system credentials for proxy authentication.
        /// </summary>
        bool ProxySystemAuth { get; set; }
        /// <summary>
        /// Set a proxy server URL to be used by HttpClient.
        /// </summary>
        string ProxyURL { get; set; }
        /// <summary>
        /// Saves as text file (licenses.txt)
        /// </summary>
        bool TextOutput { get; set; }
        /// <summary>
        /// Set HttpClient timeout in seconds.
        /// </summary>
        int Timeout { get; set; }
        /// <summary>
        /// Unique licenses list by Id/Version
        /// </summary>
        bool UniqueOnly { get; set; }
        /// <summary>
        /// Use the resolved project.assets.json file for each project as the source of package information. Requires the -t option. Requires `nuget restore` or `dotnet restore` to be run first.
        /// </summary>
        bool UseProjectAssetsJson { get; set; }
    }
}