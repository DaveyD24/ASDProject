using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class SampleData
    {
    }

    public class SampleRestaurant
    {
        //Id, Name, Description, Email, Password
        public List<Restaurant> AllRestaurants = new List<Restaurant>();
        public List<string> RestaurantNames = new List<string>();

        public SampleRestaurant()
        {
            RestaurantNames.Add("McDonalds");
            RestaurantNames.Add("Hungry Jacks");
            RestaurantNames.Add("KFC");
            RestaurantNames.Add("Char Grilled");
            RestaurantNames.Add("Sammy's Chicken");
            RestaurantNames.Add("Big Fat Steak House");
            RestaurantNames.Add("Outback Steakhouse");
            RestaurantNames.Add("Yum Factory");
            RestaurantNames.Add("Candy Store");
            RestaurantNames.Add("Pork Land");
            RestaurantNames.Add("Kanakawa");
            RestaurantNames.Add("Choc Deli");
            RestaurantNames.Add("Le Cafe");
            RestaurantNames.Add("Little Italy");
            RestaurantNames.Add("Joey's Downtown Pizzaria");
            RestaurantNames.Add("Westside Burgers");
            RestaurantNames.Add("Subway");
            RestaurantNames.Add("Oporto");
            RestaurantNames.Add("BP Convenience Store");
            RestaurantNames.Add("Red Rooster");

            foreach (string s in RestaurantNames)
            {
                string trimmed = RemoveWhitespace(s).ToLower();
                AllRestaurants.Add(new Restaurant
                {
                    Name = s,
                    Description = GetLorem(),
                    Email = trimmed + "@" + trimmed + ".com",
                    Password = RandomString(12)
                });
            }
        }

        public string GetLorem()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Nam in rhoncus ipsum, quis auctor arcu. Cras id arcu scelerisque, " +
                "luctus justo sed, vulputate ante. Integer vitae nibh velit. Praesent " +
                "eu lorem lorem. Aliquam vestibulum turpis sed tellus laoreet finibus. " +
                "Aliquam erat volutpat. Suspendisse nec sapien nec eros egestas tincidunt. " +
                "Maecenas ornare sit amet nunc eu tempor.";
        }

        public string RemoveWhitespace(string input)
        {
            //return new string(input
            //    .Where(c => !Char.IsWhiteSpace(c))
            //    .ToArray());

            string newString = input.Replace(" ", string.Empty);
            return newString;
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_!@#$%^&*";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class SampleProduct
    {
        //Id, Restaurant, Category, Name, Price, Description

        List<Tuple<string, string>> PossiblePairs = new List<Tuple<string, string>>();
        public List<Product> AllProducts = new List<Product>();

        public SampleProduct()
        {

            PossiblePairs.Add(new Tuple<string, string>("Cheese Burger", GetProductCategory("Burgers").Name));
            PossiblePairs.Add(new Tuple<string, string>("Double Cheese Burger", GetProductCategory("Burgers").Name));
            PossiblePairs.Add(new Tuple<string, string>("Zinger Burger", GetProductCategory("Burgers").Name));
            PossiblePairs.Add(new Tuple<string, string>("Small Chips", GetProductCategory("Chips").Name));
            PossiblePairs.Add(new Tuple<string, string>("Large Chips", GetProductCategory("Chips").Name));
            PossiblePairs.Add(new Tuple<string, string>("Fanta 375ml", GetProductCategory("Drinks").Name));
            PossiblePairs.Add(new Tuple<string, string>("Coca Cola 375mL", GetProductCategory("Drinks").Name));
            PossiblePairs.Add(new Tuple<string, string>("Chocolate Shake", GetProductCategory("Shakes").Name));
            PossiblePairs.Add(new Tuple<string, string>("Vanilla Shake", GetProductCategory("Shakes").Name));
            PossiblePairs.Add(new Tuple<string, string>("Oreo IceCream", GetProductCategory("Ice Cream").Name));
            PossiblePairs.Add(new Tuple<string, string>("Vanilla IceCream", GetProductCategory("Ice Cream").Name));
            PossiblePairs.Add(new Tuple<string, string>("Sizzling Steak", GetProductCategory("Beef").Name));
            PossiblePairs.Add(new Tuple<string, string>("Beef & Veggies", GetProductCategory("Beef").Name));
            PossiblePairs.Add(new Tuple<string, string>("Pork & Veggies", GetProductCategory("Pork").Name));
            PossiblePairs.Add(new Tuple<string, string>("Sweet & Sour Pork", GetProductCategory("Pork").Name));
            PossiblePairs.Add(new Tuple<string, string>("Chicken Wrap", GetProductCategory("Chicken").Name));
            PossiblePairs.Add(new Tuple<string, string>("Smoked Salmon", GetProductCategory("Fish").Name));
            PossiblePairs.Add(new Tuple<string, string>("Cookie Pack", GetProductCategory("Snacks").Name));
            PossiblePairs.Add(new Tuple<string, string>("Smiths Chips - Salt & Vinegar", GetProductCategory("Snacks").Name));
            PossiblePairs.Add(new Tuple<string, string>("Smiths Chips - Original", GetProductCategory("Snacks").Name));
            PossiblePairs.Add(new Tuple<string, string>("Zinger Combo Box", GetProductCategory("Combos").Name));
            PossiblePairs.Add(new Tuple<string, string>("3 Piece Combo Meal", GetProductCategory("Combos").Name));
            PossiblePairs.Add(new Tuple<string, string>("4 Piece Combo Meal", GetProductCategory("Combos").Name));
            PossiblePairs.Add(new Tuple<string, string>("Ultime Combo Deal", GetProductCategory("Combos").Name));

            foreach (Tuple<string, string> t in PossiblePairs)
            {
                AllProducts.Add(new Product
                {
                    Restaurant = null,
                    Category = GetProductCategory(t.Item2),
                    Name = t.Item1,
                    Price = GenerateRandomPrice(),
                    Description = GetLorem()
                });
            }
        }

        public Product GetRandomProduct()
        {
            Random random = new Random();
            int rand = random.Next(0, AllProducts.Count);
            return (AllProducts.ElementAt(rand));
        }

        public ProductCategory GetProductCategory(string name)
        {

            ASDContext3 db = new ASDContext3();

            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            foreach (ProductCategory pc in AllCategories)
            {
                if (pc.Name.Equals(name))
                {
                    return pc;
                }
            }
            return null;

            //SampleProductCategory SPC = new SampleProductCategory();
            //foreach (ProductCategory pc in SPC.SampleCategories)
            //{
            //    if (pc.Name.Equals(name))
            //    {
            //        return pc;
            //    }
            //}
            //return null;
        }

        public double GenerateRandomPrice()
        {
            double min = 1.00;
            double max = 9.99;

            Random random = new Random();
            return Math.Round((random.NextDouble() * (max - min) + min), 2);
        }

        public string GetLorem()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Nam in rhoncus ipsum, quis auctor arcu. Cras id arcu scelerisque, " +
                "luctus justo sed, vulputate ante. Integer vitae nibh velit. Praesent " +
                "eu lorem lorem. Aliquam vestibulum turpis sed tellus laoreet finibus. " +
                "Aliquam erat volutpat. Suspendisse nec sapien nec eros egestas tincidunt. " +
                "Maecenas ornare sit amet nunc eu tempor.";
        }
    }

    public class SampleProductCategory
    {
        //Id, Name
        public List<ProductCategory> SampleCategories = new List<ProductCategory>();

        public SampleProductCategory()
        {
            List<string> samples = new List<string>();
            samples.Add("Burgers");
            samples.Add("Chips");
            samples.Add("Drinks");
            samples.Add("Beef");
            samples.Add("Fish");
            samples.Add("Pork");
            samples.Add("Chicken");
            samples.Add("Ice Cream");
            samples.Add("Snacks");
            samples.Add("Shakes");
            samples.Add("Combos");

            foreach (string sample in samples)
            {
                SampleCategories.Add(new ProductCategory
                {
                    Name = sample
                });
            }
        }
    }
    public class SampleCustomer
    {
        //Id, Name, Description, Email, Password
        public List<Customer> AllCustomers = new List<Customer>();
        public List<string> CustomersFN = new List<string>();

        public SampleCustomer()
        {
            CustomersFN.Add("Patrick");
            foreach (string s in CustomersFN)
            {
                string trimmed = RemoveWhitespace(s).ToLower();
                AllCustomers.Add(new Customer
                {
                    FirstName = s,
                    LastName = s,
                    Email = trimmed + "@" + trimmed + ".com",
                    Password = RandomString(12)
                });
            }
        }
        public string RemoveWhitespace(string input)
        {
            //return new string(input
            //    .Where(c => !Char.IsWhiteSpace(c))
            //    .ToArray());

            string newString = input.Replace(" ", string.Empty);
            return newString;
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_!@#$%^&*";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}