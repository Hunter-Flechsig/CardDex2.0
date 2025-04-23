using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using PokeFind.Models;

namespace CardDex2._0.Components
{
    public partial class ViewCards : System.Web.UI.UserControl
    {
        // Property to get or set the selected card's ID
        public string SelectedCardId
        {
            get { return ViewState["selectedCard"] as string ?? string.Empty; }
            set
            {
                ViewState["selectedCard"] = value;
                // Also store in the hidden field for consistency
                if (hdnSelectedCard != null)
                    hdnSelectedCard.Value = value;
            }
        }

        // Property to hold the list of cards
        public List<PokemonCard> Cards
        {
            get { return ViewState["Cards"] as List<PokemonCard>; }
            set
            {
                ViewState["Cards"] = value;
                BindCards();
            }
        }

        // Event that fires when a card is selected
        public event EventHandler<CardSelectedEventArgs> CardSelected;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize control
                if (!string.IsNullOrEmpty(SelectedCardId) && hdnSelectedCard != null)
                {
                    hdnSelectedCard.Value = SelectedCardId;
                }
            }
        }

        // Dynamically set card CSS class
        protected string GetCardCssClass(string cardId)
        {
            return cardId == SelectedCardId ? "card selected" : "card";
        }

        // Bind data to the repeater
        private void BindCards()
        {
            if (Cards != null)
            {
                RepeaterCards.DataSource = Cards;
                RepeaterCards.DataBind();
                UpdatePanelCards.Update();
            }
        }

        // When a card is clicked
        protected void cardClicked(object sender, EventArgs e)
        {
            // Get the selected card ID from the hidden field
            string cardId = hdnSelectedCard.Value;

            if (!string.IsNullOrEmpty(cardId))
            {
                // Set the selected card ID
                SelectedCardId = cardId;

                // Force rebind to update the UI with selection
                BindCards();

                // Raise event to notify parent page of card selection
                OnCardSelected(new CardSelectedEventArgs(cardId));
            }
        }

        // Method to raise the CardSelected event
        protected virtual void OnCardSelected(CardSelectedEventArgs e)
        {
            CardSelected?.Invoke(this, e);
        }

        // Public method to refresh the display
        public void RefreshCards()
        {
            BindCards();
        }

        // Custom event args class for card selection
        public class CardSelectedEventArgs : EventArgs
        {
            public string CardId { get; private set; }

            public CardSelectedEventArgs(string cardId)
            {
                CardId = cardId;
            }
        }

        // Find a card by ID
        public PokemonCard GetCardById(string cardId)
        {
            if (Cards != null)
            {
                return Cards.FirstOrDefault(c => c.Id == cardId);
            }
            return null;
        }
    }
}