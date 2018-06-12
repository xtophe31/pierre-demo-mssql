using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using Dapper;

using DemoMsSql.Model;

namespace DemoMsSql.Dal
{
    static class Factures
    {
        private const string ConnexionString =
            "Data Source=ServerName;Initial Catalog = DatabaseName; User ID = UserName; Password=Password";

        public static void Delete(int idFacture)
        {

            string queryLigne = @"
                DELETE FROM FacturesLignes
                WHERE IdFacture=@IdFacture";

            string queryFacture = @"
                DELETE FROM Factures
                WHERE IdFacture=@IdFacture";

            using (IDbConnection connection = new SqlConnection(ConnexionString))
            {

                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    connection.Execute(queryLigne, new{IdFacture = idFacture}, transaction:tx);
                    connection.Execute(queryFacture, new { IdFacture = idFacture }, transaction: tx);

                    tx.Commit();
                }
            }
        }
    }
}
