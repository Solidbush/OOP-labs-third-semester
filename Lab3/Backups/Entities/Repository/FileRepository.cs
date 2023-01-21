using System.IO.Compression;
using Backups.Exceptions;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities.Repository;

public class FileRepository : IRepository
{
    public FileRepository(string repositoryName, string repositoryPath)
    {
        RepositoryPath = repositoryPath ?? throw new ArgumentNullException(nameof(repositoryPath));
        RepositoryName = repositoryName ?? throw new ArgumentNullException(nameof(repositoryName));
        Directory.CreateDirectory(Combine(repositoryPath, repositoryName));
    }

    public string Type => nameof(FileRepository);
    public string RepositoryPath { get; }
    public string RepositoryName { get; }

    public string Combine(string path, string name)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        return Path.Combine(path, name);
    }

    public bool IsFileExists(string path)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        FileInfo fileInfo = new FileInfo(path);
        return fileInfo.Exists;
    }

    public bool IsDirectoryExists(string path)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        return Directory.Exists(path);
    }

    public void CreateFile(string path)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
            throw new CreateFileException($"File already exists in path {path}");
        fileInfo.Create().Close();
    }

    public void CreateDirectory(string path)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        if (Directory.Exists(path))
            throw new CreateDirectoryException($"Directory already exists in {path}");
        Directory.CreateDirectory(path);
    }

    public void DeleteDirectory(string path)
    {
        if (IsDirectoryExists(path))
            Directory.Delete(path, true);
    }

    public void DeleteFile(string path)
    {
        if (File.Exists(path)) File.Delete(path);
    }

    public void CopyFile(string pathTo, string pathFrom)
    {
        if (pathTo == null)
            throw new ArgumentNullException(nameof(pathTo));
        if (pathFrom == null)
            throw new ArgumentNullException(nameof(pathFrom));
        FileInfo fileInfo = new FileInfo(pathFrom);
        if (!fileInfo.Exists)
            throw new CopyFileException($"File doesn't exists in directory {pathFrom}");
        fileInfo.CopyTo(pathTo, true);
    }

    public void CreateZip(string sourcePath, string zipName)
    {
        if (sourcePath == null)
            throw new ArgumentNullException(nameof(sourcePath));
        if (zipName == null)
            throw new ArgumentNullException(nameof(zipName));
        if (!Directory.Exists(sourcePath))
            throw new CreateZipException($"Can't create zip {zipName}, because directory {sourcePath} doesn't exists");
        ZipFile.CreateFromDirectory(sourcePath, zipName + ".zip");
    }

    public void ExtractZipFile(string zipFilePath, string targetDirectory)
    {
        FileInfo fileInfo = new FileInfo(zipFilePath);
        if (!File.Exists(zipFilePath))
            throw new ExtractFileException($"File with name {zipFilePath} doesn't exists");
        if (!Directory.Exists(targetDirectory))
            throw new ExtractFileException($"Directory with name {targetDirectory} doesn't exists");
        ZipFile.ExtractToDirectory(zipFilePath, targetDirectory);
    }

    public string FileName(string filePath)
    {
        return new FileInfo(filePath).Name;
    }

    public IReadOnlyCollection<ZipPathFileNamesPair> GetRepository()
    {
        return new List<ZipPathFileNamesPair>();
    }
}