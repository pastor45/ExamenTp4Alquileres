using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace alquileres
{
    public partial class AgregarPropiedades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPropiedades();
                CargarPropietarios();
            }
        }
        protected void CargarPropiedades()
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT p.id, p.calle, p.altura, a.monto, p.idPropietario FROM Propiedades p INNER JOIN Alquileres a ON p.id = a.idPropiedad", conn))
                {
                    DataTable dtPropiedades = new DataTable();
                    da.Fill(dtPropiedades);

                    ddlPropiedades.DataSource = dtPropiedades;
                    ddlPropiedades.DataTextField = "calle";
                    ddlPropiedades.DataValueField = "id";
                    ddlPropiedades.DataBind();

                    ddlPropiedades.Items.Insert(0, new ListItem("Seleccionar Propiedad", "0"));
                }
            }
        }

        protected void CargarPropietarios()
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT id, apellido, nombre FROM Propietarios", conn))
                {
                    DataTable dtPropietarios = new DataTable();
                    da.Fill(dtPropietarios);

                    ddlPropietarios.DataSource = dtPropietarios;
                    ddlPropietarios.DataTextField = "apellido";
                    ddlPropietarios.DataValueField = "id";
                    ddlPropietarios.DataBind();
                }
            }
        }

        protected void ddlPropiedades_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedPropertyId = int.Parse(ddlPropiedades.SelectedValue);

            if (selectedPropertyId > 0)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    string query = "SELECT p.calle, p.altura, a.monto, p.idPropietario, pr.apellido, pr.nombre " +
                                   "FROM Propiedades p " +
                                   "INNER JOIN Propietarios pr ON p.idPropietario = pr.id " +
                                   "INNER JOIN Alquileres a ON p.id = a.idPropiedad " +
                                   "WHERE p.id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedPropertyId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtId.Text = selectedPropertyId.ToString();
                            txtCalle.Text = reader["calle"].ToString();
                            txtAltura.Text = reader["altura"].ToString();
                            txtMonto.Text = reader["monto"].ToString();
                            ddlPropietarios.SelectedValue = reader["idPropietario"].ToString();

                            string propietario = $"{reader["apellido"]} {reader["nombre"]}";
                            lblPropietario.Text = $"Propietario: {propietario}";
                        }
                    }
                }
            }
            else
            {
                LimpiarCampos();
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string calle = txtCalle.Text;
            string altura = txtAltura.Text;
            decimal monto = decimal.Parse(txtMonto.Text);
            int idPropietario = Convert.ToInt32(ddlPropietarios.SelectedValue);

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string propiedadesQuery = "INSERT INTO Propiedades (calle, altura, idPropietario) VALUES (@calle, @altura, @idPropietario); SELECT SCOPE_IDENTITY()";
                        using (SqlCommand propiedadesCmd = new SqlCommand(propiedadesQuery, conn, transaction))
                        {
                            propiedadesCmd.Parameters.AddWithValue("@calle", calle);
                            propiedadesCmd.Parameters.AddWithValue("@altura", altura);
                            propiedadesCmd.Parameters.AddWithValue("@idPropietario", idPropietario);
                            int newPropertyId = Convert.ToInt32(propiedadesCmd.ExecuteScalar());

                            string alquileresQuery = "INSERT INTO Alquileres (idPropiedad, monto) VALUES (@idPropiedad, @monto)";
                            using (SqlCommand alquileresCmd = new SqlCommand(alquileresQuery, conn, transaction))
                            {
                                alquileresCmd.Parameters.AddWithValue("@idPropiedad", newPropertyId);
                                alquileresCmd.Parameters.AddWithValue("@monto", monto);
                                alquileresCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit(); 

                        CargarPropiedades();
                        LimpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); 
                                                
                    }
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            string calle = txtCalle.Text;
            string altura = txtAltura.Text;
            decimal nuevoMonto = decimal.Parse(txtMonto.Text);
            int nuevoIdPropietario = Convert.ToInt32(ddlPropietarios.SelectedValue);

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string propiedadesQuery = "UPDATE Propiedades SET calle = @calle, altura = @altura, idPropietario = @idPropietario WHERE id = @id";
                        using (SqlCommand propiedadesCmd = new SqlCommand(propiedadesQuery, conn, transaction))
                        {
                            propiedadesCmd.Parameters.AddWithValue("@id", id);
                            propiedadesCmd.Parameters.AddWithValue("@calle", calle);
                            propiedadesCmd.Parameters.AddWithValue("@altura", altura);
                            propiedadesCmd.Parameters.AddWithValue("@idPropietario", nuevoIdPropietario);
                            propiedadesCmd.ExecuteNonQuery();
                        }

                        string alquileresQuery = "UPDATE Alquileres SET monto = @monto WHERE idPropiedad = @id";
                        using (SqlCommand alquileresCmd = new SqlCommand(alquileresQuery, conn, transaction))
                        {
                            alquileresCmd.Parameters.AddWithValue("@id", id);
                            alquileresCmd.Parameters.AddWithValue("@monto", nuevoMonto);
                            alquileresCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        CargarPropiedades();
                        LimpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); 
                                                
                    }
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                string query = "DELETE FROM Propiedades WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            CargarPropiedades();
            LimpiarCampos();
        }

        protected void LimpiarCampos()
        {
            txtId.Text = "";
            txtCalle.Text = "";
            txtAltura.Text = "";
            txtMonto.Text = "";
        }
    }
}
