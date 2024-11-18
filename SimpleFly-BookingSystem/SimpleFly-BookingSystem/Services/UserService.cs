using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SimpleFly_BookingSystem.Authentication;
using SimpleFly_BookingSystem.Data;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly SimplyFlyContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(SimplyFlyContext context, IOptions<JwtSettings> jwtSettings, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = new PasswordHasher<User>(); 
        }

        public User Register(string username, string password, string role, string email)
        {
            
            var existingUser = _context.Users.SingleOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                return null; 
            }

            var user = new User
            {
                Username = username,
                PasswordHash = _passwordHasher.HashPassword(null, password),
                Role = role,
                Email = email
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public string Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null) return null;

            
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            
            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}


