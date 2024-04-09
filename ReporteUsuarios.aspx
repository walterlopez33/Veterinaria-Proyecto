<%@ Page Title="" Language="C#" MasterPageFile="~/veterinaria.Master" AutoEventWireup="true" CodeBehind="ReporteUsuarios.aspx.cs" Inherits="Veterinaria.ReporteUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Formulario Usarios</h1>
    <p>Ingresa la informacion del Usuario a registrar:</p>

    <div>
        <label>Nombre de usuario</label>
        <asp:TextBox ID="Tuser" runat="server" placeholder='Usuario....' ></asp:TextBox>
    </div>
    <br />
    <div>
        <label>Clave de usuario</label>
        <asp:TextBox ID="Tpassword" runat="server" placeholder='Clave....' TextMode="Password" ></asp:TextBox>
    </div>
    <br />
    <div>
        <label>Nombre completo</label>
        <asp:TextBox ID="Tname" runat="server" placeholder='Nombre completo....' ></asp:TextBox>
    </div>
    <br />
    <div>    
        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Usuario" OnClick="btnRegistrar_Click" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar Usuario" OnClick="btnModificar_Click"/>
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Usuario" OnClick="btnEliminar_Click" />
    </div>
    <br />

    <div>
        <h1>Reporte de Usuarios</h1>
        <p>A continuacion se detallara el reporte de las mascotas ingresadas al sistema:</p>        
        <asp:GridView ID="gvUsuarios" runat="server"></asp:GridView>
    </div>
</asp:Content>