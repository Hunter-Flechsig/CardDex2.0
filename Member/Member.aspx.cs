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
        private UserManager manager;
        private List<PokemonCard> userCards;
        private string user;

        protected void Page_init(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Member/MemberLogin.aspx");
            }
            user = User.Identity.Name;
            manager = new UserManager();
            //userCards = manager.GetPokemonCards("AshKetchum");
            userCards = manager.GetPokemonCards(user);
            if (userCards.Count == 0)
            {
                lblremoveError.Text = "No Cards in your Collection";
            }
            else
            {
                ViewCards2.Cards = userCards;
            }
            ViewCards1.ContainerHeight = "40vh"; // Set the height of the card container
            ViewCards2.ContainerHeight = "70vh"; // Set the height of the card container
            addCardContainer.Visible = false;
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lbladdError.Visible = false;
                lblremoveError.Visible = false;
            }
            else
            {
                // Show label only if ViewState flag is set
                lbladdError.Visible = ViewState["ShowLabelOnce_lbladdError"] as bool? == true;
                lblremoveError.Visible = ViewState["ShowLabelOnce_lblremoveError"] as bool? == true;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["ShowLabelOnce_lbladdError"] = false;
            ViewState["ShowLabelOnce_lblremoveError"] = false;
        }


        protected void btnAddCard_click(object sender, EventArgs e)
        {
            string selectedCardId = ViewCards1.SelectedCardId;
            List<PokemonCard> searchCards = ViewCards1.Cards;
            if (string.IsNullOrEmpty(selectedCardId))
            {
                setErrorLabel("Please select a card to add.", lbladdError, Color.Red);
                return;
            }

            PokemonCard selectedCard = searchCards.FirstOrDefault(card => card.Id == selectedCardId);
            if (selectedCard != null)
            {
                //FetchReturnType result = manager.AddPokemon("AshKetchum", selectedCard);
                FetchReturnType result = manager.AddPokemon(user, selectedCard);
                if (result.success != null)
                {
                    setErrorLabel($"Added {selectedCard.Name} to your collection.", lbladdError, Color.Green);
                    //userCards = manager.GetPokemonCards("AshKetchum");
                    userCards = manager.GetPokemonCards(user);
                    ViewCards2.Cards = userCards;
                }
                else if (result.error != null)
                {
                    setErrorLabel(result.error, lbladdError, Color.Red);
                }
            }
        }

        // Update the Contains method calls to use string.IndexOf for case-insensitive comparison
        // and check if the result is greater than or equal to 0.

        protected void btnSearchCollection_click(object sender, EventArgs e)
        {
            string nameSearch = searchUserCardsName.Text.Trim();
            string setSearch = searchUserCardsSet.Text.Trim();

            // Get all user cards if not already loaded
            if (userCards == null)
            {
                //userCards = manager.GetPokemonCards("AshKetchum");
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

            // Convert back to list and update ViewCards2
            var resultCards = filteredCards.ToList();
            ViewCards2.Cards = resultCards;

            // Update the collection label
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

        private void setErrorLabel(string text, Label label, Color color)
        {
            ViewState["ShowLabelOnce_" + label.ID] = true;
            label.Text = text;
            label.Visible = true;
            label.ForeColor = color; // Set label color based on the error type

            // Inject JavaScript to hide it after 5 seconds (5000ms)
            ScriptManager.RegisterStartupScript(this, GetType(), $"HideLabel_{label.ID}", "hideLabelAfterDelay('" + label.ClientID + "', 1000);", true);
        }

        protected void btnToggleAddCard_click(object sender, EventArgs e)
        {
            addCardContainer.Visible = !addCardContainer.Visible;
        }

        string lastBaseSearch = "";
        string lastNameSearch = "";
        protected void btnSearchPokemon_click(object sender, EventArgs e)
        {
            if (lastBaseSearch == txtSetName.Text && lastNameSearch == txtPokemonName.Text ||
                (txtSetName.Text.Trim() == "" || txtPokemonName.Text.Trim() == ""))
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

        protected void btnRemoveCard_click(object sender, EventArgs e)
        {
            ;
            string selectedCardId = ViewCards2.SelectedCardId;
            List<PokemonCard> userCards = ViewCards2.Cards;
            if (string.IsNullOrEmpty(selectedCardId))
            {
                setErrorLabel("Please select a card to remove.", lblremoveError, Color.Red);
                return;
            }

            //FetchReturnType res = manager.RemovePokemon("AshKetchum", selectedCardId);
            FetchReturnType res = manager.RemovePokemon(user, selectedCardId);

            if (res.error != null)
            {
                setErrorLabel(res.error, lblremoveError, Color.Red);
            }
            else if (res.success != null)
            {
                setErrorLabel(res.success, lblremoveError, Color.Green);
                //userCards = manager.GetPokemonCards("AshKetchum");
                userCards = manager.GetPokemonCards(user);
                ViewCards2.Cards = userCards;
            }

        }

        private static readonly HttpClient HttpClient = new HttpClient();
        private List<PokemonCard> GetCards(string set, string name)
        {
            string encodedSet = Uri.EscapeDataString(set);
            string encodedName = Uri.EscapeDataString(name);
            // Construct the API URL using the provided set and name
            string url = $"http://webstrar46.fulton.asu.edu/page9/api/PokeFind/{encodedSet}x{encodedName}";

            // Create a new HttpClient to send the GET request
            using (HttpClient client = new HttpClient())
            {
                // Send the GET request and wait for the response
                HttpResponseMessage response = client.GetAsync(url).Result;

                // Check if the response was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string result = response.Content.ReadAsStringAsync().Result;
                    // Deserialize the JSON response into a list of CardModel objects and return it
                    var cards = JsonConvert.DeserializeObject<List<PokemonCard>>(result);
                    return cards;
                }
                else
                {
                    // If the response wasn't successful, return null
                    return null;
                }
            }
        }
    }



}
