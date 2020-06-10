using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Customer_Details.Models;
using System.Configuration;

namespace Customer_Details.Controllers
{
    public class AddCustomerController : Controller
    {
        // GET: AddCustomer
        public ActionResult Index()
        {
            return View();
        }
       
        
        public ActionResult Create(Customer cd)
        {
            string maincon = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(maincon);
            string query = "Insert Into CUSTOMER (customer_id,first_name,last_name,gender,date_of_birth,nri_flag,tobacco_user_flag,Address_Line1,Address_Line2,State,City,email_id,Nominee_Name) " +
                "values (@customer_id,@first_name,@last_name,@gender,@date_of_birth,@nri_flag,@tobacco_user_flag,@Address_Line1,@Address_Line2,@State,@City,@email_id,@Nominee_Name)";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@customer_id", cd.Customer_id);
           
            if (String.IsNullOrEmpty(cd.first_name))
                //{
                //    com.Parameters.AddWithValue("@first_name", DBNull.Value);
                //}
                //else
                com.Parameters.AddWithValue("@first_name", cd.first_name);


            com.Parameters.AddWithValue("@last_name", cd.last_name);
            com.Parameters.AddWithValue("@gender", cd.gender);
            com.Parameters.AddWithValue("@date_of_birth", cd.date_of_birth);
            com.Parameters.AddWithValue("@nri_flag", cd.nri_flag);
            com.Parameters.AddWithValue("@tobacco_user_flag", cd.tobbaco_user_flag);
            com.Parameters.AddWithValue("@Address_Line1", cd.Address_Line1);
            com.Parameters.AddWithValue("@Address_Line2", cd.Address_Line2);
            com.Parameters.AddWithValue("@State", cd.State);
            com.Parameters.AddWithValue("@City", cd.City);
            com.Parameters.AddWithValue("@email_id", cd.Email_Id);
            com.Parameters.AddWithValue("@Nominee_name", cd.Nominee_name);
            com.ExecuteNonQuery();
            con.Close();
            ViewData["Message"] = "Customer details added Successfully";
            return View();
        }
    }
}