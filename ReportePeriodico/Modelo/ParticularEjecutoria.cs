using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class ParticularEjecutoria
    {
        private int usoIlicito;
        private int traficoInfluencia;
        private int cohecho;
        private int peculado;
        private int total;

        public int UsoIlicito { get => usoIlicito; set => usoIlicito = value; }
        public int TraficoInfluencia { get => traficoInfluencia; set => traficoInfluencia = value; }
        public int Cohecho { get => cohecho; set => cohecho = value; }
        public int Peculado { get => peculado; set => peculado = value; }
        public int Total { get => total; set => total = value; }
    }
}