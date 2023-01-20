namespace Core
{
    public static class RandomNumberHelper
    {
        public static string CreateRandomNumberHelper()
        {
            Random random = new Random();
            string hexValue = string.Empty;
            int num = random.Next(0, 0xFFFFFF);
            hexValue += num.ToString("X6");
            return hexValue;
        }
    }
}
