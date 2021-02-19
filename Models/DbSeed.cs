using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class DbSeed : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {

        //this seeds the database with information. Various Objects are created here
        protected override void Seed(ApplicationDbContext db)
        {
            base.Seed(db);//this recreates the database every time the application is launched


            //this is the role manager included in the framework
            //this creates the different roles the system will use
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //roleManager.Create(new IdentityRole("Guide"));
            roleManager.Create(new IdentityRole("Manager"));


            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));



            //Super liberal password validation for password for seeds

           userManager.PasswordValidator = new PasswordValidator
           {
               RequireDigit = false,
               RequiredLength = 1,
               RequireLowercase = false,
               RequireNonLetterOrDigit = false,
               RequireUppercase = false,
           };

           // creating a new Guide
           Guide Greg = new Guide
            {
                
                Name = "Ben"

            };

            //userManager.Create(Greg, "1");//the user manager add to the database
            //userManager.AddToRole(Greg.Id, "Guide");// assigns to manager role

            Guide Luca = new Guide
            {
                
                Name = "Luca"

            };

            db.Guides.Add(Luca);
            db.Guides.Add(Greg);

            db.SaveChanges();


            ApplicationUser Manager = new ApplicationUser
            {

                UserName = "WeeFish@manager.com",
                Email = "WeeFish@manager.com",
                

            };


            userManager.Create(Manager, "LittleApp3");//the user manager add to the database
            userManager.AddToRole(Manager.Id, "Manager");// assigns to manager role

            db.SaveChanges();

           


            TourType Castle = new TourType
            {
                Type = "Castle"
            };
            TourType Museum = new TourType
            {
                Type = "Museum"
            };
            TourType Whisky = new TourType
            {
                Type = "Whisky"
            };

            db.TourType.Add(Castle);
            db.TourType.Add(Museum);
            db.TourType.Add(Whisky);

            db.SaveChanges();

            MarketingOrder Booked = new MarketingOrder
            {
                Name="Booked"
            };

            MarketingOrder Found = new MarketingOrder
            {
                Name = "Found"
            };

            db.MarketingOrders.Add(Booked);
            db.MarketingOrders.Add(Found);
            db.SaveChanges();


            MarketingTypes FareHarbor = new MarketingTypes
            {
                Name = "FareHarbor",
                MarketingOrderId=Booked.Id,
                Order=1
                
            };
            MarketingTypes GYG = new MarketingTypes
            {
                Name = "GYG",
                MarketingOrderId = Booked.Id,
                Order = 1
            };
            MarketingTypes Viator = new MarketingTypes
            {
                Name = "Viator",
                MarketingOrderId = Booked.Id,
                Order = 1
            };
            MarketingTypes TourDesk = new MarketingTypes
            {
                Name = "TourDesk",
                MarketingOrderId = Booked.Id,
                Order = 1
            };
            MarketingTypes SmallOTA = new MarketingTypes
            {
                Name = "SmallOTA",
                MarketingOrderId = Booked.Id,
                Order = 1,
                LeadsTo = Found.Id
            };
            MarketingTypes AirBnB = new MarketingTypes
            {
                Name = "AirBnB",
                MarketingOrderId = Booked.Id,
                Order = 1,
                LeadsTo = Found.Id
            };
            MarketingTypes GoogleReserve = new MarketingTypes
            {
                Name = "G-Reserve",
                MarketingOrderId = Booked.Id,
                Order = 1,
                LeadsTo = Found.Id
            };

            db.MarketingTypes.Add(FareHarbor);
            db.MarketingTypes.Add(GYG);
            db.MarketingTypes.Add(Viator);
            db.MarketingTypes.Add(TourDesk);
            db.MarketingTypes.Add(SmallOTA);
            db.MarketingTypes.Add(AirBnB);
            db.MarketingTypes.Add(GoogleReserve);
            db.SaveChanges();


            MarketingTypes Google = new MarketingTypes
            {
                Name = "Google",
                MarketingOrderId = Found.Id,
                Order = 2
            };

            MarketingTypes TA = new MarketingTypes
            {
                Name = "TA",
                MarketingOrderId = Found.Id,
                Order = 2

            };
            MarketingTypes WalkUp = new MarketingTypes
            {
                Name = "WalkUp",
                MarketingOrderId = Found.Id,
                Order = 2

            };
            MarketingTypes Flyer = new MarketingTypes
            {
                Name = "Flyer",
                MarketingOrderId = Found.Id,
                Order = 2

            };
            MarketingTypes WOM = new MarketingTypes
            {
                Name = "WOM",
                MarketingOrderId = Found.Id,
                Order = 2

            };
            MarketingTypes StreetPromotion = new MarketingTypes
            {
                Name = "Promotion",
                MarketingOrderId = Found.Id,
                Order = 2

            };
            

            db.MarketingTypes.Add(Google);
            db.MarketingTypes.Add(TA);
            db.MarketingTypes.Add(WalkUp);
            db.MarketingTypes.Add(Flyer);
            db.MarketingTypes.Add(WOM);
            db.MarketingTypes.Add(StreetPromotion);
            
            db.SaveChanges();



            People CastlePeopleAdults = new People
            {
                Name = "Adults",
                TourId= Castle.Id
            };
            People CastlePeopleChilren = new People
            {
                Name = "Children",
                TourId = Castle.Id
            };
            People CastlePeopleConcession = new People
            {
                Name = "Concession",
                TourId = Castle.Id
            };
            People CastlePeopleInfants = new People
            {
                Name = "Infants",
                TourId = Castle.Id
            };
            People CastlePeopleHasTix = new People
            {
                Name = "HasTicket",
                TourId = Castle.Id
            };

            db.People.Add(CastlePeopleAdults);
            db.People.Add(CastlePeopleChilren);
            db.People.Add(CastlePeopleConcession);
            db.People.Add(CastlePeopleInfants);
            db.People.Add(CastlePeopleHasTix);

            People MuseumPeopleAdults = new People
            {
                Name = "Adults",
                TourId = Museum.Id
            };
            People MuseumPeopleChilren = new People
            {
                Name = "Children",
                TourId = Museum.Id
            };
            People MuseumPeopleConcession = new People
            {
                Name = "Concession",
                TourId = Museum.Id
            };
            People MuseumPeopleInfants = new People
            {
                Name = "Infants",
                TourId = Museum.Id
            };
            People MuseumPeopleHasTix = new People
            {
                Name = "HasTicket",
                TourId = Museum.Id
            };

            db.People.Add(MuseumPeopleAdults);
            db.People.Add(MuseumPeopleChilren);
            db.People.Add(MuseumPeopleConcession);
            db.People.Add(MuseumPeopleInfants);
            db.People.Add(MuseumPeopleHasTix);



            People WhiskyPeopleDrinkers = new People
            {
                Name = "Drinkers",
                TourId = Whisky.Id
            };

            People WhiskyPeopleNonDrinkers = new People
            {
                Name = "NonDrinkers",
                TourId = Whisky.Id
            };

            db.People.Add(WhiskyPeopleDrinkers);
            db.People.Add(WhiskyPeopleNonDrinkers);

            db.SaveChanges();



            db.SaveChanges();
        }
    }
}