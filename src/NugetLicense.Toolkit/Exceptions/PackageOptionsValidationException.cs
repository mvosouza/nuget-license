using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
