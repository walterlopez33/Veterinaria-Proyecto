using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Veterinaria.Clases;

namespace Veterinaria
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            ClsUsuario usuario = new ClsUsuario();

            usuario.Login_Usuario = Tusuario.Text;
            usuario.Clave_Usuario = Tclave.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                connection.Open();
                string query = "IF EXISTS(SELECT * FROM Usuarios WHERE Login_Usuario = '" + usuario.Login_Usuario + "' AND Clave_Usuario = '" + usuario.Clave_Usuario + "') BEGIN SELECT 1 AS resultado END ELSE BEGIN SELECT 0 AS resultado END";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adapter.Fill(dt);

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                resultado = int.Parse(dt.Rows[0]["resultado"].ToString());
                            }
                        }
                    }
                }
                              

                if (resultado == 1)
                {
                    Response.Redirect("Inicio.aspx");
                }
                else
                {
                    string Message = "Usuario no existe.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Error al Obtener el Reporte. Descripción del error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + errorMessage + "');", true);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
    }
}