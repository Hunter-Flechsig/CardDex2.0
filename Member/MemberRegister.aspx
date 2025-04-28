<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MemberRegister.aspx.cs" Inherits="CardDex2._0.Member.MemberRegister" %>
<%@ Register TagPrefix="cse" TagName="CaptchaControl" Src="~/Components/CaptchaControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Placeholder for additional head content -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <h2>Member Register</h2>
        <!-- Label to display error or success messages -->
        <asp:Label ID="lblMessage" runat="server" CssClass="error" EnableViewState="false"></asp:Label>
        <br />
        <!-- Label and input for username -->
        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <br />
        <!-- Label and input for password -->
        <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        <br />
        <!-- Captcha control to verify user is human -->
        <cse:CaptchaControl ID="UserControl" runat="server" />
        <br />
        <!-- Button to trigger the registration process -->
        <asp:Button ID="btnLogin" runat="server" Text="Register" OnClick="btnLogin_Click" />
    </div>
</asp:Content>
