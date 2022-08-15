using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class AtendidoCanceladoAnio
    {
        private string mes;
        private int anio2022;
        private int anio2021;
        private int anio2020;

        public string Mes { get => mes; set => mes = value; }
        public int Anio2022 { get => anio2022; set => anio2022 = value; }
        public int Anio2021 { get => anio2021; set => anio2021 = value; }
        public int Anio2020 { get => anio2020; set => anio2020 = value; }
    }
}