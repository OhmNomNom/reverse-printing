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
            TimeSpan DaysInPrevMonth = TimeSpan.FromDays(DateTime.DaysInMonth(Now.Year,(Now.Month+11)%12));
            //+11)%12 to get the previous month

            DateInfo Bounds = new DateInfo(Now.Subtract(DaysInPrevMonth), Now);

            return View(Bounds);
        }

        public ActionResult Stats(DateInfo bounds)
        {
            AdditionIterable Records = AdditionIterable.fromBounds(bounds);

            return View(Records);
        }

        public ActionResult Download()
        {
            bool quotaOnly = false;
            if (Request["act"] == "quota") quotaOnly = true;
            else if (Request["act"] != "data")
            {
                //Response.Write(Request["action"]);
                Response.Redirect("/Stats/");
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
            else resp += "AUBnet,Kilos,Quota,Timestamp";

            while ((rec = recs.getNext()) != null)
            {
                if(quotaOnly) resp += String.Format("{0},{1}\n", rec.AUBnet, rec.Quota);
                else resp += String.Format("{0},{1},{2},{3}\n", rec.AUBnet, rec.Kilos, rec.Quota, rec.Timestamp);
            }

            return Content(resp,"text/csv");
        }
    }
}