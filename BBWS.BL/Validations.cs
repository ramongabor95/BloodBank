using System.Linq;

namespace BBWS.BL
{
    public class Validations
    {
        public static bool ValidateUsernames(string username)
        {
            if (!username.EndsWith("MAIN") && !username.EndsWith("REQ"))
                return false;
            if (username.Length < 7 || username.Length > 9)
                return false;
            return username.Contains('0');
        }
    }
}
