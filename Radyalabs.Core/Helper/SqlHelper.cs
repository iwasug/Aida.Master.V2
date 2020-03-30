using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Helper
{
    public class SqlHelper
    {
        public static Dictionary<string, object> ExecuteNonQuery(string connString, string commandText, List<SqlParameter> listParamIn, List<SqlParameter> listParamOut = null)
        {
            SqlConnection conn = null;
            //SqlDataReader dr = null;
            //DataTable dataTable = null;

            Dictionary<string, object> result = null;

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand command = new SqlCommand(commandText, conn);

                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;

                if (listParamIn != null)
                {
                    foreach (var item in listParamIn)
                    {
                        command.Parameters.Add(item);
                    }
                }

                if (listParamOut != null)
                {
                    foreach (var item in listParamOut)
                    {
                        command.Parameters.Add(item).Direction = ParameterDirection.Output;
                    }
                }

                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                //command.ExecuteNonQuery();
                conn.Close();

                result = new Dictionary<string, object>();

                if (listParamOut != null)
                {
                    foreach (var item in listParamOut)
                    {
                        result[item.ParameterName] = command.Parameters[item.ParameterName].Value;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return result;
        }

        public static DataTable ExecuteQuery(string connString, string query, Dictionary<string, object> dcParams)
        {
            SqlConnection conn = null;
            DataTable dataTable = null;

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandTimeout = 0;
                if (dcParams != null)
                {
                    foreach (var item in dcParams)
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }

                dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dataTable;
        }

        public static int ExecuteQueryManipulation(string connString, string query, Dictionary<string, object> dcParams)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandTimeout = 0;
                if (dcParams != null)
                {
                    foreach (var item in dcParams)
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }

                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static Dictionary<int, dynamic> ExecuteProcedureWithReturnRecords(string connString, string commandText, List<SqlParameter> listParamIn)
        {
            SqlConnection conn = null;

            int status = 0;

            Dictionary<int, dynamic> result = null;
            DataTable dataTable = new DataTable();

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand command = new SqlCommand(commandText, conn);

                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;

                if (listParamIn != null)
                {
                    foreach (var item in listParamIn)
                    {
                        command.Parameters.Add(item);
                    }
                }

                dataTable.Load(command.ExecuteReader());

                conn.Close();

                status = 1;
            }
            catch (Exception ex)
            {
                dataTable = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            result = new Dictionary<int, dynamic>()
            {
                { status, dataTable }
            };

            return result;
        }

        public static DataTableCollection ExecuteProcedureWithReturnMultipleTable(string connString, string commandText, List<SqlParameter> listParamIn)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                command = new SqlCommand(commandText, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;

                if (listParamIn != null)
                {
                    foreach (var item in listParamIn)
                    {
                        command.Parameters.Add(item);
                    }
                }

                da = new SqlDataAdapter(command);
                da.Fill(ds);

                return ds.Tables;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if(command != null && command.Parameters != null)
                {
                    command.Parameters.Clear();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            return null;
        }
    }
}
