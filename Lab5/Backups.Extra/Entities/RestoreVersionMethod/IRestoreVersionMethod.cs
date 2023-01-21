using Backups.Entities;
using Backups.Entities.CompressionMethod;
using Backups.Entities.Repository;

namespace Backups.Extra.Entities.RestoreVersionMethod;

public interface IRestoreVersionMethod
{
    void Execute(RestorePoint restorePoint, ICompressionMetod method, IRepository repository, string dirPath = null);
}