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

namespace Veterinaria
{
    public partial class ReporteControlCitas : System.Web.UI.Page
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

                string query = "SELECT * FROM Citas ORDER BY Proxima_fecha ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adapter.Fill(dt);
                            gvCitas.DataSource = dt;
                            gvCitas.DataBind();
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

                if (TIDmascotas.Text == "")
                {
                    Message = "Falta dato de ID de la mascota.";
                }
                else if (TFecha.Text == "")
                {
                    Message = "Falta dato de Dia de la cita.";
                }
                else if (TMedicoAsignado.Text == "")
                {
                    Message = "Falta dato de Medico Asignado.";
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
                    ClsCita cita = new ClsCita();

                    cita.IDMascota = int.Parse(TIDmascotas.Text);
                    cita.Fecha = DateTime.Parse(TFecha.Text);
                    cita.MedicoAsignado = TMedicoAsignado.Text;

                    if (Registrar_Cita(cita) > 0)
                    {
                        string Message = "Cita Registrada.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        llenarGrid();
                    }
                    else
                    {
                        string Message = "Error al Registrar Cita.";
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

        public int Registrar_Cita(ClsCita cita)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Registrar_Cita", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID_Mascota", SqlDbType.NVarChar).Value = cita.IDMascota;
                        command.Parameters.Add("@ProximaFecha", SqlDbType.NVarChar).Value = cita.Fecha.ToString("MM/dd/yyyy");
                        command.Parameters.Add("@MedicoAsignado", SqlDbType.NVarChar).Value = cita.MedicoAsignado;
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
                string msg = validarCampos();
                if (msg == "")
                {
                    ClsCita cita = new ClsCita();

                    cita.IDMascota = int.Parse(TIDmascotas.Text);
                    cita.Fecha = DateTime.Parse(TFecha.Text);
                    cita.MedicoAsignado = TMedicoAsignado.Text;

                    if (Modificar_Cita(cita) > 0)
                    {
                        string Message = "Cita Modificada.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        llenarGrid();
                    }
                    else
                    {
                        string Message = "Error al Modificar Cita.";
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

        public int Modificar_Cita(ClsCita cita)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Modificar_Cita", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID_Mascota", SqlDbType.Int).Value = cita.IDMascota;
                        command.Parameters.Add("@ProximaFecha", SqlDbType.Date).Value = cita.Fecha;
                        command.Parameters.Add("@MedicoAsignado", SqlDbType.NVarChar).Value = cita.MedicoAsignado;
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
                string msg = validarCampos();
                if (msg == "")
                {
                    ClsCita cita = new ClsCita();

                    cita.IDMascota = int.Parse(TIDmascotas.Text);
                    cita.Fecha = DateTime.Parse(TFecha.Text);
                    cita.MedicoAsignado = TMedicoAsignado.Text;

                    if (Eliminar_Cita(cita) > 0)
                    {
                        string Message = "Cita Eliminada.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        llenarGrid();
                    }
                    else
                    {
                        string Message = "Error al Eliminar Cita.";
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

        public int Eliminar_Cita(ClsCita cita)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Eliminar_Cita", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID_Mascota", SqlDbType.Int).Value = cita.IDMascota;
                        command.Parameters.Add("@ProximaFecha", SqlDbType.Date).Value = cita.Fecha;
                        command.Parameters.Add("@MedicoAsignado", SqlDbType.NVarChar).Value = cita.MedicoAsignado;
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