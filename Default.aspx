<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CardDex2._0.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <h1>Pokemon News</h1>
        <asp:Button ID="TCGNewsButton" runat="server" Text="Pokemon TCG" OnClick="TCGNewsButton_Click" />
        <asp:Button ID="PokemonNewsButton" runat="server" Text="Pokemon" OnClick="PokemonNewsButton_Click" />
        <asp:Button ID="PokemonMobileNewsButton" runat="server" Text="Pokemon Mobile" OnClick="PokemonMobileNewsButton_Click" />
        <div>
            <br />            
            <div><asp:HyperLink ID="HyperLink1" runat="server" Text="UnloadedHyperLink"></asp:HyperLink></div>            
            <div><asp:HyperLink ID="HyperLink2" runat="server" Text="UnloadedHyperLink"></asp:HyperLink></div>
            <div><asp:HyperLink ID="HyperLink3" runat="server" Text="UnloadedHyperLink"></asp:HyperLink></div>
            <div><asp:HyperLink ID="HyperLink4" runat="server" Text="UnloadedHyperLink"></asp:HyperLink></div>
            <div><asp:HyperLink ID="HyperLink5" runat="server" Text="UnloadedHyperLink"></asp:HyperLink></div>
        </div>

        
        <h1>Service Directory</h1>
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
            <tr>
                <td>Tyler Nguyen</td>
                <td>WSDL Web Service</td>
                <td>NewsFocus Service - A service that returns links to news related to a topic - EndPoint: <a href="http://webstrar46.fulton.asu.edu/page10/NewsFocusService.svc">http://webstrar46.fulton.asu.edu/page10/NewsFocusService.svc</a></td>
                <td>Retrieves information from GDELT API; TryIt Page at: <a href="http://webstrar46.fulton.asu.edu/page10/">http://webstrar46.fulton.asu.edu/page10/</a>. Used in current page.</td>
            </tr>
            <tr>
                <td>Tyler Nguyen</td>
                <td>Captcha User Control</td>
                <td>Used To Verify If The User Registering is a Bot</td>
                <td>Used in Member Register</td>
            </tr>
            <tr>
                <td>Tyler Nguyen</td>
                <td>DLL class library for Encrypting</td>
                <td>Uses Symmetric Encryption to Encrypt and Decrypt User Passwords</td>
                <td>TryIt Page at: <a href="http://webstrar46.fulton.asu.edu/page10/">http://webstrar46.fulton.asu.edu/page10/</a>. Used in Member Register.</td>
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
