﻿using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class DeaCustDAL
    {
        //create a static string method for Database connection 
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT method for Dealer and Customer
        public DataTable Select()
        {
            // SQL connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            // DataTable to hold value
            DataTable dt = new DataTable();


            try
            {
                // write sql Query to Insert Details of Dealer and Customer 
                string sql = "select * from tbl_dea_cust";

                // sql Command to pass the values of the query and excute 
                SqlCommand cmd = new SqlCommand(sql, conn);

                // creating sql command to excute query 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // open Database connection
                conn.Open();
                //Passing the value from Data Adapter to data table 
                adapter.Fill(dt);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;

        }
        #endregion
        #region INSERT method to Add details for Dealer and Customer 
        public bool Insert(DeaCustBLL dc)
        {
            // create sql connection 
            SqlConnection conn = new SqlConnection(myconnstrng);

            // Create a boolean value and set its default
            bool isSuccess = false;

            try
            {
                // sql query to insert details of Dealer or Customer   
                string sql = "insert into tbl_dea_cust(type, name, email, contact, address, added_date, added_by) values (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                // sql command to pass the values to query and excute 
                SqlCommand cmd = new SqlCommand(sql, conn);

                // passing the values using Parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                // Open db connection 
                conn.Open();

                // integer varaiable to check weather the query excuted or not 
                int rows = cmd.ExecuteNonQuery();
                // if query excuted successfully then the value of rows will be greater than 0 else it will be less than 0.

                if (rows >0)
                {
                    // query excuted successfully
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return isSuccess;

        }
        #endregion
        #region UPDATE method for Dealer and Customer Module
        public bool Update(DeaCustBLL dc)
        {
            // sql connection for db connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //boolean variable and set it to false 
            bool isSuccess = false;
            try
            {
                //write sql query to update the database
                string sql = "Update tbl_dea_cust set type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by where id=@id";

                // create sql command to pass value in sql
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing the values through parameters 
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                //open the database connection
                conn.Open();

                // int variable to check if the query is excuted fully or not 
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                   {
                    isSuccess = true;
                   }
                else
                {
                    isSuccess = false;
                }


            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region DELETE method for Dealer and customer
        public bool Delete(DeaCustBLL dc)
        {
            // SQL connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            // create a boolean variable and set its default value to false 
            bool isSuccess = false;
            try
            {
                // SQl Query to delete data from database
                string sql = "delete from tbl_dea_cust where id=@id";
                // sql command to pass value 
                SqlCommand cmd = new SqlCommand(sql, conn);
                // pass the value 
                cmd.Parameters.AddWithValue("@id", dc.id);
                // open DB connection
                conn.Open();
                // int varable to check delete is successful
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region SEARCh method for dealer and Customer Module
        public DataTable Search(string keywords)
        {
            // create sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            // create a data table to return the value
            DataTable dt = new DataTable();
            try
            {
                // write the Query to Search Dealer or Customer based on id,type, and name
                string sql = "select * from tbl_dea_cust where id like '%"+keywords+"%' or type like '%"+keywords+"%' or name like '%"+keywords+"%'";
                
                // Sql command to excute query
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                // Sql data adapter to hold the data from db temporarily 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                
                //open data base connection
                conn.Open();
                
                //pass the value of the adapter to the datatable 
                adapter.Fill(dt);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
        #region Method to search Dealer or Customer in transaction module
        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            // create an object for DeaCustBLL
            DeaCustBLL dc = new DeaCustBLL();
            // Create a Db Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            // create a data Table to hold the value Temporarily
            DataTable dt = new DataTable();
            try
            {
                //write a sql query to search dealer or customer based on keywords
                string sql = "select name, email, contact, address from tbl_dea_cust where id like '%" + keyword + "%' or name like '%" + keyword + "%'";

                // create sql Data Adapter to excute the query 
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                // transfer the data to datatable
                adapter.Fill(dt);
                
                // if we have values in dt we need to save it in dealerCustomer BLL
                if (dt.Rows.Count > 0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dc;
        }
        #endregion
        #region Method to Get ID of The Dealer or Customer Based on Name 
        public DeaCustBLL GetDeaCustIDFromName( string Name)
        {
            // first create an obj of Deacust BLL and return it 
            DeaCustBLL dc = new DeaCustBLL();
            // Sql Connection here 
            SqlConnection conn = new SqlConnection(myconnstrng);

            // Data table to hold data temporarily
            DataTable dt = new DataTable();

            try
            {
                // sql query to get ID based oon name 
                string sql = "select id from tbl_dea_cust where name='"+Name+"'";
                // sql Data Adapter to excute query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                // passing the value from adapter to data table 
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dc;
        }
        #endregion

    }

}
