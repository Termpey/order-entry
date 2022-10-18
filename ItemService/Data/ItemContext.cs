using ItemService.Models;

using Microsoft.EntityFrameworkCore;

namespace ItemService.Data {
    public class ItemContext : DbContext {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options){}

        public DbSet<Item> Items => Set<Item>();

        public DbSet<ItemCategory> ItemCategories => Set<ItemCategory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //Map models to ItemService database tables
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<ItemCategory>().ToTable("ItemCategory");

            //Item Has One Item Category
            modelBuilder.Entity<Item>()
                .HasOne(i => i.ItemCategory)
                .WithMany()
                .HasForeignKey(i => i.ItemCategoryId);
        }
    }
}