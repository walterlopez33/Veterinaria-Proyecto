using System;
using System.Data.SqlClient;
using System.Data;
using Veterinaria.Clases;
using System.Configuration;
using System.Web.UI;

namespace Veterinaria
{
    public partial class ReporteUsuarios : System.Web.UI.Page
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

                string query = "SELECT * FROM Usuarios";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adapter.Fill(dt);
                            gvUsuarios.DataSource = dt;
                            gvUsuarios.DataBind();
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

                if (Tuser.Text == "")
                {
                    Message = "Falta dato de Nombre de usuario.";
                }
                else if (Tpassword.Text == "")
                {
                    Message = "Falta dato de Clave de usuario.";
                }
                else if (Tname.Text == "")
                {
                    Message = "Falta dato de Nombre completo.";
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
                    ClsUsuario usuario = new ClsUsuario();

                    usuario.Login_Usuario = Tuser.Text;
                    usuario.Clave_Usuario = Tpassword.Text;
                    usuario.Nombre_Usuario = Tname.Text;

                    if (Registrar_Usuario(usuario) > 0)
                    {
                        string Message = "Usuario Registrado.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        llenarGrid();
                    }
                    else
                    {
                        string Message = "Error al Registrar Usuario.";
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

        public int Registrar_Usuario(ClsUsuario usuario)
        {            
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Registrar_Usuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@User_Registro", SqlDbType.NVarChar).Value = usuario.Login_Usuario;
                        command.Parameters.Add("@Password_Registro", SqlDbType.NVarChar).Value = usuario.Clave_Usuario;
                        command.Parameters.Add("@Name_Registro", SqlDbType.NVarChar).Value = usuario.Nombre_Usuario;
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
                    ClsUsuario usuario = new ClsUsuario();

                    usuario.Login_Usuario = Tuser.Text;
                    usuario.Clave_Usuario = Tpassword.Text;
                    usuario.Nombre_Usuario = Tname.Text;

                    if (Modificar_Usuario(usuario) > 0)
                    {
                        string Message = "Usuario Modificado.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        llenarGrid();
                    }
                    else
                    {
                        string Message = "Error al Modificar Usuario.";
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

        public int Modificar_Usuario(ClsUsuario usuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Modificar_Usuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@User_Registro", SqlDbType.NVarChar).Value = usuario.Login_Usuario;
                        command.Parameters.Add("@Password_Registro", SqlDbType.NVarChar).Value = usuario.Clave_Usuario;
                        command.Parameters.Add("@Name_Registro", SqlDbType.NVarChar).Value = usuario.Nombre_Usuario;
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
                if (Tuser.Text == "")
                {
                    string Message = "Falta dato de Nombre de usuario.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                }
                else
                {
                    ClsUsuario usuario = new ClsUsuario();

                    usuario.Login_Usuario = Tuser.Text;
                    usuario.Clave_Usuario = Tpassword.Text;
                    usuario.Nombre_Usuario = Tname.Text;

                    if (Eliminar_Usuario(usuario) > 0)
                    {
                        string Message = "Usuario Eliminado.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Message + "');", true);
                        llenarGrid();
                    }
                    else
                    {
                        string Message = "Error al Eliminar Usuario.";
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

        public int Eliminar_Usuario(ClsUsuario usuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                int resultado = 0;

                using (connection)
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Eliminar_Usuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@User_Registro", SqlDbType.NVarChar).Value = usuario.Login_Usuario;
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