/// <summary>
/// Author: Connor Trempe
/// 
/// DB Context that encompasses the ClientService Database
/// Initializes necessary DB sets and points towards proper DB tables
/// Configures entity relationships between different tables
/// </summary>

using ClientService.Models;

using Microsoft.EntityFrameworkCore;

namespace ClientService.Data{
    public class ClientContext : DbContext {
        
        public ClientContext(DbContextOptions<ClientContext> options) : base(options){}

        //Shorthand for initializing Clients DbSet before we exit the constructor
        public DbSet<Client> Clients => Set<Client>();  

        public DbSet<Location> Locations => Set<Location>();

        public DbSet<ClientContact> ClientContacts => Set<ClientContact>();
        
        public DbSet<ContactType> ContactTypes => Set<ContactType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //Map models to ClientService database tables
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<ClientContact>().ToTable("ClientContact");
            modelBuilder.Entity<ContactType>().ToTable("ContactType");

            //Client May have Many Contacts
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Contacts)
                .WithOne()
                .HasForeignKey(cc => cc.ClientId);

            //Client May have Many Locations
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Locations)
                .WithOne()
                .HasForeignKey(l => l.ClientId);

            //Location May have Many Contacts
            modelBuilder.Entity<Location>()
                .HasMany(l => l.Contacts)
                .WithOne()
                .HasForeignKey(cc => cc.LocationId);

            //ClientContact Will have One ContactType  
            modelBuilder.Entity<ClientContact>()
                .HasOne(cc => cc.ContactType)
                .WithMany()
                .HasForeignKey(cc => cc.ContactTypeId);
                
        }
    }
}