
using FintexClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FintexClient.Controllers
{
    public class HomeController : Controller
    {
        CreditContext creditContext = new CreditContext();


        public ActionResult Index()
        {
            IEnumerable<CreditOffer> creditOffers = creditContext.CreditOffers;

            ViewBag.CreditOffers = creditOffers;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}