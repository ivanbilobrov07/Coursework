using System.Text.Json.Serialization;

namespace DAL
{
    public enum RealEstateType
    {
        OneRoom,
        TwoRooms,
        ThreeRooms,
        PrivatePlot,
        Flat, 
        House,
    }

    public class Customer
    {
        private string firstName;
        private string lastName;
        private string email;
        private string bankAccount;
        private string id;
        private List<string> realEstates;
        private List<RealEstateType> options;

        public string Id
        {
            get { return id; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string BankAccount
        {
            get { return bankAccount; }
            set { bankAccount = value; }
        }

        public List<string> RealEstates
        {
            get { return realEstates; }
            set { realEstates = value; }
        }

        [JsonIgnore]
        public List<RealEstateType> Options
        {
            get { return options; }
            set { options = value; }
        } 

        [JsonConstructor]
        public Customer(string firstName, string lastName, string email, string bankAccount, string id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.bankAccount = bankAccount;
            this.id = id;
            realEstates = new List<string>();
        }

        public Customer() { }

        public override string ToString()
        {
            return "Customer - " + FirstName + " " + LastName + " " + Email + " " + BankAccount + " (" + Id + ")" + ";";
        }
    }

    public class RealEstate
    {
        private string id;
        private int price;
        private string city;
        private string address;
        private string owner;
        private List<RealEstateType> types;

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public List<RealEstateType> Types
        {
            get { return types; }
            set { types = value; }
        }

        public string Id
        {
            get { return id; }
        }

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public RealEstate() { }

        [JsonConstructor]
        public RealEstate(string city, string address, int price, List<RealEstateType> types, string id ) 
        {
            this.city = city;
            this.address = address;
            this.price = price;
            this.types = types;
            this.id = id;
        }

        public override string ToString()
        {
            string result = "Real Estate - " + City + " " + Address + " " + Price;

            foreach(RealEstateType type in Types)
            {
                result += " " + type.ToString();
            }

            result += " (" + Id + ")" + ";";

            return result;
        }
    }
}
