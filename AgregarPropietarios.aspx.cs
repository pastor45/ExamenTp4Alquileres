using System;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace alquileres
{
    public partial class Propietarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPropietarios();
            }
        }

        protected void CargarPropietarios()
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT id, nombre, apellido FROM Propietarios", conn))
                {
                    DataTable dtPropietarios = new DataTable();
                    da.Fill(dtPropietarios);

                    ddlPropietarios.DataTextField = "nombre";
                    ddlPropietarios.DataValueField = "id";
                    ddlPropietarios.DataSource = dtPropietarios;
                    ddlPropietarios.DataBind();

                    ddlPropietarios.Items.Insert(0, new ListItem("Seleccionar Propietario", "0"));

                    gvPropietarios.DataSource = dtPropietarios;
                    gvPropietarios.DataBind();
                }
            }
        }

        protected void ddlPropietarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedOwnerId = int.Parse(ddlPropietarios.SelectedValue);
            if (selectedOwnerId != 0)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    string query = "SELECT nombre, apellido FROM Propietarios WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@id", selectedOwnerId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtId.Text = selectedOwnerId.ToString();

                            txtNombre.Text = reader["nombre"].ToString();
                            txtApellido.Text = reader["apellido"].ToString();
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
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                string query = "INSERT INTO Propietarios (nombre, apellido) VALUES (@nombre, @apellido)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            CargarPropietarios();
            LimpiarCampos();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                string query = "UPDATE Propietarios SET nombre = @nombre, apellido = @apellido WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            CargarPropietarios();
            LimpiarCampos();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                string query = "DELETE FROM Propietarios WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            CargarPropietarios();
            LimpiarCampos();
        }

        protected void LimpiarCampos()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
        }
    }
}
