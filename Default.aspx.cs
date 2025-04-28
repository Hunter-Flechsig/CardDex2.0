using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardDex2._0.NewsFocus;
using DLLLibrary;

namespace CardDex2._0
{
    public partial class Default : System.Web.UI.Page
    {
        NewsFocusServiceClient client; // Client to interact with the NewsFocus service

        // Handles the page load event and initializes the NewsFocus client
        NewsFocusServiceClient client;
        // inputs particular topics for the news service
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new NewsFocusServiceClient(); // Initialize the service client
            updateHyperLinks("Pokemon TCG"); // Load default news links for "Pokemon TCG"
        }

        // Handles the click event for the "Pokemon TCG" news button
        protected void TCGNewsButton_Click(object sender, EventArgs e)
        {
            updateHyperLinks("Pokemon TCG"); // Update links for "Pokemon TCG" news
        }

        // Handles the click event for the "Pokemon Video Game" news button
        protected void PokemonNewsButton_Click(object sender, EventArgs e)
        {
            updateHyperLinks("Pokemon Video Game"); // Update links for "Pokemon Video Game" news
        }

        // Handles the click event for the "Pokemon Mobile" news button
        protected void PokemonMobileNewsButton_Click(object sender, EventArgs e)
        {
            updateHyperLinks("Pokemon Mobile"); // Update links for "Pokemon Mobile" news
        }

        // Updates the hyperlinks on the page with news links for the specified topic
        // updates news service with appropriate links
        private void updateHyperLinks(string topic)
        {
            if (string.IsNullOrEmpty(topic)) return; // Exit if the topicn is null or empty

            string[] topicArray = new string[1] { topic }; // Create an array with the topic
            string[] links = client.NewsFocus(topicArray); // Fetch news links from the service

            // Update the hyperlinks with the fetched links
            HyperLink1.NavigateUrl = links[0];
            HyperLink2.NavigateUrl = links[1];
            HyperLink3.NavigateUrl = links[2];
            HyperLink4.NavigateUrl = links[3];
            HyperLink5.NavigateUrl = links[4];

            // Set default text for the hyperlinks
            HyperLink1.Text = "News Link 1";
            HyperLink2.Text = "News Link 2";
            HyperLink3.Text = "News Link 3";
            HyperLink4.Text = "News Link 4";
            HyperLink5.Text = "News Link 5";

            // Uncomment the following code to use titles from the service (if available)
            // string[] titles = client.NewsFocusTitles(topicArray);
            // HyperLink1.Text = titles[0];
            // HyperLink2.Text = titles[1];
            // HyperLink3.Text = titles[2];
            // HyperLink4.Text = titles[3];
            // HyperLink5.Text = titles[4];
        }

        // Encrypts the input text and displays the result
        // Used for tryit section for DLL library 
        protected void EncryptButton_Click(object sender, EventArgs e)
        {
            string encryptedText = EncryptionDecryption.Encrypt(EncryptTextBox.Text); // Encrypt the input text
            EncryptResult.Text = "Result: " + encryptedText; // Display the encrypted result
        }

        // Decrypts the input text and displays the result
        protected void DecryptButton_Click(object sender, EventArgs e)
        {
            string decryptedText = EncryptionDecryption.Decrypt(DecryptTextBox.Text); // Decrypt the input text
            DecryptResult.Text = "Result: " + decryptedText; // Display the decrypted result
        }
    }
}