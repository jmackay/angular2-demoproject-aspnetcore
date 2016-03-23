using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;

namespace Angular2Demo.Web.Models
{
	public class Angular2DemoContext : DbContext
	{
		public Angular2DemoContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Stack> Stacks { get; set; }
		public DbSet<Card> Cards { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<CardAssignment> CardAssignments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Card>().HasOne(x => x.CreatedBy).WithMany(x => x.CreatedCards).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Card>().HasOne(x => x.Category).WithMany(x => x.Cards).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Card>().HasOne(x => x.PreviousCard).WithMany().IsRequired(false);

			modelBuilder.Entity<Stack>().HasOne(x => x.PreviousStack).WithMany().IsRequired(false);
			
			modelBuilder.Entity<CardAssignment>()
				.HasOne(x => x.AssignedTo)
				.WithMany(x => x.AssignedTo)
				.HasForeignKey(x => x.AssignedToId).OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<CardAssignment>()
				.HasOne(x => x.CreatedBy)
				.WithMany(x => x.CreatedAssignments)
				.HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<CardAssignment>()
				.HasOne(x => x.Card)
				.WithMany(x => x.CardAssignments)
				.HasForeignKey(x => x.CardId).OnDelete(DeleteBehavior.Restrict);

			//base.OnModelCreating(modelBuilder);
		}
	}
}