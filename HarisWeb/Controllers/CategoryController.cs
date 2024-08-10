using HarisWeb.Data;
using HarisWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HarisWeb.Controllers
{
    /* Klasse CategoryController: Diese Klasse erbt von der Controller-Basisklasse
     * und ist für die Bearbeitung von HTTP-Anfragen zuständig.
     */
    public class CategoryController : Controller
    {
        /* Das Feld private readonly ApplicationDbContext _db; wird dazu verwendet,
         * den Datenbankkontext (ApplicationDbContext) in der gesamten Controller-Klasse
         * zugänglich zu machen.Warum? Datenzugriff: Der ApplicationDbContext stellt Methoden bereit,
         * um auf die Datenbank zuzugreifen, Abfragen auszuführen und Änderungen zu speichern. 
         * Dependency Injection: ASP.NET Core nutzt Dependency Injection, um Instanzen von
         * Abhängigkeiten (wie ApplicationDbContext) in den Controller zu injizieren. Dadurch kann
         * der Controller auf die Datenbank zugreifen, ohne direkt eine neue Instanz des
         * Datenbankkontexts erstellen zu müssen. In der Methode Index wird dieses Feld verwendet,
         * um auf die Categories-Tabelle in der Datenbank zuzugreifen und die Kategorien als 
         * Liste abzurufen. Das Feld _db ermöglicht es also, den Datenbankkontext innerhalb des 
         * Controllers wiederzuverwenden, was den Code sauberer und effizienter macht.
         */
        private readonly ApplicationDbContext _db;
        /* Der Konstruktor akzeptiert eine Instanz der Klasse ApplicationDbContext,
         * die für den Zugriff auf die Datenbank verwendet wird, und weist sie dem privaten
         * Feld _db zu. ApplicationDbContext ist typischerweise eine Klasse, die den
         * Datenbankkontext darstellt und für das Entity Framework genutzt wird.
         */
        public CategoryController(ApplicationDbContext db) 
        {
            _db = db;
        }
        /* Der Code innerhalb der Index-Methode führt eine Abfolge von Schritten durch,
         * die in ASP.NET Core MVC für die Verarbeitung einer HTTP-Anfrage typisch sind.
         * Hier ist der genaue Ablauf:
         * _db.Categories: Der ApplicationDbContext-Datenbankkontext (_db) enthält eine
         * Categories-Eigenschaft, die eine Sammlung von Kategorien (vermutlich als 
         * DbSet<Category> definiert) repräsentiert. .ToList(): Diese Methode konvertiert
         * die Abfrage in eine Liste (List<Category>), indem sie alle Datensätze der 
         * Categories-Tabelle aus der Datenbank abruft und in objCategoryList speichert.
         */
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View();
        }
    }
}
