using System;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Interfaces
{
	public interface IUserService
	{
        User Register(string username, string password, string role,string email);
        string Authenticate(string username, string password);
        //User GetById(int id);

    }
}

