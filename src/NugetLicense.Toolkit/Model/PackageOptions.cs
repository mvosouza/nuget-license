using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using static NugetLicense.Toolkit.Utilities;

namespace NugetLicense.Toolkit
{
    public class PackageOptions : IPackageOptions
    {
        private readonly Regex UserRegexRegex = new Regex("^([/#])(.+)\\1$");

        private ICollection<string> _allowedLicenseTypes = new Collection<string>();
        private ICollection<string> _forbiddenLicenseTypes = new Collection<string>();
        private ICollection<LibraryInfo> _manualInformation = new Collection<LibraryInfo>();
        private ICollection<string> _projectFilter = new Collection<string>();
        private ICollection<string> _packagesFilter = new Collection<string>();
        private Dictionary<string, string> _customLicenseToUrlMappings = new();

        
        public string AllowedLicenseTypesOption { get; set; } = default!;
        public string ForbiddenLicenseTypesOption { get; set; } = default!;
        public bool IncludeProjectFile { get; set; }
        public LogLevel LogLevelThreshold { get; set; }
        public string ManualInformationOption { get; set; } = default!;
        public string LicenseToUrlMappingsOption { get; set; } = default!;
        public bool TextOutput { get; set; }
        public string OutputFileName { get; set; } = default!;
        public string OutputDirectory { get; set; } = default!;
        public string ProjectDirectory { get; set; } = default!;
        public string ProjectsFilterOption { get; set; } = default!;
        public string PackagesFilterOption { get; set; } = default!;
        public bool UniqueOnly { get; set; }
        public bool? Print { get; set; }
        public bool JsonOutput { get; set; }
        public bool MarkDownOutput { get; set; }
        public bool ExportLicenseTexts { get; set; }
        public bool IncludeTransitive { get; set; }
        public bool ConvertHtmlToText { get; set; }
        public bool IgnoreSslCertificateErrors { get; set; }
        public bool UseProjectAssetsJson { get; set; }
        public int Timeout { get; set; }
        public string ProxyURL { get; set; } = default!;
        public bool ProxySystemAuth { get; set; }
        public int PageWidth { get; set; }

        public ICollection<string> AllowedLicenseType
        {
            get
            {
                if (_allowedLicenseTypes.Any()) { return _allowedLicenseTypes; }

                return _allowedLicenseTypes = ReadListFromFile<string>(AllowedLicenseTypesOption);
            }
        }

        public ICollection<string> ForbiddenLicenseType
        {
            get
            {
                if (_forbiddenLicenseTypes.Any()) { return _forbiddenLicenseTypes; }

                return _forbiddenLicenseTypes = ReadListFromFile<string>(ForbiddenLicenseTypesOption);
            }
        }

        public ICollection<LibraryInfo> ManualInformation
        {
            get
            {
                if (_manualInformation.Any()) { return _manualInformation; }

                return _manualInformation = ReadListFromFile<LibraryInfo>(ManualInformationOption);
            }
        }

        public ICollection<string> ProjectFilter
        {
            get
            {
                if (_projectFilter.Any()) { return _projectFilter; }

                return _projectFilter = ReadListFromFile<string>(ProjectsFilterOption)
                    .Select(x => x.EnsureCorrectPathCharacter())
                    .ToList();
            }
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public Regex? PackageRegex
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            get
            {
                if (PackagesFilterOption == null) return null;

                // Check if the input is a regular expression that is defined between two forward slashes '/';
                if (UserRegexRegex.IsMatch(PackagesFilterOption))
                {
                    var userRegexString = UserRegexRegex.Replace(PackagesFilterOption, "$2");
                    // Try parse regular expression between forward slashes or hashes
                    try
                    {
                        var parsedExpression = new Regex(userRegexString, RegexOptions.IgnoreCase);
                        return parsedExpression;
                    }
                    // Catch and suppress Argument exception thrown when pattern is invalid
                    catch (ArgumentException e)
                    {
                        throw new ArgumentException($"Cannot parse regex '{userRegexString}'", e);
                    }
                }

                return null;
            }
        }

        public ICollection<string> PackageFilter
        {
            get
            {
                // If we've already found package filters, or the user input is a regular expression,
                // Return the packagesFilter
                if (_packagesFilter.Any() ||
                    (PackagesFilterOption != null && UserRegexRegex.IsMatch(PackagesFilterOption)))
                {
                    return _packagesFilter;
                }

                return _packagesFilter = ReadListFromFile<string>(PackagesFilterOption);
            }
        }

        public IReadOnlyDictionary<string, string> LicenseToUrlMappingsDictionary
        {
            get
            {
                if (_customLicenseToUrlMappings.Any()) { return _customLicenseToUrlMappings; }

                return _customLicenseToUrlMappings = ReadDictionaryFromFile(LicenseToUrlMappingsOption, LicenseToUrlMappings.Default);
            }
        }
    }
}