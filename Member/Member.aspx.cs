using System;
using System.CodeDom;
using System.Collections.Generic;
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

        protected void Page_init(object sender, EventArgs e)
        {
            // Check if the user is logged in
            //if (Session["username"] == null)
            //{
            //    Response.Redirect("~/Login.aspx");
            //}
            //else
            //{
                //lblUsername.Text = Session["username"].ToString();
                manager = new UserManager();
                userCards = manager.GetPokemonCards("AshKetchum");
                if (userCards.Count == 0)
                {
                    lblCollection.Text = "No Cards in your Collection";
                }
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbladdError.Visible = false;
            }
            else
            {
                // Show label only if ViewState flag is set
                lbladdError.Visible = ViewState["ShowLabelOnce"] as bool? == true;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["ShowLabelOnce"] = false;
        }


        protected void btnAddCard_click(object sender, EventArgs e)
        {
            string selectedCardId = ViewCards1.SelectedCardId;
            List<PokemonCard> searchCards = ViewCards1.Cards;
            if (string.IsNullOrEmpty(selectedCardId))
            {
                lbladdError.ForeColor = System.Drawing.Color.Red; // Set label color to red for error
                setErrorLabel("Please select a card to add.", lbladdError);
                return;
            }

            PokemonCard selectedCard = searchCards.FirstOrDefault(card => card.Id == selectedCardId);
            if (selectedCard != null)
            {
                FetchReturnType result = manager.AddPokemon("AshKetchum", selectedCard);
                if (result.success != null)
                {
                    lbladdError.ForeColor = System.Drawing.Color.Green; // Set label color to green for success
                    setErrorLabel($"Added {selectedCard.Name} to your collection.", lbladdError);
                    userCards = manager.GetPokemonCards("AshKetchum");
                }
                else if (result.error != null)
                {
                    setErrorLabel(result.error, lbladdError);
                    lbladdError.ForeColor = System.Drawing.Color.Red; // Set label color to red for error
                }
            }
        }

        private void setErrorLabel(string text, Label label)
        {
            ViewState["ShowLabelOnce"] = true;
            label.Text = text;
            label.Visible = true;

            // Inject JavaScript to hide it after 5 seconds (5000ms)
            ScriptManager.RegisterStartupScript(this, GetType(), "HideLabel", "hideLabelAfterDelay('" + label.ClientID + "', 1000);", true);
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
                HttpResponseMessage response =  client.GetAsync(url).Result;

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
