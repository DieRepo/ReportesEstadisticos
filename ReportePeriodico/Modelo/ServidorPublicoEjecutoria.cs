using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class ServidorPublicoEjecutoria
    {
        private int incumplimiento;
        private int coalicion;
        private int abusoAutoridad;
        private int usoIlicito;
        private int concusion;
        private int intimidacion;
        private int ejercicioAbusivo;
        private int traficoInfluencia;
        private int cohecho;
        private int peculado;
        private int enriquecimientoIlicito;
        private int delitos;
        private int total;

        public int Incumplimiento { get => incumplimiento; set => incumplimiento = value; }
        public int Coalicion { get => coalicion; set => coalicion = value; }
        public int AbusoAutoridad { get => abusoAutoridad; set => abusoAutoridad = value; }
        public int UsoIlicito { get => usoIlicito; set => usoIlicito = value; }
        public int Concusion { get => concusion; set => concusion = value; }
        public int Intimidacion { get => intimidacion; set => intimidacion = value; }
        public int EjercicioAbusivo { get => ejercicioAbusivo; set => ejercicioAbusivo = value; }
        public int TraficoInfluencia { get => traficoInfluencia; set => traficoInfluencia = value; }
        public int Cohecho { get => cohecho; set => cohecho = value; }
        public int Peculado { get => peculado; set => peculado = value; }
        public int EnriquecimientoIlicito { get => enriquecimientoIlicito; set => enriquecimientoIlicito = value; }
        public int Delitos { get => delitos; set => delitos = value; }
        public int Total { get => total; set => total = value; }
    }
}