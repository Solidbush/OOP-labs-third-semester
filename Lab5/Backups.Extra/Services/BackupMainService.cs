using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Extra.Entities.Logger;
using Backups.Extra.Entities.RestoreVersionMethod;
using Backups.Extra.Entities.TakePointsMethod;
using Backups.Models;
using Backups.Services;

namespace Backups.Extra.Services;

public class BackupMainService
{
    private readonly ReRunService _reRunService;
    private readonly BackupTaskExtra _backupService;

    public BackupMainService(string reRunFilePath, BackupTask oldTask = null, ITakePointsMethod takePointsMethod = null, IRestoreVersionMethod restoreVersionMethod = null, ILoggerMessenger messenger = null)
    {
        _reRunService = new ReRunService(reRunFilePath);
        _backupService = _reRunService.RestoreState() ?? new BackupTaskExtra(oldTask, takePointsMethod, restoreVersionMethod, messenger);
    }

    public ITakePointsMethod TakePointsMethod => _backupService.TakePointsMethod;
    public IRestoreVersionMethod RestoreVersionMethod => _backupService.RestoreVersionMethod;
    public IReadOnlyCollection<BackupObject> GetBackupObjects()
    {
        return _backupService.GetBackupObjects();
    }

    public IReadOnlyCollection<RestorePoint> GetRestorePoints()
    {
        return _backupService.GetRestorePoints();
    }

    public BackupObject AddBackupObject(BackupObject backupObject)
    {
        return _backupService.AddBackupObject(backupObject);
    }

    public BackupObject DeleteBackupObject(BackupObject backupObject)
    {
        return _backupService.DeleteBackupObject(backupObject);
    }

    public RestorePoint MakeBackUp(string pointName, DateTime dateOfCreation)
    {
        RestorePoint data = _backupService.MakeBackUp(pointName, dateOfCreation);
        _reRunService.SetState(_backupService);
        return data;
    }

    public void Restore(RestorePoint restorePoint, ICompressionMetod method, IRepository repository, string dirPath = null)
    {
        _backupService.Restore(restorePoint, method, repository, dirPath);
    }

    public void Clean()
    {
        _backupService.Clean();
        _reRunService.SetState(_backupService);
    }

    public void Merge(string pointName)
    {
        _backupService.Merge(pointName);
        _reRunService.SetState(_backupService);
    }
}