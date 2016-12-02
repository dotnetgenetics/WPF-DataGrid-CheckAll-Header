using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WpfDatagridCheckAllHeader
{
    public class Connections
    {
        public static DataTable GetProduct()
        {
            DataSet ds = new DataSet();
            string query = "Select * from Products;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["products"].ConnectionString.ToString()))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //log or throw exception
            }

            return ds.Tables[0];
        }

        public static int UpdateProductDiscontinue(bool value, string product_name)
        {
            int result = 0;
            string query = String.Format("Update Products set discontinue = '{0}' where productname = '{1}' ;", value, product_name);

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["products"].ConnectionString.ToString()))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //log or throw exception
            }

            return result;
        }
    }
}
