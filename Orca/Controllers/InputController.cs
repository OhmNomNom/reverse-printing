using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orca.Models;

namespace Orca.Controllers
{
    public class InputController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            RedirectToAction("Add").ExecuteResult(this.ControllerContext); //Redirect invalid actions
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Added(Addition value)
        {
            if (Request.Form["action"] != "add" || !ModelState.IsValid) Response.Redirect("Add"); //Invalid request redirection

            DB db = new DB();

            db.addDonation(value, AUB.getCurrentUser());

            db.Dispose();

            return View(value);
        }

    }
}