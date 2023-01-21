using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Models;

namespace Backups.Services;

public interface IBackupTask
{
    IReadOnlyCollection<RestorePoint> GetRestorePoints();
    IReadOnlyCollection<BackupObject> GetBackupObjects();
    BackupObject AddBackupObject(BackupObject backupObject);
    BackupObject DeleteBackupObject(BackupObject backupObject);
    RestorePoint MakeBackUp(string pointName, DateTime dateOfCreation);
}