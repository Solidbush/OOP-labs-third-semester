using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Extra.Entities.Logger;
using Backups.Extra.Entities.RestoreVersionMethod;
using Backups.Extra.Entities.TakePointsMethod;
using Backups.Models;
using Backups.Services;
using Newtonsoft.Json;

namespace Backups.Extra.Services;

public class BackupTaskExtra
{
    [JsonProperty]
    private readonly BackupTask _oldTask;
    [JsonProperty]
    private readonly ILoggerMessenger _messenger;
    public BackupTaskExtra(BackupTask oldTask, ITakePointsMethod takePointsMethod, IRestoreVersionMethod restoreVersionMethod, ILoggerMessenger messenger)
    {
        _oldTask = oldTask;
        _messenger = messenger;
        TakePointsMethod = takePointsMethod;
        RestoreVersionMethod = restoreVersionMethod;
    }

    public ITakePointsMethod TakePointsMethod { get; }
    public IRestoreVersionMethod RestoreVersionMethod { get; }
    [JsonIgnore]
    public IRepository Repository => _oldTask.Repository;
    public IReadOnlyCollection<BackupObject> GetBackupObjects()
    {
        return _oldTask.GetBackupObjects();
    }

    public IReadOnlyCollection<RestorePoint> GetRestorePoints()
    {
        return _oldTask.GetRestorePoints();
    }

    public BackupObject AddBackupObject(BackupObject backupObject)
    {
        _messenger.SendLog("Added obj");
        return _oldTask.AddBackupObject(backupObject);
    }

    public BackupObject DeleteBackupObject(BackupObject backupObject)
    {
        _messenger.SendLog("Deleted obj");
        return _oldTask.DeleteBackupObject(backupObject);
    }

    public RestorePoint MakeBackUp(string pointName, DateTime dateOfCreation)
    {
        _messenger.SendLog("Backup created");
        RestorePoint data = _oldTask.MakeBackUp(pointName, dateOfCreation);
        return data;
    }

    public void Restore(RestorePoint restorePoint, ICompressionMetod method, IRepository repository, string dirPath = null)
    {
        _messenger.SendLog($"Restored to {restorePoint.PointName}");
        RestoreVersionMethod.Execute(restorePoint, method, repository, dirPath);
    }

    public void Clean()
    {
        IReadOnlyList<RestorePoint> restorePoints = TakePointsMethod.Execute(_oldTask.GetRestorePoints().ToList());
        foreach (RestorePoint restorePoint in restorePoints)
        {
            _oldTask.DeleteRestorePoint(restorePoint.PointName);
            _oldTask.Repository.DeleteDirectory(restorePoint.FullPointPath);
        }

        _messenger.SendLog("Cleaned");
    }

    public void Merge(string pointName)
    {
        var restorePoints = _oldTask.GetRestorePoints().ToList();
        RestorePoint toMerge = restorePoints.Single(p => p.PointName == pointName);
        int version = restorePoints.IndexOf(toMerge) + 1;
        var toDel = restorePoints.Skip(version).ToList();
        foreach (RestorePoint restorePoint in toDel)
        {
            _oldTask.Repository.DeleteDirectory(restorePoint.FullPointPath);
        }

        _messenger.SendLog("Merged");
    }
}
