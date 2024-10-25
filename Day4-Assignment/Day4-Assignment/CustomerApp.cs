using System;
using System.Text.RegularExpressions;

namespace Day4_Assignment
{
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public static class Validator
    {
        public static bool IsValidEmail(string email) =>
            Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public static bool IsValidPhoneNumber(string phoneNumber) =>
            Regex.IsMatch(phoneNumber, @"^\+?\d{10,15}$");

        public static bool IsValidDateOfBirth(DateTime dob) =>
            dob <= DateTime.Today.AddYears(-18);
    }
    public class CustomerApp
	{
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1. Add Customer\n2. Exit\nChoose an option: ");
                var choice = Console.ReadLine();

                if (choice == "2") break;
                if (choice == "1") AddCustomer();
                else Console.WriteLine("Invalid choice.");
            }
        }

        private void AddCustomer()
        {
            var customer = new Customer
            {
                Name = Prompt("Enter Name: "),
                Email = Prompt("Enter Email: "),
                PhoneNumber = Prompt("Enter Phone Number: "),
            };

            if (DateTime.TryParse(Prompt("Enter Date of Birth (yyyy-mm-dd): "), out DateTime dob))
                customer.DateOfBirth = dob;

            bool isValid = Validator.IsValidEmail(customer.Email) &&
                           Validator.IsValidPhoneNumber(customer.PhoneNumber) &&
                           Validator.IsValidDateOfBirth(customer.DateOfBirth);

            if (isValid)
                Console.WriteLine($"Customer added: {customer.Name}, {customer.Email}, {customer.PhoneNumber}, {customer.DateOfBirth.ToShortDateString()}");
            else
                Console.WriteLine("Invalid details.");
        }

        private string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}

