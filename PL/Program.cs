using System;
using BLL;

namespace PL
{
    public enum AdminActions
    {
        AddCustomer,
        AddRealEstate,
        RemoveCustomer,
        RemoveRealEstate,
        PrintCustomers,
        PrintRealEstate,
        Search,
        ChangeConsole,
        Restart,
        Stop,
    }
    public enum UserActions
    {
        DefineCustomer,
        ViewAvailableRealEstate,
        ConfigureOptions,
        ShowMyRealEstate,
        ChangeConsole,
        Restart,
        Stop,
    }
    public enum ActionsWithRealEstateList
    {
        Next, 
        Prev,
        Show,
        GoToMenu
    }
    public enum ActionsWithRealEstate
    {
        Buy,
        Reject,
    }
    public enum ConsoleTypes
    {
        AdminConsole,
        UserConsole
    }
    public enum SearchType
    {
        Customers,
        RealEstate,
        All,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            bool typeFlag = true;
            bool consoleFlag = true;

            while (true)
            {
                string type = ConsoleMenu.defineSerealizationType();
                typeFlag = true;
                Console.Clear();

                CustomerService customerService = new CustomerService(type, "customers");
                RealEstateService realEstateService = new RealEstateService(type, "real-estates");
                UserService userService = new UserService(type, "customers");

                while (typeFlag)
                {
                ConsoleTypes ct = ConsoleMenu.defineConsoleType();
                consoleFlag = true;
                Console.Clear();

                    if (ct == ConsoleTypes.AdminConsole)
                    {
                        AdminActions action = AdminConsole.defineAction();
                        Console.Clear();

                        while (consoleFlag)
                        {
                            Console.Clear();

                            switch (action)
                            {
                                case AdminActions.AddCustomer:
                                    {
                                        var studentInfo = ConsoleMenu.GetCustomerInfo();

                                        string firstName = studentInfo.Item1;
                                        string lastName = studentInfo.Item2;
                                        string email = studentInfo.Item3;
                                        string bankAccount = studentInfo.Item4;

                                        string customer = customerService.AddCustomer(firstName, lastName, email, bankAccount);

                                        Console.WriteLine("{" + customer! + "} was added successfully");

                                        action = AdminConsole.defineAction();
                                        break;
                                    }
                                case AdminActions.AddRealEstate:
                                    {
                                        var realEstateInfo = ConsoleMenu.GetRealEstateInfo();

                                        string city = realEstateInfo.Item1;
                                        string address = realEstateInfo.Item2;
                                        int price = realEstateInfo.Item3;
                                        string[] types = realEstateInfo.Item4;

                                        string realEstate = realEstateService.AddRealEstate(city, address, price, types);

                                        Console.WriteLine("{" + realEstate! + "} was added successfully");

                                        action = AdminConsole.defineAction();
                                        break;
                                    }
                                case AdminActions.RemoveCustomer:
                                    {
                                        customerService.Print();

                                        Console.WriteLine("Enter the id of the customer, you want to delete: ");
                                        string customerId = Console.ReadLine();

                                        try
                                        {
                                            string customer = customerService.RemoveCustomer(customerId);
                                            Console.WriteLine("{" + customer + "} was deleted successfully");
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }

                                        action = AdminConsole.defineAction();
                                        break;
                                    }
                                case AdminActions.RemoveRealEstate:
                                    {
                                        realEstateService.Print();

                                        Console.WriteLine("Enter the id of the real estate, you want to delete: ");
                                        string realEstateId = Console.ReadLine();

                                        try
                                        {
                                            string realEstate = realEstateService.RemoveRealEstate(realEstateId);
                                            Console.WriteLine("{" + realEstate + "} was deleted successfully");
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }

                                        action = AdminConsole.defineAction();
                                        break;
                                    }
                                case AdminActions.PrintCustomers:
                                    {
                                        customerService.PrintWithSort();
                                        CustomerSortActions sortType = AdminConsole.defineCustomerSortAction();
                                        Console.Clear();

                                        while(sortType != CustomerSortActions.Back)
                                        {
                                            customerService.PrintWithSort(sortType);
                                            sortType = AdminConsole.defineCustomerSortAction();
                                            Console.Clear();
                                        }

                                        action = AdminConsole.defineAction();
                                        break;
                                    }
                                case AdminActions.PrintRealEstate:
                                    {
                                        realEstateService.Print();

                                        action = AdminConsole.defineAction();
                                        break;
                                    }
                                case AdminActions.Search:
                                    {
                                        SearchType searchType = AdminConsole.defineSearchType();

                                        switch (searchType)
                                        {
                                            case SearchType.Customers:
                                                {
                                                    customerService.Print();
                                                    Console.WriteLine("Enter a keyword, which you want to find: ");
                                                    string keyword = Console.ReadLine();
                                                    Console.Clear();

                                                    customerService.PrintByQuery(keyword);

                                                    break;
                                                }
                                            case SearchType.RealEstate:
                                                {
                                                    realEstateService.Print();
                                                    Console.WriteLine("Enter a keyword, which you want to find: ");
                                                    string keyword = Console.ReadLine();
                                                    Console.Clear();

                                                    realEstateService.PrintByQuery(keyword);

                                                    break;

                                                }
                                            case SearchType.All:
                                                {
                                                    customerService.Print();
                                                    Console.WriteLine();
                                                    realEstateService.Print();
                                                    Console.Clear();

                                                    Console.WriteLine("Enter a keyword, which you want to find: ");
                                                    string keyword = Console.ReadLine();

                                                    customerService.PrintByQuery(keyword);
                                                    Console.WriteLine();
                                                    realEstateService.PrintByQuery(keyword);

                                                    break;
                                                }
                                        }

                                        action = AdminConsole.defineAction();
                                        break;
                                    }
                                case AdminActions.ChangeConsole:
                                    {
                                        Console.Clear();
                                        consoleFlag = false;
                                        break;
                                    }
                                case AdminActions.Restart:
                                    {
                                        Console.Clear();
                                        consoleFlag=false;
                                        typeFlag = false;
                                        break;
                                    }
                                case AdminActions.Stop:
                                    {
                                        return;
                                    }
                            }
                        }
                    }
                    else
                    {
                        UserActions action = UserConsole.defineAction();
                        Console.Clear();

                        while (consoleFlag)
                        {
                            Console.Clear();

                            switch (action)
                            {
                                case UserActions.DefineCustomer:
                                    {
                                        customerService.Print();

                                        Console.WriteLine("Enter the id of the user: ");
                                        string userId = Console.ReadLine();

                                        try
                                        {
                                            userService.DefineUser(userId);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }

                                        action = UserConsole.defineAction();
                                        break;
                                    }
                                case UserActions.ViewAvailableRealEstate:
                                    {
                                        if (userService.User == null)
                                        {
                                            Console.WriteLine("Please define a user before doing it");
                                            action = UserConsole.defineAction();
                                            break;
                                        }

                                        int page = 1;
                                        int step = UserConsole.defineStep();
                                        int limit = realEstateService.GetFilteredData(userService.User.Options).Count() / step;
                                        bool showREList = true;

                                        if (realEstateService.DataList.Count() % step != 0)
                                        {
                                            limit += 1;
                                        }

                                        while (showREList)
                                        {
                                            Console.Clear();
                                            realEstateService.Print(page, step, userService.User.Options);
                                            ActionsWithRealEstateList action1 = UserConsole.defineActionWithRealEstateList(page, limit);

                                            switch (action1)
                                            {
                                                case ActionsWithRealEstateList.Prev: 
                                                    {
                                                        --page;
                                                        break;
                                                    }
                                                case ActionsWithRealEstateList.Next:
                                                    {
                                                        ++page;
                                                        break;
                                                    }
                                                case ActionsWithRealEstateList.Show:
                                                    {
                                                        
                                                        Console.Clear();
                                                        realEstateService.Print(page, step, userService.User.Options);

                                                        Console.WriteLine("Enter id of real estate: ");

                                                        string reId = Console.ReadLine();

                                                        try
                                                        {
                                                            Console.WriteLine(realEstateService.GetRealEstate(reId).ToString());
                                                            ActionsWithRealEstate action2 = UserConsole.defineActionWithRealEstate();

                                                            switch (action2)
                                                            {
                                                                case ActionsWithRealEstate.Buy:
                                                                    {
                                                                        customerService.AddRealEstateToCustomer(userService.User.Id, reId);
                                                                        realEstateService.AddRealEstateToCustomer(reId, userService.User.Id);
                                                                        break;
                                                                    }
                                                                case ActionsWithRealEstate.Reject:
                                                                    {
                                                                        continue;
                                                                    }
                                                                default:
                                                                    {
                                                                        break;
                                                                    }
                                                            }
                                                            break;
                                                        } catch(Exception ex)
                                                        {
                                                            Console.WriteLine(ex.Message);
                                                        }
                                                        break;
                                                    }
                                                case ActionsWithRealEstateList.GoToMenu:
                                                    {
                                                        showREList = false;
                                                        Console.Clear();
                                                        break;  
                                                    }
                                            }
                                        }
                                                                      
                                        action = UserConsole.defineAction();
                                        break;
                                    }
                                case UserActions.ShowMyRealEstate:
                                    {
                                        userService.PrintUserRealEstate(realEstateService.DataList);

                                        action = UserConsole.defineAction();
                                        break;
                                    }
                                case UserActions.ConfigureOptions:
                                    {
                                        string[] realEstateOptions = ConsoleMenu.getRealEstateTypes();

                                        userService.AddOptions(realEstateService.convertTypes(realEstateOptions));

                                        action = UserConsole.defineAction();
                                        break;
                                    }
                                case UserActions.ChangeConsole:
                                    {
                                        Console.Clear();
                                        consoleFlag = false;
                                        break;
                                    }
                                case UserActions.Restart:
                                    {
                                        Console.Clear();
                                        consoleFlag = false;
                                        typeFlag = false;
                                        break;
                                    }
                                case UserActions.Stop:
                                    {
                                        return;
                                    }
                            }
                        }
                    }
                }
            }
        }
    }
}
