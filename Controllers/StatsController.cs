using LittleFish3._0.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleFish3._0.Controllers
{
    public class StatsController : Controller
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly Manager m = new Manager();
        // GET: Stats


        [Authorize(Roles = "Manager")]
        public ActionResult EndOfDay()
        {

            
            List<TourType> tourTypes = db.TourType.ToList();
            List<Guide> guides = db.Guides.ToList();
          
            foreach (TourType t in tourTypes)
            {
                List<GuideNumbers> gun = new List<GuideNumbers>();

                int total = 0;
                foreach (Guide g in guides)
                {
                    GuideNumbers gn = new GuideNumbers();
                    gn.Pax = m.GuideGetter(t, g.Id, DateTime.Now);
                    gn.GuideId = g.Id;
                    gn.TourTypeId = t.Id;
                    gn.TourType = t;
                    gn.Guide = db.Guides.Find(g.Id);
                    
                    if(gn.Pax>0)
                    {
                        total = total - gn.Pax;
                        gun.Add(gn);
                    }
                    
                }
                

                
                
                t.People = m.PeopleGetter(t.Id);
                t.GuideNumbers = gun;
                
                foreach (People p in t.People)
                {
                   
                    Tour tour = new Tour();
                    tour.PersonId = p.Id;
                    tour.Person = p;
                    tour.Pax = m.TourGetter(t.Id, p.Id, DateTime.Now);
                    t.Tours.Add(tour);
                    total = total + tour.Pax;
                   



                }


                t.Pax = total;
            }

            
             ViewBag.TourTypes = tourTypes;
             ViewBag.Guides = guides;

            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult ProcessEndOfDay()
        {
            List<TourType> tours = db.TourType.ToList();

            if (Request["submit"]!=null)
            {
                
                List<Guide> guides = db.Guides.ToList();
               

                string guideName = Request["submit"];

                Guide guide=null;
                foreach(Guide g in guides)
                {
                    if(g.Name== guideName)
                    {
                        guide = g;
                    }
                }
                if (Request["groupNumber"] != "")
                {
                    
                    int group = int.Parse(Request["groupNumber"].ToString());
                    int tourId = int.Parse(Request["tourId"].ToString());
                    int check = int.Parse(Request["check"].ToString());
                    if (group <= check)
                    {
                        m.GuideSetter(guide.Id, group, tourId);

                        
                    }
                }

               

            }

            if (Request["DeleteGuide"] != null)
            {
                int gId = int.Parse(Request["DeleteGuideId"]);
                int tourId = int.Parse(Request["tourId"].ToString());

                m.DeleteGuideAssignment(tourId, gId, DateTime.Now);

            }

            return RedirectToAction("EndOfDay") ;
        }

        [Authorize(Roles = "Manager")]
        public ActionResult GuideStats()
        {
            List<Guide> guides = db.Guides.ToList();
            
            foreach (Guide g in guides)
            {

                int totalPax = m.GuideTotalPaxGetter(g.Id);
                int sales = m.SalesAllGetter(g.Id);
                
                double testo = 100 * (double.Parse(sales.ToString()) / double.Parse(totalPax.ToString()));
                g.SalesPercent = Math.Round(testo);
                
                
                
            }

            ViewBag.Guides = guides;
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult MarketStats()
        {
            List<MarketingTypes> marketsTypes = db.MarketingTypes.ToList();
            List<MarketingOrder> marketOrder = db.MarketingOrders.ToList();
            
            List<Marketing> marketing = new List<Marketing>();
            int marketNo = m.TotalMarketGetter();
            



            if(Request["dates"]!=null)
            {
                string day1 = Request["firstDay"];
                string day2 = Request["secondDay"];
                ViewBag.day1 = day1;
                ViewBag.day2 = day2;

                foreach (MarketingTypes ma in marketsTypes)
                {

                    int totalPax = m.MarketingBetweenGetter(ma.Id,day1,day2);


                    Marketing market = new Marketing();
                    market.MarketingType = ma;
                    market.MarketTypeId = ma.Id;

                    double percentage = 100 * (double.Parse(totalPax.ToString()) / double.Parse(marketNo.ToString()));


                    try
                    {
                        market.Pax = Convert.ToInt32(percentage);
                    }
                    catch
                    {
                        market.Pax = 0;
                    }




                    marketing.Add(market);


                }
            }
            else
            {
                foreach (MarketingTypes ma in marketsTypes)
                {

                    int totalPax = m.MarketAllGetter(ma.Id);


                    Marketing market = new Marketing();
                    market.MarketingType = ma;
                    market.MarketTypeId = ma.Id;

                    double percentage = 100 * (double.Parse(totalPax.ToString()) / double.Parse(marketNo.ToString()));

                    try
                    {
                        market.Pax = Convert.ToInt32(percentage);
                    }
                    catch
                    {
                        market.Pax = 0;
                    }




                    marketing.Add(market);


                }
            }
            ViewBag.Types = marketOrder;
            ViewBag.Markets = marketing;
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult Tourstats()
        {

            List<TourType> tours = db.TourType.ToList();

           


            if (Request["dates"] != null)
            {
                string day1 = Request["firstDay"];
                string day2 = Request["secondDay"];

                foreach(TourType t in tours)
                {
                    Tour tour = new Tour();
                    tour.Pax = m.BetweentourGetter(t.Id,day1,day2 );
                    t.Tours.Add(tour);
                }

                ViewBag.day1 = day1;
                ViewBag.day2 = day2;

            }
            else
            {
                foreach (TourType t in tours)
                {
                    Tour tour = new Tour();
                    tour.Pax = m.TotaltourGetter(t.Id);
                    t.Tours.Add(tour);
                }

            }



            ViewBag.Tours = tours;
            return View();
        }

    }
}