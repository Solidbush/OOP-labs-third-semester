using Backups.Exceptions;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Entities;

public class RestorePoint
{
    [JsonProperty]
    private readonly List<BackupObject> _objects;
    [JsonIgnore]
    private readonly List<Storage> _storages;

    public RestorePoint(string pointPath, string pointName, DateTime dateOfCreation, List<BackupObject> objects)
    {
        if (string.IsNullOrWhiteSpace(pointPath))
            throw new PathNullException("Restore point can't has null path!");
        if (string.IsNullOrWhiteSpace(pointName))
            throw new NameValidationException("Restore point can't has null name!");
        PointPath = pointPath;
        PointName = pointName;
        FullPointPath = Path.Combine(pointPath, pointName);
        _objects = objects;
        DateOfCreation = dateOfCreation;
        _storages = new List<Storage>();
    }

    public string PointPath { get; }
    public string PointName { get; }
    public string FullPointPath { get; }
    public DateTime DateOfCreation { get; }

    public IReadOnlyList<BackupObject> GetBackupObjects()
    {
        return _objects;
    }

    public IReadOnlyCollection<Storage> GetStorages()
    {
        return _storages;
    }
}