using HarisWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace HarisWeb.Data
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

    }
}
