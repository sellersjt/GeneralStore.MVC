using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Models
{
    public class EditTransactionViewModel
    {
        public int TransactionId { get; set; }

        // Drop down products
        public IEnumerable<SelectListItem> Products { get; set; }

        // Selected produce
        public int ProductId { get; set; }

        // Drop down customer
        public IEnumerable<SelectListItem> Customers { get; set; }

        // Selected customer
        public int CustomerId { get; set; }

        // Transaction Date
        public DateTimeOffset TransactionDateUtc { get; set; }
    }
}