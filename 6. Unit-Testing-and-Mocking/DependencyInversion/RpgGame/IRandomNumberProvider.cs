namespace DependencyInversion.RpgGame
{
    public interface IRandomNumberProvider
    {
        int GetRandomNumber(int min, int max);
    }
}
