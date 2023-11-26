using InvoiceManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    public class ClientController : Controller
    {

        private ClientRepository _clientRepository = new ClientRepository();


        public ActionResult Index()
        {
            return View();
        }
    }
}