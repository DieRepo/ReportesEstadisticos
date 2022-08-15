using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class DatosAdicionalesPenal
    {
        private string idJuzgado;
        private string nombreJuzgado;
        private string cveDatoAdicional;
        private string nombreDatoAdicional;
        private int total;

        public string IdJuzgado { get => idJuzgado; set => idJuzgado = value; }
        public string NombreJuzgado { get => nombreJuzgado; set => nombreJuzgado = value; }
        public string CveDatoAdicional { get => cveDatoAdicional; set => cveDatoAdicional = value; }
        public string NombreDatoAdicional { get => nombreDatoAdicional; set => nombreDatoAdicional = value; }
        public int Total { get => total; set => total = value; }
    }

}