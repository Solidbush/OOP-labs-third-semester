using Backups.Models;

namespace Backups.Entities.CompressionMethod;

public interface ICompressionMetod
{
    string Type { get; }
    IReadOnlyCollection<ZipPathFileNamesPair> StorageMethod(string zipPath, List<BackupObject> objects);
}