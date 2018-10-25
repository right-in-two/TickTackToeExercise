namespace TickTackToe.Interfaces
{
    public interface ILocationOnBoard
    {
        int X { get; }
        int Y { get; }
        bool BelongsToSuccessRow { get; set; }
        PlayerId? TakenBy { get; }
        bool TryTakeLocation(PlayerId id);
    }
}
