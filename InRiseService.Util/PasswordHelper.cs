namespace InRiseService.Util
{
    public static class PasswordHelper
    {
        static string salt = BCrypt.Net.BCrypt.GenerateSalt();

        public static string EncryptPassword(string input){
            return BCrypt.Net.BCrypt.HashPassword(input, salt);
        }
        
        public static bool CheckPassword(string passwordTyped, string passwordDB){
            return BCrypt.Net.BCrypt.Verify(passwordTyped, passwordDB);
        }
    }
}