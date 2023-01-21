using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class TableOfCouple
{
    private const int MinCountOfCoupleInSchedule = 0;

    public TableOfCouple(IReadOnlyCollection<Couple> schedule)
    {
        EnsureSchedule(schedule);
        Schedule = schedule;
    }

    public IReadOnlyCollection<Couple> Schedule { get; }
    public void EnsureSchedule(IReadOnlyCollection<Couple> schedule)
    {
        if (schedule.Count == MinCountOfCoupleInSchedule)
            throw new ScheduleValidationException($"Schedule should has more then {MinCountOfCoupleInSchedule} couples");
    }
}