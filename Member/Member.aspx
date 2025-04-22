<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Async="true" Inherits="CardDex2._0.Member.Member" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register TagName="ViewCards" TagPrefix="uc" src="~/Components/ViewCards.ascx" %>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap JS Bundle (includes Popper) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div style="display: flex; justify-content: center; margin-top: 10px;">
        <asp:Button runat="server" id="btnToggleAddCard" class="search-button" onclick="btnToggleAddCard_click" Text="Toggle Add Cards" />
    </div>
    
    <!-- Expandable Search Area -->
    <div id="addCardContainer" class="add-card-container" runat="server">
        <h4>Add Pokémon Card</h4>
        <div class="inline-form">
            <asp:TextBox ID="txtPokemonName" runat="server" CssClass="form-control" Placeholder="Name" />
            <asp:TextBox ID="txtSetName" runat="server" CssClass="form-control" Placeholder="Set" />
            <asp:Button ID="btnSearchPokemon" runat="server" Text="Search" onClick="btnSearchPokemon_click"
                 OnClientClick="showLoadingSpinner();" CssClass="search-button"/>

        </div>
            
        <asp:UpdatePanel ID="UpdatePanelSearch" runat="server">
            <ContentTemplate>
                <div class="loading">
                    <asp:Label ID="lblSearchPokemon" runat="server" CssClass="d-block mb-2" />
                    <div id="loadingSpinner" style="display: none;">
                        <div class="loader"></div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="card-container">
            <uc:ViewCards id="ViewCards1" runat="server" />
        </div>
    </div>
    
    <div class="add-card-container">
         <h4>Collection</h4>
         <asp:Label ID="lblCollection" runat="server" CssClass="d-block mb-2" />
         <uc:ViewCards id="ViewCards2" runat="server" />
    </div>

<style>
    .add-card-container {
        border: 1px solid #ccc;
        padding: 20px;
        border-radius: 8px;
        background-color: #fefefe;
        margin: 10px auto;
        box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
        display: flex;
        justify-content: center;
        flex-direction: column;
                text-align: center;
    }

    .card-container {
        max-height: 40vh; /* Restrict max height of the card container */
        overflow-y: auto; /* Allow scrolling for overflowing content */
        padding: 8px;
        border: 1px solid #ccc;
        border-radius: 8px;
        background: #fff;
    }


    .inline-form {
        display: flex;
        gap: 10px;
        align-items: center;
        justify-content: center;
        flex-wrap: wrap;
        margin-bottom: 15px;
    }

    .inline-form input[type="text"],
    .inline-form input[type="submit"] {
        padding: 6px 10px;
        font-size: 14px;
    }

    h4 {
        margin-bottom: 15px;
        font-size: 1.25rem;
    }

    .card-container label {
        display: block;
        margin-top: 10px;
        font-weight: bold;
    }

    .inline-form {
        display: flex;
        gap: 10px; /* spacing between items */
        align-items: center;
        flex-wrap: wrap; /* wrap on small screens */
    }

    .inline-form input[type="text"] {
        width: 30%;
        padding: 6px;
    }

    .search-button {
        background-color: deepskyblue;
        color: black;
        border: none;
        padding: 10px 15px;
        border-radius: 5px;
        cursor: pointer;
        font-size: 1em;
        text-align: center; /* Centers text within the button */
    }

    .search-button: hover {
        background-color: lightskyblue;
    }

    .loader {
        border: 4px solid #f3f3f3;
        border-top: 4px solid #3498db;
        border-radius: 50%;
        width: 28px;
        height: 28px;
        animation: spin 0.8s linear infinite;
        display: inline-block;
    }

    .loading {
        display: flex;
        gap: 10px; /* spacing between items */
        align-items: center;
        flex-wrap: wrap; /* wrap on small screens */
    }

    @keyframes spin {
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }
</style>

<script>
    function showLoadingSpinner() {
    var spinner = document.getElementById("loadingSpinner");
    var label = document.getElementById('<%= lblSearchPokemon.ClientID %>');

    if (spinner) spinner.style.display = "inline-block";
    if (label) label.innerText = "Searching...";
}
</script>
    
</asp:Content>


