<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCards.ascx.cs" Inherits="CardDex2._0.Components.ViewCards" %>
<asp:UpdatePanel ID="UpdatePanelCards" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSearchPokemon" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnAddCard" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <div class="card-container" id="scrollDiv">
            <asp:HiddenField ID="hdnScrollPos" runat="server" />
            <asp:HiddenField ID="hdnSelectedCard" runat="server" Value="" />
            <asp:Button ID="btnCardSelected" runat="server" OnClick="cardClicked" Style="display: none" />
            
            <asp:Repeater ID="RepeaterCards" runat="server">
                <HeaderTemplate>
                    <div class="card-grid">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="unstyled-button" onclick="selectCard('<%# Eval("Id") %>')">
                        <div id='card-<%# Eval("Id") %>' class='<%# GetCardCssClass(Eval("Id").ToString()) %>'>
                            <img src='<%# Eval("Image") %>' alt='<%# Eval("Name") %>' class="card-image" />
                            <h3><%# Eval("Name") %></h3>
                            <p><%# Eval("SetName") %></p>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    var isScrolling = false;
    var scrollTimeout;

    function selectCard(cardId) {
        // Save scroll position
        var scrollDiv = document.getElementById('scrollDiv');
        document.getElementById('<%= hdnScrollPos.ClientID %>').value = scrollDiv.scrollTop;
        
        // Set the selected card ID in hidden field
        document.getElementById('<%= hdnSelectedCard.ClientID %>').value = cardId;
        
        // Trigger server-side event
        __doPostBack('<%= btnCardSelected.UniqueID %>', '');
    }

    // For UpdatePanel partial postbacks
    function setupScrollHandling() {
        if (typeof Sys !== 'undefined') {
            var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
            pageRequestManager.add_beginRequest(function() {
                // Set scrolling flag when postback begins
                isScrolling = true;
            });
            pageRequestManager.add_endRequest(function() {
                // Clear any existing timeout
                if (scrollTimeout) {
                    clearTimeout(scrollTimeout);
                }
                
                // Restore scroll position after a short delay
                scrollTimeout = setTimeout(function() {
                    restoreScrollPosition();
                    isScrolling = false;
                }, 50);
            });
        }
    }

    function restoreScrollPosition() {
        var scrollDiv = document.getElementById('scrollDiv');
        var savedPosition = document.getElementById('<%= hdnScrollPos.ClientID %>').value;
        if (savedPosition && scrollDiv && !isScrolling) {
            scrollDiv.scrollTop = parseInt(savedPosition);
        }
    }

    // Execute on page load
    if (window.addEventListener) {
        window.addEventListener('load', function () {
            setupScrollHandling();
            restoreScrollPosition();
        });
    } else {
        window.attachEvent('onload', function () {
            setupScrollHandling();
            restoreScrollPosition();
        });
    }
</script>

<style>
    .card-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
        gap: 7px;
        width: 100%;
    }
    .add-card-container {
        transition: opacity 0.3s ease;
    }
    .card {
        border: 1px solid #ddd;
        padding: 6px;
        text-align: center;
        border-radius: 8px;
        background: #f9f9f9;
        box-sizing: border-box;
        width: 100%;
        height: 100%;
        transition: none;
        transform: translateZ(0);
        margin: 0;
    }
    .card:hover {
        box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);
        background-color: #eef;
    }
    .card.selected {
        border: 2px solid #007bff;
        background-color: #d0e7ff;
        padding: 5px;
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
        opacity: 1;
        transition: none;
    }
    .card h3 {
        margin: 4px 0 2px;
        font-size: 1rem;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }
    .card p {
        margin: 2px 0;
        font-size: 0.85rem;
        color: #555;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }
    .unstyled-button {
        cursor: pointer;
        display: block;
        width: 100%;
        box-sizing: border-box;
        margin: 0;
        padding: 0;
        background: none;
        border: none;
    }
    .card-container {
        max-height: 40vh; /* Restrict max height of the card container */
        overflow-y: auto; /* Allow scrolling for overflowing content */
        padding: 8px;
        border: 1px solid #ccc;
        border-radius: 8px;
        background: #fff;
    }
</style>