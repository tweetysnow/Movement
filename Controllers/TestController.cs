using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomelyApp.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Move()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Move(string strValue)
        {

            string[] tmp = strValue.Trim(' ').Split('|');
            bool isValid = false;
            int X = 0;
            int Y = 0;
            String F = "";
            foreach (string s in tmp)
            {
                if (s.ToUpper().StartsWith("PLACE "))
                {
                    string stmp = s.Remove(0, 6);
                    string[] st = stmp.Split(',');
                    X = Convert.ToInt32(st[0]);
                    Y = Convert.ToInt32(st[1]);
                    F = st[2].ToUpper();
                    X = X < 0 ? 0 : X > 5 ? 5 : X;
                    Y = Y < 0 ? 0 : Y > 5 ? 5 : Y;
                    //if(-1<X && X<6 && -1<Y&& Y<6)
                        isValid = true;
                }
                else if (s.ToUpper() == "MOVE" && isValid)
                {
                    if (F == "NORTH" && Y < 5)
                    {
                        Y++;
                    }
                    else if (F == "SOUTH" && Y > 0)
                    {
                        Y--;
                    }
                    else if (F == "EAST" && X < 5)
                    {
                        X++;
                    }
                    else if (F == "WEST" && X > 0)
                    {
                        X--;
                    }
                }
                else if (s.ToUpper() == "LEFT" && isValid)
                {

                    switch (F)
                    {
                        case "NORTH":
                            F = "WEST";
                            break;
                        case "WEST":
                            F = "SOUTH";
                            break;
                        case "SOUTH":
                            F = "EAST";
                            break;
                        case "EAST":
                            F = "NORTH";
                            break;

                    }
                }
                else if (s.ToUpper() == "RIGTH" && isValid)
                {

                    switch (F)
                    {
                        case "NORTH":
                            F = "EAST";
                            break;
                        case "EAST":
                            F = "SOUTH";
                            break;
                        case "SOUTH":
                            F = "WEST";
                            break;
                        case "WEST":
                            F = "NORTH";
                            break;

                    }
                }
                else if (s.ToUpper() == "REPORT")
                {

                    ViewBag.Output = isValid ? string.Format("Output: {0},{1},{2}", X.ToString(), Y.ToString(), F) : "The robot is out of the table";
                }
            }

            return View();
        }
    }
}
