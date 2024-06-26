﻿namespace NugetLicense.Toolkit.Model
{
    public class ValidationResult<T> : IValidationResult<T>
    {
        public bool IsValid { get; set; } = false;

        public IReadOnlyCollection<T> InvalidPackages { get; set; } = new List<T>();
    }
}