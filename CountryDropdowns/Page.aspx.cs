using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CountryDropdowns
{
    public partial class Page : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack==false)
            {
                GetCountries();
                GetStates();
                GetCity();
                FillData();
            }
            
        }
        private void GetCountries()
        {
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            string q1 = "select * from country";
            SqlDataAdapter command=new SqlDataAdapter(q1, conn);
            DataSet ds = new DataSet();
            command.Fill(ds, "country");
            DropDownList1.DataSource = ds;
            DropDownList1.DataTextField = "countryname";
            DropDownList1.DataValueField = "countryid";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0,"--Select country---");
        }
        private void GetStates()
        {
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            string q2 = "select s1.stateid,s1.statename from country c1 inner join state s1 on s1.countryid=c1.countryid where c1.countryname='"+DropDownList1.SelectedItem.ToString()+"'";
            SqlDataAdapter command = new SqlDataAdapter(q2, conn);
            DataSet ds = new DataSet();
            command.Fill(ds, "state");
            DropDownList2.DataSource = ds;
            DropDownList2.DataTextField = "statename";
            DropDownList2.DataValueField ="stateid";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "--Select state---");
        }
        private void GetCity()
        {
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            string q3 = "select c.cityid,c.cityname from state s1 inner join city c on s1.stateid=c.stateid where s1.statename='"+DropDownList2.SelectedItem.ToString()+"'";
            SqlDataAdapter command = new SqlDataAdapter(q3, conn);
            DataSet ds = new DataSet();
            command.Fill(ds, "city");
            DropDownList3.DataSource = ds;
            DropDownList3.DataTextField ="cityname";
            DropDownList3.DataValueField ="cityid";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "--Select city---");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
                GetStates();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
           GetCity();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillData();

        }

        protected void FillData()
        {
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            string q3 = "select e1.eno,e1.ename,e1.salary from emp e1 inner join city c1 on e1.cityid=c1.cityid where c1.cityname='" + DropDownList3.SelectedItem.ToString() + "'";
            SqlDataAdapter command = new SqlDataAdapter(q3, conn);
            DataSet ds = new DataSet();
            command.Fill(ds, "city");
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
}