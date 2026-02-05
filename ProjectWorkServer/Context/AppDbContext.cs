using Microsoft.EntityFrameworkCore;
using ProjectWorkServer.Models;
using System.Collections.Generic;

namespace ProjectWork.Server.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<User> User { get; set; }
		public DbSet<CodiceComune> CodiceComune { get; set; }
	}
}