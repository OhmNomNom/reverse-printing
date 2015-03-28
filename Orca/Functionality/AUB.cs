using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orca
{
    public class AUB
    {

        public static string getCurrentUser()
        {
            //Get the user logged in in our system
            return "sma90"; ///TODO: Implement logging according to logged in user
        }

        public static string getMajor(string AUBnet)
        {
            return "ENHL"; ///TODO: get major based on AUBnet
        }

        public static double getFactorForMajor(string major)
        {
            try {
                DB db = new DB();
                return db.getFactorForMajor(major);
            } catch(Exception) {
                return 0;
            }
        }
    }
}