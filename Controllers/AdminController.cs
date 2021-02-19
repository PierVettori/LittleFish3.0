using LittleFish3._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleFish3._0.Controllers
{
    public class AdminController : Controller
    {
        private readonly Manager m = new Manager();
        private  ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin

        [Authorize(Roles = "Manager")]
        public ActionResult CreateGuide(Guide g)
        {
            if (g.Name != null)
            {
                db.Guides.Add(g);
                db.SaveChanges();
            }






            return View();


            
        }


        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult CreateTour()
        {

           if(Request["submit"]!=null)
            {
                if (Request["newTourName"] != null)
                {
                    string tourNewName = Request["newTourName"];
                    TourType tt = new TourType();

                    tt.Type = tourNewName;

                    db.TourType.Add(tt);
                    db.SaveChanges();


                }
            }

            List<TourType> tours = db.TourType.ToList();
            List<People> participants = db.People.ToList();
            foreach (TourType t in tours)
            {

                t.People = m.PeopleGetter(t.Id);
                //foreach (People p in participants)
                //{
                //    if (p.TourId == t.Id)
                //    {
                //        t.People.Add(p);
                //    }
                //}
            }

            ViewBag.Tours = tours;




            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult AddParticipants(TourType t)
        {

            List<People> peoples = db.People.ToList();
            foreach(People p in peoples)
            {
                if(p.TourId == t.Id)
                {
                    t.People.Add(p);
                }
            }

            ViewBag.Tour = t;
            return View();
        }

        [HttpGet]
        public ActionResult ProcessParticipants()
        {
            int tourId = int.Parse(Request["tourId"]);

            if(Request["submit"]!=null)
            {
                if (Request["addPar"] != null)
                {
                    string newPar = Request["addPar"];

                    People p = new People();
                    p.TourId = tourId;
                    p.Name = newPar;
                    db.People.Add(p);
                    db.SaveChanges();

                }
            }

            return RedirectToAction("AddParticipants", new { id= tourId });
        }



        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult CreateMarkets()
        {



            List<MarketingOrder> mo = db.MarketingOrders.ToList();

            ViewBag.MarketOrders = mo;


            if(Request["submit"]!=null)
            {
                if (Request["name"] != null)
                {


                    string name = Request["name"];
                    string leadsTo = Request["leadsTo"];
                    int marketId = int.Parse(Request["marketType"]);

                    MarketingTypes newMarket = new MarketingTypes();

                    newMarket.Name = name;
                    newMarket.MarketingOrderId = marketId;

                    
                    if(Request["submit"].Equals("Found"))
                    {
                        newMarket.Order = 2;
                    }
                    else if (Request["submit"].Equals("Booked"))
                    {
                        if (leadsTo != null)
                        {
                            if (leadsTo.Equals("on"))
                            {
                                newMarket.LeadsTo = 2;
                                
                            }

                        }

                        newMarket.Order = 1;
                    }

                    db.MarketingTypes.Add(newMarket);
                    db.SaveChanges();
                }

            }


            return View();
        }


        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult DeleteStuff()
        {
           
           

            

            //     < input type = "text" name = "marketId" id = "marketId" />
            if (Request["tourId"] !=null && Request["marketId"] == "" && Request["participantId"] == "" && Request["guideId"] == "")
            {
                int tourId = int.Parse(Request["tourId"]);

                TourType t = db.TourType.Find(tourId);
                db.TourType.Remove(t);
                db.SaveChanges();
            }

            if (Request["participantId"] != null && Request["marketId"] == "" && Request["tourId"] == "" && Request["guideId"] == "")
            {
                int peopleId = int.Parse(Request["participantId"]);

                People p = db.People.Find(peopleId);
                db.People.Remove(p);
                db.SaveChanges();
            }

            if (Request["marketId"] != null && Request["participantId"] == "" && Request["tourId"] == "" && Request["guideId"] == "")
            {
                int marketId = int.Parse(Request["marketId"]);

                MarketingTypes m = db.MarketingTypes.Find(marketId);
                db.MarketingTypes.Remove(m);
                db.SaveChanges();
            }

            if (Request["guideId"] != null && Request["marketId"] == "" && Request["participantId"] == "" && Request["tourId"] == "")
            {
                int guideId = int.Parse(Request["guideId"]);

                Guide g = db.Guides.Find(guideId);
                db.Guides.Remove(g);
                db.SaveChanges();
            }


            List<TourType> tours = db.TourType.ToList();
            List<MarketingOrder> types = db.MarketingOrders.ToList();
            List<Guide> guides = db.Guides.ToList();

            foreach (TourType t in tours)
            {
                t.People = m.PeopleGetter(t.Id);

               
            }

            foreach(MarketingOrder marketO in types)
            {

                marketO.Marketingtypes = m.MarketDeleteGetter(marketO.Id);
            }

            ViewBag.Tours = tours;
            ViewBag.Types = types;
            ViewBag.Guides = guides;

            return View();
        }

    }
}