using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.OleDb;

namespace DemoMsSql.Access
{
    public static class Annuaire_Emails
    {
        private static readonly string CnxString =
            ConfigurationManager.ConnectionStrings["access"].ConnectionString;

        public static List<Annuaire_Email> ReadAll()
        {
            var liste = new List<Annuaire_Email>();

            using (var cnx = new OleDbConnection())
            {
                string query = "SELECT * FROM [ANNUAIRE_EMAIL]";

                cnx.ConnectionString = CnxString;

                cnx.Open();

                var cmd = new OleDbCommand(query,cnx);

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Annuaire_Email
                        {
                            Email = reader.GetString(0),
                            DateDernierEnvoi = reader.GetDateTime(1),
                            CptErreur = reader.GetInt32(2)
                        };

                        liste.Add(item);
                    }
                }
            }

            return liste;
        }
    }

    public class Annuaire_Email
    {
        public string Email;
        public DateTime DateDernierEnvoi;
        public int CptErreur;
    }
}
