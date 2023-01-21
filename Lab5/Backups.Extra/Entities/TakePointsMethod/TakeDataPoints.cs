using Backups.Entities;
using Newtonsoft.Json;

namespace Backups.Extra.Entities.TakePointsMethod;

public class TakeDataPoints : ITakePointsMethod
{
    [JsonProperty]
    private DateTime _data;

    public TakeDataPoints(DateTime date)
    {
        _data = date;
    }

    public List<RestorePoint> Execute(List<RestorePoint> points)
    {
        return points.Where(point => point.DateOfCreation >= _data).ToList();
    }
}