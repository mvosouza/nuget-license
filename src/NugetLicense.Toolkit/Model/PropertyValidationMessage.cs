namespace NugetLicense.Toolkit.Model
{
    public class PropertyValidationMessage
    {
        public string Property { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string[] PropertyNameMessageReplacer { get; set; } = new string[] { };
    }
}
