<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaControl.ascx.cs" Inherits="Assignment_5.CaptchaControl" %>
<div>
<asp:Image ID="CaptchaImage" runat="server"/>
</div>     
     <asp:TextBox ID="CaptchaTextBox" runat="server"/>
     <asp:Button ID="VerifyButton" runat="server" Text="Verify" OnClick="VerifyButton_Click"/>
     <asp:Label ID="ResultLabel" runat="server"/>