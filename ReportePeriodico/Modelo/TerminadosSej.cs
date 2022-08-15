using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class TerminadosSej
    {
        private string idJuzgado;
        private String nombreJuzgado;
        private int totalTerminados;

        public string IdJuzgado { get => idJuzgado; set => idJuzgado = value; }
        public string NombreJuzgado { get => nombreJuzgado; set => nombreJuzgado = value; }
        public int TotalTerminados { get => totalTerminados; set => totalTerminados = value; }
    }
}