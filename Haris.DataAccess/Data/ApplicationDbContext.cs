using Haris.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Haris.DataAccess.Data
/* ApplicationDbContext : DbContext ist eine Klasse, die den Zugriff auf die Datenbank
 * in einer ASP.NET Core-Anwendung ermöglicht.
 * ApplicationDbContext: Deine benutzerdefinierte Klasse, die von DbContext erbt und die
 * Datenbankzugriffslogik kapselt.
 * DbContext: Eine Basisklasse von Entity Framework Core, die für die Kommunikation mit der Datenbank
 * zuständig ist.
 * Hauptfunktionen:
 * Definiert Datenbanktabellen: DbSet<TEntity>-Eigenschaften 
 * (z.B. public DbSet<Category> Categories { get; set; }) repräsentieren Tabellen in der Datenbank.
 * Konfiguriert das Datenmodell: Die Methode OnModelCreating wird verwendet, um das Datenmodell und 
 * die Datenbankstruktur anzupassen (z.B. Beziehungen und Seed-Daten).
 * Verwaltet Datenbankoperationen: Ermöglicht Abfragen, Einfügungen, Aktualisierungen und Löschungen 
 * von Daten.
 */
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        /* Dependency Injection (DI): In ASP.NET Core wird der DbContext häufig über Dependency Injection (DI) verwaltet.
         * Der Konstruktor akzeptiert ein DbContextOptions<ApplicationDbContext>-Objekt, das alle Konfigurationsoptionen 
         * (wie z.B. die Verbindung zur Datenbank) enthält. Diese Optionen werden in der Regel im Startup-Code oder beim
         * Einrichten von DI definiert.
         * Weitergabe der Konfiguration: Der Konstruktor ruft die Basisklasse (DbContext) auf und übergibt die options.
         * Dadurch wird sichergestellt, dass die DbContext-Basisklasse korrekt konfiguriert ist, um mit der Datenbank zu
         * kommunizieren. Dies umfasst Konfigurationsdetails wie den Verbindungstyp (z.B. SQL Server), den Connection String
         * und andere Optionen. Ohne diesen Konstruktor wüsste der DbContext nicht, wie er mit der Datenbank verbunden 
         * werden soll, was zu Fehlern führen würde.
         */
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        /*  Ein DbSet in Entity Framework Core ist eine Sammlung von Entitäten,
         *  die einer Tabelle in der Datenbank entspricht. Es repräsentiert die Datenbanktabelle
         *  im Code und ermöglicht die Durchführung von CRUD-Operationen (Create, Read, Update, Delete)
         *  auf den Entitäten dieser Tabelle.
         *  Ein DbSet<T> enthält alle Instanzen einer bestimmten Entität T, die in der Datenbank gespeichert sind.
         *  Es ermöglicht auch LINQ-Abfragen, um Daten zu filtern, zu sortieren oder zu projizieren.
         *  Kurz gesagt, DbSet ist der Hauptmechanismus, über den du in Entity Framework Core auf
         *  die Daten in deiner Datenbank zugreifst und sie manipulierst.
         */
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Compmanies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /* OnModelCreating ist eine Methode zur Konfiguration des Datenmodells in 
         * Entity Framework Core.
         * modelBuilder: Erlaubt detaillierte Anpassungen für Entitäten, Beziehungen und
         * Datenbankstrukturen.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //required for Identity
            base.OnModelCreating(modelBuilder);

            /* Die angegebenen Daten werden automatisch in die Tabelle eingefügt,
             * wenn die Datenbank erstellt oder aktualisiert wird. Dies ist nützlich, 
             * um sicherzustellen, dass bestimmte Grunddaten vorhanden sind, wenn die Anwendung 
             * zum ersten Mal gestartet wird.
             */
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Company>().HasData(
                new Company 
                { 
                    Id = 1, Name = "TechSolution", 
                    StreetAddress = "123 Tech St", 
                    City = "Tech City", 
                    PostalCode = "43000",
                    State = "TC",
                    PhoneNumber = "23123213"
                },
                new Company
                {
                    Id = 2,
                    Name = "VividBooks",
                    StreetAddress = "456 Book St",
                    City = "Book City",
                    PostalCode = "82320",
                    State = "BC",
                    PhoneNumber = "4121213"
                },
                new Company
                {
                    Id = 3,
                    Name = "ReadersClub",
                    StreetAddress = "789 Read St",
                    City = "Reader City",
                    PostalCode = "123000",
                    State = "RC",
                    PhoneNumber = "009823213"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                }
                );
        }
    }
}
