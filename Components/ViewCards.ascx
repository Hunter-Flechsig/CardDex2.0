<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCards.ascx.cs" Inherits="CardDex2._0.Components.ViewCards" %>
<!-- Repeater control to display Pokémon cards -->
<asp:Repeater ID="RepeaterCards" runat="server">
    <HeaderTemplate>
        <div class="card-grid">
    </HeaderTemplate>

    <ItemTemplate>
        <div class="card">
            <!-- Card image, name, and set name -->
            <img src='<%# Eval("Image") %>' alt='<%# Eval("Name") %>' class="card-image" />
            <h3><%# Eval("Name") %></h3>
            <p><%# Eval("SetName") %></p>
        </div>
    </ItemTemplate>

    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>

<style>
    .card-grid {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
        gap: 5px;
    }

    .card {
        border: 1px solid #ddd;
        padding: 6px;
        text-align: center;
        border-radius: 8px;
        background: #f9f9f9;
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
</style>