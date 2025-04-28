<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CardDex2._0.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="testing">
        <h2>Welcome to CardDex!</h2>
        <!-- Introduction to the application -->
        <p>
            CardDex is a web application that allows you to search for and manage your Pokémon card collection. You can add cards, view details, and even get the latest news about Pokémon.
        </p>

        <h3>Signing Up:</h3>
        <!-- Instructions for signing up -->
        <p>
            To get started, you can sign up for an account by selecting register. After registering, you can log in to your account to access the member page.
        </p>

        <h3>Testing</h3>
        <!-- Instructions for testing member functionality -->
        <p>
            (Member Testing) As a member you can add cards to your collection by pressing add cards, then searching for your card by set and name (try Charizard and Base). 
            Then you can select a card and add it to your collection by pressing add. They will then show up in your collection. 
            You can also delete from your collection by pressing on a card and selecting the delete button. Your collection will persist even after logging out.
        </p>

        <!-- Instructions for testing staff functionality -->
        <p>
            (Staff Testing) Staff can log in with their credentials by selecting staff login (Username: “TA” and Password: Cse445! already in the system). From there, you can
            add more staff members and then try logging in with them. 
        </p>

        <!-- Authorization testing instructions -->
        <p>
            (Authorization) You will also see that buttons for accessing staff and member pages are not available after logging in. You can test
            authorization by logging in as a staff or member, then going to their respective pages (Member/Member.aspx or Staff/Staff.aspx).
        </p>

        <!-- News section instructions -->
        <p>
            (News) You can see Pokémon news by clicking on the buttons below. 
        </p>
    </div>

    <div>
        <h1>Pokemon News</h1>
        <!-- Buttons to fetch Pokémon news -->
        <asp:Button ID="TCGNewsButton" runat="server" Text="Pokemon TCG" OnClick="TCGNewsButton_Click" />
        <asp:Button ID="PokemonNewsButton" runat="server" Text="Pokemon" OnClick="PokemonNewsButton_Click" />
        <asp:Button ID="PokemonMobileNewsButton" runat="server" Text="Pokemon Mobile" OnClick="PokemonMobileNewsButton_Click" />
        <div>
            <br />
            <!-- Hyperlinks to display news -->
            <div>
                <asp:HyperLink ID="HyperLink1" runat="server" Text="UnloadedHyperLink"></asp:HyperLink>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink2" runat="server" Text="UnloadedHyperLink"></asp:HyperLink>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink3" runat="server" Text="UnloadedHyperLink"></asp:HyperLink>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink4" runat="server" Text="UnloadedHyperLink"></asp:HyperLink>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink5" runat="server" Text="UnloadedHyperLink"></asp:HyperLink>
            </div>
        </div>

        <h1>Service Directory</h1>
        <!-- Table summarizing application components -->
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
            <!-- Example row for a component -->
            <tr>
                <td>Hunter Flechsig</td>
                <td>User Control</td>
                <td>Display an array of Pokémon Card Objects. Show its image, name, set, and click on a card to select. Can see and test on member page.</td>
                <td>C# code behind GUI. Linked to Member page</td>
            </tr>
            <!-- Additional rows for other components -->
            <tr>
                <td>Hunter Flechsig</td>
                <td>Cookies</td>
                <td>Store auth information including role - Test by: 1. press F12 and navigate to application->storage->cookies 2. login 3. you will now see .ASPXAUTH cookie with your info and now you will be automatically redirected to page as well as header bar changing to reflect you are logged in</td>
                <td>GUI design and C# code behind GUI using FormsAuthenticationTicket. It is linked to the login page</td>
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
                <td>Retrieves links from GDELT API; public string[] newsFocus(string[] topics); Used in current page.</td>
            </tr>
            <tr>
                <td>Tyler Nguyen</td>
                <td>Captcha User Control</td>
                <td>Used To Verify If The User Registering is a Bot or user</td>
                <td>Used in <a href="/Member/MemberRegister.aspx">Member Register</a></td>
            </tr>
            <tr>
                <td>Tyler Nguyen</td>
                <td>DLL class library for Encrypting</td>
                <td>Uses Symmetric Encryption to Encrypt and Decrypt User Passwords</td>
                <td>public string Encrypt(string plaintext); public string Decrypt(string cyphertext); Used in Member Register and Login.</td>
            </tr>
            <tr>
                <td>Justin Yi</td>
                <td>User Control (Login)</td>
                <td>Displays a login window that allows for both Staff and Members to log in, using .xml files to store.</td>
                <td>private bool ValidateStaff(string username, string eteredPassword); private bool ValidateMember(string username, string enteredPassword); Used in Member and Staff Login.</td>
            </tr>
            <tr>
                <td>Justin Yi</td>
                <td>Web Service (WSDL)</td>
                <td>Word Filter Service - A service that can be used to filter out any bank of words. In this case, it is used to censor a username that contains common bad words. - EndPoint: <a href="http://webstrar46.fulton.asu.edu/page8/Service1.svc">http://webstrar46.fulton.asu.edu/page8/Service1.svc</a></td>
                <td>public string WordFilter(string toFilter), Used in Member and Staff Register.</td>
            </tr>
        </table>

        <div>
            <h3>TryIt Section</h3>
            <!-- Encryption and decryption testing section -->
            <asp:Label ID="Label1" runat="server" Text="public string Encrypt(string plaintext)"></asp:Label>
            <br />
            <asp:TextBox ID="EncryptTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="EncryptButton" runat="server" Text="Encrypt" OnClick="EncryptButton_Click" />
            <br />
            <asp:Label ID="EncryptResult" runat="server" Text="Result:"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" Text="public string Decrypt(string cyphertext)"></asp:Label>
            <br />
            <asp:TextBox ID="DecryptTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="DecryptButton" runat="server" Text="Decrypt" OnClick="DecryptButton_Click" />
            <br />
            <asp:Label ID="DecryptResult" runat="server" Text="Result:"></asp:Label>
        </div>
    </div>

    <style>
        /* Styling for the table */
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

        /* Styling for table cells */
        th, td {
            border-bottom: 1px solid grey;
            padding: 12px;
            text-align: left;
            border-right: 1px solid grey;
        }

        /* Remove line under the last row */
        tr:last-child td {
            border-bottom: none;
        }

        /* Styling for the testing section */
        .testing {
            margin-top: 20px;
            background: white;
            border: 1px solid #ccc;
            border-radius: 8px;
            padding: 20px;
        }
    </style>
</asp:Content>
