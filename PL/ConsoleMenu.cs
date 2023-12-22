using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using static PL.Program;

namespace PL
{
    public class ConsoleMenu
    {
        static protected string getPropertyValue(Func<string, bool> isValid, string message, string errorMessage)
        {
            string value;

            Console.WriteLine(message);
            value = Console.ReadLine()!;

            while (true)
            {
                try
                {
                    if (isValid(value))
                    {
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(errorMessage);
                    value = Console.ReadLine()!;
                }
            }
        }

        static protected int getPropertyValue(Func<int, bool> isValid, string message, string errorMessage)
        {
            string value;

            Console.WriteLine(message);
            value = Console.ReadLine()!;

            while (true)
            {
                try
                {
                    int parsedValue = int.Parse(value);
                    if (isValid(parsedValue))
                    {
                        return parsedValue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please, enter only a number");
                    value = Console.ReadLine()!;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(errorMessage);
                    value = Console.ReadLine()!;
                }
            }
        }

        static public string[] getRealEstateTypes()
        {
            string message = "Enter the indexes of the types of the real estate through the space:";
            string errorMessage = "Please, enter the valid types:";
            string[] types = { "1-room", "2-rooms", "3-rooms", "flat", "house", "private plot" };

            foreach (string type in types)
            {
                Console.WriteLine($"{Array.IndexOf(types, type) + 1}: {type}");
            }

            Console.WriteLine(message);
            string result = Console.ReadLine()!;

            while (true)
            {
                try
                {
                    string[] numbers = result.Split(" ");
                    List<string> values = new List<string>();

                    foreach (string number in numbers)
                    {
                        int parsedNumber = int.Parse(number);
                        values.Add(types[parsedNumber-1]);
                    }

                    if (RealEstateValidation.isValidTypes(values.ToArray()))
                    {
                        return values.ToArray();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please, enter only numbers");
                    result = Console.ReadLine()!;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(errorMessage);
                    result = Console.ReadLine()!;
                }
            }
        }

        static protected int getUserIndexFromList(string askMessage, string[] values)
        {
            Console.WriteLine(askMessage);

            List<string> valuesList = new List<string>(values);
            int index = -1;

            foreach (string value in valuesList)
            {
                Console.WriteLine($"{valuesList.IndexOf(value) + 1}. {value}");
            }

            bool firstIteration = true;

            while (!Helpers.isValidIndex(index, valuesList.Count))
            {
                if (!firstIteration) Console.WriteLine("Please choose the index from the list above");

                try
                {
                    index = int.Parse(Console.ReadLine()!) - 1;
                }
                catch
                {
                    index = -1;
                }

                firstIteration = false;
            }

            return index;
        }

        static public string defineSerealizationType()
        {
            string[] types = { "JSON", "Binary", "XML", "Custom" };
            int typeIndex = getUserIndexFromList("Choose type of serialization to work with: ", types);

            return types[typeIndex];
        }

        static public ConsoleTypes defineConsoleType()
        {
            string[] types = { "Admin menu", "User menu" };
            int typeIndex = getUserIndexFromList("Choose type of menu to work with: ", types);

            if(typeIndex == 0) 
            {
                return ConsoleTypes.AdminConsole;
            }

            return ConsoleTypes.UserConsole;
        }

        static public CustomerSortActions defineCustomerSortAction() 
        {
            string[] types = { "Sort by firstName", "Sort by lastName", "Back" };
            int typeIndex = getUserIndexFromList("You can sort the customers: ", types);

            switch (typeIndex)
            {
                case 0: 
                    {
                        return CustomerSortActions.SortByFirstName;
                    }
                case 1:
                    {
                        return CustomerSortActions.SortByLastName;
                    }
                case 2:
                    {
                        return CustomerSortActions.Back;
                    }
                default:
                    {
                        return CustomerSortActions.Back;
                    }
            }
        }

        static public (string, string, string, string) GetCustomerInfo()
        {
            string firstName = getPropertyValue(CustomerValidation.isValidName, "Enter the first name of the customer:", "Please, enter the valid first name:");
            string lastName = getPropertyValue(CustomerValidation.isValidName, "Enter the last name of the customer:", "Please, enter the valid last name:");
            string email = getPropertyValue(CustomerValidation.isValidEmail, "Enter the email of the customer:", "Please, enter the valid email:");
            string bankAccount = getPropertyValue(CustomerValidation.isValidBankAccount, "Enter the bank account:", "Please, enter the valid bank account:");

            return (firstName, lastName, email, bankAccount);
        }

        static public (string, string, int, string[]) GetRealEstateInfo()
        {
            string city = getPropertyValue(RealEstateValidation.isValidCity, "Enter the city, where the real estate is located:", "Please, enter the valid city:");
            string address = getPropertyValue(RealEstateValidation.isValidAddress, "Enter the address of the real estate:", "Please, enter the valid address:");
            int price = getPropertyValue(RealEstateValidation.isValidPrice, "Enter the price of the real estate:", "Please, enter the valid price:");
            string[] types = getRealEstateTypes();

            return (city, address, price, types);
        }

    }

    public class UserConsole : ConsoleMenu
    {
        public static UserActions defineAction()
        {
            string[] actions = { "Define Customer", "View available real estate", "Configure options", "Show user real estate", "Choose another console menu", "Choose another type of serialization", "Exit" };
            int actionIndex = getUserIndexFromList("Choose an action, you want to do: ", actions);

            switch (actionIndex)
            {
                case 0: return UserActions.DefineCustomer;
                case 1: return UserActions.ViewAvailableRealEstate;
                case 2: return UserActions.ConfigureOptions;
                case 3: return UserActions.ShowMyRealEstate;
                case 4: return UserActions.ChangeConsole;
                case 5: return UserActions.Restart;
                case 6: return UserActions.Stop;
                default: return UserActions.Stop;
            }
        }

        public static int defineStep()
        {
            Console.WriteLine("Define number of real estate to load (from 1 to 5)");
            string value = Console.ReadLine()!;

            while (true)
            {
                try
                {
                    int parsedValue = int.Parse(value);
                    if (parsedValue >= 1 && parsedValue < 5)
                    {
                        return parsedValue;
                    }
                    throw new Exception("Number of steps must be from 1 to 5");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    value = Console.ReadLine()!;
                }
            }
        }

        public static ActionsWithRealEstateList defineActionWithRealEstateList(int page, int limit)
        {
            List<string> actions = new List<string> { "Prev", "Show real estate", "Next", "Back to menu" };

            if (page == limit)
            {
                actions.Remove("Next");
            }

            if (page == 1)
            {
                actions.Remove("Prev");
            }

            int actionIndex = getUserIndexFromList("Choose action: ", actions.ToArray());

            if(!actions.Contains("Prev") && !actions.Contains("Next"))
            {
                switch (actionIndex)
                {
                    case 0: return ActionsWithRealEstateList.Show;
                    case 1: return ActionsWithRealEstateList.GoToMenu;
                    default: return ActionsWithRealEstateList.Show;
                }
            }
            else if (!actions.Contains("Prev"))
            {
                switch (actionIndex)
                {
                    case 0: return ActionsWithRealEstateList.Show;
                    case 1: return ActionsWithRealEstateList.Next;
                    case 2: return ActionsWithRealEstateList.GoToMenu;
                    default: return ActionsWithRealEstateList.Show;
                }
            }
            else
            {
                switch (actionIndex)
                {
                    case 0: return ActionsWithRealEstateList.Prev;
                    case 1: return ActionsWithRealEstateList.Show;
                    case 2: return ActionsWithRealEstateList.Next;
                    case 3: return ActionsWithRealEstateList.GoToMenu;
                    default: return ActionsWithRealEstateList.Show;
                }
            }
        }

        public static ActionsWithRealEstate defineActionWithRealEstate()
        {
            string[] actions = { "Buy", "Reject" };
            int actionIndex = getUserIndexFromList("Choose an action, you want to do: ", actions);

            switch (actionIndex)
            {
                case 0: return ActionsWithRealEstate.Buy;
                case 1: return ActionsWithRealEstate.Reject;               
                default: return ActionsWithRealEstate.Reject;
            }
        }
    }

    public class AdminConsole : ConsoleMenu
    {
        public static AdminActions defineAction()
        {
            string[] actions = { "Add a new customer", "Add new real estate", "Delete a customer", "Delete real estate", "Print all customers", "Print all real estate", "Choose another console menu", "Choose another type of serialization", "Exit" };
            int actionIndex = getUserIndexFromList("Choose an action, you want to do: ", actions);

            switch (actionIndex)
            {
                case 0: return AdminActions.AddCustomer;
                case 1: return AdminActions.AddRealEstate;
                case 2: return AdminActions.RemoveCustomer;
                case 3: return AdminActions.RemoveRealEstate;
                case 4: return AdminActions.PrintCustomers;
                case 5: return AdminActions.PrintRealEstate;
                case 6: return AdminActions.ChangeConsole;
                case 7: return AdminActions.Restart;
                case 8: return AdminActions.Stop;
                default: return AdminActions.Stop;
            }
        }
    }
}
