using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class ModeloCorreccion
    {
        private string idJuzgado;
        private string nombreJuzgado;
        private int anio;
        private DateTime fechaSolicita;
        private DateTime fechaCorrige;
        private int totalAltas;
        private int totalModificaciones;
        private int totalEliminaciones;
        private int totalProceso;
        private int totalCancelados;
        private int totalAtendido;
        private int totalRegistros;

        public string IdJuzgado { get => idJuzgado; set => idJuzgado = value; }
        public string NombreJuzgado { get => nombreJuzgado; set => nombreJuzgado = value; }
        public int Anio { get => anio; set => anio = value; }
        public DateTime FechaSolicita { get => fechaSolicita; set => fechaSolicita = value; }
        public DateTime FechaCorrige { get => fechaCorrige; set => fechaCorrige = value; }
        public int TotalAltas { get => totalAltas; set => totalAltas = value; }
        public int TotalModificaciones { get => totalModificaciones; set => totalModificaciones = value; }
        public int TotalEliminaciones { get => totalEliminaciones; set => totalEliminaciones = value; }
        public int TotalProceso { get => totalProceso; set => totalProceso = value; }
        public int TotalCancelados { get => totalCancelados; set => totalCancelados = value; }
        public int TotalAtendido { get => totalAtendido; set => totalAtendido = value; }
        public int TotalRegistros { get => totalRegistros; set => totalRegistros = value; }
    }
}