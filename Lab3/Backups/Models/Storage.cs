using Backups.Exceptions;

namespace Backups.Models;

public class Storage : IEquatable<Storage>
{
    public Storage(string zipPath, string storageName)
    {
        if (string.IsNullOrWhiteSpace(zipPath))
            throw new PathNullException("Storage can't has null path!");
        if (string.IsNullOrWhiteSpace(storageName))
            throw new NameValidationException("Storage name can't be null!");
        ZipPath = zipPath;
        StorageName = storageName;
        FullZipPath = Path.Combine(zipPath, storageName);
    }

    public string ZipPath { get; }
    public string StorageName { get; }
    public string FullZipPath { get; }

    public bool Equals(Storage other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ZipPath == other.ZipPath && StorageName == other.StorageName && FullZipPath == other.FullZipPath;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Storage)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ZipPath, StorageName, FullZipPath);
    }
}