﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Orca.Models
{
    public class AdditionIterable
    {
        private DB conn;
        private SqlDataReader reader;
        public DateTime StartDate, EndDate;
        private AdditionIterable(DateTime start, DateTime end)
        {
            conn = new DB();
            reader = conn.getDonations(start, end);
            StartDate = start;
            EndDate = end;
        }

        public static AdditionIterable fromBounds(DateInfo Bounds)
        {
            return new AdditionIterable(Bounds.StartDate, Bounds.EndDate);
        }

        public Addition getNext()
        {
            if (!reader.Read()) return null;
            Addition ret = new Addition();

            ret.AUBnet = reader.GetString(0);
            //Console.Write(reader.GetFieldType(1));
            ret.Kilos = (double)reader.GetDecimal(1);
            ret.Timestamp = reader.GetDateTime(2);

            return ret;
        }

        public RouteValueDictionary getBounds()
        {
            return new RouteValueDictionary(new
            {
                StartDate = StartDate,
                EndDate = EndDate
            });
        }
    }
}