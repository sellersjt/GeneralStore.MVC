using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        // Add the application DB Context (link to the database)
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Transactions
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

        // GET: Delete
        // Transaction/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Transaction transaction = _db.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        // POST: Delete
        // Transaction/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Transaction model)
        {
            var transaction = _db.Transactions.Find(model.TransactionId);

            _db.Transactions.Remove(transaction);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Edit
        // Transaction/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Transaction transaction = _db.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditTransactionViewModel();

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

            viewModel.TransactionId = transaction.TransactionId;
            viewModel.CustomerId = transaction.CustomerId;
            viewModel.ProductId = transaction.ProductId;

            return View(viewModel);
        }

        // POST: Edit
        // Transaction/Edit/{id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = _db.Transactions.Find(viewModel.TransactionId);

                transaction.CustomerId = viewModel.CustomerId;
                transaction.ProductId = viewModel.ProductId;

                _db.Entry(transaction).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Details
        // Transaction/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Transaction transaction = _db.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }
    }
}