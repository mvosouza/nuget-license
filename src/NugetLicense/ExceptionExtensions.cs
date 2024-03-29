using CommandLine;
using NugetLicense.Toolkit.Exceptions;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NugetLicense.Toolkit.Extensions
{
    public static class ExceptionExtensions
    {
        public static IList<string> GetErrorMessages(this PackageOptionsValidationException ex)
        {
            var messages = new List<string>();

            foreach (var propertyValidation in ex.Errors)
            {
                var message = propertyValidation.Message;

                foreach (string replacer in propertyValidation.PropertyNameMessageReplacer)
                {
                    PropertyInfo? prop = typeof(CommandPackageOptions).GetProperty(replacer);
                    if (prop != null)
                    {
                        OptionAttribute? optionAttribute = (OptionAttribute?)Attribute.GetCustomAttribute(prop, typeof(OptionAttribute));
                        if (optionAttribute != null)
                        {
                            var name = optionAttribute.LongName ?? optionAttribute.ShortName;
                            message = message.Replace(propertyValidation.Property, name);
                        }
                    }
                }

                messages.Add(message);
            }

            return messages;
        }
    }
}
