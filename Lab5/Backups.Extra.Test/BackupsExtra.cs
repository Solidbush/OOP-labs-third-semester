using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Extra.Entities.Logger;
using Backups.Extra.Entities.RestoreVersionMethod;
using Backups.Extra.Entities.TakePointsMethod;
using Backups.Extra.Services;
using Backups.Models;
using Backups.Services;
using Xunit;

namespace Backups.Extra.Test;

public class TestBackups
{
    [Fact]
    public void CreateSomeFilesInRepository_RepositoryContainsThisFiles()
    {
        var backupTaskExtra = new BackupTaskExtra(new BackupTask("FirstBackup", new MemoryRepository("Repository", @"D:\Users\Volirvag\Desktop"), new SingleStorage()), new TakeCountPoints(2), new RestoreToOrigin(), new ConsoleLogger());
        backupTaskExtra.Repository.CreateFile("C:\\Test\\a.txt");
        backupTaskExtra.Repository.CreateFile("C:\\Test\\b.txt");
        backupTaskExtra.Repository.CreateFile("C:\\Test\\c.txt");

        Assert.True(backupTaskExtra.Repository.IsFileExists("C:\\Test\\a.txt"));
        Assert.True(backupTaskExtra.Repository.IsFileExists("C:\\Test\\b.txt"));
        Assert.True(backupTaskExtra.Repository.IsFileExists("C:\\Test\\c.txt"));
    }

    [Fact]
    public void MakeTwoBackupSplitMethod_CreatedTwoRestorePointAndTreeStorages()
    {
        var backupTaskExtra = new BackupTaskExtra(new BackupTask("FirstBackup", new MemoryRepository("Repository", @"D:\Users\Volirvag\Desktop"), new SingleStorage()), new TakeCountPoints(2), new RestoreToOrigin(), new ConsoleLogger());

        backupTaskExtra.Repository.CreateFile("C:\\Test\\a.txt");
        backupTaskExtra.Repository.CreateFile("C:\\Test\\b.txt");
        backupTaskExtra.Repository.CreateFile("C:\\Test\\c.txt");
        var backupObjectFirst = new BackupObject("C:\\Test\\", "a.txt");
        var backupObjectSecond = new BackupObject("C:\\Test\\", "b.txt");
        var backupObjectThree = new BackupObject("C:\\Test\\", "c.txt");

        var backupFirst = new BackupTask("BackupFirs", backupTaskExtra.Repository, new SplitStorage());
        backupFirst.AddBackupObject(backupObjectFirst);
        backupFirst.AddBackupObject(backupObjectSecond);
        backupFirst.AddBackupObject(backupObjectThree);
        backupFirst.DeleteBackupObject(backupObjectThree);
        Assert.Equal(2, backupFirst.GetBackupObjects().Count);
        backupFirst.MakeBackUp("FirstPoint", DateTime.UnixEpoch);
        backupFirst.DeleteBackupObject(backupObjectSecond);
        backupFirst.MakeBackUp("SecondPoint", DateTime.UnixEpoch);
        Assert.Equal(7, backupTaskExtra.Repository.GetRepository().Count);
    }

    [Fact]
    public void MakeTwoBackupSingleMethod_CreatedTwoRestorePointAndTreeStorages()
    {
        var backupTaskExtra = new BackupTaskExtra(new BackupTask("FirstBackup", new MemoryRepository("Repository", @"D:\Users\Volirvag\Desktop"), new SingleStorage()), new TakeCountPoints(2), new RestoreToOrigin(), new ConsoleLogger());

        backupTaskExtra.Repository.CreateFile("C:\\Test\\a.txt");
        backupTaskExtra.Repository.CreateFile("C:\\Test\\b.txt");
        backupTaskExtra.Repository.CreateFile("C:\\Test\\c.txt");
        var backupObjectFirst = new BackupObject("C:\\Test\\", "a.txt");
        var backupObjectSecond = new BackupObject("C:\\Test\\", "b.txt");
        var backupObjectThree = new BackupObject("C:\\Test\\", "c.txt");

        var backupFirst = new BackupTask("BackupFirs",  backupTaskExtra.Repository, new SingleStorage());
        backupFirst.AddBackupObject(backupObjectFirst);
        backupFirst.AddBackupObject(backupObjectSecond);
        backupFirst.AddBackupObject(backupObjectThree);
        backupFirst.DeleteBackupObject(backupObjectThree);
        Assert.Equal(2, backupFirst.GetBackupObjects().Count);
        backupFirst.MakeBackUp("FirstPoint", DateTime.UnixEpoch);
        backupFirst.DeleteBackupObject(backupObjectSecond);
        backupFirst.MakeBackUp("SecondPoint", DateTime.UnixEpoch);
        Assert.Equal(5,  backupTaskExtra.Repository.GetRepository().Count());
    }
}