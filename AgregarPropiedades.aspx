<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarPropiedades.aspx.cs" Inherits="alquileres.AgregarPropiedades" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ABM Propiedades</title>
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
            <h1 class="text-center mb-4">Gestión de Propiedades</h1>
            <div class="form-group">
                <label for="ddlPropiedades">Propiedades:</label>
                <asp:DropDownList ID="ddlPropiedades" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPropiedades_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="txtId">ID:</label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="True"> </asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtCalle">Calle:</label>
                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtAltura">Altura:</label>
                <asp:TextBox ID="txtAltura" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtMonto">Monto:</label>
                <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="lblPropietario">Propietario:</label>
                <asp:Label ID="lblPropietario" runat="server" CssClass="form-control" Text=""></asp:Label>
            </div>
            <div class="form-group">
                <label for="ddlPropietarios">Propietario:</label>
                <asp:DropDownList ID="ddlPropietarios" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Propiedad" OnClick="btnAgregar_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar Propiedad" OnClick="btnModificar_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Propiedad" OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
            </div>
            <div class="form-group">
                <asp:GridView ID="gvPropiedades" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="calle" HeaderText="Calle" />
                        <asp:BoundField DataField="altura" HeaderText="Altura" />
                    </Columns>
                    <SelectedRowStyle CssClass="table-info" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
