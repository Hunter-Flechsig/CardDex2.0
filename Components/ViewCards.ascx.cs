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
        protected HiddenField hdnScrollPosition;

        // Property to get or set the selected card's ID
        public string SelectedCardId
        {
            get { return (string)ViewState["selectedCard"]; }
            set { ViewState["selectedCard"] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize control, bind data, etc.
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
            RepeaterCards.DataSource = Cards;
            RepeaterCards.DataBind();
        }

        // When a card is clicked
        protected void cardClicked(object sender, EventArgs e)
        {
            var clickedButton = (HtmlButton)sender;
            var item = (RepeaterItem)clickedButton.NamingContainer;

            var hiddenId = item.FindControl("HiddenCardID") as HiddenField;
            string cardId = hiddenId?.Value;

            if (!string.IsNullOrEmpty(cardId) && cardId != SelectedCardId)
            {
                SelectedCardId = cardId;
                BindCards();
            }
        }
    }
}