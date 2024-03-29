using NugetLicense.Toolkit.Model;

namespace NugetLicense.Toolkit.Exceptions
{
    public class PackageOptionsValidationException : Exception
    {
        public ICollection<PropertyValidationMessage> Errors;

        public PackageOptionsValidationException(ICollection<PropertyValidationMessage>? errors) 
            : base("Package options validations error")
        {
            Errors = errors ?? new List<PropertyValidationMessage>();
        }
    }
}
