using Backups.Entities;

namespace Backups.Extra.Entities.TakePointsMethod;

public interface ITakePointsMethod
{
    List<RestorePoint> Execute(List<RestorePoint> points);
}