using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMsSql.Model
{
    class Facture
    {
        private int _idFacture;

        public int IdFacture
        {
            get { return _idFacture; }
            set { _idFacture = value; }
        }

        private int _idClient;

        public int IdClient
        {
            get { return _idClient; }
            set { _idClient = value; }
        }
    }
}
