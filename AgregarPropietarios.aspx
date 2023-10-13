<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Propietarios.aspx.cs" Inherits="alquileres.Propietarios" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ABM Propietarios</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .container {
            max-width: 800px;
            margin: 0 auto;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .btn {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="text-center mb-4">Gestión de Propietarios</h1>
                 <div class="form-group">
         <label for="ddlPropietarios">Seleccionar Propietario:</label>
         <asp:DropDownList ID="ddlPropietarios" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPropietarios_SelectedIndexChanged" AutoPostBack="True">
             <asp:ListItem Text="Seleccionar Propietario" Value="0" />
         </asp:DropDownList>
     </div>
            <div class="form-group">
                <label for="txtId">ID:</label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtNombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtApellido">Apellido:</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

       

            <div class="form-group">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Propietario" OnClick="btnAgregar_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar Propietario" OnClick="btnModificar_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Propietario" OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
            </div>

            <div class="form-group">
                <asp:GridView ID="gvPropietarios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                    </Columns>
                    <SelectedRowStyle CssClass="table-info" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>