using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProductsShop.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using ProductsShop.Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace ProductsShop.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //using (var db = new ProductsShopContext())
            //{
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();
            //}


            // these are Json Methods And Imports
            //Console.WriteLine(ImportUsersFromJson());
            //Console.WriteLine(ImportCategoriesFromJson());
            //Console.WriteLine(ImportProductsFromJson());
            //SetCategories();
            //ProductsInRange();
            //CategoriesByProductsCount();
            //UsersAndProducts();


            // these are Xml Methods And Imports
            //Console.WriteLine(ImportUsersFromXml());
            //Console.WriteLine(ImportCategoriesFromXml());
            //Console.WriteLine(ImportProductsFromXml());
            UsersAndProductsXml();

        }



        static void Test()
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products.Select(p => new
                {
                    p.Buyer,
                    p.Seller
                }).ToArray();

                var jsonString = JsonConvert.SerializeObject(products, Formatting.Indented,new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("test.json", jsonString);

                //foreach (var p in products)
                //{
                //    Console.WriteLine($"PrBuyer {p.Buyer} \nSeller {p.Seller}");
                //}
            }
        }


        // methods for Xml Export begin from here

        static void UsersAndProductsXml()
        {
            using (var db =new ProductsShopContext())
            {
                var users = db.Users
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        u.Age,
                        Products = u.ProductsSold
                            .Where(ps => ps.BuyerId != null)
                            .Select(p => new
                            {
                                p.Name,
                                p.Price
                            }).ToArray()
                    })
                    .OrderByDescending(u => u.Products.Length)
                    .ToArray();


                //Console.WriteLine($"Users count: {users.Length}");
                //foreach (var u in users)
                //{
                //    Console.WriteLine($"user : firstName {u.FirstName} | lastName {u.LastName} | age {u.Age}");

                //    Console.WriteLine($"products sold count : {u.Products.Count()}");

                //    foreach (var p in u.Products)
                //    {
                //        Console.WriteLine($"Product: name {p.Name} | price {p.Price}");
                //    }
                //}

                var xmlDoc=new XDocument(new XElement("users"
                    ,new XAttribute("count",users.Length)));

                foreach (var u in users)
                {
                    var firstName = "no first name";

                    if (u.FirstName != null)
                    {
                        firstName = u.FirstName;
                    }

                    int? age = 0;

                    if (u.Age != null)
                    {
                        age = u.Age;
                    }

                    var user =new XElement("user",new XAttribute("first-name", firstName)
                        ,new XAttribute("last-name",u.LastName)
                        ,new XAttribute("age", age)
                        ,new XElement("sold-products",new XAttribute("count",u.Products.Length)));

                    foreach (var p in u.Products)
                    {
                        var products=new XElement("product",
                            new XAttribute("name",p.Name),
                            new XAttribute("price",p.Price));

                        user.Element("sold-products").Add(products);
                    }

                    xmlDoc.Root.Add(user);

                }

                var xmlString = xmlDoc.ToString();

                File.WriteAllText("users-and-products.xml",xmlString);

            }
        }

        static void CategoriesByProductsCountXml()
        {
            using (var db=new ProductsShopContext())
            {
                var categories = db.Categories
                    .Select(c => new
                    {
                        c.Name,
                        productsCount = c.CategoryProducts.Count,
                        productsAveragePrice = c.CategoryProducts.Select(cp => cp.Product.Price).Average(),
                        totalRevenue = c.CategoryProducts.Select(cp => cp.Product.Price).Sum()
                    })
                    .OrderBy(c => c.productsCount)
                    .ToArray();

                //foreach (var c in categories)
                //{
                //    Console.WriteLine($"Category name {c.Name}");
                //    Console.WriteLine($"    ProductsCount {c.productsCount}");
                //    Console.WriteLine($"    AveragePrice {c.productsAveragePrice}");
                //    Console.WriteLine($"    TotalRevenue {c.totalRevenue}");
                //}

                var xmlDoc=new XDocument(new XElement("categories"));

                foreach (var c in categories)
                {
                    xmlDoc.Root.Add(new XElement("category",new XAttribute("name",c.Name)
                        ,new XElement("products-count",c.productsCount)
                        ,new XElement("average-price",c.productsAveragePrice)
                        ,new XElement("total-revenue",c.totalRevenue)));
                }

                var xmlString = xmlDoc.ToString();

                File.WriteAllText("categories-by-products.xml",xmlString);
            }
        }

        static void SoldProductsXml()
        {
            using (var db=new ProductsShopContext())
            {
                var users = db.Users
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        productsSold = u.ProductsSold.Select(p => new
                        {
                            p.Name,
                            p.Price
                        })
                    }).ToArray();

                var xmlDoc=new XDocument(new XElement("users"));

                foreach (var u in users)
                {
                    var firstName = "no firstName";

                    if (u.FirstName != null)
                    {
                        firstName = u.FirstName;
                    }

                    var user=new XElement("user",
                        new XAttribute("first-name", firstName),
                        new XAttribute("last-name",u.LastName),
                        new XElement("sold-products"));

                    foreach (var p in u.productsSold)
                    {
                        var soldProducts=new XElement("product",
                            new XElement("name",p.Name),
                            new XElement("price",p.Price));

                        user.Element("sold-products").Add(soldProducts);
                    }

                    xmlDoc.Root.Add(user);
                }

                string xmlString = xmlDoc.ToString();

                File.WriteAllText("SoldProducts.xml", xmlString);
            }
        }

        static void ProductsInRangeXml()
        {
            using (var db =new ProductsShopContext())
            {
                var products = db.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000 && p.BuyerId != null)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        p.Buyer
                    }).ToArray();

                var xmlDoc=new XDocument(new XElement("products"));

                foreach (var p in products)
                {
                    var entity = new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                    };

                    xmlDoc.Root.Add(new XElement("product",
                        new XAttribute("name", entity.name),
                        new XAttribute("price", entity.price),
                        new XAttribute("buyer", entity.buyer)));
                }

                string xmlString = xmlDoc.ToString();

                File.WriteAllText("ProductsInRange.xml", xmlString);
            }
        }
        
        static string ImportUsersFromXml()
        {
            var xmlString = File.ReadAllText("Resources/users.xml");

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var users =new List<User>();

            foreach (var e in elements)
            {
                var firstName = e.Attribute("firstName")?.Value;

                var lastName = e.Attribute("lastName").Value;

                int? age = null;

                if (e.Attribute("age") != null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }

                var user = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age=age
                };

                users.Add(user);

                
            }

            using (var db=new ProductsShopContext())
            {
                db.Users.AddRange(users);

                db.SaveChanges();
            }

            return $"{users.Count} users were added to database";
        }

        static string ImportCategoriesFromXml()
        {
            var xmlString = File.ReadAllText("Resources/categories.xml");

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();
            
            var categories=new List<Category>();

            foreach (var e in elements)
            {
                var category = new Category()
                {
                    Name=e.Element("name").Value
                };

                categories.Add(category);
            }

            using (var db=new ProductsShopContext())
            {
                db.Categories.AddRange(categories);

                db.SaveChanges();
            }

            return $"{categories.Count} categories were added to database";
        }

        static string ImportProductsFromXml()
        {
            var path = "Resources/products.xml";

            var xmlString = File.ReadAllText(path);

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var categoryProducts = new List<CategoryProduct>();

            using (var db=new ProductsShopContext())
            {
                

                var userIds = db.Users.Select(u => u.Id).ToArray();

                var categoryIds = db.Categories.Select(c => c.Id).ToArray();

                Random rnd = new Random();

                foreach (var e in elements)
                {

                    string name = e.Element("name").Value;

                    decimal price =decimal.Parse(e.Element("price").Value);

                    int index = rnd.Next(0, userIds.Length);

                    int sellerId = userIds[index];

                    int? buyerId = sellerId;

                    while (buyerId == sellerId)
                    {
                        int buyerIndex = rnd.Next(0, userIds.Length);

                        buyerId = userIds[buyerIndex];
                    }

                    if (buyerId - sellerId < 5)
                    {
                        buyerId = null;
                    }

                    var product = new Product()
                    {
                        Name = name,
                        Price = price,
                        SellerId = sellerId,
                        BuyerId = buyerId,
                    };

                    int categoryIndex = rnd.Next(0, categoryIds.Length);

                    int categoryId = categoryIds[categoryIndex];

                    var catProduct = new CategoryProduct()
                    {
                        Product = product,
                        CategoryId = categoryId
                    };

                    categoryProducts.Add(catProduct);
                }

                db.CategoryProducts.AddRange(categoryProducts);

                db.SaveChanges();

            }

            return $"{categoryProducts.Count} products were added to database";
        }

        

        // methods for Json Export begin from here 
        static void UsersAndProducts()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        u.Age,
                        ProductsSold=new
                        {
                          count=u.ProductsSold.Count(),
                          products=u.ProductsSold.Select(p => new
                          {
                              name=p.Name,
                              price=p.Price
                          })
                        }
                    })
                    .OrderByDescending(u => u.ProductsSold.count)
                    .ThenBy(u => u.LastName)
                    .ToArray();

                var finalFormatting = new
                {
                    usersCount = users.Length,
                    users
                };

                var jsonString = JsonConvert.SerializeObject(finalFormatting, Formatting.Indented);

                File.WriteAllText("UsersAndProducts.json",jsonString);
            }
        }

        static void CategoriesByProductsCount()
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        c.Name,
                        ProductsCount=c.CategoryProducts.Count,
                        AveragePrice=c.CategoryProducts.Select(cp => cp.Product.Price).Average(),
                        TotalSum=c.CategoryProducts.Select(cp => cp.Product.Price).Sum()
                    }).ToArray();

                //foreach (var c in categories)
                //{
                //    Console.WriteLine($"Category: {c.Name}\nProducts Count: {c.ProductsCount}" +
                //                      $"\nAverage Price: {c.AveragePrice}\nTotal Revenue: {c.TotalSum}");
                //}

                var jsonString = JsonConvert.SerializeObject(categories,Formatting.Indented,new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("CategoriesByProductsCount.json", jsonString);
            }
        }

        static void SuccessfullySoldProducts()
        {
            using (var db = new ProductsShopContext())
            {
                var sellers = db.Users
                    .Where(u => u.ProductsSold
                    .Any(p => p.BuyerId != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,     
                        SoldProducts=u.ProductsSold
                        .Where(p => p.BuyerId!= null) // ???
                        .Select(p => new
                        {
                            p.Name,
                            p.Price,
                            BuyerFirstName = p.Buyer.FirstName, 
                            BuyerLastName =p.Buyer.LastName,
                        })
                    }).ToArray();

                var jsonString = JsonConvert.SerializeObject(sellers, Formatting.Indented,new JsonSerializerSettings()
                {
                    DefaultValueHandling =DefaultValueHandling.Ignore 
                });

                File.WriteAllText("SuccessfullySoldProducts.json", jsonString);
            }
        }

        static void ProductsInRange()
        {
            using (var db=new ProductsShopContext())
            {
                var products = db.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        FullName = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    })
                    .OrderBy(p => p.Price)
                    .ToArray();

                //foreach (var p in products)
                //{
                //    Console.WriteLine($"{p.Name} {p.Price} Seller: {p.FullName}");
                //}

                var jsonString = JsonConvert.SerializeObject(products,Formatting.Indented);

                File.WriteAllText("ProductsInRange.json",jsonString);
            }
        }

        static void SetCategories()
        {
            using (var context=new ProductsShopContext())
            {
                var categoryIds = context.Categories.Select(c => c.Id).ToArray();

                var productIds = context.Products.Select(p => p.Id).ToArray();

                int categoryCount = categoryIds.Length;

                Random rnd = new Random();

                var categoryProducts = new List<CategoryProduct>();

                foreach (var p in productIds)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        
                        var index = rnd.Next(0, categoryCount);

                        while (categoryProducts.Any(cp => cp.ProductId==p 
                        && cp.CategoryId==categoryIds[index]))
                        {
                            index = rnd.Next(0, categoryCount);
                        }

                        var categoryProduct=new CategoryProduct()
                        {
                            ProductId = p,
                            CategoryId = categoryIds[index]
                        };

                        categoryProducts.Add(categoryProduct);
                    }
                    
                }

                context.CategoryProducts.AddRange(categoryProducts);

                context.SaveChanges();

            }    
        }

        static string ImportProductsFromJson()
        {
            var path = "Resources/products.json";

            Random rnd = new Random();

            Product[] products = ImportJson<Product>(path);

            using (var context = new ProductsShopContext())
            {
                var userIds = context.Users.Select(u => u.Id).ToArray();
                
                foreach (var p in products)
                {
                    int index = rnd.Next(0, userIds.Length);

                    int sellerId = userIds[index];

                    int? buyerId = sellerId;

                    while (buyerId == sellerId)
                    {
                        int buyerIndex = rnd.Next(0, userIds.Length);

                        buyerId = userIds[buyerIndex];
                    }

                    if (buyerId - sellerId < 5)
                    {
                        buyerId = null;
                    }

                    p.SellerId = sellerId;
                    p.BuyerId = buyerId;
                }

                context.Products.AddRange(products);

                context.SaveChanges();
            }

            return $"{products.Length} products were imported from file :{path}";
        }

        static string ImportCategoriesFromJson()
        {
            string path = "Resources/categories.json";

            Category[] categories = ImportJson<Category>(path);

            using (var context = new ProductsShopContext())
            {
                context.Categories.AddRange(categories);

                context.SaveChanges();
            }

            return $"{categories.Length} categories were imported from file :{path}";

        }

        static string ImportUsersFromJson()
        {
            string path = "Resources/users.json";

            User[] users = ImportJson<User>(path);

            using (var context = new ProductsShopContext())
            {
                context.Users.AddRange(users);

                context.SaveChanges();
            }

            return $"{users.Length} users were imported from file :{path}";
        }

        static T[] ImportJson<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
        }
    }
}
