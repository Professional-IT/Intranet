using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessmentWork.Models;

namespace AssessmentWork.Controllers
{

    public class ParamFromView{
        string ClientId;
        string StatusId;
        string StateId;
    }
    public class JsonReturnValue {
        int StatusCode;
        public JsonReturnValue(int code)
        {
            this.StatusCode = code;
        }
    }

    public class WorkOrderCount {
        int count;
        public WorkOrderCount(int c)
        {
            this.count = c;
        }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string myConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection con = new SqlConnection(myConn);

            SqlCommand command = new SqlCommand("Sp_GetClients", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            List<CleintModel> c_list = new List<CleintModel>();

            foreach (DataRow row in table.Rows)
            {
                c_list.Add(new CleintModel(int.Parse(row["ClientId"].ToString()), row["Name"].ToString(), row["Phone"].ToString()));
            }
            con.Close();


            SqlCommand command1 = new SqlCommand("Sp_GetStates",con);
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            DataTable table1 = new DataTable();
            table1.Load(command1.ExecuteReader());
            List<StateModel> st_list = new List<StateModel>();

            foreach (DataRow row in table1.Rows)
            {
                st_list.Add(new StateModel(int.Parse(row["StateId"].ToString()), row["Code"].ToString(), row["Name"].ToString()));
            }
            con.Close();

            SqlCommand command2 = new SqlCommand("Sp_GetStatus", con);
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            DataTable table2 = new DataTable();
            table2.Load(command2.ExecuteReader());
            List<StatusModel> ss_list = new List<StatusModel>();

            foreach (DataRow row in table2.Rows)
            {
                ss_list.Add(new StatusModel(int.Parse(row["StatusId"].ToString()), row["Name"].ToString()));
            }



            ViewBag.ss_list = ss_list;
            ViewBag.st_list = st_list;



            ViewBag.c_list = c_list;

            con.Close();


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult AddData2WorkOrder(string clientName, string statusName, string stateName)
        {
            string myConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection con = new SqlConnection(myConn);
            con.Open();
            string sql1 = "Select ClientId From Clients Where Name=\'" + clientName + "\'";
            SqlCommand command1 = new SqlCommand(sql1, con);
            SqlDataReader dataReader1 = command1.ExecuteReader();
            dataReader1.Read();

            int ClientId = int.Parse(dataReader1.GetValue(0).ToString());
            dataReader1.Close();
            command1.Dispose();
            con.Close();

            string sql2 = "Select StatusId From Status Where Name=\'" + statusName + "\'";
            con.Open();
            SqlCommand command2 = new SqlCommand(sql2, con);
            SqlDataReader dataReader2 = command2.ExecuteReader();
            dataReader2.Read();

            int StatusId = int.Parse(dataReader2.GetValue(0).ToString());

            dataReader2.Close();
            command2.Dispose();
            con.Close();

            string sql3 = "Select StateId From States Where Code=\'" + stateName + "\'";
            con.Open();
            SqlCommand command3 = new SqlCommand(sql3, con);
            SqlDataReader dataReader3 = command3.ExecuteReader();
            dataReader3.Read();

            int StateId = int.Parse(dataReader3.GetValue(0).ToString());

            dataReader3.Close();
            command3.Dispose();
            con.Close();


            
            SqlConnection connection = new SqlConnection(myConn);
            connection.Open();
            SqlCommand insert_command = new SqlCommand("Sp_InsertWorkOrder", connection);
            insert_command.CommandType = System.Data.CommandType.StoredProcedure;
            insert_command.Parameters.AddWithValue("@WorkOrderNumber", new Random().Next());
            insert_command.Parameters.AddWithValue("@ClientId", ClientId);
            insert_command.Parameters.AddWithValue("@StateId", StateId);
            insert_command.Parameters.AddWithValue("@StatusId", StatusId);
            insert_command.Parameters.AddWithValue("@ETA", DateTime.Now);

           int n =  insert_command.ExecuteNonQuery();
           
            insert_command.Dispose();
            connection.Close();

            return Json(new JsonReturnValue(200));
        }

     
        public int GetCountWorkOrders()
        {
            int count = 0;
            string myConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection con = new SqlConnection(myConn);

            SqlCommand command = new SqlCommand("Sp_GetWorkOrderCount", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            List<CleintModel> c_list = new List<CleintModel>();

            if (table.Rows.Count == 1)
            {
               count =  int.Parse(table.Rows[0]["Count"].ToString());
            }    
         
            con.Close();

            return count;

        }
    }
}