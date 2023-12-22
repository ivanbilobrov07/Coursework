using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class CustomerValidation
    {
        static public bool isValidName(string name)
        {
            if (name.Length >= 2)
            {
                return true;
            }

            throw new ValidationException("Invalid name. It must be at least 2 characters long");
        }

        static public bool isValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (Regex.IsMatch(email, pattern))
            {
                return true;
            }

            throw new ValidationException("Invalid email. The valid example: test@example.com");
        }

        static public bool isValidBankAccount(string bankAccount)       
        {
            string pattern = @"^\d{16}$";

            if (Regex.IsMatch(bankAccount, pattern))
            {
                return true;
            }

            throw new ValidationException("Invalid bank account. It must consist of 16 numbers");
        }
    }
}
