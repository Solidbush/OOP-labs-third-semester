using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Extra.Entities.Logger;
using Backups.Extra.Entities.RestoreVersionMethod;
using Backups.Extra.Entities.TakePointsMethod;
using Backups.Extra.Services;
using Backups.Models;
using Backups.Services;

namespace Backups.Extra;

internal class Program
{
    private static void Main()
    {
        var fileRepository = new FileRepository("Repository", @"D:\Users\Volirvag\Desktop");
        Console.WriteLine(fileRepository.RepositoryPath);
        Console.WriteLine(fileRepository.RepositoryName);
        var backupFirst = new BackupTask("FirstBackup", fileRepository, new SingleStorage());
        var service = new BackupMainService(@"D:\OOP_labs\Solidbush\Lab5\Backups.Extra\reRunFile.json", backupFirst, new TakeCountPoints(1), new RestoreToDirectory(), new ConsoleLogger());
        Console.WriteLine();
        /*var backupObject1 = new BackupObject("D:\\Users\\Volirvag\\Downloads", "OS6_test.docx");
        var backupObject2 = new BackupObject("D:\\Users\\Volirvag\\Downloads", "Лабораторная_работа_Исследование_репродуктивного_поведения_семьи_Гаврилов_Алексей_М32111.docx");
        service.AddBackupObject(backupObject1);
        service.AddBackupObject(backupObject2);
        RestorePoint restorePoint = service.MakeBackUp("FirstPoint", DateTime.Now);
        RestorePoint restorePoint2 = service.MakeBackUp("SecondePoint", DateTime.Now);
        RestorePoint restorePoint3 = service.MakeBackUp("ThreePoint", DateTime.Now);*/

        // backupExtra.Merge("FirstPoint");

        // backupExtra.Restore(restorePoint, new SingleStorage(), fileRepository, "D:\\toDir");
    }
}