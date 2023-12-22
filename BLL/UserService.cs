using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UserService
    {
        private Customer user;
        private CustomerService customerService;

        public Customer User
        {
            get { return user; }
        }

        public UserService(string type) 
        {
            customerService = new CustomerService(type);
        }

        public void DefineUser(string userId)
        {
            user = customerService.GetCustomer(userId);
        }

        public void PrintUserRealEstate(List<RealEstate> realEstateList)
        {
            List<RealEstate> ownRealEstates = realEstateList.Where(re => user.RealEstates.Contains(re.Id)).ToList();

            foreach (RealEstate realEstate in ownRealEstates)
            {
                Console.WriteLine(realEstate);
            }
        }

        public void AddOptions(List<RealEstateType> options)
        {
            user.Options = options;
        }
    }
}
