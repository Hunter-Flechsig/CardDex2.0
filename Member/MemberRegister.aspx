<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MemberRegister.aspx.cs" Inherits="CardDex2._0.Member.MemberRegister" %>
<%@ Register TagPrefix="cse" TagName="CaptchaControl" Src="~/Components/CaptchaControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <h2>Member Register</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="error" EnableViewState="false"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        <br />
        <cse:CaptchaControl ID="UserControl" runat="server" />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Register" OnClick="btnLogin_Click" />
    </div>
</asp:Content>
