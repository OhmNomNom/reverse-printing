using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orca
{
    public class AUB
    {
        private static Dictionary<string, double> _factors = new Dictionary<string, double> {
            {"ECE", 2},
            {"CCE", 2},
            {"MECH", 2},
            {"LAND", 1},
            {"ENVH", 1}
        }; ///TODO: make list based on external file

        public static string getUser()
        {
            return "sma90"; ///TODO: Implement logging according to logged in user
        }

        public static string getMajor(string AUBnet)
        {
            return "ECE"; ///TODO: get major based on AUBnet
        }

        public static double getFactorForMajor(string major)
        {
            double result;
            if (_factors.TryGetValue(major, out result)) return result;
            else return 0;
        }
    }
}