namespace NugetLicense.Toolkit.Model
{
    public interface IValidationResult<T>
    {
        bool IsValid { get; }
        IReadOnlyCollection<T> InvalidPackages { get; }
    }
}