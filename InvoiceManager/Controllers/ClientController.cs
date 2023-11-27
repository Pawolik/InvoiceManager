using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using Microsoft.AspNet.Identity;
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


        public ActionResult Client()
        {
            var userId = User.Identity.GetUserId();
            var clients = _clientRepository.GetClients(userId);
            return View(clients);
        }

        public ActionResult Create()
        {
            var client = new Client { UserId = User.Identity.GetUserId() };
            return View("ClientForm", client);
        }

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var client = _clientRepository.GetClient(id, userId);

            if (client == null)
                return HttpNotFound();

            return View("ClientForm", client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Client(Client client)
        {
            if (!ModelState.IsValid)
                return View("ClientForm", client);

            if (client.Id == 0)
                _clientRepository.AddClient(client);
            else
                _clientRepository.UpdateClient(client);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _clientRepository.DeleteClient(id, userId);
            }
            catch (Exception exception)
            {
                //powinno tutaj być jeszcze logowanie błędu do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }
    }
}