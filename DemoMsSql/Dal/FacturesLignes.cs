using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

using System.Data.SqlClient;

using Dapper;

using DemoMsSql.Model;


namespace DemoMsSql.Dal
{
    public static class FacturesLignes
    {
        private const string ConnexionString =
            "Data Source=ServerName;Initial Catalog = DatabaseName; User ID = UserName; Password=Password";

        public static void Create(FactureLigne ligne)
        {
            string query = @"
                INSERT INTO FacturesLignes (IdFacture, Produit, Quantite)
                VALUES (@IdFacture, @Produit, @Quantite);
                SELECT CAST(SCOPE_IDENTITY() as int)
                ";

            using (IDbConnection connection = new SqlConnection(ConnexionString))
            {

                connection.Open();

                ligne.IdFactureLigne = connection.Query<int>(query, new {IdFacture = ligne.IdFacture, Produit = ligne.Produit, Quantite = ligne.Quantite }).Single();
            }

        }


        public static FactureLigne Read(int idFactureLigne)
        {
            string query = @"
                SELECT *
                FROM FacturesLignes
                WHERE IdFactureLigne=@IdFactureLigne";

            using (IDbConnection connection = new SqlConnection(ConnexionString))
            {

                connection.Open();

                return connection.QuerySingle<FactureLigne>(query, new { IdFactureLigne = idFactureLigne });
            }
        }

        public static void Update(FactureLigne ligne)
        {
            string query = @"
                UPDATE FacturesLignes
                SET IdFacture=@IdFacture, Produit=@Produit, Quantite=@Quantite";

            using (IDbConnection connection = new SqlConnection(ConnexionString))
            {

                connection.Open();

                connection.Execute(query, new { IdFacture = ligne.IdFacture, Produit = ligne.Produit, Quantite = ligne.Quantite });
            }

        }

        public static void Delete(int idFactureLigne)
        {
            string query = @"
                DELETE FROM FacturesLignes
                WHERE IdFactureLigne=@IdFactureLigne";

            using (IDbConnection connection = new SqlConnection(ConnexionString))
            {

                connection.Open();

                connection.Execute(query, new { IdFactureLigne = idFactureLigne });
            }
        }

        public static List<FactureLigne> ReadAll(int idFacture)
        {
            string query = @"
                SELECT * 
                FROM FacturesLignes
                WHERE IdFactureLigne=@IdFactureLigne
                ";

            using (IDbConnection connection = new SqlConnection(ConnexionString))
            {

                connection.Open();

                return connection.Query<FactureLigne>(query, new {IdFacture = idFacture}).ToList();
            }
        }

    }
}
