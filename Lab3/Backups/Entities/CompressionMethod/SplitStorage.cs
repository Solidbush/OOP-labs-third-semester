using Backups.Models;

namespace Backups.Entities.CompressionMethod;
public class SplitStorage : ICompressionMetod
{
    public string Type => nameof(SplitStorage);
    public IReadOnlyCollection<ZipPathFileNamesPair> StorageMethod(string zipPath, List<BackupObject> objects)
    {
        if (string.IsNullOrWhiteSpace(zipPath))
            throw new ArgumentNullException(zipPath);
        var tempStorage = new List<ZipPathFileNamesPair>();
        string filePath;
        var fileNamesPath = new List<string>();
        foreach (var backupObject in objects)
        {
            filePath = Path.Combine(zipPath, Path.GetFileNameWithoutExtension(backupObject.Name));
            foreach (var backupObjectName in objects)
            {
                fileNamesPath.Add(Path.Combine(backupObject.Path, backupObject.Name));
            }

            var tempFileNamesPair = new ZipPathFileNamesPair(filePath, fileNamesPath);
            tempStorage.Add(tempFileNamesPair);
        }

        return tempStorage;
    }
}