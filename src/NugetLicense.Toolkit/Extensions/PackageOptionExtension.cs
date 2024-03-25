using NugetLicense.Toolkit.Exceptions;

namespace NugetLicense.Toolkit.Extensions
{
    public static class PackageOptionExtension
    {
        public static void Validate(this IPackageOptions options) 
        {
            var errors = new List<PropertyValidationMessage>();

            if (string.IsNullOrWhiteSpace(options.ProjectDirectory))
                errors.Add(new PropertyValidationMessage{
                    Property = nameof(options.ProjectDirectory),
                    Message = "Input the Directory Path (csproj or fsproj file)"
                });

            if (options.ConvertHtmlToText && !options.ExportLicenseTexts)
                errors.Add(new PropertyValidationMessage
                {
                    Property = nameof(options.ConvertHtmlToText),
                    Message = $"{nameof(options.ConvertHtmlToText)}\tThis option requires the {nameof(options.ExportLicenseTexts)}"
                });

            if (options.ForbiddenLicenseType.Any() && options.AllowedLicenseType.Any())
                errors.Add(new PropertyValidationMessage
                {
                    Property = nameof(options.AllowedLicenseType),
                    Message = $"{nameof(options.AllowedLicenseType)}\tCannot be used with the {nameof(options.ForbiddenLicenseType)}"
                });

            if (options.UseProjectAssetsJson && !options.IncludeTransitive)
                errors.Add(new PropertyValidationMessage
                {
                    Property = nameof(options.UseProjectAssetsJson),
                    Message = $"{nameof(options.UseProjectAssetsJson)}\tThis option always includes transitive references, so you must also provide the {nameof(options.IncludeTransitive)}"
                });

            if (options.Timeout < 1)
                errors.Add(new PropertyValidationMessage
                {
                    Property = nameof(options.Timeout),
                    Message = $"{nameof(options.Timeout)}\tThe timeout must be a positive number."
                });

            if (errors.Count > 0)
                throw new PackageOptionsValidationException(errors);
        }
    }
}
