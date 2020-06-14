using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Customer_Details.Models;

namespace Customer_Details.Controllers
{
    public class PolicyController : Controller
    {
        private DataSet myds;
        private int rc;
        // GET: Policy
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(PolicyDetails cusObj)
        {
            string con = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("select cover_amount,payout_option,policy_term,payment_term,plan_type,add_on from POLICY_DETAILS where policy_id=(Select max (policy_id) From POLICY_DETAILS)", sqlCon);
            SqlDataReader sdr = sqlCmd.ExecuteReader();
            if (sdr.Read())
            {
                cusObj.cover_amount = Convert.ToInt32(sdr["cover_amount"]);
                cusObj.payout_option = sdr["payout_option"].ToString();
                cusObj.policy_term = Convert.ToInt32(sdr["policy_term"]);
                cusObj.payment_term = Convert.ToInt32(sdr["payment_term"]);
                cusObj.plan_type = sdr["plan_type"].ToString();
                cusObj.add_on = sdr["add_on"].ToString();


            }
            else
            {
                ViewData["Message"] = "User Login Failed";
            }
            sqlCon.Close();




            return View(cusObj);
        }


        [HttpPost]
        public ActionResult Index(PolicyDetails cusObj)
        {
            Response.Write("Phone NUmber = " + TempData["Phone_Number"]);
            long Phone = (long)Convert.ToDouble(TempData["Phone_Number"]);
            string con = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(con);
            SqlCommand command = new SqlCommand("spGETPKCUSTOMER", sqlCon);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@phone_number", Phone);

            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myds = new DataSet();
            myadapter.Fill(myds, "CUSTOMER");
            rc = myds.Tables["CUSTOMER"].Rows.Count;
            if (rc > 0)
            {
                ViewBag.customer_id = myds.Tables["CUSTOMER"].Rows[0][0].ToString();

            }

            Response.Write("customer id + " + ViewBag.customer_id);


            SqlCommand sqlCmd = new SqlCommand("INSERT into POLICY_DETAILS (customer_id,cover_amount,payout_option,policy_term,payment_term,plan_type,add_on,policy_start_date,policy_active_flag,policy_applied) values (@customer_id,@cover_amount,@payout_option,@policy_term,@payment_term,@plan_type,@add_on,@policy_start_date,@policy_active_flag,@policy_applied)", sqlCon);
            sqlCon.Open();


            sqlCmd.Parameters.AddWithValue("@customer_id", ViewBag.customer_id);
            sqlCmd.Parameters.AddWithValue("@cover_amount", cusObj.cover_amount);
            sqlCmd.Parameters.AddWithValue("@payout_option", cusObj.payout_option);
            sqlCmd.Parameters.AddWithValue("@policy_term", cusObj.policy_term);
            sqlCmd.Parameters.AddWithValue("@payment_term", cusObj.payment_term);
            sqlCmd.Parameters.AddWithValue("@plan_type", cusObj.plan_type);
            sqlCmd.Parameters.AddWithValue("@add_on", String.IsNullOrWhiteSpace(cusObj.add_on) ? (object)DBNull.Value : (object)cusObj.add_on);
            sqlCmd.Parameters.AddWithValue("@policy_start_date", DateTime.Now);
            //sqlCmd.Parameters.AddWithValue("@policy_end_date", );
            sqlCmd.Parameters.AddWithValue("@policy_active_flag", 0);
            sqlCmd.Parameters.AddWithValue("@policy_applied", DateTime.Now);


            SqlDataReader sdr = sqlCmd.ExecuteReader();
            sqlCon.Close();
            return RedirectToRoute(new
            {
                controller = "Policy",
                action = "Details"

            });

            
        }
    }
}