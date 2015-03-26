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
            RedirectToAction("Add").ExecuteResult(this.ControllerContext);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Added(Addition val)
        {
            if (Request.Form["action"] != "add") Response.Redirect("Add");
            if (!ModelState.IsValid) Response.Redirect("Add");

            DB db = new DB();

            db.addDonation(val, AUB.getUser());

            return View(val);
        }

    }
}