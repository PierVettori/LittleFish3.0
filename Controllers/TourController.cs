using LittleFish3._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleFish3._0.Controllers
{
    public class TourController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager manager = new Manager();
        // GET: Tour

        [Authorize(Roles = "Manager")]
        public ActionResult TourSelect()
        {

            List<TourType> tourTypes = db.TourType.ToList();


            ViewBag.TourType = tourTypes;

            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Participants(TourType t)
        {
            List<People> peoples = manager.PeopleGetter(t.Id);
            List<MarketingTypes> marketingTypes = db.MarketingTypes.ToList();
            List<Guide> guides = db.Guides.ToList();
            //foreach(MarketingTypes mt in marketingTypes)
            //{
                
            //}
            ViewBag.Tour = t;
            ViewBag.People = peoples;
            ViewBag.MarketTypes = marketingTypes;
            ViewBag.Guides = guides;

            return View();
        }


        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult ProcessParticipants()
        {
            //List<People> peoples = db.People.ToList();

            if (Request["submit"]!=null)
            {

               

                int tourId = int.Parse(Request["TourId"]);
                int totalPax = 0;
                List<People> peoples = manager.PeopleGetter(tourId);
                foreach (People p in peoples)
                {
                    if (Request[p.Name] != null)
                    {

                        int person = int.Parse(Request[p.Name]);
                        if (person > 0)
                        {
                            Tour t = new Tour();
                            t.Pax = person;
                            t.PersonId = p.Id;
                            t.TourTypeId = tourId;
                            t.Day = DateTime.Now.Date;

                            db.Tour.Add(t);
                        }

                        totalPax = totalPax + person;
                    }

                }

                
                {
                    Marketing market = new Marketing();
                    market.Day = DateTime.Now.Date;
                    market.Pax = totalPax;
                    market.MarketTypeId = int.Parse(Request["first"]);

                    db.Marketing.Add(market);



                    if (Request["second"] != "")
                    {

                        int secondMarket = int.Parse(Request["second"]);





                        Marketing additonalMarket = new Marketing();
                        additonalMarket.Day = DateTime.Now.Date;
                        additonalMarket.Pax = totalPax;
                        additonalMarket.MarketTypeId = secondMarket;
                        db.Marketing.Add(additonalMarket);

                    }

                    Manager m = new Manager();
                    if (Request["GuideId"] != "")
                    {
                        int GuideId = int.Parse(Request["GuideId"]);
                        m.GuideSale(GuideId, totalPax);
                    }

                }

                db.SaveChanges();




            }
            return RedirectToAction("Participants");
        }


        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult TourPage()
        {
            List<TourType> tourTypes = db.TourType.ToList();
            List<People> people = db.People.ToList();
            List<MarketingTypes> markets = db.MarketingTypes.ToList();
            List<Guide> guides = db.Guides.ToList();

            foreach (TourType t in tourTypes)
            {
                foreach (People p in people)
                {
                    if (p.TourId == t.Id)
                    {
                        t.People.Add(p);
                    }
                }
            
            }
            ViewBag.People = people;
            ViewBag.TourType = tourTypes;
            ViewBag.Markets = markets;
            ViewBag.Guides = guides;

            if (Request["submit"]!=null)
            {
                int tourId = int.Parse(Request["tourId"]);

                int totalPax = 0;
                List<People> peoples = manager.PeopleGetter(tourId);
                foreach (People p in peoples)
                {
                    if (Request[p.Name] != null)
                    {

                        int person = int.Parse(Request[p.Name]);
                        if (person > 0)
                        {
                            Tour t = new Tour();
                            t.Pax = person;
                            t.PersonId = p.Id;
                            t.TourTypeId = tourId;
                            t.Day = DateTime.Now.Date;

                            db.Tour.Add(t);
                        }

                        totalPax = totalPax + person;
                    }

                }
                if (Request["firstMarket"] != "")
                {
                    Marketing market = new Marketing();
                    market.Day = DateTime.Now.Date;
                    market.Pax = totalPax;
                    market.MarketTypeId = int.Parse(Request["firstMarket"]);

                    db.Marketing.Add(market);
                }



                if (Request["secondMarket"] != "")
                {
                    int secondMarket = int.Parse(Request["secondMarket"]);





                    Marketing additonalMarket = new Marketing();
                    additonalMarket.Day = DateTime.Now.Date;
                    additonalMarket.Pax = totalPax;
                    additonalMarket.MarketTypeId = secondMarket;
                    db.Marketing.Add(additonalMarket);

                }

                //Manager m = new Manager();
                if (Request["guideId"] != "")
                {
                    int GuideId = int.Parse(Request["guideId"]);
                    manager.GuideSale(GuideId, totalPax);
                }

            }

            db.SaveChanges();

        
        
            return View();
        }
    }
}