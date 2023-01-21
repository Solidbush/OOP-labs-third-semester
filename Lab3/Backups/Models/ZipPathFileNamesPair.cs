using Newtonsoft.Json;

namespace Backups.Models;

public class ZipPathFileNamesPair : IEquatable<ZipPathFileNamesPair>
{
    [JsonProperty]
    private readonly List<string> _objects;
    public ZipPathFileNamesPair(string zipPath, List<string> objects)
    {
        if (string.IsNullOrWhiteSpace(zipPath))
            throw new ArgumentNullException(nameof(zipPath));
        ZipPath = zipPath;
        _objects = objects;
    }

    public string ZipPath { get; }

    public IReadOnlyCollection<string> GetObjects()
    {
        return _objects;
    }

    public bool Equals(ZipPathFileNamesPair other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ZipPath == other.ZipPath;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ZipPathFileNamesPair)obj);
    }

    public override int GetHashCode()
    {
        return ZipPath != null ? ZipPath.GetHashCode() : 0;
    }
}