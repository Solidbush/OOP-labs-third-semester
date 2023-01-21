using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Models;

namespace Backups.Extra.Entities.RestoreVersionMethod;

public class RestoreToOrigin : IRestoreVersionMethod
{
    public void Execute(RestorePoint restorePoint, ICompressionMetod method, IRepository repository, string dirPath = null)
    {
        switch (method)
        {
            case SplitStorage:
                var backupObjects = restorePoint.GetBackupObjects().ToList();
                BackupObject backupObject = backupObjects.First();
                string zipPath = $"{restorePoint.FullPointPath}\\{Path.GetFileNameWithoutExtension(backupObject.Name)}.zip";
                repository.ExtractZipFile(zipPath, restorePoint.FullPointPath);
                foreach (BackupObject o in backupObjects)
                {
                    File.Move($"{restorePoint.FullPointPath}\\{o.Name}", $"{Path.Combine(o.Path, o.Name)}", true);
                }

                break;
            case SingleStorage:
                var backupObjectsSingle = restorePoint.GetBackupObjects().ToList();
                BackupObject backupObjectSingle = backupObjectsSingle.First();
                string zipPathSingle = $"{restorePoint.FullPointPath}\\zip.zip";
                repository.ExtractZipFile(zipPathSingle, restorePoint.FullPointPath);
                foreach (BackupObject o in backupObjectsSingle)
                {
                    File.Move($"{restorePoint.FullPointPath}\\{o.Name}", $"{Path.Combine(o.Path, o.Name)}", true);
                }

                break;
        }
    }
}