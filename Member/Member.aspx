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
    <!-- Description of the Member page functionality -->
    <p>The Member page allows users to track the pokemon cards that they own by adding the pokemon cards to the member page.
        The user can toggle add cards to begin adding cards to their account and search for their card from a particular set or 
        by pokemon name. Once the card is added, it will show up under the collection section. The user is able see what 
        cards they have from the collection section, and are be able to remove any cards from the collection associated to their account.
    </p>
    </div>
  <div style="display: flex; justify-content: center; margin-top: 10px">
    <!-- Placeholder for additional content -->
  </div>

  <!-- Expandable Search Area -->
  <div id="addCardContainer" class="add-card-container" runat="server">
    <h4>Add Pokémon Card</h4>
    <div class="inline-form">
      <!-- Input for Pokémon name -->
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
      <!-- Input for Pokémon set -->
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
      <!-- Button to search for Pokémon cards -->
      <asp:Button
        ID="btnSearchPokemon"
        runat="server"
        Text="Search"
        onClick="btnSearchPokemon_click"
        OnClientClick="showLoadingSpinner();"
        CssClass="search-button"
      />
      <!-- Button to add a Pokémon card -->
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
          <!-- Label to display search results -->
          <asp:Label
            ID="lblSearchPokemon"
            runat="server"
            CssClass="d-block mb-2"
          />
          <!-- Loading spinner -->
          <div id="loadingSpinner" style="display: none">
            <div class="loader"></div>
          </div>
        </div>

        <!-- Component to display search results -->
        <uc:ViewCards id="ViewCards1" runat="server" />
        <!-- Label to display errors when adding cards -->
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
      <!-- Input for searching cards by name -->
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
      <!-- Input for searching cards by set -->
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
      <!-- Button to search the user's collection -->
      <asp:Button
        ID="searchButton"
        runat="server"
        Text="Search"
        CssClass="search-button"
        onClick="btnSearchCollection_click"
      />
      <!-- Button to remove a card from the collection -->
      <asp:Button
        ID="btnRemoveCard"
        runat="server"  
        Text="Remove Card"
        CssClass="search-button"
        onclick="btnRemoveCard_click"
      />
      <!-- Button to toggle the add card section -->
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
        <!-- Label to display errors when removing cards -->
        <asp:Label
          ID="lblremoveError"
          runat="server"
          Text=""
          CssClass="error-label"
        />
        <!-- Component to display the user's collection -->
        <uc:ViewCards ID="ViewCards2" runat="server" />
      </ContentTemplate>
      <Triggers>
        <asp:PostBackTrigger ControlID="btnRemoveCard" />
        <asp:PostBackTrigger ControlID="searchButton" />
      </Triggers>
    </asp:UpdatePanel>
  </div>

  <style>
    /* Styling for the add card container */
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

    /* Styling for the card container */
    .card-container {
      max-height: 40vh; /* Restrict max height of the card container */
      overflow-y: auto; /* Allow scrolling for overflowing content */
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 8px;
      background: #fff;
    }

    /* Styling for error labels */
    .error-label {
      font-weight: bold;
      text-align: center;
      transition: opacity 1s ease;
    }

    /* Styling for inline forms */
    .inline-form {
      display: flex;
      gap: 5px;
      align-items: center;
      justify-content: center;
      flex-wrap: wrap;
      margin-bottom: 15px;
    }

    /* Styling for search buttons */
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
  </style>

  <script>
    // Displays the loading spinner during search
    function showLoadingSpinner() {
      var spinner = document.getElementById("loadingSpinner");
      var label = document.getElementById("<%= lblSearchPokemon.ClientID %>");

      if (spinner) spinner.style.display = "inline-block";
      if (label) label.innerText = "Searching...";
    }

    // Hides a label after a delay
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
