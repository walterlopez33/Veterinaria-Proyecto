<%@ Page Title="" Language="C#" MasterPageFile="~/veterinaria.Master" AutoEventWireup="true" CodeBehind="ReporteControlCitas.aspx.cs" Inherits="Veterinaria.ReporteControlCitas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Formulario Citas</h1>
    <p>Ingresa la informacion de la cita como se solicita a continuacion:</p>

    <div>
        <label>ID de la mascota</label>
        <asp:TextBox ID="TIDmascotas" runat="server" placeholder='ID mascota....' ></asp:TextBox>
    </div>
    <br />
    <div>
        <label>Dia de la cita</label>
        <asp:TextBox ID="TFecha" runat="server" Type="Date"></asp:TextBox>
    </div>    
    <br />
    <div>
        <label>Medico Asignado</label>
        <asp:TextBox ID="TMedicoAsignado" runat="server" placeholder='Medico....' ></asp:TextBox>
    </div>
    <br />
    <div>
        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Cita" OnClick="btnRegistrar_Click" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar Cita" OnClick="btnModificar_Click"/>
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Cita" OnClick="btnEliminar_Click" />
    </div>
    <br />

    <div>
        <h1>Reporte de Citas</h1>
        <p>A continuacion se detalla el reporte de las citas proximas::</p>        
        <asp:GridView ID="gvCitas" runat="server"></asp:GridView>
    </div>
</asp:Content>
