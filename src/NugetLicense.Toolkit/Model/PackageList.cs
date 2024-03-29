namespace NugetLicense.Toolkit.Model
{
    public class PackageList : Dictionary<string, Package>
    {
        public PackageList(int capacity = 50) : base(capacity) { }
    }
}