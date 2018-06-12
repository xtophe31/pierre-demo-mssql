using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using DemoMsSql.Report;

using DemoMsSql.Model;
using DemoMsSql.Dal;
using DemoMsSql.Access;

namespace DemoMsSql
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PdfBuilder.TestConvertHtmlToPdf();

            List<Annuaire_Email> liste = Annuaire_Emails.ReadAll();

            if (!AnnuaireEmails.TableExists())
                AnnuaireEmails.TableCreation();

            foreach (var item in liste)
            {
                var newItem = new AnnuaireEmail
                {
                    Email = item.Email,
                    DateDernierEnvoi = item.DateDernierEnvoi,
                    CptErreur = item.CptErreur
                };

                AnnuaireEmails.Create(newItem);

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
