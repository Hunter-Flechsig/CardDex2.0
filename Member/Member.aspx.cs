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
        protected void Page_Load(object sender, EventArgs e)
        {
            manager = new UserManager();
            userCards = manager.GetPokemonCards("Ash");
            if (userCards.Count == 0)
            {
                lblCollection.Text = "No Cards in your Collection";
            }            
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
                    return JsonConvert.DeserializeObject<List<PokemonCard>>(result);
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