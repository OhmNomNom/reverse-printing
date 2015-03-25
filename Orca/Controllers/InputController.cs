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
        // GET: Input
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Added(Addition val)
        {
            if (Request.Form["action"] != "add") Response.Redirect("Add");
            if (!ModelState.IsValid) Response.Redirect("Add");

            DB db = new DB();

            db.addDonation(val);

            return View(val);
        }

    }
}