 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data.SqlTypes;

using DemoMsSql.Model;

namespace DemoMsSql.Dal
{
    /// <summary>
    /// Operations CRUD sur la table Personnes
    /// </summary>
    static class Personnes
    {
        private const string ConnexionString =
            "Data Source=ServerName;Initial Catalog = DatabaseName; User ID = UserName; Password=Password";

        public static void Create(Personne personne)
        {
            using (SqlConnection oConn = new SqlConnection())
            {
                oConn.ConnectionString = ConnexionString;

                oConn.Open();

                string query = @"
                    INSERT INTO Personnes (Nom, Prenom)
                    VALUES '@Nom, @Prenom)";

                SqlCommand cmd = new SqlCommand(query, oConn);

                cmd.Parameters.Add(new SqlParameter("@Nom", personne.Nom));
                cmd.Parameters.Add(new SqlParameter("@Prenom", personne.Prenom));

                personne.IdPersonne = (int) cmd.ExecuteScalar();
            }
        }

        public static Personne Read(int idPersonne)
        {
            Personne personne;
            using (SqlConnection oConn = new SqlConnection())
            {
                oConn.ConnectionString = ConnexionString;

                oConn.Open();

                string query = @"
                    SELECT Nom, Prenom
                    FROM Personnes
                    WHERE IdPersonne = @IdPersonne";

                SqlCommand cmd = new SqlCommand(query, oConn);

                cmd.Parameters.Add(new SqlParameter("@IdPersonne", idPersonne));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        personne = ReaderToPersonne(reader);
                    else
                        personne = null;
                }
            }

            return personne;
        }

        public static void Update(Personne personne)
        {
            using (SqlConnection oConn = new SqlConnection())
            {
                oConn.ConnectionString = ConnexionString;

                oConn.Open();

                string query = @"
                    UPDATE Personnes
                    SET Nom=@Nom, Prenom=@Prenom
                    WHERE IdPersonne = @IdPersonne";

                SqlCommand cmd = new SqlCommand(query, oConn);

                cmd.Parameters.AddWithValue("@IdPersonne", personne.IdPersonne);
                cmd.Parameters.AddWithValue("@Nom", personne.Nom);
                cmd.Parameters.AddWithValue("@Prenom", personne.Prenom);

                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int idPersonne)
        {
            try
            {
                using (SqlConnection oConn = new SqlConnection())
                {
                    oConn.ConnectionString = ConnexionString;

                    oConn.Open();

                    string query = @"
                        DELETE FROM Personnes
                        WHERE IdPersonne = @IdPersonne";

                    SqlCommand cmd = new SqlCommand(query, oConn);

                    cmd.Parameters.AddWithValue("@IdPersonne", idPersonne);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // erreurs à traiter
                throw;
            }
        }

        public static List<Personne> ReadAll()
        {
            List<Personne> list = new List<Personne>();

            using (SqlConnection oConn = new SqlConnection())
            {
                oConn.ConnectionString = ConnexionString;

                oConn.Open();

                string query = @"
                    SELECT Nom, Prenom
                    FROM Personnes";

                SqlCommand cmd = new SqlCommand(query, oConn);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(ReaderToPersonne(reader));
                    }
                }

                return list;
            }
        }

        private static Personne ReaderToPersonne(SqlDataReader reader)
        {
            var personne = new Personne
            {
                IdPersonne = reader.GetInt32(0),
                Nom = reader.GetString(1),
                Prenom = reader.GetString(2)
            };

            return personne;
        }
    }
}
