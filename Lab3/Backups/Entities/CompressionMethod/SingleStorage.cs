using Backups.Models;

namespace Backups.Entities.CompressionMethod;

public class SingleStorage : ICompressionMetod
{
    public string Type => nameof(SingleStorage);

    public IReadOnlyCollection<ZipPathFileNamesPair> StorageMethod(string zipPath, List<BackupObject> objects)
    {
        if (string.IsNullOrWhiteSpace(zipPath))
            throw new ArgumentNullException(zipPath);
        string directoryPath = Path.Combine(zipPath, "zip");
        var tempListBackupObjects = new List<string>();
        foreach (var backupObject in objects)
        {
            tempListBackupObjects.Add(Path.Combine(backupObject.Path, backupObject.Name));
        }

        var tempZipPathNamePair = new ZipPathFileNamesPair(directoryPath, tempListBackupObjects);
        var tempStorage = new List<ZipPathFileNamesPair>();
        tempStorage.Add(tempZipPathNamePair);

        return tempStorage;
    }
}