using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql.Data.MySqlClient;
using ReportePeriodico.Modelo;
using System.Data;
using Xceed.Words.NET;
using System.IO;

namespace ReportesEstadisticos
{
    public partial class FormatoAnticorrupcion : System.Web.UI.Page
    {

        string str;
        MySqlCommand com;
        MySqlDataReader r;
        MateriaCorrupcion matCorrupcion;
        ParticularEjecutoria parEjecutoria;
        ServidorPublicoEjecutoria spEjecutoria;
        Dictionary<String, int> mapDelitosSP;
        Dictionary<String, int> mapDelitosPar;
        Dictionary<String, int> mapDelitosSililares;
        Boolean banderaServidor = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] is null)
                {
                    Response.Redirect("~/Login.aspx");
                }

            }

        }


        protected void archivo_Click(object sender, EventArgs e)
        {

            obtenerInformacionFormato();
            ModificarArchivoDocX();
            Console.WriteLine("En proceso: " + parEjecutoria.Total);
        }

        public void inicializarMaps() {
            mapDelitosSP = new Dictionary<string, int>();
            mapDelitosSP.Add("Incumplimiento, ejercicio indebido y abandono de funciones públicas.", 1);
            mapDelitosSP.Add("coalición", 2);
            mapDelitosSP.Add("abuso de autoridad", 3);
            mapDelitosSP.Add("uso ilicito de atribuciones y facultades", 4);
            mapDelitosSP.Add("concusión", 5);
            mapDelitosSP.Add("intimidación", 6);
            mapDelitosSP.Add("ejercicio abusivo", 7);
            mapDelitosSP.Add("trafico de influencia", 8);
            mapDelitosSP.Add("cohecho", 9);
            mapDelitosSP.Add("peculado", 10);
            mapDelitosSP.Add("enriquecimiento", 11);
            mapDelitosSP.Add("cometidos por servidores", 12);

            mapDelitosPar = new Dictionary<string, int>();
            mapDelitosPar.Add("uso ilicito de atribuciones y facultades", 1);
            mapDelitosPar.Add("trafico de influencia", 2);
            mapDelitosPar.Add("cohecho", 3);
            mapDelitosPar.Add("peculado", 4);


            mapDelitosSililares = new Dictionary<string, int>();
            mapDelitosSililares.Add("cohecho", 1);
            mapDelitosSililares.Add("peculado", 2);
        }




        public void obtenerInformacionFormato()
        {
            matCorrupcion = new MateriaCorrupcion();
            try
            {
                inicializarMaps();
                ConsultarIniciadosMateriaCorrupcion();
                ConsultarIniciadosMateriaCorrupcionParteDos();
                ConsultarSentenciadosMateriaCorrupcion();
                ConsultarDelitosServidoresPublicosParticularesEjecutoria();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }




        protected void ConsultarIniciadosMateriaCorrupcion()
        {
            MySqlConnection con = null;
            try
            {
                con = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["sigejupe"]);
                con.Open();
                str = "select count(distinct ic.idImputadoCarpeta) as total" +
                    "   from tblcarpetasjudiciales cj" +
                    "   inner" +
                    "   join tblimpofedelcarpetas ifc on ifc.idCarpetaJudicial = cj.idCarpetaJudicial" +
                    "   inner" +
                    "   join tbldelitoscarpetas dc on dc.idDelitoCarpeta = ifc.idDelitoCarpeta" +
                    "   inner" +
                    "   join tblimputadoscarpetas ic on ic.idImputadoCarpeta = ifc.idImputadoCarpeta" +
                    "   where" +
                    "   cj.fechaRadicacion between @fechaIni and @fechaFin  " +
                    "   and cj.activo = 'S' and ifc.activo = 'S' and dc.activo = 'S' and ic.activo = 'S' " +
                    "   and dc.cveDelito in (11, 12, 13, 134, 345, 15, 346, 347, 14, 137, 10, 16, 17, 26) " +
                    "   and cj.cveTipoCarpeta in (2, 3, 4); ";
                com = new MySqlCommand(str, con);
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@fechaIni", fechaInicio.Text );
                com.Parameters.AddWithValue("@fechaFin", fechaFin.Text + " 23:59:59");
                r = com.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        matCorrupcion.NoPersonasIniciados = r.GetInt32("total");
                    }
                }
                else
                {
                    matCorrupcion.NoPersonasIniciados = 0;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                String errorText = ex.ToString();
                Console.WriteLine("Error al consultar información: " + errorText);
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                    con.Close();
                }
            }
        }





        protected void ConsultarIniciadosMateriaCorrupcionParteDos()
        {
            MySqlConnection con = null;
            try
            {
                con = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["sigejupe"]);
                con.Open();
                str = "select " +
                    " case " +
                    " when i.cveOcupacion = 1 then 'SP'" +
                    " else 'P'end tipo," +
                    " d.desDelito," +
                    " count(distinct a.idActuacion) 'Sentencias', count(distinct i.idImputadoCarpeta) 'Sentenciados'" +
                    " from htsj_sigejupe.tblcarpetasjudiciales c" +
                    " inner" +
                    " join htsj_sigejupe.tblimpofedelcarpetas r on r.idCarpetaJudicial = c.idCarpetaJudicial" +
                    " inner" +
                    " join htsj_sigejupe.tblimputadoscarpetas i on i.idImputadoCarpeta = r.idImputadoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tblofendidoscarpetas o on o.idOfendidoCarpeta = r.idOfendidoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tbldelitoscarpetas dc on dc.idDelitoCarpeta = r.idDelitoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tbldelitos d on d.cveDelito = dc.cveDelito" +
                    " inner" +
                    " join htsj_sigejupe.tblimputadossentencias ise on ise.idImpOfeDelCarpeta = r.idImpOfeDelCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tblactuaciones a on a.idActuacion = ise.idActuacion" +
                    " where" +
                    " a.fechaSentencia between @fechaIni and @fechaFin" +
                    " and a.cveTipoActuacion = 3 and c.cveTipoCarpeta in (2, 3, 4) and c.cvejuzgado <> 11353" +
                    " and dc.cveDelito in (11, 12, 13, 134, 345, 15, 346, 347, 14, 137, 10, 16, 17, 26)" +
                    " and c.activo = 'S' and r.activo = 'S' and i.activo = 'S' and o.activo = 'S' and dc.activo = 'S' and ise.activo = 'S' and a.activo = 'S'" +
                    " group by tipo, d.cvedelito; ";
                com = new MySqlCommand(str, con);
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@fechaIni", fechaInicio.Text);
                com.Parameters.AddWithValue("@fechaFin", fechaFin.Text + " 23:59:59");
                r = com.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        string delito = r.GetString("desDelito").ToLower();
                        int sentenciados = r.GetInt32("Sentenciados");
                        int sentencias = r.GetInt32("Sentencias");
                        matCorrupcion.NoPersonasSentenciaEjecutoria = matCorrupcion.NoPersonasSentenciaEjecutoria + sentenciados;

                        if (delito.Contains("cometido por particulares"))
                        {
                            matCorrupcion.NoSentenciasParticulares = matCorrupcion.NoSentenciasParticulares + sentencias;

                        }
                        else if (delito.Contains("cometido por sevidores publicos"))
                        {
                            matCorrupcion.NoSentenciasServidoresPublicos = matCorrupcion.NoSentenciasServidoresPublicos + sentenciados;
                        }
                        else {
                            int valueDSP, valueDP;
                            Boolean banderaDelitoSP = mapDelitosSP.TryGetValue(delito, out valueDSP);
                            Boolean banderaDelitoPar = mapDelitosPar.TryGetValue(delito, out valueDP);
                            string tipo = r.GetString("tipo");
                            if (banderaDelitoSP)
                            {

                                if (tipo.Equals("P"))
                                {
                                    matCorrupcion.NoSentenciasParticulares = matCorrupcion.NoSentenciasParticulares + sentenciados;
                                }
                                else if (tipo.Equals("SP"))
                                {
                                    matCorrupcion.NoSentenciasServidoresPublicos = matCorrupcion.NoSentenciasServidoresPublicos + sentenciados;
                                }

                            }
                            else if (banderaDelitoPar)
                            {
                                if (tipo.Equals("P"))
                                {
                                    matCorrupcion.NoSentenciasParticulares = matCorrupcion.NoSentenciasParticulares + sentenciados;
                                }
                                else if (tipo.Equals("SP"))
                                {
                                    matCorrupcion.NoSentenciasServidoresPublicos = matCorrupcion.NoSentenciasServidoresPublicos + sentenciados;
                                }

                            }
                            else {
                                Console.WriteLine("No ingresa en la condicion");
                            }
                        
                        }
                    }
                }
                else
                {
                    matCorrupcion.NoPersonasIniciados = 0;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                String errorText = ex.ToString();
                Console.WriteLine("Error al consultar información: " + errorText);
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                    con.Close();
                }
            }
        }




        protected void ConsultarSentenciadosMateriaCorrupcion()
        {
            MySqlConnection con = null;
            int totalSentenciados = 0;
            try
            {
                con = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["sigejupe"]);
                con.Open();
                str = "select " +
                    "   count(distinct a.idActuacion) 'Sentencias', count(distinct i.idImputadoCarpeta) 'Sentenciados'" +
                    "   from htsj_sigejupe.tblcarpetasjudiciales c" +
                    "   inner" +
                    "   join htsj_sigejupe.tblimpofedelcarpetas r on r.idCarpetaJudicial = c.idCarpetaJudicial" +
                    "   inner" +
                    "   join htsj_sigejupe.tblimputadoscarpetas i on i.idImputadoCarpeta = r.idImputadoCarpeta" +
                    "   inner" +
                    "   join htsj_sigejupe.tblofendidoscarpetas o on o.idOfendidoCarpeta = r.idOfendidoCarpeta" +
                    "   inner" +
                    "   join htsj_sigejupe.tbldelitoscarpetas dc on dc.idDelitoCarpeta = r.idDelitoCarpeta" +
                    "   inner" +
                    "   join htsj_sigejupe.tbldelitos d on d.cveDelito = dc.cveDelito" +
                    "   inner" +
                    "   join htsj_sigejupe.tblimputadossentencias ise on ise.idImpOfeDelCarpeta = r.idImpOfeDelCarpeta" +
                    "   inner" +
                    "   join htsj_sigejupe.tblactuaciones a on a.idActuacion = ise.idActuacion" +
                    "   where" +
                    "   a.fechaSentencia between @fechaIni and @fechaFin" +
                    "   and a.cveTipoActuacion = 3 and c.cveTipoCarpeta in (2, 3, 4) and c.cvejuzgado <> 11353" +
                    "   and dc.cveDelito in (11, 12, 13, 134, 345, 15, 346, 347, 14, 137, 10, 16, 17, 26)" +
                    "   and c.activo = 'S' and r.activo = 'S' and i.activo = 'S' and o.activo = 'S' and dc.activo = 'S' and ise.activo = 'S' and a.activo = 'S'; ";
                com = new MySqlCommand(str, con);
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@fechaIni", fechaInicio.Text);
                com.Parameters.AddWithValue("@fechaFin", fechaFin.Text + " 23:59:59");
                r = com.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        matCorrupcion.NoPersonasSentenciaDictada = totalSentenciados + r.GetInt32("Sentenciados");
                    }
                }
                else
                {
                    matCorrupcion.NoPersonasIniciados = 0;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                String errorText = ex.ToString();
                Console.WriteLine("Error al consultar información: " + errorText);
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                    con.Close();
                }
            }
        }




        protected void ConsultarDelitosServidoresPublicosParticularesEjecutoria()
        {
            MySqlConnection con = null;
            spEjecutoria = new ServidorPublicoEjecutoria();
            parEjecutoria = new ParticularEjecutoria();
            try
            {
                con = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["sigejupe"]);
                con.Open();
                str = "select case when i.cveOcupacion = 1 then 'SP' else 'P'end tipo, d.desDelito, " +
                    " count(distinct a.idActuacion) 'Sentencias', count(distinct i.idImputadoCarpeta) 'Sentenciados'" +
                    " from htsj_sigejupe.tblcarpetasjudiciales c" +
                    " inner" +
                    " join htsj_sigejupe.tblimpofedelcarpetas r on r.idCarpetaJudicial = c.idCarpetaJudicial" +
                    " inner" +
                    " join htsj_sigejupe.tblimputadoscarpetas i on i.idImputadoCarpeta = r.idImputadoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tblofendidoscarpetas o on o.idOfendidoCarpeta = r.idOfendidoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tbldelitoscarpetas dc on dc.idDelitoCarpeta = r.idDelitoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tbldelitos d on d.cveDelito = dc.cveDelito" +
                    " inner" +
                    " join htsj_sigejupe.tblimputadossentencias ise on ise.idImpOfeDelCarpeta = r.idImpOfeDelCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tblactuaciones a on a.idActuacion = ise.idActuacion" +
                    " where" +
                    " a.fechaejecutoria between @fechaIni and @fechaFin" +
                    " and a.cveTipoActuacion = 3 and c.cveTipoCarpeta in (2, 3, 4) and c.cvejuzgado <> 11353" +
                    " and dc.cveDelito in (11, 12, 13, 134, 345, 15, 346, 347, 14, 137, 10, 16, 17, 26)" +
                    " and c.activo = 'S' and r.activo = 'S' and i.activo = 'S' and o.activo = 'S'" +
                    " and dc.activo = 'S' and ise.activo = 'S' and a.activo = 'S'" +
                    " group by tipo, d.cvedelito; ";
                com = new MySqlCommand(str, con);
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@fechaIni", fechaInicio.Text);
                com.Parameters.AddWithValue("@fechaFin", fechaFin.Text + " 23:59:59");
                r = com.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        String delito = r.GetString("desDelito").ToLower();
                        int valueDSP, valueDP;
                        Boolean banderaDelitoSP = mapDelitosSP.TryGetValue(delito, out valueDSP);
                        Boolean banderaDelitoPar = mapDelitosPar.TryGetValue(delito, out valueDP);
                        int sentencias = r.GetInt32("Sentencias");
                        int sentenciados = r.GetInt32("Sentenciados");
                        if (delito.Contains("cometido por servidores publicos") || banderaDelitoSP)
                        {

                            if (delito.Contains("atribuciones y facultades"))
                            {
                                spEjecutoria.UsoIlicito = spEjecutoria.UsoIlicito + sentenciados;
                            }
                            else if (delito.Contains("trafico de influencia"))
                            {
                                spEjecutoria.TraficoInfluencia = spEjecutoria.TraficoInfluencia + sentenciados;
                            }
                            else if (delito.Contains("cohecho"))
                            {
                                spEjecutoria.Cohecho = spEjecutoria.Cohecho + sentenciados;
                            }
                            else if (delito.ToLower().Contains("peculado"))
                            {
                                spEjecutoria.Peculado = spEjecutoria.Peculado + sentenciados;
                            }
                            else if (delito.ToLower().Contains("abuso de autoridad"))
                            {
                                spEjecutoria.AbusoAutoridad = spEjecutoria.AbusoAutoridad + sentenciados;
                            }
                        }
                        else if (delito.Contains("cometido por particulares") || banderaDelitoPar)
                        {
                            if (delito.Contains("atribuciones y facultades"))
                            {
                                parEjecutoria.UsoIlicito = parEjecutoria.UsoIlicito + sentenciados;
                            }
                            else if (delito.Contains("trafico de influencia"))
                            {
                                parEjecutoria.TraficoInfluencia = parEjecutoria.TraficoInfluencia + sentenciados;
                            }
                            else if (delito.Contains("cohecho"))
                            {
                                parEjecutoria.Cohecho = parEjecutoria.Cohecho + sentenciados;
                            }
                            else if (delito.Contains("peculado"))
                            {
                                parEjecutoria.Peculado = parEjecutoria.Peculado + sentenciados;
                            }
                        }
                        else {

                            Boolean banderaDelito  = mapDelitosSililares.TryGetValue(delito, out valueDSP);
                            if (banderaDelito) {
                                String tipo = r.GetString("tipo");
                                if (tipo.Equals("P")) {
                                    if (delito.Contains("trafico de influencia"))
                                    {
                                        spEjecutoria.TraficoInfluencia = spEjecutoria.TraficoInfluencia + sentenciados;
                                    }
                                    else if (delito.Contains("cohecho"))
                                    {
                                        spEjecutoria.Cohecho = spEjecutoria.Cohecho + sentenciados;
                                    }
                                    else if (delito.ToLower().Contains("peculado"))
                                    {
                                        spEjecutoria.Peculado = spEjecutoria.Peculado + sentenciados;
                                    }

                                } else if (tipo.Equals("SP")) {
                                    if (delito.Contains("trafico de influencia"))
                                    {
                                        parEjecutoria.TraficoInfluencia = parEjecutoria.TraficoInfluencia + sentenciados;
                                    }
                                    else if (delito.Contains("cohecho"))
                                    {
                                        parEjecutoria.Cohecho = parEjecutoria.Cohecho + sentenciados;
                                    }
                                    else if (delito.Contains("peculado"))
                                    {
                                        parEjecutoria.Peculado = parEjecutoria.Peculado + sentenciados;
                                    }

                                }
                            }


                        }
                    }
                }
                else
                {
                }
                con.Close();

                //SUMAR TOTALES DE PARTICULARES Y SERVIDORES PUBLICOS
                spEjecutoria.Total = spEjecutoria.AbusoAutoridad + spEjecutoria.Coalicion + spEjecutoria.Cohecho + spEjecutoria.Concusion + spEjecutoria.Delitos
                    + spEjecutoria.EjercicioAbusivo + spEjecutoria.EnriquecimientoIlicito + spEjecutoria.Incumplimiento + spEjecutoria.UsoIlicito + spEjecutoria.Intimidacion
                    + spEjecutoria.TraficoInfluencia + spEjecutoria.Peculado;

                parEjecutoria.Total = parEjecutoria.UsoIlicito + parEjecutoria.TraficoInfluencia + parEjecutoria.Cohecho + parEjecutoria.Peculado;
            }
            catch (Exception ex)
            {
                String errorText = ex.ToString();
                Console.WriteLine("Error al consultar información: " + errorText);
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                    con.Close();
                }
            }
        }



        protected void ConsultarDelitosServidoresPublicosEjecutoriaNuevo()
        {
            MySqlConnection con = null;
            spEjecutoria = new ServidorPublicoEjecutoria();
            parEjecutoria = new ParticularEjecutoria();
            try
            {
                con = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["sigejupe"]);
                con.Open();
                str = "select case when i.cveOcupacion = 1 then 'SP' else 'P'end tipo, d.desDelito, " +
                    " count(distinct a.idActuacion) 'Sentencias', count(distinct i.idImputadoCarpeta) 'Sentenciados'" +
                    " from htsj_sigejupe.tblcarpetasjudiciales c" +
                    " inner" +
                    " join htsj_sigejupe.tblimpofedelcarpetas r on r.idCarpetaJudicial = c.idCarpetaJudicial" +
                    " inner" +
                    " join htsj_sigejupe.tblimputadoscarpetas i on i.idImputadoCarpeta = r.idImputadoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tblofendidoscarpetas o on o.idOfendidoCarpeta = r.idOfendidoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tbldelitoscarpetas dc on dc.idDelitoCarpeta = r.idDelitoCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tbldelitos d on d.cveDelito = dc.cveDelito" +
                    " inner" +
                    " join htsj_sigejupe.tblimputadossentencias ise on ise.idImpOfeDelCarpeta = r.idImpOfeDelCarpeta" +
                    " inner" +
                    " join htsj_sigejupe.tblactuaciones a on a.idActuacion = ise.idActuacion" +
                    " where" +
                    " a.fechaejecutoria between @fechaIni and @fechaFin" +
                    " and a.cveTipoActuacion = 3 and c.cveTipoCarpeta in (2, 3, 4) and c.cvejuzgado <> 11353" +
                    " and dc.cveDelito in (11, 12, 13, 134, 345, 15, 346, 347, 14, 137, 10, 16, 17, 26)" +
                    " and c.activo = 'S' and r.activo = 'S' and i.activo = 'S' and o.activo = 'S'" +
                    " and dc.activo = 'S' and ise.activo = 'S' and a.activo = 'S'" +
                    " group by tipo, d.cvedelito; ";
                com = new MySqlCommand(str, con);
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@fechaIni", fechaInicio.Text);
                com.Parameters.AddWithValue("@fechaFin", fechaFin.Text + " 23:59:59");
                r = com.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        String delito = r.GetString("desDelito").ToLower();
                        if (delito.Contains("cometido por servidores publicos"))
                        {

                            if (delito.Contains("atribuciones y facultades"))
                            {
                                spEjecutoria.UsoIlicito = spEjecutoria.UsoIlicito + r.GetInt32("Sentenciados");
                            }
                            else if (delito.Contains("trafico de influencia"))
                            {
                                spEjecutoria.TraficoInfluencia = spEjecutoria.TraficoInfluencia + r.GetInt32("Sentenciados");
                            }
                            else if (delito.Contains("cohecho"))
                            {
                                spEjecutoria.Cohecho = spEjecutoria.Cohecho + r.GetInt32("Sentenciados");
                            }
                            else if (delito.ToLower().Contains("peculado"))
                            {
                                spEjecutoria.Peculado = spEjecutoria.Peculado + r.GetInt32("Sentenciados"); ;
                            }
                            else if (delito.ToLower().Contains("abuso de autoridad"))
                            {
                                spEjecutoria.AbusoAutoridad = spEjecutoria.Peculado + r.GetInt32("Sentenciados"); ;
                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                }
                con.Close();
                spEjecutoria.Total = spEjecutoria.AbusoAutoridad + spEjecutoria.Coalicion + spEjecutoria.Cohecho + spEjecutoria.Concusion + spEjecutoria.Delitos
                    + spEjecutoria.EjercicioAbusivo + spEjecutoria.EnriquecimientoIlicito + spEjecutoria.Incumplimiento + spEjecutoria.UsoIlicito + spEjecutoria.Intimidacion
                    + spEjecutoria.TraficoInfluencia + spEjecutoria.Peculado;
            }
            catch (Exception ex)
            {
                String errorText = ex.ToString();
                Console.WriteLine("Error al consultar información: " + errorText);
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                    con.Close();
                }
            }
        }


        protected void Consulta_Generica(object sender, EventArgs e)
        {
            MySqlConnection con = null;
            try
            {

                con = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["local"]);
                con.Open();
                str = "SELECT * FROM  ";
                com = new MySqlCommand(str, con);
                /*com.CommandType = CommandType.Text;
                passw = TextBox_password.Text;

                com.Parameters.AddWithValue("@UserName", TextBox_user_name.Text);
                com.Parameters.AddWithValue("@Password", TextBox_password.Text);*/
                r = com.ExecuteReader();


                if (r.HasRows)
                {


                    while (r.Read())
                    {
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                String error = ex.ToString();
                Console.WriteLine("ERROR : " + error);

            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                    con.Close();
                }
            }

        }





        public void ModificarArchivoDocX()
        {
            String urlFormato = banderaServidor ? "//10.22.157.69/Users/Estadistica/Documents/Compartida_10_22_157_69/Publicaciones/ReportesAnticorrupcionPruebas/formatos/FORMATO_ANTICORRUPCION.docx"
                : "C:/Users/Esther/Documents/FORMATO_ANTICORRUPCION.docx";
            try
            {

                using (var document = DocX.Load(urlFormato))
                {
                    //Remplazar valores en materia de corrupción
                    document.ReplaceText("#MC01", matCorrupcion.NoPersonasIniciados.ToString());
                    document.ReplaceText("#MC02", matCorrupcion.NoPersonasSentenciaDictada.ToString());
                    document.ReplaceText("#MC03", matCorrupcion.NoPersonasSentenciaEjecutoria.ToString());
                    document.ReplaceText("#MC04", matCorrupcion.NoSentenciasServidoresPublicos.ToString());
                    document.ReplaceText("#MC05", matCorrupcion.NoSentenciasParticulares.ToString());
                    document.ReplaceText("#MC06", spEjecutoria.Total.ToString());
                    document.ReplaceText("#MC07", parEjecutoria.Total.ToString());

                    //Desgloce del número y tipo de delitos determinados en contra de servidores públicos en sentencia ejecutoria
                    document.ReplaceText("#SPSE01", spEjecutoria.Incumplimiento.ToString());
                    document.ReplaceText("#SPSE02", spEjecutoria.Coalicion.ToString());
                    document.ReplaceText("#SPSE03", spEjecutoria.AbusoAutoridad.ToString());
                    document.ReplaceText("#SPSE04", spEjecutoria.UsoIlicito.ToString());
                    document.ReplaceText("#SPSE05", spEjecutoria.Concusion.ToString());
                    document.ReplaceText("#SPSE06", spEjecutoria.Intimidacion.ToString());
                    document.ReplaceText("#SPSE07", spEjecutoria.EjercicioAbusivo.ToString());
                    document.ReplaceText("#SPSE08", spEjecutoria.TraficoInfluencia.ToString());
                    document.ReplaceText("#SPSE09", spEjecutoria.Cohecho.ToString());
                    document.ReplaceText("#SPSE10", spEjecutoria.Peculado.ToString());
                    document.ReplaceText("#SPSE11", spEjecutoria.EnriquecimientoIlicito.ToString());
                    document.ReplaceText("#SPSE12", spEjecutoria.Delitos.ToString());
                    document.ReplaceText("#SPSET", spEjecutoria.Total.ToString());

                    //Desgloce del número t tipo de delitos determinados en contra de particulares en sentencia ejecutoria
                    document.ReplaceText("#PSE01", parEjecutoria.UsoIlicito.ToString());
                    document.ReplaceText("#PSE02", parEjecutoria.TraficoInfluencia.ToString());
                    document.ReplaceText("#PSE03", parEjecutoria.Cohecho.ToString());
                    document.ReplaceText("#PSE04", parEjecutoria.Peculado.ToString());
                    document.ReplaceText("#PSET", parEjecutoria.Total.ToString());



                    /*Response.ContentType = "application/ms-word";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AppendHeader("NombreCabecera", "MensajeCabecera");
                    Response.TransmitFile(Server.MapPath("~/tuRuta/TuArchivo.xml"));
                    Response.End();*/

                    MemoryStream memoryStream = new MemoryStream();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=Reporte(Anticorrupcion_organos).docx");
                    document.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();


                    //document.SaveAs("C:/Users/Esther/Documents/documento_2.docx");


                }

            }
            catch (Exception ex)
            {
                String error = ex.ToString();
                Console.WriteLine("Error : " + error.ToString());
            }
        }




    }



    
}