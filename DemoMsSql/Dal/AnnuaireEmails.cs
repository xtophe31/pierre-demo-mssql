using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using DemoMsSql.Model;

namespace DemoMsSql.Dal
{
    public static class AnnuaireEmails
    {
        #region opérations CRUD

        public static void Create(AnnuaireEmail item)
        {
            using (SqlConnection oConn = new SqlConnection())
            {
                oConn.ConnectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;

                oConn.Open();

                string query = @"
                    INSERT INTO AnnuaireEmails (Email, DateDernierEnvoi,CptErreur)
                    VALUES (@Email, @DateDernierEnvoi,@CptErreur)";

                SqlCommand cmd = new SqlCommand(query, oConn);

                cmd.Parameters.AddWithValue("Email", item.Email);
                cmd.Parameters.AddWithValue("DateDernierEnvoi", item.DateDernierEnvoi);
                cmd.Parameters.AddWithValue("CptErreur", item.CptErreur);

                cmd.ExecuteNonQuery();
            }
        }


        #endregion

        #region opérations maintenance

        public static bool TableExists()
        {
            string query = @"SELECT count(*) as IsExists FROM dbo.sysobjects where id = object_id('[dbo].[AnnuaireEmails]')";

            using (SqlConnection cnx = new SqlConnection())
            {
                cnx.ConnectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
                cnx.Open();

                using (SqlCommand cmd = new SqlCommand(query, cnx))
                {
                    bool checkTable;
                    try
                    {
                        checkTable = ((int)cmd.ExecuteScalar() == 1);
                        {
                            return checkTable;
                        }
                    }
                    catch
                    {
                        checkTable = false;
                        return checkTable;
                    }
                }

            }
            
        }

        public static void TableCreation()
        {
            string query = @"
                IF OBJECT_ID('AnnuaireEmails', 'U') IS NULL
                    BEGIN
                        CREATE TABLE AnnuaireEmails(
                            Email VARCHAR(150),
                            DateDernierEnvoi [datetime],
                            CptErreur [int])
                    END
            ";

            using (SqlConnection cnx = new SqlConnection())
            {
                cnx.ConnectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
                cnx.Open();

                using (SqlCommand cmd = new SqlCommand(query,cnx))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        #endregion
    }
}
