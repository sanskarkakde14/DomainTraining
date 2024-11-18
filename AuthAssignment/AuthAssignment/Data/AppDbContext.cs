using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthAssignment.Data
{
	public class AppDbContext:IdentityDbContext<IdentityUser>
	{
		public AppDbContext(DbContextOptions options):base(options)
		{
		}
	}
}

