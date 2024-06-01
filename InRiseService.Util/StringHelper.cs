using System.Text.RegularExpressions;

namespace InRiseService.Util
{
    public static class StringHelper
    {
        public static bool CheckPostalCode(string input)
        {
            string pattern = @"^\d{7}$";

            if (Regex.IsMatch(input, pattern))
                return true;
            return false;
        }

        public static string NormalizePostalCode(string input)
        {
            string pattern = @"\D";
            return Regex.Replace(input, pattern, "");
        }
    }
}