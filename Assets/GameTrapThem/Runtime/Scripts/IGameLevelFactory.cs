namespace TrapThem
{
    public interface IGameLevelFactory
    {
        (IGameLevel, bool) BuildNext();
    }
}