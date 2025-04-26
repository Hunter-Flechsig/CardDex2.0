<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CardDex2._0.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <h1>
            Pokemon News
        </h1>

        <h1>
            Service Directory
        </h1>
        
    <table>
  <tr>
    <th colspan="4">Application and Components Summary Table</th>
  </tr>
  <tr>
    <th>Provider Name</th>
    <th>Type</th>
    <th>Description</th>
    <th>Resources, Methods, and Use</th>
  </tr>
  <tr>
    <td>Hunter Flechsig</td>
    <td>User Control</td>
    <td>Display an array of Pokemon Card Objects. Show its image, name, set, and click on a card to select</td>
    <td>C# code behind GUI. Liked to Member page</td>
  </tr>
  <tr>
    <td>Hunter Flechsig</td>
    <td>REST Web Service</td>
    <td>PokeFind Service - Pokémon Card Searching Service deployed to webstar - EndPoint: <a href="http://webstrar46.fulton.asu.edu/page9/api/PokeFind/basexcharizard">http://webstrar46.fulton.asu.edu/page9/api/PokeFind/{set}x{name}</a></td>
    <td>Retrieve information from PokemonTCG API at: <a href="https://pokemontcg.io/">https://pokemontcg.io/</a>. Linked into Member Page</td>
  </tr>
</table>

    </div>

    <style>
  table {
    width: auto;
    border-collapse: separate;
    border-spacing: 0;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
    border-radius: 8px;
    overflow: hidden;
    background: white;
    border: 1px solid #ccc;
border-radius: 8px;
  }
  th, td {
    border-bottom: 1px solid grey;
    padding: 12px;
    text-align: left;
    border-right: 1px solid grey; 
  }
  tr:last-child td {
    border-bottom: none; /* Remove line under last row */
  }
</style>
</asp:Content>
