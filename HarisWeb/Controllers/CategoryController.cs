using Haris.Models;
using Haris.DataAccess.Data;
using Haris.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HarisWeb.Controllers
{
    public class CategoryController : Controller
    {       
        private readonly IUnitOfWork _unitOfWork;
        
        public CategoryController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
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
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        //CREATE
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
            //Validation
            if (ModelState.IsValid) 
            {

                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();

            TempData["success"] = "Category created successfully";
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

        //EDIT
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u=>u.Id==id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        //DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u=>u.Id==id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //With ActionName("Delete") we say that even the Methods name ist DeletePOST, the Action is Delete 
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            //When we want to Delete we first need to find that Category from Database
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if(obj == null)
            {
                return NotFound();
            }
            //Now we remove the Category and save the Changes
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            //Then Redirect to Index View to load the Categorie List again
            return RedirectToAction("Index");
        }
    }
}
