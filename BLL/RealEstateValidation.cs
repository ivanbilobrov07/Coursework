using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class RealEstateValidation
    {
        static public bool isValidCity(string city)
        {
            if (city.Length >= 2)
            {
                return true;
            }

            throw new ValidationException("Invalid city. It must be at least 2 characters long");
        }

        static public bool isValidAddress(string address)
        {
            if (address.Length >= 5)
            {
                return true;
            }

            throw new ValidationException("Invalid address. It must be at least 5 characters long");
        }

        static public bool isValidPrice(int price)
        {
            if (price > 0)
            {
                return true;
            }

            throw new ValidationException("Invalid price. It must be not less than 1");
        }

        static public bool isValidTypes(string[] types) 
        {
            string[] roomsTypes = types.Where(type => type.Contains("room")).ToArray();

            if(roomsTypes.Length > 1) 
            {
                throw new ValidationException("Invalid types. You cannot add different number of rooms to real estate");
            }

            if(roomsTypes.Length == 0) 
            { 
                throw new ValidationException("Invalid types. Missing number of rooms, it is required");
            }

            if(types.Contains("flat") && types.Contains("house")) 
            {
                throw new ValidationException("Invalid types. Choose flat or house, you cant assign both");
            }

            return true;
        }
    }
}
