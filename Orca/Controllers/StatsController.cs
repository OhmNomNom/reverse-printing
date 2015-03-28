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
            RedirectToAction("Filter").ExecuteResult(this.ControllerContext); //Invalid actions get redirected
        }
        
        public ActionResult Filter()
        {            
            DateTime Now = DateTime.Now;
            TimeSpan DaysInMonth = TimeSpan.FromDays(30);

            DateInfo Bounds = new DateInfo(Now.Subtract(DaysInMonth), Now); //Default values to populate textboxes

            return View(Bounds);
        }

        public ActionResult Stats(DateInfo bounds)
        {
            if (Request["StartDate"] == null || Request["EndDate"] == null) { 
                RedirectToAction("Filter").ExecuteResult(this.ControllerContext);
                return Content(""); //The redirect is asynchronous
                                    //Need to exit the function and not call the view
            }

            AdditionIterable Records = AdditionIterable.fromBounds(bounds); //Don't worry, we'll dispose of it in the cshtml

            return View(Records);
        }

        public ActionResult Download()
        {
            if (Request["act"] != "data") //Invalid request
            {
                RedirectToAction("Filter").ExecuteResult(this.ControllerContext); 
                return Content("");
            }

            Response.Clear(); //Start with a raw file

            DateTime start = DateTime.ParseExact(Request["StartDate"], "dd-MM-yy",CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(Request["EndDate"], "dd-MM-yy", CultureInfo.InvariantCulture);


            Response.AddHeader("Content-Disposition", "attachment; filename=" + 
                "ReversePrinting_data_" + start.ToString("dMMMyy") + "-" + end.ToString("dMMMyy") +".csv"); //We're going to send a csv file
            
            AdditionIterable recs = AdditionIterable.fromBounds(new DateInfo(start, end));

            Addition rec;

            string resp = "AUBnet,Major,Kilos,Quota,Timestamp,Processed\n"; //Header for the files

            while ((rec = recs.getNext()) != null)
            {
                resp += String.Format("{0},{1},{2},{3},{4},{5}\n", rec.AUBnet, rec.Major, rec.Kilos, rec.Quota, rec.Timestamp, rec.Processed?1:0);
            }

            recs.Dispose();

            return Content(resp,"text/csv");
        }
    }
}