using Backups.Models;

namespace Backups.Entities.Repository;

public interface IRepository
{
    string Type { get; }
    string RepositoryPath { get; }
    string RepositoryName { get; }

    bool IsFileExists(string path);
    bool IsDirectoryExists(string path);
    void CreateFile(string path);
    void CopyFile(string pathTo, string pathFrom);
    void CreateDirectory(string path);
    void DeleteDirectory(string path);
    void DeleteFile(string path);
    void CreateZip(string sourcePath, string zipName);
    string Combine(string path, string name);
    void ExtractZipFile(string zipFilePath, string targetDirectory);
    string FileName(string filePath);
    IReadOnlyCollection<ZipPathFileNamesPair> GetRepository();
}