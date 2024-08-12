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
            //Die Liste übergeben wir in View um Zugriff in Views/Category/Index.cshtml zu erhalten.
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        /* Creating Method to be invoked when we click on Create New Category.
         * This Method will be Invoked when we click in Category/Index.cshtml on Create New Category
         * In Index.cshtml we will use a TagHelper to bind the button with this Method so we will 
         * write in Index.cshtml on line 18 <a asp-action="Action"> This will tell the Program that 
         * needs to invoke this Method and return a View in this case the Create.cshtml
         */
        public IActionResult Create()
        {
            return View();
        }

        /* [HttpPost]: Diese Annotation gibt an, dass die Methode nur auf HTTP-POST-Anfragen reagiert.
         * Es ist typisch für Aktionen, die Daten von einem Formular empfangen und verarbeiten sollen.
         * In dem Fall das erstellen einer Category in der Webanwendung.
         * Create(Category obj) nimmt ein Category-Objekt als Parameter entgegen. 
         * Dieses Objekt wird normalerweise durch die Datenbindung des von einem Formular 
         * gesendeten Werts befüllt.
         */
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Custom Validations
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            //if(obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value.");
            //}


            //Validation
            if (ModelState.IsValid) 
            { 
            /* _db: Dies ist eine Instanz deines Datenbankkontexts vom Typ ApplicationDbContext
             * Categories: Eine DbSet<Category>-Eigenschaft innerhalb deines Datenbankkontexts,
             * die die Tabelle Categories in der Datenbank repräsentiert.
             * Add(obj): Diese Methode fügt das übergebene Category-Objekt zur Categories-Collection 
             * im Datenbankkontext hinzu. Das Objekt wird dadurch für eine spätere Speicherung in der 
             * Datenbank vorgemerkt.
             */
            _db.Categories.Add(obj);
            /* Diese Methode speichert alle Änderungen, die im Kontext vorgenommen wurden,
             * dauerhaft in der Datenbank. In diesem Fall wird das neue Category-Objekt tatsächlich
             * in die Datenbank eingefügt.
             */
            _db.SaveChanges();

                /* RedirectToAction("Index"): Nach dem Speichern leitet diese Methode den Benutzer zur
            * "Index"-Aktion innerhalb desselben Controllers weiter. Index könnte eine Methode sein,
            * die eine Liste aller Kategorien anzeigt.
            * Diese Umleitung sorgt dafür, dass der Benutzer nach dem Erstellen einer neuen Kategorie
            * nicht auf der selben Seite bleibt, sondern zur Übersicht aller Kategorien 
            * (oder einer anderen Zielseite) geleitet wird.
            */
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
