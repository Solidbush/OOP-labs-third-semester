using Backups.Exceptions;
using Backups.Models;

namespace Backups.Entities.Repository;

public class MemoryRepository : IRepository
{
    private readonly Dictionary<string, List<string>> _repository;
    private readonly Dictionary<string, List<string>> _ziprepository;

    private readonly List<string> _filesPath;
    public MemoryRepository(string name, string path)
    {
        RepositoryPath = path;
        RepositoryName = name;
        _filesPath = new List<string>();
        _repository = new Dictionary<string, List<string>>();
        _ziprepository = new Dictionary<string, List<string>>();
    }

    public string RepositoryPath { get; }
    public string RepositoryName { get; }
    public string Type => nameof(MemoryRepository);

    public bool IsFileExists(string path)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        return _filesPath.Contains(path);
    }

    public bool IsDirectoryExists(string path)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        return _repository.ContainsKey(path);
    }

    public void CreateFile(string path)
    {
        if (IsFileExists(path))
            throw new CreateFileException($"File already exists {path}");
        _filesPath.Add(path);
    }

    public void CopyFile(string pathTo, string pathFrom)
    {
        if (!IsDirectoryExists(pathTo))
            _repository.Add(pathTo, new List<string>());
        _repository[pathTo].Add(pathFrom);
    }

    public void CreateDirectory(string path)
    {
        if (IsDirectoryExists(path))
            throw new CreateDirectoryException($"Directory with path {path} already exists");
        _repository.Add(path, new List<string>());
    }

    public void DeleteDirectory(string path)
    {
        if (!IsDirectoryExists(path))
            throw new DeleteDirectoryException($"Directory with path {path} doesn't exists");
        _repository.Remove(path);
    }

    public void DeleteFile(string path)
    {
    }

    public void CreateZip(string sourcePath, string zipName)
    {
        if (!IsDirectoryExists(sourcePath))
            throw new CopyFileException($"Directory {sourcePath} doesn't exists");
        if (_ziprepository.ContainsKey(Combine(sourcePath, zipName)))
            throw new CreateZipException($"Zip {zipName} already exist");
        _ziprepository[sourcePath] = _repository[sourcePath];
    }

    public string Combine(string path, string name)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        return path + "\\" + name;
    }

    public void ExtractZipFile(string zipFilePath, string targetDirectory)
    {
        if (!_ziprepository.ContainsKey(zipFilePath))
            throw new ExtractFileException($"Zip file {zipFilePath} doesn't exist");
        if (!_repository.ContainsKey(targetDirectory))
            throw new ExtractFileException($"Dictionary {targetDirectory} doesn't exist");
        _repository[targetDirectory] = _ziprepository[zipFilePath];
    }

    public string FileName(string filePath)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));
        return filePath;
    }

    public IReadOnlyCollection<ZipPathFileNamesPair> GetRepository()
    {
        var storages = new List<ZipPathFileNamesPair>();
        foreach (var tempPair in _repository)
        {
            var tempStorage = new ZipPathFileNamesPair(tempPair.Key, tempPair.Value);
            storages.Add(tempStorage);
        }

        return storages;
    }

    public IReadOnlyCollection<ZipPathFileNamesPair> GetZipRepository()
    {
        var storages = new List<ZipPathFileNamesPair>();
        foreach (var tempPair in _ziprepository)
        {
            var tempStorage = new ZipPathFileNamesPair(tempPair.Key, tempPair.Value);
            storages.Add(tempStorage);
        }

        return storages;
    }
}