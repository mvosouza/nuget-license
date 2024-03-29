using System.Diagnostics.CodeAnalysis;
using NugetLicense.Toolkit.Model;

namespace NugetLicense.Toolkit
{
    public class LibraryNameAndVersionComparer : IEqualityComparer<LibraryInfo>
    {
        public static LibraryNameAndVersionComparer Default = new LibraryNameAndVersionComparer();

        public bool Equals([AllowNull] LibraryInfo x, [AllowNull] LibraryInfo y)
        {
            return x?.PackageName == y?.PackageName
                && x?.PackageVersion == y?.PackageVersion;
        }

        public int GetHashCode([DisallowNull] LibraryInfo obj)
        {
            return obj.PackageName.GetHashCode() ^ obj.PackageVersion.GetHashCode();
        }
    }
}