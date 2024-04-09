<%@ Page Title="" Language="C#" MasterPageFile="~/veterinaria.Master" AutoEventWireup="true" CodeBehind="ReporteMascotas.aspx.cs" Inherits="Veterinaria.ReporteMascotas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Formulario Mascotas</h1>
    <p>Ingresa la informacion de la mascota como se solicita a continuacion:</p>
    <div>
        <label>Este campo es exlusivo para Modificar o Eliminar una Mascota</label>
        <br />
        <label>ID de la mascota</label>
        <asp:TextBox ID="tIdMascota" runat="server" placeholder='ID Mascota....' ></asp:TextBox>
    </div>
    <br />
    <div>
          <label>Nombre de la mascota</label>
          <asp:TextBox ID="Tnombre_mascota" runat="server" placeholder='Nombre mascota....' ></asp:TextBox>
    </div>
    <br />
    <div>
          <label>Tipo de mascota</label>
          <asp:TextBox ID="Ttipo_mascota" runat="server" placeholder='Tipo mascota....' ></asp:TextBox>
    </div>
    <br />
    <div>
        <label>Alimento para mascota</label>
        <asp:TextBox ID="Talimento_mascota" runat="server" placeholder='Alimento....' ></asp:TextBox>
    </div>
    <br />
    <div>
        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Mascota" OnClick="btnRegistrar_Click" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar Mascota" OnClick="btnModificar_Click"/>
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Mascota" OnClick="btnEliminar_Click" />
    </div>
    <br />

    <div>
    <h1>Reporte de Mascotas</h1>
    <p>A continuacion se detallara el reporte de los Usuarios del sistema:</p>        
    <asp:GridView ID="gvMascotas" runat="server"></asp:GridView>
</div>

</asp:Content>
