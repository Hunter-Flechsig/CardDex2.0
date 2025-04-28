<%@ Page Title="Member" MaintainScrollPositionOnPostback="true" Language="C#"
MasterPageFile="~/Site1.Master" AutoEventWireup="true"
CodeBehind="Member.aspx.cs" Async="true" Inherits="CardDex2._0.Member.Member" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <%@ Register TagName="ViewCards" TagPrefix="uc"
  src="~/Components/ViewCards.ascx" %>
</asp:Content>
<asp:Content
  ID="Content2"
  ContentPlaceHolderID="ContentPlaceHolder"
  runat="server"
>
    <div>
    <h2>Member Page Description</h2>
    <p>The Member page allows users to track the pokemon cards that they own by adding the pokemon cards to the member page.
        The user can toggle add cards to begin adding cards to their account and search for their card from a particular set or 
        by pokemon name. Once the card is added, it will show up under the collection section. The user is able see what 
        cards they have from the collection section, and are be able to remove any cards from the collection associated to their account.
    </p>
    </div>
  <div style="display: flex; justify-content: center; margin-top: 10px">
    
  </div>

  <!-- Expandable Search Area -->
  <div id="addCardContainer" class="add-card-container" runat="server">
    <h4>Add Pokémon Card</h4>
    <div class="inline-form">
      <asp:TextBox
        ID="txtPokemonName"
        runat="server"
        CssClass="form-control"
        Placeholder="Name"
        Text="Charizard"
        autocomplete="off"
        autocorrect="off"
        autocapitalize="off"
        spellcheck="false"
      />
      <asp:TextBox
        ID="txtSetName"
        runat="server"
        CssClass="form-control"
        Placeholder="Set"
        Text="Base Set"
        autocomplete="off"
        autocorrect="off"
        autocapitalize="off"
        spellcheck="false"
      />
      <asp:Button
        ID="btnSearchPokemon"
        runat="server"
        Text="Search"
        onClick="btnSearchPokemon_click"
        OnClientClick="showLoadingSpinner();"
        CssClass="search-button"
      />
        <asp:Button
          ID="btnAddCard"
          runat="server"
          Text="Add Card"
          onclick="btnAddCard_click"
          CssClass="search-button"
        />
    </div>

    <asp:UpdatePanel ID="UpdatePanelSearch" runat="server">
      <ContentTemplate>
        <div class="loading">
          <asp:Label
            ID="lblSearchPokemon"
            runat="server"
            CssClass="d-block mb-2"
          />
          <div id="loadingSpinner" style="display: none">
            <div class="loader"></div>
          </div>
        </div>

        <uc:ViewCards id="ViewCards1" runat="server" />
        <asp:Label
          ID="lbladdError"
          runat="server"
          Text=""
          CssClass="error-label"
        ></asp:Label>
      </ContentTemplate>
    </asp:UpdatePanel>
  </div>

  <div class="add-card-container">
    <h4>Collection</h4>
    <div class="inline-form">
      <asp:TextBox
        ID="searchUserCardsName"
        runat="server"
        CssClass="form-control"
        Placeholder="Name"
        autocomplete="off"
        autocorrect="off"
        autocapitalize="off"
        spellcheck="false"
      />
    <asp:TextBox
          ID="searchUserCardsSet"
          runat="server"
          CssClass="form-control"
          Placeholder="Set"
          autocomplete="off"
          autocorrect="off"
          autocapitalize="off"
          spellcheck="false"
        />
      <asp:Button
        ID="searchButton"
        runat="server"
        Text="Search"
        CssClass="search-button"
        onClick="btnSearchCollection_click"
      />
        <asp:Button
        ID="btnRemoveCard"
        runat="server"  
        Text="Remove Card"
        CssClass="search-button"
        onclick="btnRemoveCard_click"
        />
        <asp:Button
          runat="server"
          id="btnToggleAddCard"
          class="search-button"
          onClick="btnToggleAddCard_click"
          Text="Toggle Add Cards"
        />
    </div>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
      <ContentTemplate>
        <asp:Label
          ID="lblremoveError"
          runat="server"
          Text=""
          CssClass="error-label"
        />
        <uc:ViewCards ID="ViewCards2" runat="server" />
      </ContentTemplate>
      <Triggers>
        <asp:PostBackTrigger ControlID="btnRemoveCard" />
        <asp:PostBackTrigger ControlID="searchButton" />
      </Triggers>
    </asp:UpdatePanel>
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

    .error-label {
      font-weight: bold;
      text-align: center;
      transition: opacity 1s ease;
    }

    .inline-form {
      display: flex;
      gap: 5px;
      align-items: center;
      justify-content: center;
      flex-wrap: wrap;
      margin-bottom: 15px;
    }

    .inline-form input[type="text"] {
      padding: 8px 12px;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 14px;
      min-width: 30%;
    }

    .inline-form input[type="text"]:focus {
      outline: none;
      border-color: #007bff;
      box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.25);
    }

    .search-button {
      background-color: #007bff;
      color: white;
      border: none;
      padding: 8px 12px;
      border-radius: 4px;
      cursor: pointer;
      font-size: 14px;
      transition: background-color 0.2s ease;
      display: inline-block;

    }

    .search-button:hover {
      background-color: #0056b3;
    }

    .form-control {
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 14px;
      min-width: 200px;
    }

    .form-control:focus {
      outline: none;
      border-color: #007bff;
      box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.25);
    }

    h4 {
      margin-top: 0; /* Remove top margin */
      margin-bottom: 10px; /* Reduced bottom margin */
      font-size: 1.25rem;
    }

      .card-container label {
          display: block;
          font-weight: bold;
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
      0% {
        transform: rotate(0deg);
      }
      100% {
        transform: rotate(360deg);
      }
    }
  </style>

  <script>
    function showLoadingSpinner() {
      var spinner = document.getElementById("loadingSpinner");
      var label = document.getElementById("<%= lblSearchPokemon.ClientID %>");

      if (spinner) spinner.style.display = "inline-block";
      if (label) label.innerText = "Searching...";
    }
  </script>

  <script>
    function hideLabelAfterDelay(id, delay) {
      setTimeout(function () {
        var label = document.getElementById(id);
        if (label) {
          label.style.opacity = "0";
          setTimeout(function () {
            label.style.display = "none";
          }, 1000); // Wait for fade to finish
        }
      }, delay);
    }
  </script>
</asp:Content>
