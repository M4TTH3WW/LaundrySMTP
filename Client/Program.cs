﻿using System;
using System.Collections.Generic;
using LaundryModel;
using LaundryBL;
using MailKit.Net.Smtp;
using MimeKit;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool active = true;
            UserGetServices userGetServices = new UserGetServices();
            UserTransactionServices userTransactionServices = new UserTransactionServices();

            while (active)
            {
                Console.WriteLine("Matthew's Laundry Shop");
                Console.WriteLine("How may I help you?");
                Console.WriteLine("1.Need to wash some clothes?");
                Console.WriteLine("2.Customer wants to claim their clothes");
                Console.WriteLine("3.Customer Queue Details");
                Console.WriteLine("4. Exit");

                Console.WriteLine("Pick an option:");
                string number = Console.ReadLine();

                switch (number)
                {
                    case "1":
                        Console.WriteLine("What is the name?");
                        string name = Console.ReadLine();

                        Console.WriteLine("What is the weight of their clothes?");
                        string clWeight = Console.ReadLine();

                        User newUser = new User { name = name, clWeight = clWeight, status = "Success" };
                        userTransactionServices.CreateUser(newUser);
                        Console.WriteLine("Welcome to our shop!");

                        
                        SendEmail(name, clWeight);
                        break;

                    case "2":
                        Console.WriteLine("What is the name?");
                        string customerDone = Console.ReadLine();

                        User userToRemove = new User { name = customerDone };
                        userTransactionServices.DeleteUser(userToRemove);
                        Console.WriteLine("Thank you! Come Again!");
                        break;

                    case "3":
                        Console.WriteLine("Okay, the customer details are listed below:");
                        DisplayUsers(userGetServices.GetAllUsers());
                        break;

                    case "4":
                        active = false;
                        break;

                    default:
                        Console.WriteLine("ERROR: Invalid input, please try again.");
                        break;
                }
            }
        }

        public static void DisplayUsers(List<User> users)
        {
            foreach (var item in users)
            {
                Console.WriteLine($"name: {item.name}, clWeight: {item.clWeight}, Status: {item.status}");
            }
        }

        public static void SendEmail(string userName, string clothingWeight)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Laundry Shop", "do-not-reply@laundryshop.com"));
            message.To.Add(new MailboxAddress("Client", "matthewpascua22@gmail.com")); 
            message.Subject = "New Laundry Order";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Hi, this is from {userName}!</h1>" +
                       $"<p>Your laundry weighing {clothingWeight} kg has been processed.</p>" +
                       "<p>Thank you for choosing us!</p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("5c428092e1c889", "8046cdc054a257"); 

                    client.Send(message);
                    Console.WriteLine("Email sent successfully through Mailtrap.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}