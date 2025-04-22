<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCards.ascx.cs" Inherits="CardDex2._0.Components.ViewCards" %>

            <asp:UpdatePanel ID="UpdatePanelCards" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

<!-- Repeater control to display Pokémon cards -->
<asp:Repeater ID="RepeaterCards" runat="server">
    <HeaderTemplate>
        <div class="card-grid">
    </HeaderTemplate>

    <ItemTemplate>
        


            
             <button runat="server" onserverclick="cardClicked" class="unstyled-button">
            <div id="CardDiv" runat="server" class='<%# GetCardCssClass(Eval("Id").ToString()) %>'>
                <!-- Card image, name, and set name -->
                <img src='<%# Eval("Image") %>' alt='<%# Eval("Name") %>' class="card-image" />
                <h3><%# Eval("Name") %></h3>
                <p><%# Eval("SetName") %></p>
                <!-- Hidden field for storing card ID -->
                <asp:HiddenField ID="HiddenCardID" runat="server" Value='<%# Eval("Id") %>' />
            </div>
             </button>



    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>

            </ContentTemplate>
            </asp:UpdatePanel>

<style>
    .card-grid {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
        gap: 7px;
    }

    .card {
        border: 1px solid #ddd;
        padding: 6px;
        text-align: center;
        border-radius: 8px;
        background: #f9f9f9;
    }

    .card:hover {
        box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);
        background-color: #eef;
    }

    .card.selected {
        border: 2px solid #007bff;
        background-color: #d0e7ff;
    }

    .card-image {
        width: auto;
        max-width: 100%;
        height: auto;
        max-height: 220px;
        object-fit: contain;
        border-radius: 4px;
        display: block;
        margin: 0 auto;
        opacity: 0;
        transition: opacity 0.3s ease-in;
    }

    .card-image[src] {
        opacity: 1;
    }


    .card h3 {
        margin: 4px 0 2px;
        font-size: 1rem;
    }

    .card p {
        margin: 2px 0;
        font-size: 0.85rem;
        color: #555;
    }

    .unstyled-button {
        all: unset;
        cursor: pointer;
        display: block;
        width: 100%;
        text-align: left;
    }

</style>