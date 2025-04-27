<%@ Page Title="Staff" MaintainScrollPositionOnPostback="true" Language="C#"
    MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="Staff.aspx.cs" Inherits="CardDex2._0.Staff.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <h1>Staff Page</h1>
        <h3>Add Staff Credentials</h3>
        <asp:Label ID="StaffUsername" runat="server" Text="Username:"></asp:Label>
        <asp:TextBox ID="StaffUsernameTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="StaffPassword" runat="server" Text="Password:"></asp:Label>
        <asp:TextBox ID="StaffPasswordTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="CredentialCreateButton" runat="server" Text="Create Staff Credentials" OnClick="CredentialCreateButton_Click" />
        <br />
        <asp:Label ID="Result" runat="server"></asp:Label>
    </div>
</asp:Content>

