using Microsoft.AspNetCore.Components.Forms;

namespace CRUD_Operation_MVC.Utility
{
    public class Utility1
    {
        public static bool hasSpecialCharacter(string Name)
        {
            string specialChar = @"&*#()<>!@#$%";
            foreach (char item in specialChar)
            {
                if(Name.Contains(item))
                    {
                    return true;
                }
                
            }
            return false;
        }
    }
}
