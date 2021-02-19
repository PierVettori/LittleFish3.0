using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class Manager
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static string SqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static SqlConnection sqlCon = new SqlConnection(SqlConnectionString);

        public List<People> PeopleGetter(int tourId)
        {
            List<People> people = new List<People>();


            sqlCon.Open();
            string commandString = "SELECT * FROM People WHERE TourId = '" + tourId + "'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();


            while (reader.Read())
            {
                People p = new People();

                p.Id = int.Parse(reader["Id"].ToString());
                p.Name = reader["Name"].ToString();
                p.TourId = tourId;
                people.Add(p);

            }

            reader.Close();
            sqlCon.Close();

            return people;
        }




        public int TourGetter(int tourId, int personId, DateTime day)
        {
            //List<Tour> tours = new List<Tour>();
            //List<Tour> toursFinal = new List<Tour>();

            int total = 0;
            sqlCon.Open();

            string commandString = "SELECT SUM(Pax) as Total FROM Tours WHERE TourTypeId = '" + tourId + "'  AND personId = '" + personId + "' AND Day = '" + day.ToString("yyyy/MM/dd") + "'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();


            while (reader.Read())
            {

                string totSt= reader["Total"].ToString();
                try
                {
                    total = int.Parse(totSt);
                }
                catch { }
                
            }

            reader.Close();
            sqlCon.Close();

            

            return total;
        }

        public int GuideGetter(TourType t,int guideId, DateTime day)
        {

            int total = 0;
            
            sqlCon.Open();

            string commandString = "SELECT SUM(Pax) as Total FROM GuideNumbers WHERE TourTypeId = '" + t.Id + "' AND GuideId = '" + guideId + "' AND Day = '" + day.ToString("yyyy/MM/dd") + "' ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();

           
                while (reader.Read())
                {
                    string totSt = reader["Total"].ToString();
                    try
                    {
                        total = int.Parse(totSt);
                    }
                    catch { }

                }
            
           



            reader.Close();
            sqlCon.Close();



            return total;
        }


        

        public void DeleteGuideAssignment(int t, int guideId, DateTime day)
        {
            sqlCon.Open();
            string commandString = "DELETE FROM GuideNumbers WHERE TourTypeId = '" + t + "' AND GuideId = '" + guideId + "' AND Day = '" + day.ToString("yyyy/MM/dd") + "' ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }




        public void GuideSale(int guideId, int sales)
        {
            sqlCon.Open();
            string commandString = "INSERT INTO GuideSales (GuideId,Sales, Day) VALUES ('" + guideId + "','" + sales + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "')";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }


        public void GuideSetter(int guideId, int pax, int tourId)
        {
            sqlCon.Open();
            string commandString = "INSERT INTO GuideNumbers (GuideId,Pax,TourTypeId, Day) VALUES ('" + guideId + "','" + pax + "','" + tourId + "', '" + DateTime.Now.ToString("yyyy/MM/dd") + "')";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }


        public List<GuideSales> SalesBetweenGetter(int guideId, DateTime day1, DateTime day2)
        {
            List<GuideSales> guideSales = new List<GuideSales>();

            sqlCon.Open();
            string commandString = "SELECT * FROM GuideSales WHERE GuideId = '" + guideId + "' AND Day BETWEEN '" + day1.ToString("yyyy/MM/dd") + "' AND '" + day2.ToString("yyyy/MM/dd") + "'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();


            while (reader.Read())
            {
                GuideSales g = new GuideSales();
                g.GuideId = guideId;
                g.sales = int.Parse(reader["sales"].ToString());

                guideSales.Add(g);
            }

            //foreach (GuideNumbers gn in guideNumbers)
            //{
            //    gn.Guide = db.Guides.Find(gn.GuideId);

            //}
            reader.Close();
            sqlCon.Close();

            return guideSales;
        }

        public int SalesAllGetter(int guideId)
        {
            int guideSales = 0;

            sqlCon.Open();
            string commandString = "SELECT * FROM GuideSales WHERE GuideId = '" + guideId + "' ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();


            while (reader.Read())
            {


                guideSales = guideSales + int.Parse(reader["sales"].ToString());


            }

            //foreach (GuideNumbers gn in guideNumbers)
            //{
            //    gn.Guide = db.Guides.Find(gn.GuideId);

            //}
            reader.Close();
            sqlCon.Close();

            return guideSales;
        }

        public int GuideTotalPaxGetter(int guideId)
        {
            int guideNumber = 0;

            sqlCon.Open();
            string commandString = "SELECT * FROM GuideNumbers WHERE GuideId = '" + guideId + "' ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();


            while (reader.Read())
            {


                guideNumber = guideNumber + int.Parse(reader["Pax"].ToString());


            }

            //foreach (GuideNumbers gn in guideNumbers)
            //{
            //    gn.Guide = db.Guides.Find(gn.GuideId);

            //}
            reader.Close();
            sqlCon.Close();

            return guideNumber;
        }


        public int MarketAllGetter(int marketId)
        {
            int guideNumber = 0;

            sqlCon.Open();
            string commandString = "SELECT * FROM Marketings WHERE MarketTypeId = '" + marketId + "' ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();


            while (reader.Read())
            {


                guideNumber = guideNumber + int.Parse(reader["Pax"].ToString());


            }

            //foreach (GuideNumbers gn in guideNumbers)
            //{
            //    gn.Guide = db.Guides.Find(gn.GuideId);

            //}
            reader.Close();
            sqlCon.Close();

            return guideNumber;
        }


        public int TotalMarketGetter()
        {
            int total = 0;

            sqlCon.Open();
            string commandString = "SELECT SUM(Pax) AS Total FROM Marketings";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {


                    total = int.Parse(reader["Total"].ToString());


                }
            }
            catch
            {
                total = 1;
            }

            //foreach (GuideNumbers gn in guideNumbers)
            //{
            //    gn.Guide = db.Guides.Find(gn.GuideId);

            //}
            reader.Close();
            sqlCon.Close();

            return total;
        }


        public int MarketingBetweenGetter(int marketId,string day1, string day2)
        {

            int pax = 0;
            sqlCon.Open();
            string commandString = "SELECT * FROM Marketings WHERE MarketTypeId = '" + marketId + "' AND day BETWEEN '" + day1 + "' AND '" + day2 + "'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();


            while (reader.Read())
            {
                pax = pax + int.Parse(reader["Pax"].ToString());
            }

            //foreach (GuideNumbers gn in guideNumbers)
            //{
            //    gn.Guide = db.Guides.Find(gn.GuideId);

            //}
            reader.Close();
            sqlCon.Close();

            return pax;
        }



        public int TotaltourGetter(int tourId)
        {
            int total = 0;

            sqlCon.Open();
            string commandString = "SELECT SUM(Pax) as Total FROM Tours WHERE TourTypeId = '" + tourId + "'  ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    total = int.Parse(reader["Total"].ToString());
                }
            }
            catch
            {

            }

            //foreach (GuideNumbers gn in guideNumbers)
            //{
            //    gn.Guide = db.Guides.Find(gn.GuideId);

            //}
            reader.Close();
            sqlCon.Close();

            return total;
        }


        public int BetweentourGetter(int tourId, string day1, string day2)
        {
            int total = 0;

            sqlCon.Open();
            string commandString = "SELECT SUM(Pax) as Total FROM Tours WHERE TourTypeId = '" + tourId + "'  AND Day BETWEEN '" + day1 + "' AND '" + day2 + "'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    total = int.Parse(reader["Total"].ToString());
                }
            }
            catch
            {

            }

            //foreach (GuideNumbers gn in guideNumbers)
            //{
            //    gn.Guide = db.Guides.Find(gn.GuideId);

            //}
            reader.Close();
            sqlCon.Close();

            return total;
        }



        public List<MarketingTypes> MarketDeleteGetter(int mOrderId)
        {
            List<MarketingTypes> markets = new List<MarketingTypes>();

            sqlCon.Open();
            string commandString = "SELECT * FROM MarketingTypes WHERE MarketingOrderId = '" + mOrderId + "' ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();


            while (reader.Read())
            {
                MarketingTypes m = new MarketingTypes();
                m.Name = reader["Name"].ToString();
                m.Id = int.Parse(reader["Id"].ToString());
                m.LeadsTo = int.Parse(reader["LeadsTo"].ToString());
                markets.Add(m);
            }

            
            reader.Close();
            sqlCon.Close();

            return markets;
        }
    }
}