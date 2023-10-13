<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuAlquileres.aspx.cs" Inherits="alquileres.Alquileres" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alquileres</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            background-color: #fff;
            border: 1px solid #ccc;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        }
        h1 {
            text-align: center;
        }
        .form-group {
            margin-bottom: 10px;
        }
        label {
            display: block;
        }
        .btn-filtrar {
            background-color: #007bff;
            color: #fff;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
        }
        .btn-filtrar:hover {
            background-color: #0056b3;
        }
        .custom-dropdown {
    position: relative;
    display: inline-block;
}

.custom-dropdown-list {
    width: 100%;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #fff;
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);
}
        .gridview {
    width: 100%;
    border-collapse: collapse;
    border: 1px solid #ddd;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.gridview-header {
    background-color: #007bff;
    color: #fff;
    font-weight: bold;
}

.gridview-row {
    background-color: #fff;
}

.gridview-alt-row {
    background-color: #f2f2f2;
}

.gridview-cell {
    padding: 8px;
    text-align: left;
    border-bottom: 1px solid #ddd;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Lista de Alquileres</h1>
           <div class="form-group">
    <label for="ddlPropietarios">Selecciona un propietario:</label>
    <div class="custom-dropdown">
        <asp:DropDownList ID="ddlPropietarios" runat="server" CssClass="custom-dropdown-list"></asp:DropDownList>
    </div>
</div>
               <div class="form-group">
       <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="btn-filtrar" />
   </div>
            <div class="form-group">
                <asp:Button  runat="server" Text="Propietarios" OnClick="btnAgregarPropietario_Click" CssClass="btn-filtrar"  />
            </div>
             <div class="form-group">
     <asp:Button  runat="server" Text="Propiedades" OnClick="btnAgregarPropiedad_Click" CssClass="btn-filtrar" />
 </div>
                   
         
   <asp:GridView ID="gvAlquileres" runat="server" AutoGenerateColumns="false" CssClass="gridview">
    <HeaderStyle CssClass="gridview-header" />
    <RowStyle CssClass="gridview-row" />
    <AlternatingRowStyle CssClass="gridview-alt-row" />
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-CssClass="gridview-cell" />
        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-CssClass="gridview-cell" />
        <asp:BoundField DataField="apellido" HeaderText="Apellido" ItemStyle-CssClass="gridview-cell" />
        <asp:BoundField DataField="calle" HeaderText="Calle" ItemStyle-CssClass="gridview-cell" />
        <asp:BoundField DataField="altura" HeaderText="Altura" ItemStyle-CssClass="gridview-cell" />
        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:C}" ItemStyle-CssClass="gridview-cell" />
    </Columns>
</asp:GridView>

        </div>
    </form>
</body>
</html>
