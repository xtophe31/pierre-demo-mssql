using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DemoMsSql.Model
{
    public class Personne
    {
        private int _idPersonne;

        public int IdPersonne
        {
            get { return _idPersonne; }
            set { _idPersonne = value; }
        }

        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _prenom;

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

#region constructeurs utilisant DRY

        public Personne()
        {
            // on ne fait rien
        }

        public Personne(string nom, string prenom)
            : this()
        {
            this.Nom = nom;
            this.Prenom = prenom;
        }

        public Personne(int idPersonne, string nom, string prenom)
            : this (nom,prenom)
        {
            this.IdPersonne = idPersonne;
        }

#endregion
    }
}
