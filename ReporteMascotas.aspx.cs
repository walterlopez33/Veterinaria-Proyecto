using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Veterinaria.Clases;
using System.Web.Services.Description;

namespace Veterinaria
{
    public partial class ReporteMascotas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void llenarGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {

                connection.Open();

                string query = "SELECT * FROM Mascotas";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adapter.Fill(dt);
                            gvMascotas.DataSource = dt;
                            gvMascotas.DataBind();
                        }
                    }
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

        private string validarCampos()
        {
            try
            {
                string Message = "";

                if (Tnombre_mascota.Text == "")
                {
                    Message = "Falta dato de Nombre de la mascota.";
                }
                else if (Ttipo_mascota.Text == "")
                {
                    Message = "Falta dato de Tipo de mascota.";
                }
                else if (Talimento_mascota.Text == "")
                {
                    Message = "Falta dato de Alimento para mascota.";
                }

                return Message;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al realizar el proceso. Descripción del error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + errorMessage + "');", true);
                return "";
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = validarCampos();
                if (msg == "")
                {
                    ClsMascota mascota = new ClsMascota();

                    mascota.Nombre_Mascota = Tnombre_mascota.Text;
                    mascota.Tipo_Mascota = Ttipo_mascota.Text;
                    mascota.Comida_Favorita = Talimento_mascota.Text;

                    if (Registrar_Mascota(mascota) > 0)
                    {
                        string Message = "Mascota Registrada.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        llenarGrid();
                    }
                    else
                    {
                        string Message = "Error al Registrar Mascota.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + msg + "');", true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al realizar el proceso. Descripción del error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + errorMessage + "');", true);
            }
        }

        public int Registrar_Mascota(ClsMascota mascota)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Registrar_Mascota", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Nombre_Mascota", SqlDbType.NVarChar).Value = mascota.Nombre_Mascota;
                        command.Parameters.Add("@Tipo_Mascota", SqlDbType.NVarChar).Value = mascota.Tipo_Mascota;
                        command.Parameters.Add("@Comida_Favorita", SqlDbType.NVarChar).Value = mascota.Comida_Favorita;
                        command.ExecuteNonQuery();
                        connection.Close();
                        resultado = 1;
                    }
                }

                return resultado;          
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al realizar el proceso. Descripción del error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + errorMessage + "');", true);
                return 0;
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tIdMascota.Text == "")
                {
                    string Message = "Falta dato de ID de la mascota.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                }
                else
                {
                    string msg = validarCampos();
                    if (msg == "")
                    {
                        ClsMascota mascota = new ClsMascota();

                        mascota.ID_Mascota = int.Parse(tIdMascota.Text);
                        mascota.Nombre_Mascota = Tnombre_mascota.Text;
                        mascota.Tipo_Mascota = Ttipo_mascota.Text;
                        mascota.Comida_Favorita = Talimento_mascota.Text;

                        if (Modificar_Mascota(mascota) > 0)
                        {
                            string Message = "Mascota Modificada.";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                            llenarGrid();
                        }
                        else
                        {
                            string Message = "Error al Modificar Mascota.";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + msg + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al realizar el proceso. Descripción del error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + errorMessage + "');", true);
            }
        }

        public int Modificar_Mascota(ClsMascota mascota)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {                
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Modificar_Mascota", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID_Mascota", SqlDbType.NVarChar).Value = mascota.ID_Mascota;
                        command.Parameters.Add("@Nombre_Mascota", SqlDbType.NVarChar).Value = mascota.Nombre_Mascota;
                        command.Parameters.Add("@Tipo_Mascota", SqlDbType.NVarChar).Value = mascota.Tipo_Mascota;
                        command.Parameters.Add("@Comida_Favorita", SqlDbType.NVarChar).Value = mascota.Comida_Favorita;
                        command.ExecuteNonQuery();
                        connection.Close();
                        resultado = 1;
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al realizar el proceso. Descripción del error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + errorMessage + "');", true);
                return 0;
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tIdMascota.Text == "")
                {
                    string Message = "Falta dato de Nombre de usuario.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                }
                else
                {
                    ClsMascota mascota = new ClsMascota();

                    mascota.ID_Mascota = int.Parse(tIdMascota.Text);
                    mascota.Nombre_Mascota = Tnombre_mascota.Text;
                    mascota.Tipo_Mascota = Ttipo_mascota.Text;
                    mascota.Comida_Favorita = Talimento_mascota.Text;

                    if (Eliminar_Mascota(mascota) > 0)
                    {
                        string Message = "Mascota Eliminada.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        llenarGrid();
                    }
                    else
                    {
                        string Message = "Error al Eliminar Mascota.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                    }
                }
             }
            catch (Exception ex)
            {
                string errorMessage = "Error al realizar el proceso. Descripción del error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + errorMessage + "');", true);
            }
        }

        public int Eliminar_Mascota(ClsMascota mascota)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Eliminar_Mascota", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID_Mascota", SqlDbType.NVarChar).Value = mascota.ID_Mascota;
                        command.ExecuteNonQuery();
                        connection.Close();
                        resultado = 1;
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al realizar el proceso. Descripción del error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + errorMessage + "');", true);
                return 0;
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