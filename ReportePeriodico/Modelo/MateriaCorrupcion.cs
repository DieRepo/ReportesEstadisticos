using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportePeriodico.Modelo
{
    public class MateriaCorrupcion
    {
        private int noPersonasIniciados;
        private int noPersonasSentenciaDictada;
        private int noPersonasSentenciaEjecutoria;
        private int noSentenciasServidoresPublicos;
        private int noSentenciasParticulares;
        private int noSentenciasServidoresPublicosSE;
        private int noSentenciasParticularesSE;

        public int NoPersonasIniciados { get => noPersonasIniciados; set => noPersonasIniciados = value; }
        public int NoPersonasSentenciaDictada { get => noPersonasSentenciaDictada; set => noPersonasSentenciaDictada = value; }
        public int NoPersonasSentenciaEjecutoria { get => noPersonasSentenciaEjecutoria; set => noPersonasSentenciaEjecutoria = value; }
        public int NoSentenciasServidoresPublicos { get => noSentenciasServidoresPublicos; set => noSentenciasServidoresPublicos = value; }
        public int NoSentenciasParticulares { get => noSentenciasParticulares; set => noSentenciasParticulares = value; }
        public int NoSentenciasServidoresPublicosSE { get => noSentenciasServidoresPublicosSE; set => noSentenciasServidoresPublicosSE = value; }
        public int NoSentenciasParticularesSE { get => noSentenciasParticularesSE; set => noSentenciasParticularesSE = value; }
    }
}