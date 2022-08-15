using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class IniciadosSej
    {
        private string idJuzgado;
        private String nombreJuzgado;
        private int totalIniciados;

        public string IdJuzgado { get => idJuzgado; set => idJuzgado = value; }
        public string NombreJuzgado { get => nombreJuzgado; set => nombreJuzgado = value; }
        public int TotalIniciados { get => totalIniciados; set => totalIniciados = value; }
    }
}