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

        List<Tuple<string, ProductCategory>> PossiblePairs = new List<Tuple<string, ProductCategory>>();
        public List<Product> AllProducts = new List<Product>();

        public SampleProduct()
        {

            PossiblePairs.Add(new Tuple<string, ProductCategory>("Cheese Burger", GetProductCategory("Burgers")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Double Cheese Burger", GetProductCategory("Burgers")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Zinger Burger", GetProductCategory("Burgers")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Small Chips", GetProductCategory("Chips")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Large Chips", GetProductCategory("Chips")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Fanta 375ml", GetProductCategory("Drinks")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Coca Cola 375mL", GetProductCategory("Drinks")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Chocolate Shake", GetProductCategory("Shakes")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Vanilla Shake", GetProductCategory("Shakes")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Oreo IceCream", GetProductCategory("Ice Cream")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Vanilla IceCream", GetProductCategory("Ice Cream")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Sizzling Steak", GetProductCategory("Beef")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Beef & Veggies", GetProductCategory("Beef")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Pork & Veggies", GetProductCategory("Pork")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Sweet & Sour Pork", GetProductCategory("Pork")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Chicken Wrap", GetProductCategory("Chicken")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Smoked Salmon", GetProductCategory("Fish")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Cookie Pack", GetProductCategory("Snacks")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Smiths Chips - Salt & Vinegar", GetProductCategory("Snacks")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Smiths Chips - Original", GetProductCategory("Snacks")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Zinger Combo Box", GetProductCategory("Combos")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("3 Piece Combo Meal", GetProductCategory("Combos")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("4 Piece Combo Meal", GetProductCategory("Combos")));
            PossiblePairs.Add(new Tuple<string, ProductCategory>("Ultime Combo Deal", GetProductCategory("Combos")));

            foreach (Tuple<string, ProductCategory> t in PossiblePairs)
            {
                AllProducts.Add(new Product
                {
                    Restaurant = null,
                    Category = t.Item2,
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
            return random.NextDouble() * (max - min) + min;
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
            
            foreach(string sample in samples)
            {
                SampleCategories.Add(new ProductCategory
                {
                    Name = sample
                });
            }
        }
    }
}