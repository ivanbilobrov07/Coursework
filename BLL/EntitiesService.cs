using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using DAL;

namespace BLL
{
    public enum CustomerSortActions
    {
        SortByFirstName,
        SortByLastName,
        Back,
    }
    public class EntitiesService<T> where T : new()
    {
        protected EntitiesContext<T> db;
        protected SerializationType type;
        protected string fileName;
        protected List<T> dataList;

        public List<T> DataList
        {
            get { return dataList; }
        }

        public EntitiesService(string type, string fileName)
        {
            if (type.ToUpper() == "JSON")
            {
                this.type = SerializationType.JSON;
            }
            else if (type.ToUpper() == "BINARY")
            {
                this.type = SerializationType.Binary;
            }
            else if (type.ToUpper() == "XML")
            {
                this.type = SerializationType.XML;
            }
            else if (type.ToUpper() == "CUSTOM")
            {
                this.type = SerializationType.Custom;
            }
            else
            {
                //throw new InvalidSerializationTypeException(type);
            }

            this.fileName = fileName;
            db = new EntitiesContext<T>(this.type, fileName);
            dataList = db.Read();
        }

        public void Print()
        {
            if (dataList.Count == 0)
            {
                Console.WriteLine("The list is empty");
                return;
            }

            int index = 0;

            foreach (T entity in dataList)
            {
                Console.WriteLine(index + 1 + ") " + entity.ToString());
                index++;
            }
        }

        public void Clear()
        {
            db.Clear();
            dataList = new List<T>();
        }
    }

    public class CustomerService : EntitiesService<Customer>
    {
        public CustomerService(string type, string fileName) : base(type, fileName)
        { }

        public string AddCustomer(string firstName, string lastName, string email, string bankAccount)
        {
            Guid guid = Guid.NewGuid();
            string id = guid.ToString();

            Customer newCustomer = new Customer(firstName, lastName, email, bankAccount, id);
            dataList.Add(newCustomer);

            db.Write(dataList);

            return newCustomer.ToString();
        }

        public string RemoveCustomer(string inputId)
        {
            Customer customer = dataList.Find(i => i.Id == inputId);

            if(customer == null) {
                throw new EntityNotFoundException("Not found customer with id " + inputId);
            }

            dataList.Remove(customer);
            db.Rewrite(dataList);

            return customer.ToString();
        }

        public Customer GetCustomer(string inputId)
        {
            return dataList.FirstOrDefault(i => i.Id == inputId)!;
        }

        protected void Print(List<Customer> data)
        {
            int index = 0;

            foreach (Customer customer in data)
            {
                Console.WriteLine(index + 1 + ") " + customer.ToString());
                index++;
            }
        }

        public void PrintByQuery(string query)
        {
            int index = 0;

            foreach (Customer customer in DataList)
            {
                if (customer.ToString().Contains(query))
                {
                    Console.WriteLine(index + 1 + ") " + customer.ToString());
                    index++;
                }
            }
        }

        public void PrintWithSort(CustomerSortActions? sortQuery = null)
        {
            if (dataList.Count == 0)
            {
                Console.WriteLine("The list is empty");
                return;
            }

            if (sortQuery == null)
            {
                Print(dataList);
            }

            if (sortQuery == CustomerSortActions.SortByFirstName)
            {
                List<Customer> sortedList = dataList.OrderBy(customer => customer.FirstName).ToList();
                Print(sortedList);
            }

            if (sortQuery == CustomerSortActions.SortByLastName)
            {
                List<Customer> sortedList = dataList.OrderBy(customer => customer.LastName).ToList();
                Print(sortedList);
            }
        }

        public void AddRealEstateToCustomer(string userId, string realEstateId)
        {
            Customer user = dataList.Find(i => i.Id == userId);

            if(user == null)
            {
                return;
            }

            user.RealEstates.Add(realEstateId);
            db.Rewrite(dataList);
        }
    }

    public class RealEstateService : EntitiesService<RealEstate>
    {
        public RealEstateService(string type, string fileName) : base(type, fileName)
        { }

        public List<RealEstateType> convertTypes(string[] types) 
        {
            List<RealEstateType> convertedTypes = new List<RealEstateType>();

            foreach(string type in types)
            {
                switch(type)
                {
                    case "1-room":
                        {
                            convertedTypes.Add(RealEstateType.OneRoom);
                            break;
                        }
                    case "2-rooms":
                        {
                            convertedTypes.Add(RealEstateType.TwoRooms);
                            break;
                        }
                    case "3-rooms":
                        {
                            convertedTypes.Add(RealEstateType.ThreeRooms);
                            break;
                        }
                    case "flat":
                        {
                            convertedTypes.Add(RealEstateType.Flat);
                            break;
                        }
                    case "house":
                        {
                            convertedTypes.Add(RealEstateType.House);
                            break;
                        }
                    case "private plot":
                        {
                            convertedTypes.Add(RealEstateType.PrivatePlot);
                            break;
                        }
                }
            }

            return convertedTypes;
        }

        public string AddRealEstate(string city, string address, int price, string[] types)
        {
            Guid guid = Guid.NewGuid();
            string id = guid.ToString();

            RealEstate newRealEstate = new RealEstate(city, address, price, convertTypes(types), id);
            dataList.Add(newRealEstate);

            db.Write(dataList);

            return newRealEstate.ToString();
        }

        public void PrintByQuery(string query)
        {
            int index = 0;

            foreach (RealEstate realEstate in DataList)
            {
                if (realEstate.ToString().Contains(query))
                {
                    Console.WriteLine(index + 1 + ") " + realEstate.ToString());
                    index++;
                }
            }
        }

        public List<RealEstate> GetFilteredData(List<RealEstateType> options)
        {
            if (dataList.Count == 0)
            {
                Console.WriteLine("The list is empty");
                return dataList;
            }

            List<RealEstate> filteredList = options != null ? dataList.Where(re => options.All(opt => re.Types.Contains(opt))).ToList() : dataList;

            return filteredList;
        }

        public string RemoveRealEstate(string inputId)
        {
            RealEstate realEstate = dataList.Find(i => i.Id == inputId);

            if (realEstate == null)
            {
                throw new EntityNotFoundException("Not found real estate with id " + inputId);
            }

            dataList.Remove(realEstate);
            db.Rewrite(dataList);

            return realEstate.ToString();
        }

        public void Print(List<RealEstateType>? options = null)
        {
            if (dataList.Count == 0)
            {
                Console.WriteLine("The list is empty");
                return;
            }

            int index = 0;

            List<RealEstate> filteredList = options != null ? GetFilteredData(options) : dataList;

            foreach (RealEstate realEstate in filteredList)
            {
                Console.WriteLine(index + 1 + ") " + realEstate.ToString());
                index++;
            }            
        }

        public void Print(int page, int step, List<RealEstateType>? options = null)
        {
            if (dataList.Count == 0)
            {
                Console.WriteLine("The list is empty");
                return;
            }

            List<RealEstate> filteredList = options != null ? GetFilteredData(options) : dataList;
            List<RealEstate> listWithoutOwnedRE = filteredList.Where(re => re.Owner == null).ToList();

            int index = (page - 1) * step;

            for(int i = index; i < index + step; i++)
            {
                if(i < listWithoutOwnedRE.Count)
                {
                    Console.WriteLine(i + 1 + ") " + listWithoutOwnedRE[i].ToString());
                }
            }
        }

        public RealEstate GetRealEstate(string realEstateId)
        {
            RealEstate foundRealEstate = dataList.FirstOrDefault(re => re.Id == realEstateId);

            if(foundRealEstate != null)
            {
                return foundRealEstate;
            }

            throw new Exception("Not found");
        }

        public void AddRealEstateToCustomer(string realEstateId, string userId)
        {
            RealEstate realEstate = dataList.Find(i => i.Id == realEstateId);

            if (realEstate == null)
            {
                return;
            }

            realEstate.Owner = userId;
            db.Rewrite(dataList);
        }
    }
}
