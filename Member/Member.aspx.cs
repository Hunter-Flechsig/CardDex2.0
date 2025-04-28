using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardDex2._0.Data;
using Newtonsoft.Json;
using PokeFind.Models;

namespace CardDex2._0.Member
{
    public partial class Member : System.Web.UI.Page
    {
        private UserManager manager; // Manages user-related operations
        private List<PokemonCard> userCards; // Stores the user's Pokemon cards
        private string user; // Stores the current user's identity

        // Initializes the page and loads user data
        protected void Page_init(object sender, EventArgs e)
        {
            // Redirect to login page if the user is not authenticated
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Member/MemberLogin.aspx");
            }

            user = User.Identity.Name; // Get the logged-in user's name
            manager = new UserManager(); // Initialize the UserManager

            // Load the user's Pokemon cards
            userCards = manager.GetPokemonCards(user);

            // Display a message if no cards are found
            if (userCards.Count == 0)
            {
                lblremoveError.Text = "No Cards in your Collection";
            }
            else
            {
                ViewCards2.Cards = userCards; // Bind cards to the view
            }

            // Set container heights for card views
            ViewCards1.ContainerHeight = "40vh";
            ViewCards2.ContainerHeight = "70vh";

            addCardContainer.Visible = false; // Hide the add card container by default
        }

        // Handles page load events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Hide error labels on initial load
                lbladdError.Visible = false;
                lblremoveError.Visible = false;
            }
            else
            {
                // Show error labels based on ViewState flags
                lbladdError.Visible = ViewState["ShowLabelOnce_lbladdError"] as bool? == true;
                lblremoveError.Visible = ViewState["ShowLabelOnce_lblremoveError"] as bool? == true;
            }
        }

        // Resets ViewState flags before rendering the page
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["ShowLabelOnce_lbladdError"] = false;
            ViewState["ShowLabelOnce_lblremoveError"] = false;
        }

        // Adds a selected card to the user's collection
        protected void btnAddCard_click(object sender, EventArgs e)
        {
            string selectedCardId = ViewCards1.SelectedCardId; // Get the selected card ID
            List<PokemonCard> searchCards = ViewCards1.Cards; // Get the list of cards in the search view

            // Check if a card is selected
            if (string.IsNullOrEmpty(selectedCardId))
            {
                setErrorLabel("Please select a card to add.", lbladdError, Color.Red);
                return;
            }

            // Find the selected card
            PokemonCard selectedCard = searchCards.FirstOrDefault(card => card.Id == selectedCardId);
            if (selectedCard != null)
            {
                // Add the card to the user's collection
                FetchReturnType result = manager.AddPokemon(user, selectedCard);

                if (result.success != null)
                {
                    setErrorLabel($"Added {selectedCard.Name} to your collection.", lbladdError, Color.Green);
                    userCards = manager.GetPokemonCards(user); // Refresh the user's cards
                    ViewCards2.Cards = userCards; // Update the view
                }
                else if (result.error != null)
                {
                    setErrorLabel(result.error, lbladdError, Color.Red);
                }
            }
        }

        // Searches the user's card collection based on name and set
        protected void btnSearchCollection_click(object sender, EventArgs e)
        {
            string nameSearch = searchUserCardsName.Text.Trim(); // Get the name search input
            string setSearch = searchUserCardsSet.Text.Trim(); // Get the set search input

            // Load user cards if not already loaded
            if (userCards == null)
            {
                userCards = manager.GetPokemonCards(user);
            }

            // Filter cards based on search criteria
            var filteredCards = userCards.AsQueryable();

            if (!string.IsNullOrEmpty(nameSearch))
            {
                filteredCards = filteredCards.Where(c => c.Name.IndexOf(nameSearch, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrEmpty(setSearch))
            {
                filteredCards = filteredCards.Where(c => c.SetName.IndexOf(setSearch, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Update the view with the filtered cards
            var resultCards = filteredCards.ToList();
            ViewCards2.Cards = resultCards;

            // Display search results
            if (resultCards.Count == 0)
            {
                lblremoveError.Text = "No cards found matching your search criteria";
                lblremoveError.Visible = true;
            }
            else
            {
                lblremoveError.ForeColor = Color.Black;
                lblremoveError.Visible = true;
                lblremoveError.Text = $"Found {resultCards.Count} cards";
            }
        }

        // Sets an error label with a message and color
        private void setErrorLabel(string text, Label label, Color color)
        {
            ViewState["ShowLabelOnce_" + label.ID] = true; // Set ViewState flag
            label.Text = text; // Set the label text
            label.Visible = true; // Make the label visible
            label.ForeColor = color; // Set the label color

            // Inject JavaScript to hide the label after 5 seconds
            ScriptManager.RegisterStartupScript(this, GetType(), $"HideLabel_{label.ID}", "hideLabelAfterDelay('" + label.ClientID + "', 1000);", true);
        }

        // Toggles the visibility of the add card container
        protected void btnToggleAddCard_click(object sender, EventArgs e)
        {
            addCardContainer.Visible = !addCardContainer.Visible;
        }

        // Searches for Pokemon cards based on set and name
        protected void btnSearchPokemon_click(object sender, EventArgs e)
        {
            if ((txtSetName.Text.Trim() == "" || txtPokemonName.Text.Trim() == ""))
            {
                return;
            }

            var cards = GetCards(txtSetName.Text, txtPokemonName.Text);

            if (cards == null || cards.Count == 0)
            {
                lblSearchPokemon.Text = "No cards found.";
                ViewCards1.Cards = null;
            }
            else
            {
                lblSearchPokemon.Text = $"Found {cards.Count} Cards";
                ViewCards1.Cards = cards;
            }
        }

        // Removes a selected card from the user's collection
        protected void btnRemoveCard_click(object sender, EventArgs e)
        {
            string selectedCardId = ViewCards2.SelectedCardId; // Get the selected card ID
            List<PokemonCard> userCards = ViewCards2.Cards; // Get the user's cards

            // Check if a card is selected
            if (string.IsNullOrEmpty(selectedCardId))
            {
                setErrorLabel("Please select a card to remove.", lblremoveError, Color.Red);
                return;
            }

            // Remove the card from the user's collection
            FetchReturnType res = manager.RemovePokemon(user, selectedCardId);

            if (res.error != null)
            {
                setErrorLabel(res.error, lblremoveError, Color.Red);
            }
            else if (res.success != null)
            {
                setErrorLabel(res.success, lblremoveError, Color.Green);
                userCards = manager.GetPokemonCards(user); // Refresh the user's cards
                ViewCards2.Cards = userCards; // Update the view
            }
        }

        // Fetches cards from an external API based on set and name
        private List<PokemonCard> GetCards(string set, string name)
        {
            string encodedSet = Uri.EscapeDataString(set); // Encode the set name
            string encodedName = Uri.EscapeDataString(name); // Encode the card name

            // Construct the API URL
            string url = $"http://webstrar46.fulton.asu.edu/page9/api/PokeFind/{encodedSet}x{encodedName}";

            // Send a GET request to the API
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var cards = JsonConvert.DeserializeObject<List<PokemonCard>>(result); // Deserialize the response
                    return cards;
                }
                else
                {
                    return null; // Return null if the request fails
                }
            }
        }
    }
}
