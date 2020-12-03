using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        // Add the application DB Context (link to the database)
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _db.Transactions.ToList();
            List<Transaction> orderedList = transactionList.OrderBy(t => t.TransactionDateUtc).ToList();

            return View(orderedList);
        }

        // GET: Transaction
        public ActionResult Create()
        {
            return View(new Transaction());
        }

        // POST: Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _db.Transactions.Add(transaction);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }
    }
}