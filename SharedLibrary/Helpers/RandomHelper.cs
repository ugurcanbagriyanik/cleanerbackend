namespace SharedLibrary.Helpers
{
    public class RandomHelper
    {
        public static int GetRandomInt(int min, int max, double minChance = 0.1, double maxChance = 0.05)
        {
            int random = new Random().Next(min, max);
            if ((max - min) * minChance > random)
            {
                random = min;
            }
            else if ((max - min) * (1 - maxChance) < random)
            {
                random = max;
            }
            return random;
        }
    }
}
