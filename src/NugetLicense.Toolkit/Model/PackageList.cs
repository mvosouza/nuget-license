namespace NugetLicense.Toolkit
{
    public class PackageList : Dictionary<string, Package>
    {
        public PackageList(int capacity = 50) : base(capacity) { }
    }
}