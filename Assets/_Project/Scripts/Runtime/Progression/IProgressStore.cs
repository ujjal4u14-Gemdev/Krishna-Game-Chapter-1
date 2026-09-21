namespace MythicPuzzle.Runtime
{
    public interface IProgressStore
    {
        PlayerProgressData Load();
        void Save(PlayerProgressData data);
    }
}
