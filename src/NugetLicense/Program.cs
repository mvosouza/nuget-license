using CommandLine;
using NugetLicense.Toolkit.Exceptions;
using System.Reflection;

namespace NugetLicense.Toolkit
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<CommandPackageOptions>(args);
            return await result.MapResult(
                options => Execute(options),
                errors => Task.FromResult(1));
        }

        private static async Task<int> Execute(CommandPackageOptions options)
        {
            //if (string.IsNullOrWhiteSpace(options.ProjectDirectory))
            //{
            //    Console.WriteLine("ERROR(S):");
            //    Console.WriteLine("-i\tInput the Directory Path (csproj or fsproj file)");

            //    return 1;
            //}

            //if (options.ConvertHtmlToText && !options.ExportLicenseTexts)
            //{
            //    Console.WriteLine("ERROR(S):");
            //    Console.WriteLine("--convert-html-to-text\tThis option requires the --export-license-texts option.");

            //    return 1;
            //}

            //if (options.ForbiddenLicenseType.Any() && options.AllowedLicenseType.Any())
            //{
            //    Console.WriteLine("ERROR(S):");
            //    Console.WriteLine("--allowed-license-types\tCannot be used with the --forbidden-license-types option.");

            //    return 1;
            //}

            //if (options.UseProjectAssetsJson && !options.IncludeTransitive)
            //{
            //    Console.WriteLine("ERROR(S):");
            //    Console.WriteLine("--use-project-assets-json\tThis option always includes transitive references, so you must also provide the -t option.");

            //    return 1;
            //}

            //if (options.Timeout < 1)
            //{
            //    Console.WriteLine("ERROR(S):");
            //    Console.WriteLine("--timeout\tThe timeout must be a positive number.");

            //    return 1;
            //}

            try
            {
                var methods = new Methods(options);
                var projectsWithPackages = await methods.GetPackages();
                var mappedLibraryInfo = methods.MapPackagesToLibraryInfo(projectsWithPackages);

                HandleInvalidLicenses(methods, mappedLibraryInfo, options);

                if (options.ExportLicenseTexts)
                {
                    await methods.ExportLicenseTexts(mappedLibraryInfo);
                }

                mappedLibraryInfo = methods.HandleDeprecateMSFTLicense(mappedLibraryInfo);

                if (options.Print == true)
                {
                    Console.WriteLine("\nProject Reference(s) Analysis...");
                    methods.PrintLicenses(mappedLibraryInfo);
                }

                methods.Output(mappedLibraryInfo);

                return 0;
            }
            catch (PackageOptionsValidationException ex) 
            {
                foreach (var propertyValidation in ex.Errors) 
                {
                    var message = propertyValidation.Message;

                    // TODO: Replace the property name to option name
                    PropertyInfo? prop = typeof(CommandPackageOptions).GetProperty(propertyValidation.Property);
                    if (prop != null) { 
                        OptionAttribute? optionAttribute = (OptionAttribute?)Attribute.GetCustomAttribute(prop, typeof(OptionAttribute));
                        if (optionAttribute != null) 
                        {
                            var name = optionAttribute.LongName ?? optionAttribute.ShortName;
                            message.Replace(propertyValidation.Property, name);
                        }
                    }
                    Console.Error.WriteLine(message);
                }
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        private static void HandleInvalidLicenses(Methods methods, List<LibraryInfo> libraries, IPackageOptions options)
        {
            var invalidPackages = methods.ValidateLicenses(libraries);

            if (!invalidPackages.IsValid)
            {
                throw new InvalidLicensesException<LibraryInfo>(invalidPackages, options);
            }
        }
    }
}
