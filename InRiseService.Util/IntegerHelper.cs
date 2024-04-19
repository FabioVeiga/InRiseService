namespace InRiseService.Util
{
    public static class IntegerHelper
    {
        public static int GenerateRandomSixDigitNumber()
        {
            var random = new Random();
            return random.Next(100000, 999999);
        }
    }
}