using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Models;
using Backups.Services;
using Xunit;

namespace Backups.Test;

public class TestBackups
{
    private readonly MemoryRepository _memoryRepository;

    public TestBackups()
    {
        _memoryRepository = new MemoryRepository("Test", "C:\\");
    }

    [Fact]
    public void CreateSomeFilesInRepository_RepositoryContainsThisFiles()
    {
        _memoryRepository.CreateFile("C:\\Test\\a.txt");
        _memoryRepository.CreateFile("C:\\Test\\b.txt");
        _memoryRepository.CreateFile("C:\\Test\\c.txt");

        Assert.True(_memoryRepository.IsFileExists("C:\\Test\\a.txt"));
        Assert.True(_memoryRepository.IsFileExists("C:\\Test\\b.txt"));
        Assert.True(_memoryRepository.IsFileExists("C:\\Test\\c.txt"));
    }

    [Fact]
    public void MakeTwoBackupSplitMethod_CreatedTwoRestorePointAndTreeStorages()
    {
        _memoryRepository.CreateFile("C:\\Test\\a.txt");
        _memoryRepository.CreateFile("C:\\Test\\b.txt");
        _memoryRepository.CreateFile("C:\\Test\\c.txt");
        var backupObjectFirst = new BackupObject("C:\\Test\\", "a.txt");
        var backupObjectSecond = new BackupObject("C:\\Test\\", "b.txt");
        var backupObjectThree = new BackupObject("C:\\Test\\", "c.txt");

        var backupFirst = new BackupTask("BackupFirs", _memoryRepository, new SplitStorage());
        backupFirst.AddBackupObject(backupObjectFirst);
        backupFirst.AddBackupObject(backupObjectSecond);
        backupFirst.AddBackupObject(backupObjectThree);
        backupFirst.DeleteBackupObject(backupObjectThree);
        Assert.Equal(2, backupFirst.GetBackupObjects().Count);
        backupFirst.MakeBackUp("FirstPoint", DateTime.UnixEpoch);
        backupFirst.DeleteBackupObject(backupObjectSecond);
        backupFirst.MakeBackUp("SecondPoint", DateTime.UnixEpoch);
        Assert.Equal(3, _memoryRepository.GetZipRepository().Count());
    }

    [Fact]
    public void MakeTwoBackupSingleMethod_CreatedTwoRestorePointAndTreeStorages()
    {
        _memoryRepository.CreateFile("C:\\Test\\a.txt");
        _memoryRepository.CreateFile("C:\\Test\\b.txt");
        _memoryRepository.CreateFile("C:\\Test\\c.txt");
        var backupObjectFirst = new BackupObject("C:\\Test\\", "a.txt");
        var backupObjectSecond = new BackupObject("C:\\Test\\", "b.txt");
        var backupObjectThree = new BackupObject("C:\\Test\\", "c.txt");

        var backupFirst = new BackupTask("BackupFirs", _memoryRepository, new SingleStorage());
        backupFirst.AddBackupObject(backupObjectFirst);
        backupFirst.AddBackupObject(backupObjectSecond);
        backupFirst.AddBackupObject(backupObjectThree);
        backupFirst.DeleteBackupObject(backupObjectThree);
        Assert.Equal(2, backupFirst.GetBackupObjects().Count);
        backupFirst.MakeBackUp("FirstPoint", DateTime.UnixEpoch);
        backupFirst.DeleteBackupObject(backupObjectSecond);
        backupFirst.MakeBackUp("SecondPoint", DateTime.UnixEpoch);
        Assert.Equal(2, _memoryRepository.GetZipRepository().Count());
    }
}