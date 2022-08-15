using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class IniciadosPenalTradicional
    {
        private string idJuzdago;
        private string nombreJuzgado;
        private int totalIniciados;

        public string IdJuzdago { get => idJuzdago; set => idJuzdago = value; }
        public string NombreJuzgado { get => nombreJuzgado; set => nombreJuzgado = value; }
        public int TotalIniciados { get => totalIniciados; set => totalIniciados = value; }
    }
}