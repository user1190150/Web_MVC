using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Haris.DataAccess.Repository.IRepository
{
    /* Ein Repository ist wie eine Schnittstelle zwischen deiner Anwendung und der Datenbank.
     * Es hilft dir, den Zugriff auf Daten sauber und organisiert zu halten, ohne dass deine
     * Anwendungslogik direkt mit der Datenbank sprechen muss. Stell dir vor, es ist wie ein
     * Mittelsmann, der sich um alle CRUD-Operationen (Create, Read, Update, Delete) kümmert,
     * sodass du dich in deiner Anwendung nicht ständig mit den Details der Datenbank herumschlagen
     * musst. In deinem Beispiel hast du ein generisches Repository-Interface IRepository<T>,
     * das für jede Art von Datenobjekt (z. B. Category) verwendet werden kann. Es bietet Methoden
     * wie GetAll(), um alle Einträge zu holen, oder Add(), um einen neuen Eintrag hinzuzufügen.
     * Dadurch bleibt dein Code flexibler und wiederverwendbar.
     */
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll();

        /* Sie hat die Aufgabe, ein einzelnes Objekt vom Typ T aus einer Sammlung oder Datenbank
         * zu holen, das einem bestimmten Filterkriterium entspricht. 
         * 
         * T: Der Rückgabewert dieser Methode ist ein Objekt des Typs T, der in deinem Interface
         * als generischer Typ definiert ist. T könnte z. B. eine Category, Product, User oder jede
         * andere Klasse sein.
         * 
         * Expression<Func<T, bool>>: Das ist ein LINQ-Ausdruck (Lambda-Ausdruck), der eine Funktion
         * beschreibt, die ein Objekt vom Typ T nimmt und einen bool-Wert zurückgibt.
         * 
         * Func<T, bool>: Diese Funktion nimmt ein Objekt vom Typ T als Eingabe und gibt true oder
         * false zurück, je nachdem, ob das Objekt die Bedingung erfüllt oder nicht.
         * 
         * Expression<...>: Das Expression-Teil bedeutet, dass der Ausdruck als abstrakter Syntaxbaum
         * (statt als kompiliertes Code) übergeben wird. Das ermöglicht es der Methode, den Ausdruck
         * zu analysieren und z. B. in eine SQL-Abfrage zu übersetzen, wenn du mit einer Datenbank 
         * arbeitest.
         */
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entitiy);
        void RemoveRange(IEnumerable<T> entity);
    }
}
