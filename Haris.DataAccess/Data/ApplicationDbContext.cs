using Haris.Models;
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
    public class ApplicationDbContext : DbContext
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

        /* OnModelCreating ist eine Methode zur Konfiguration des Datenmodells in 
         * Entity Framework Core.
         * modelBuilder: Erlaubt detaillierte Anpassungen für Entitäten, Beziehungen und
         * Datenbankstrukturen.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Die angegebenen Daten werden automatisch in die Tabelle eingefügt,
             * wenn die Datenbank erstellt oder aktualisiert wird. Dies ist nützlich, 
             * um sicherzustellen, dass bestimmte Grunddaten vorhanden sind, wenn die Anwendung 
             * zum ersten Mal gestartet wird.
             */
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "KTM 1190 Adventure S", DisplayOrder = 2 },
                new Category { Id = 2, Name = "BMW R 1200 GS Adventure", DisplayOrder = 1 },
                new Category { Id = 3, Name = "Honda Africa Twin 1100", DisplayOrder = 3 }
                );
        }
    }
}
