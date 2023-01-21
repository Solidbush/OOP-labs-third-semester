using Backups.Exceptions;

namespace Backups.Models
{
    public class BackupObject : IEquatable<BackupObject>
    {
        public BackupObject(string path, string name)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new PathNullException();
            if (string.IsNullOrWhiteSpace(name))
                throw new NameValidationException();
            Path = path;
            Name = name;
        }

        public string Path { get; }
        public string Name { get; }

        public bool Equals(BackupObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Path == other.Path && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BackupObject)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path, Name);
        }
    }
}