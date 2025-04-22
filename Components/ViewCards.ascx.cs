using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PokeFind.Models;

namespace CardDex2._0.Components
{
    public partial class ViewCards : System.Web.UI.UserControl
    {
        // Private field to store the list of cards
        private List<PokemonCard> _cards;

        // Public property to get and set the list of cards
        public List<PokemonCard> Cards
        {
            get { return _cards; }  // Return the current list of cards
            set
            {
                _cards = value;  // Set the list of cards
                BindCards();  // Automatically bind the data to the Repeater control
            }
        }

        // Method to bind the cards data to the Repeater control
        private void BindCards()
        {
            RepeaterCards.DataSource = _cards;  // Set the Repeater's data source
            RepeaterCards.DataBind();  // Bind the data to the Repeater control
        }
    }
}