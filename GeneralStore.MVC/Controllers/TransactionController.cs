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
            var viewModel = new CreateTransactionViewModel();

            viewModel.Products = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductId.ToString()
            });

            viewModel.Customers = _db.Customers.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.CustomerId.ToString()
            });

            return View(viewModel);
        }

        // POST: Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _db.Transactions.Add(new Transaction
                {
                    CustomerId = viewModel.CustomerId,
                    ProductId = viewModel.ProductId
                });
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}