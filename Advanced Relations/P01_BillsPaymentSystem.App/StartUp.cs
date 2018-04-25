using System.Globalization;
using System.Linq;

namespace P01_BillsPaymentSystem.App
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;

    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db =new BillsPaymentSystemContext())
            {

                //1 exercise
                //db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                //2nd exercise 
                Seed(db);

                
            }

            //3 exercise
            var userId = int.Parse(Console.ReadLine());
            using (var db = new BillsPaymentSystemContext())
            {
                try
                {
                    var user = db.Users.Where(u => u.UserId == userId)
                        .Select(u => new
                        {
                            UserName = u.FirstName + " " + u.LastName,
                            BankAccounts = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.BankAccount)
                                .Select(pm => pm.BankAccount).ToList(),
                            CreditCards = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.CreditCard)
                                .Select(pm => pm.CreditCard).ToList()
                        }).FirstOrDefault();

                    Console.WriteLine($"User: {user.UserName}");

                    if (user.BankAccounts.Any())
                    {
                        Console.WriteLine("Bank Accounts:");
                        foreach (var acc in user.BankAccounts)
                        {
                            Console.WriteLine($"-- ID: {acc.BankAccountId}");
                            Console.WriteLine($"--- Balance: {acc.Balance:f2}");
                            Console.WriteLine($"--- Bank: {acc.BankName}");
                            Console.WriteLine($"--- SWIFT: {acc.Swift}");
                        }
                    }

                    if (user.CreditCards.Any())
                    {
                        Console.WriteLine($"Credit Cards:");
                        foreach (var card in user.CreditCards)
                        {
                            Console.WriteLine($"-- ID: {card.CreditCardId}");
                            Console.WriteLine($"--- Limit: {card.Limit:f2}");
                            Console.WriteLine($"--- Money Owed: {card.MoneyOwed:f2}");
                            Console.WriteLine($"--- Limit Left: {card.LimitLeft:f2}");
                            Console.WriteLine($@"--- Expiration Date: {card.ExpirationDate
                                .ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine($"User with Id {userId} not found!");
                }
                

            }

            //4 exercise only Deposit & Withdraw methods in BankAccount And CreditCard classes
        }

        private static void Seed(BillsPaymentSystemContext db)//2
        {
            var users = new User[]
            {
                new User()
                {
                    FirstName = "Gosho",
                    LastName = "Kalchev",
                    Email = "blatoto@abv.bg",
                    Password = "12cxg324"
                },
                new User()
                {
                    FirstName = "Misho",
                    LastName = "Mitkov",
                    Email = "sqnkata@abv.bg",
                    Password = "qweqwe13shg324"
                },
                new User()
                {
                    FirstName = "Stancho",
                    LastName = "Parashkevov",
                    Email = "ludaksi@abv.bg",
                    Password = "azsamtamwa1123"
                },
                new User()
                {
                    FirstName = "Coco",
                    LastName = "Bongo",
                    Email = "raketata@gmail.bg",
                    Password = "izlitameee"
                },
                new User()
                {
                    FirstName = "Max",
                    LastName = "Chapman",
                    Email = "nzkostaa@gmail.bg",
                    Password = "naistinaneznam"
                }
            };

            var creditCards = new CreditCard[]
            {
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("20.05.2020", "dd.MM.yyyy", null),
                    Limit = 259m,
                    MoneyOwed = 128m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("16.02.2018", "dd.MM.yyyy", null),
                    Limit = 1643m,
                    MoneyOwed = 876m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("18.11.2019", "dd.MM.yyyy", null),
                    Limit = 853m,
                    MoneyOwed = 765m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("22.05.2021", "dd.MM.yyyy", null),
                    Limit = 3254m,
                    MoneyOwed = 1657m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("16.06.2020", "dd.MM.yyyy", null),
                    Limit = 12234m,
                    MoneyOwed = 11245m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("24.12.2017", "dd.MM.yyyy", null),
                    Limit = 1666m,
                    MoneyOwed = 123m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("29.01.2022", "dd.MM.yyyy", null),
                    Limit = 4326m,
                    MoneyOwed = 3214m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("13.03.2019", "dd.MM.yyyy", null),
                    Limit = 865m,
                    MoneyOwed = 822m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("22.07.2019", "dd.MM.yyyy", null),
                    Limit = 865m,
                    MoneyOwed = 822m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("11.09.2018", "dd.MM.yyyy", null),
                    Limit = 865m,
                    MoneyOwed = 822m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("02.02.2022", "dd.MM.yyyy", null),
                    Limit = 1253m,
                    MoneyOwed = 825m
                },
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("30.12.2023", "dd.MM.yyyy", null),
                    Limit = 865m,
                    MoneyOwed = 822m
                },
            };

            var bankAccounts = new BankAccount[]
            {
                new BankAccount()
                {
                    Balance = 2200m,
                    BankName = "KFC Bank",
                    Swift = "KFCMM"
                },
                new BankAccount()
                {
                    Balance = 1666m,
                    BankName = "Fibank Bank",
                    Swift = "FBBB"
                },
                new BankAccount()
                {
                    Balance = 12500m,
                    BankName = "Rotari Bank",
                    Swift = "R666"
                },
                new BankAccount()
                {
                    Balance = 40000m,
                    BankName = "KGB Bank",
                    Swift = "KGB Bnk"
                },
                new BankAccount()
                {
                    Balance = 4367m,
                    BankName = "Uomo Bank",
                    Swift = "OMOB"
                },
                new BankAccount()
                {
                    Balance = 6464m,
                    BankName = "Rotari Bank",
                    Swift = "R666"
                },
                new BankAccount()
                {
                    Balance = 3253m,
                    BankName = "Post Bank",
                    Swift = "PSTBNK"
                },
                new BankAccount()
                {
                    Balance = 2142m,
                    BankName = "Generali Bank",
                    Swift = "GNRLBNK"
                },
                new BankAccount()
                {
                    Balance = 5212m,
                    BankName = "KFC Bank",
                    Swift = "KFCMM"
                },
            };

            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod()
               {
                   User =users[0],
                   CreditCard = creditCards[4],
                   Type = PaymentMethodType.CreditCard
               },
                new PaymentMethod()
                {
                    User =users[0],
                    CreditCard = creditCards[10],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[1],
                    CreditCard = creditCards[0],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[1],
                    CreditCard = creditCards[3],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[2],
                    CreditCard = creditCards[1],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[2],
                    CreditCard = creditCards[2],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[2],
                    CreditCard = creditCards[9],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[3],
                    CreditCard = creditCards[6],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[3],
                    CreditCard = creditCards[7],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[4],
                    CreditCard = creditCards[5],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[4],
                    CreditCard = creditCards[8],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[4],
                    CreditCard = creditCards[11],
                    Type = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User =users[0],
                    BankAccount = bankAccounts[1],
                    Type = PaymentMethodType.BankAccount
                },
                new PaymentMethod()
                {
                    User =users[0],
                    BankAccount = bankAccounts[3],
                    Type = PaymentMethodType.BankAccount
                },
                new PaymentMethod()
                {
                    User =users[1],
                    BankAccount = bankAccounts[5],
                    Type = PaymentMethodType.BankAccount
                },
                new PaymentMethod()
                {
                    User =users[2],
                    BankAccount = bankAccounts[0],
                    Type = PaymentMethodType.BankAccount
                },
                new PaymentMethod()
                {
                    User =users[2],
                    BankAccount = bankAccounts[2],
                    Type = PaymentMethodType.BankAccount
                },
                new PaymentMethod()
                {
                    User =users[3],
                    BankAccount = bankAccounts[6],
                    Type = PaymentMethodType.BankAccount
                },
                new PaymentMethod()
                {
                    User =users[3],
                    BankAccount = bankAccounts[7],
                    Type = PaymentMethodType.BankAccount
                },
                new PaymentMethod()
                {
                    User =users[4],
                    BankAccount = bankAccounts[8],
                    Type = PaymentMethodType.BankAccount
                },
            };

            db.Users.AddRange(users);
            db.CreditCards.AddRange(creditCards);
            db.BankAccounts.AddRange(bankAccounts);
            db.PaymentMethods.AddRange(paymentMethods);

            db.SaveChanges();

        }

        private static void PayBills(int userId, decimal amount) 
        {
            
        }
    }
}
