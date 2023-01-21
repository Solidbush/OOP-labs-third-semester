using System.Drawing;
using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Exceptions;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Services;

public class BackupTask : IBackupTask
{
    [JsonProperty]
    private List<RestorePoint> _points;
    [JsonProperty]
    private List<BackupObject> _backupObjects;
    public BackupTask(string backupName, IRepository repository, ICompressionMetod method, List<RestorePoint> points = null, List<BackupObject> backupObjects = null)
    {
        if (string.IsNullOrWhiteSpace(backupName))
            throw new ArgumentNullException(backupName);
        BackupName = backupName;
        Repository = repository;
        Method = method;
        _points = points ?? new List<RestorePoint>();
        _backupObjects = backupObjects ?? new List<BackupObject>();
        FullBackupTaskPath = Repository.Combine(repository.RepositoryPath, Repository.Combine(repository.RepositoryName, backupName));
    }

    public string BackupName { get; }
    public string FullBackupTaskPath { get; }
    public ICompressionMetod Method { get; }
    public IRepository Repository { get; }

    public BackupObject AddBackupObject(BackupObject backupObject)
    {
        _backupObjects.Add(backupObject);
        return backupObject;
    }

    public IReadOnlyCollection<BackupObject> GetBackupObjects()
    {
        return _backupObjects;
    }

    public IReadOnlyCollection<RestorePoint> GetRestorePoints()
    {
        return _points;
    }

    public BackupObject DeleteBackupObject(BackupObject backupObject)
    {
        try
        {
            _backupObjects.Remove(backupObject);
            return backupObject;
        }
        catch
        {
            throw new DeleteBackupObjectException(
                $"File with name: {backupObject.Name} with path: {backupObject.Path} can't be delete!");
        }
    }

    public void DeleteRestorePoint(string pointName)
    {
        if (string.IsNullOrWhiteSpace(pointName)) throw new ArgumentNullException(nameof(pointName));
        RestorePoint point = _points.Single(p => p.PointName == pointName);
        _points.Remove(point);
    }

    public RestorePoint MakeBackUp(string pointName, DateTime dateOfCreation)
    {
        if (string.IsNullOrWhiteSpace(pointName))
            throw new ArgumentNullException(pointName);
        var tempRestorePoint = new RestorePoint(FullBackupTaskPath, pointName, dateOfCreation, _backupObjects);
        if (!Repository.IsDirectoryExists(tempRestorePoint.FullPointPath))
            Repository.CreateDirectory(tempRestorePoint.FullPointPath);
        var storages = Method.StorageMethod(Repository.Combine(FullBackupTaskPath, pointName), _backupObjects);
        foreach (var keyPair in storages)
        {
            if (!Repository.IsDirectoryExists(keyPair.ZipPath))
                Repository.CreateDirectory(keyPair.ZipPath);
            foreach (var filePath in keyPair.GetObjects())
            {
                Repository.CopyFile(Repository.Combine(keyPair.ZipPath, Repository.FileName(filePath)), filePath);
            }

            Repository.CreateZip(keyPair.ZipPath, keyPair.ZipPath);

            Repository.DeleteDirectory(keyPair.ZipPath);
        }

        _points.Add(tempRestorePoint);
        return tempRestorePoint;
    }
}