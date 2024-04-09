<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Veterinaria.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class='bold-line'></div>
        <div class='overlay'></div>
            <div class='content'>
                <div class='welcome'>Bienvenido a la Vete Carrizal!</div>
                <div class='subtitle'>Antes de continuar por favor ingresa tus credenciales.</div>           
                <div class='input-fields'>
                    <asp:TextBox ID="Tusuario" runat="server" class='input-line full-width' placeholder='Usuario' ></asp:TextBox>
                    <asp:TextBox ID="Tclave" runat="server" class='input-line full-width' placeholder='Clave' TextMode="Password" ></asp:TextBox>
                </div>
                <br />
                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" class='ghost-round full-width' />
            </div>
        <div>
    </div>
    </form>
</body>
</html>