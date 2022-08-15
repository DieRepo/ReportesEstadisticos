using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class DatosAdicionalesSej
    {

        private String idJuzgado;
        private String nombreJuzgado;
        private String cveDato;
        private String nombreDatoAdicional;
        private int totalDatoAdicional;

        public string IdJuzgado { get => idJuzgado; set => idJuzgado = value; }
        public string NombreJuzgado { get => nombreJuzgado; set => nombreJuzgado = value; }
        public string CveDato { get => cveDato; set => cveDato = value; }
        public string NombreDatoAdicional { get => nombreDatoAdicional; set => nombreDatoAdicional = value; }
        public int TotalDatoAdicional { get => totalDatoAdicional; set => totalDatoAdicional = value; }
    }
}