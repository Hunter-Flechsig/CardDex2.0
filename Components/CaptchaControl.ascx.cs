using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_5
{
	public partial class CaptchaControl : System.Web.UI.UserControl
	{
        private string captchaCode;
        protected void Page_Load(object sender, EventArgs e)
		{
            // Makes sure that the Captcha Is Being Loaded for the first time and is not reloaded wwhen clicking on verify
            if (!IsPostBack)
            {
                Session["CaptchaResult"] = false;
                GenerateCaptcha();
            }
        }
        private void GenerateCaptcha()
        {
            // Creates the Captcha from a bitmap with width of 100 and height of 30 with regular looking text,
            // could change to transfom the text or make the bitmap bigger as an image
            captchaCode = GenerateRandomCode();
            Bitmap bitmap = new Bitmap(200, 30);
            using (Graphics captcha = Graphics.FromImage(bitmap))
            {
                captcha.Clear(Color.White);
                captcha.DrawString(captchaCode, new Font("Arial", 20), Brushes.Black, new Point(10, 0));
            }
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Saves the bitmap to the memory stream
                bitmap.Save(memoryStream, ImageFormat.Png);
                string imageInBase64 = Convert.ToBase64String(memoryStream.ToArray());
                // Allows the image to be rendered from the URL
                CaptchaImage.ImageUrl = "data:image/png;base64," + imageInBase64;
            }
            Session["CaptchaCode"] = captchaCode;
        }

        private string GenerateRandomCode()
        {
            // Choose from capital letters and numbers, generate a string of length 3 for the Captcha
            const string chars = "QWERWTYUIOPASDDFGHJKLZXCVBNM0123456789";
            Random random = new Random();
            string randomCode = "";
            for (int ii = 0; ii <5; ii++)
            {
                // Selects a random number within the length of chars to pick a char from chars
                randomCode += chars[random.Next(chars.Length)];
            }
            return randomCode;
        }

        protected void VerifyButton_Click(object sender, EventArgs e)
        {
            string currentAttempt = CaptchaTextBox.Text;
            // Click the button and checks if the captcha is correct, if not, create a new Captcha Image
            if (currentAttempt == ((string)Session["CaptchaCode"]))
            {
                Session["CaptchaResult"] = true;
                ResultLabel.Text = "Correct!";
            }
            else
            {
                Session["CaptchaResult"] = false;
                ResultLabel.Text = "Incorrect! :(";
                GenerateCaptcha();
            }
        }
    }
}