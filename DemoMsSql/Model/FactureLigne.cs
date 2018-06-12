using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMsSql.Model
{
    public class FactureLigne
    {
        private int _idFactureLigne;

        public int IdFactureLigne
        {
            get { return _idFactureLigne; }
            set { _idFactureLigne = value; }
        }

        private int _idFacture;

        public int IdFacture
        {
            get { return _idFacture; }
            set { _idFacture = value; }
        }

        private string _produit;

        public string Produit
        {
            get { return _produit; }
            set { _produit = value; }
        }

        private int _quantite;

        public int Quantite
        {
            get { return _quantite; }
            set { _quantite = value; }
        }
    }
}
