using System;
namespace Day5_Assignment
{
    public class UserFactory
    {
        public static IUserpermission CreateUser(string userType)
        {
            return userType switch
            {
                "Student" => new Student(),
                "Teacher" => new Teacher(),
                "Librarian" => new Librarian(),
                _ => throw new ArgumentException("Invalid user type.")
            };
        }
    }
}

