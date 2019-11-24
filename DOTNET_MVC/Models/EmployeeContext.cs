using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DOTNET_MVC.Models
{
    public class EmployeeContext
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public List<Employee> GetEmployeeDetails()
        {           
            List<Employee> emp_list = new List<Employee>();
           
            using(SqlConnection con=new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("GetEmployeeDetails", con);
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                da.Fill(dt);
                
                if (dt.Rows.Count != 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        Employee obj = new Employee();
                        obj.Id = Convert.ToInt32(dr[0]);
                        obj.Name =Convert.ToString(dr[1]);
                        obj.Salary = Convert.ToInt32(dr[2]);
                        obj.Email = Convert.ToString(dr[3]);
                        obj.Password = Convert.ToString(dr[4]);
                        obj.Age = Convert.ToInt32(dr[5]);
                        obj.Address = Convert.ToString(dr[6]);

                        emp_list.Add(obj);


                    }
                    
                }
                
            }
            return emp_list;
        }
        
        public int CreateEmployee(Employee obj)
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@Salary", obj.Salary);
                cmd.Parameters.AddWithValue("@email", obj.Email);
                cmd.Parameters.AddWithValue("@password", obj.Password);
                cmd.Parameters.AddWithValue("@confirm_pwd", obj.Confirm_pwd);
                cmd.Parameters.AddWithValue("@age", obj.Age);
                cmd.Parameters.AddWithValue("@address", obj.Address);
                int rows_affected = cmd.ExecuteNonQuery();
                if (rows_affected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

        }

        public Employee GetEmployeeDetailsById(int Id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
               

                SqlDataAdapter da = new SqlDataAdapter("usp_selectemployeebyid", con);
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ID", Id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Employee obj = new Employee();

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        
                        obj.Id = Convert.ToInt32(dr[0]);
                        obj.Name = Convert.ToString(dr[1]);
                        obj.Salary = Convert.ToInt32(dr[2]);
                        obj.Email = Convert.ToString(dr[3]);
                        obj.Password = Convert.ToString(dr[4]);
                        obj.Confirm_pwd = Convert.ToString(dr[5]);
                        obj.Age = Convert.ToInt32(dr[6]);
                        obj.Address = Convert.ToString(dr[7]);

                        
                    }


                }
                return obj;

            }


        }

        public int UpdateEmployee(Employee obj)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_editemployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@Salary", obj.Salary);
                cmd.Parameters.AddWithValue("@email", obj.Email);
                cmd.Parameters.AddWithValue("@password", obj.Password);
                cmd.Parameters.AddWithValue("@confirm_pwd", obj.Confirm_pwd);
                cmd.Parameters.AddWithValue("@age", obj.Age);
                cmd.Parameters.AddWithValue("@address", obj.Address);
                int rows_affected = cmd.ExecuteNonQuery();
                if (rows_affected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }


        }
        public int DeleteEmployee(int Id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_deleteemployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id",Id);
                
                int rows_affected = cmd.ExecuteNonQuery();
                if (rows_affected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }


        }
    }
}