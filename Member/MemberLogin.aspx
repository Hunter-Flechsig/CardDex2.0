<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MemberLogin.aspx.cs" Inherits="CardDex2._0.Member.MemberLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Placeholder for additional head content -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <h2>Member Login</h2>
        <!-- Label to display error or success messages -->
        <asp:Label ID="lblMessage" runat="server" CssClass="error" EnableViewState="false"></asp:Label>
        <br />
        <!-- Label and input for username -->
        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <br />
        <!-- Label and input for password -->
        <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <!-- Button to trigger login action -->
        <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" />
    </div>
</asp:Content>
