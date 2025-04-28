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
        NewsFocusServiceClient client;

        protected void Page_Load(object sender, EventArgs e)
        {
            client = new NewsFocusServiceClient();
            updateHyperLinks("Pokemon TCG");
        }

        protected void TCGNewsButton_Click(object sender, EventArgs e)
        {
            updateHyperLinks("Pokemon TCG");
        }

        protected void PokemonNewsButton_Click(object sender, EventArgs e)
        {
            updateHyperLinks("Pokemon Video Game");
        }

        protected void PokemonMobileNewsButton_Click(object sender, EventArgs e)
        {
            updateHyperLinks("Pokemon Mobile");
        }

        private void updateHyperLinks(string topic)
        {
            if (string.IsNullOrEmpty(topic)) { }
            string[] topicArray = new string[1] { topic };
            string[] links = client.NewsFocus(topicArray);
            HyperLink1.NavigateUrl = links[0];
            HyperLink2.NavigateUrl = links[1];
            HyperLink3.NavigateUrl = links[2];
            HyperLink4.NavigateUrl = links[3];
            HyperLink5.NavigateUrl = links[4];
            HyperLink1.Text = "News Link 1";
            HyperLink2.Text = "News Link 2";
            HyperLink3.Text = "News Link 3";
            HyperLink4.Text = "News Link 4";
            HyperLink5.Text = "News Link 5";
            //Uncomment once the NewsFocusTitles operation contract is seen
            //string[] titles = client.NewsFocusTitles(topicArray);
            //HyperLink1.Text = titles[0];
            //HyperLink2.Text = titles[1];
            //HyperLink3.Text = titles[2];
            //HyperLink4.Text = titles[3];
            //HyperLink5.Text = titles[4];
        }

        protected void EncryptButton_Click(object sender, EventArgs e)
        {
            string encryptedText = EncryptionDecryption.Encrypt(EncryptTextBox.Text);
            EncryptResult.Text = "Result: " + encryptedText;
        }

        protected void DecryptButton_Click(object sender, EventArgs e)
        {
            string decryptedText = EncryptionDecryption.Decrypt(DecryptTextBox.Text);
            DecryptResult.Text = "Result: " + decryptedText;
        }
    }
}