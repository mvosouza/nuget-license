namespace NugetLicense.Toolkit
{
    public interface IValidationResult<T>
    {
        bool IsValid { get; }
        IReadOnlyCollection<T> InvalidPackages { get; }
    }
}