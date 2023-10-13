using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace alquileres
{
    public partial class Alquileres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAlquileres();
                CargarPropietarios();
            }
        }
        protected void CargarAlquileres()
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();

                string query = "SELECT A.id, P.nombre, P.apellido, Pr.calle, Pr.altura, A.monto " +
                               "FROM Alquileres A " +
                               "INNER JOIN Propiedades Pr ON A.idPropiedad = Pr.id " +
                               "INNER JOIN Propietarios P ON Pr.idPropietario = P.id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvAlquileres.DataSource = dt;
                        gvAlquileres.DataBind();
                    }
                }
            }
        }

        protected void CargarPropietarios()
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))

            {
                conn.Open();

                string query = "SELECT id, CONCAT(apellido, ', ', nombre) AS NombreCompleto FROM Propietarios";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        ddlPropietarios.DataSource = dt;
                        ddlPropietarios.DataTextField = "NombreCompleto";
                        ddlPropietarios.DataValueField = "id";
                        ddlPropietarios.DataBind();

                        ddlPropietarios.Items.Insert(0, new ListItem("Selecciona un propietario", "0"));
                    }
                }
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            int propietarioId = int.Parse(ddlPropietarios.SelectedValue);

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                conn.Open();

                string query = "SELECT A.id, P.nombre, P.apellido, Pr.calle, Pr.altura, A.monto " +
                               "FROM Alquileres A " +
                               "INNER JOIN Propiedades Pr ON A.idPropiedad = Pr.id " +
                               "INNER JOIN Propietarios P ON Pr.idPropietario = P.id " +
                               "WHERE P.id = @propietarioId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@propietarioId", propietarioId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvAlquileres.DataSource = dt;
                        gvAlquileres.DataBind();
                    }
                }
            }
        }

        protected void btnAgregarPropietario_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarPropietarios.aspx");
        }

        protected void btnAgregarPropiedad_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarPropiedades.aspx");
        }

      
    }
}