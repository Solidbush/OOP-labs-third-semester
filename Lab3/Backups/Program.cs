using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;
using Backups.Models;
using Backups.Services;

namespace Backups;

internal class Program
{
    private static void Main()
    {
        var fileRepository = new FileRepository("Repository", @"D:\Users\Volirvag\Desktop");
        Console.WriteLine(fileRepository.RepositoryPath);
        Console.WriteLine(fileRepository.RepositoryName);
        var backupFirst = new BackupTask("FirstBackup", fileRepository, new SplitStorage());
        var backupObject1 = new BackupObject("D:\\Users\\Volirvag\\Downloads", "OS6_test.docx");
        var backupObject2 = new BackupObject("D:\\Users\\Volirvag\\Downloads", "Лабораторная_работа_Исследование_репродуктивного_поведения_семьи_Гаврилов_Алексей_М32111.docx");
        backupFirst.AddBackupObject(backupObject1);
        backupFirst.AddBackupObject(backupObject2);
        backupFirst.MakeBackUp("FirstPoint", DateTime.Now);
        backupFirst.MakeBackUp("SecondPoint", DateTime.Now);
    }
}