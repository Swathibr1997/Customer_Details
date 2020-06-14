
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {

        [HttpGet]
        // GET: Customer
        public ActionResult Index()
        {
            string maincon = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(maincon);

            string sqlquery = "select * from STATE";
            SqlCommand com = new SqlCommand(sqlquery, con);
            SqlDataAdapter da = new SqlDataAdapter(com);

            con.Open();

            DataSet ds = new DataSet();
            da.Fill(ds);


            ViewBag.statename = ds.Tables[0];
            List<SelectListItem> getstatename = new List<SelectListItem>();


            foreach (System.Data.DataRow dr in ViewBag.statename.Rows)
            {
                getstatename.Add(new SelectListItem { Text = @dr["state_name"].ToString(), Value = @dr["state_id"].ToString() });

                ViewBag.state = getstatename;
            }





            string sqlquery1 = "select * from GENDER";
            SqlCommand com1 = new SqlCommand(sqlquery1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            ViewBag.gendername = ds1.Tables[0];
            List<SelectListItem> getgendername = new List<SelectListItem>();

            foreach (System.Data.DataRow dr in ViewBag.gendername.Rows)
            {
                getgendername.Add(new SelectListItem { Text = @dr["gender_"].ToString(), Value = @dr["gender_id"].ToString() });

                ViewBag.gender_ = getgendername;
            }


            con.Close();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formdata)
        {
            string NRI_DATA = formdata["nri_flag"].ToString();
            string Tobacco = formdata["tobbaco_user_flag"].ToString();

            Response.Write("Form = " + NRI_DATA + "  " + Tobacco);

            if (NRI_DATA == "true,false")
            {
                NRI_DATA = "1";
                Response.Write("NRI = " + NRI_DATA);

            }
            else if (NRI_DATA == "false")
            {
                NRI_DATA = "0";
            }

            if (Tobacco == "true,false")
            {
                Tobacco = "1";
                Response.Write("NRI = " + Tobacco);

            }
            else if (Tobacco == "false")
            {
                Tobacco = "0";
            }

            Response.Write("Form = " + NRI_DATA + "  " + Tobacco);


            string maincon = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(maincon);

            string sqlquery = "select * from STATE";
            SqlCommand com = new SqlCommand(sqlquery, con);
            SqlDataAdapter da = new SqlDataAdapter(com);

            con.Open();

            DataSet ds = new DataSet();
            da.Fill(ds);


            ViewBag.statename = ds.Tables[0];
            List<SelectListItem> getstatename = new List<SelectListItem>();


            foreach (System.Data.DataRow dr in ViewBag.statename.Rows)
            {
                getstatename.Add(new SelectListItem { Text = @dr["state_name"].ToString(), Value = @dr["state_id"].ToString() });

                ViewBag.state = getstatename;
            }

            string sqlquery1 = "select * from GENDER";
            SqlCommand com1 = new SqlCommand(sqlquery1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            ViewBag.gendername = ds1.Tables[0];
            List<SelectListItem> getgendername = new List<SelectListItem>();

            foreach (System.Data.DataRow dr in ViewBag.gendername.Rows)
            {
                getgendername.Add(new SelectListItem { Text = @dr["gender_"].ToString(), Value = @dr["gender_id"].ToString() });

                ViewBag.gender_ = getgendername;
            }

            SqlCommand command = new SqlCommand("spADDCUSTOMER", con);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.AddWithValue("@first_name", formdata["first_name"]);
            command.Parameters.AddWithValue("@last_name", formdata["last_name"]);
            command.Parameters.AddWithValue("@gender_id", Convert.ToInt32(formdata["gender_"]));
            command.Parameters.AddWithValue("@date_of_birth", Convert.ToDateTime(formdata["date_of_birth"]));
            command.Parameters.AddWithValue("@nri_flag", NRI_DATA);
            command.Parameters.AddWithValue("@tobacco_user_flag", Tobacco);
            command.Parameters.AddWithValue("@email_id", formdata["Email_id"]);

            command.Parameters.AddWithValue("@address_1", formdata["Address_Line1"]);
            command.Parameters.AddWithValue("@address_2", formdata["Address_Line1"]);
            command.Parameters.AddWithValue("@state_id", Convert.ToInt32(formdata["state"]));

            command.Parameters.AddWithValue("@phone_number", Convert.ToInt64(formdata["Phone_Number"]));
            command.Parameters.AddWithValue("@Nominee_name", formdata["Nominee_name"]);
            command.ExecuteNonQuery();

            TempData["Phone_Number"] = Convert.ToInt64(formdata["Phone_Number"]);

          


            con.Close();
            ViewData["Message"] = "Details saved Successfully";



            return RedirectToAction("Index", "Policy");
        }



    }
}

