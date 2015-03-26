using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orca.Models;
using System.Globalization;

namespace Orca.Controllers
{
    public class StatsController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            RedirectToAction("Filter").ExecuteResult(this.ControllerContext);
        }
        
        public ActionResult Filter()
        {            
            DateTime Now = DateTime.Now;
            TimeSpan DaysInMonth = TimeSpan.FromDays(30);

            DateInfo Bounds = new DateInfo(Now.Subtract(DaysInMonth), Now);

            return View(Bounds);
        }

        public ActionResult Stats(DateInfo bounds)
        {
            ///TODO: if no dates, redirect
            AdditionIterable Records = AdditionIterable.fromBounds(bounds);

            return View(Records);
        }

        public ActionResult Download()
        {
            bool quotaOnly = false;
            if (Request["act"] == "quota") quotaOnly = true;
            else if (Request["act"] != "data")
            {
                RedirectToAction("Filter").ExecuteResult(this.ControllerContext);
                return Content("");
            }

            Response.Clear();

            DateTime start = DateTime.ParseExact(Request["StartDate"], "dd-MM-yy",CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(Request["EndDate"], "dd-MM-yy", CultureInfo.InvariantCulture);


            Response.AddHeader("Content-Disposition", "attachment; filename=" + 
                "ReversePrinting_" + (quotaOnly?"quota":"data") + "_" + start.ToString("dMMMyy") + "-" + end.ToString("dMMMyy") +".csv");
            
            AdditionIterable recs = AdditionIterable.fromBounds(new DateInfo(start, end));

            string resp = "";
            Addition rec;

            if (quotaOnly) resp += "AUBnet,Quota\n";
            else resp += "AUBnet,Major,Kilos,Quota,Timestamp\n";

            while ((rec = recs.getNext()) != null)
            {
                if(quotaOnly) resp += String.Format("{0},{1}\n", rec.AUBnet, rec.Quota);
                else resp += String.Format("{0},{1},{2},{3},{4}\n", rec.AUBnet, rec.Major, rec.Kilos, rec.Quota, rec.Timestamp);
            }

            return Content(resp,"text/csv");
        }
    }
}