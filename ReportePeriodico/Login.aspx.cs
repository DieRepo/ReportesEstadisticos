using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace ReportesEstadisticos
{
    public partial class Login : System.Web.UI.Page
    {
        string str;
        MySqlCommand com;
        MySqlDataReader r;
        string passw;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            MySqlConnection con = null;
            try
            {

                con = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["reportes"]);
                con.Open();
                str = "SELECT * FROM tblusuarios " +
                    "  where usuario=@UserName and pas=@Password and activo = 1 ";
                com = new MySqlCommand(str, con);
                com.CommandType = CommandType.Text;
                passw = TextBox_password.Text;



                com.Parameters.AddWithValue("@UserName", TextBox_user_name.Text);
                com.Parameters.AddWithValue("@Password", TextBox_password.Text);
                r = com.ExecuteReader();


                if (r.HasRows)
                {
                    string juzgado = "";
                    string id = "";
                    string perfil = "";
                    string nombre = "";
                    //string idEdificio = "";
                    //string nombreEdificio = "";

                    while (r.Read())
                    {
                        id = r.GetString("idUsuario");
                        perfil = r.GetString("perfil");
                        nombre = r.GetString("nombre") + " " + r.GetString("apellidoPaterno") + " " + r.GetString("apellidoMaterno");
                        //idEdificio = r.GetString("idEdificio");
                        //nombreEdificio = (r.GetString("nombreEdificio") is null) ? " - - " : r.GetString("nombreEdificio");
                    }
                    Dictionary<String, String> uss = new Dictionary<String, String>();
                    Random rnd = new Random();
                    uss.Add("user", TextBox_user_name.Text);
                    uss.Add("pass", passw);
                    uss.Add("idUsuario", id);
                    uss.Add("perfil", perfil);
                    uss.Add("nombre", nombre);
                    //uss.Add("idEdificio", idEdificio);
                    //uss.Add("nombreEdificio", nombreEdificio);
                    Session["usuario"] = uss;

                    Response.Redirect("~/FormatoAnticorrupcion");

                }
                else
                {
                    //textoError.Visible = true;
                    //textoError.Text = "Usuario o contraseña incorrectos";
                }


                con.Close();

            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.ToString());
                //textoError.Visible = true;
                //textoError.Text = "Usuario o contraseña incorrectos";

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
    }
}