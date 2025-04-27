using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.IO;
using PokeFind.Models;
using DLLLibrary;

namespace CardDex2._0.Data
{
    public class UserManager
    {
        string filePath = HttpRuntime.AppDomainAppPath + @"\Data\Members.xml";
        private XDocument doc;

        public UserManager()
        {
            if (File.Exists(filePath))
                doc = XDocument.Load(filePath);
            else
                doc = new XDocument(new XElement("Users"));
        }

        public void SaveChanges()
        {
            doc.Save(filePath);
        }

        public FetchReturnType AddUser(string username, string password)
        {
            if (UserExists(username)) return new FetchReturnType(error: "Username already being used");

            System.Diagnostics.Debug.WriteLine("hello");

            var user = new XElement("User",
                new XAttribute("username", username),
                new XAttribute("password", password),
                new XElement("PokemonCards")
            );

            doc.Root.Add(user);
            SaveChanges();
            return new FetchReturnType(success: "User added successfully"); 
        }

        public bool UserExists(string username)
        {
            return doc.Root.Elements("User")
                     .Any(u => u.Attribute("username")?.Value == username);
        }

        public void RemoveUser(string username)
        {
            var user = GetUserElement(username);
            user?.Remove();
            SaveChanges();
        }

        public FetchReturnType AddPokemon(string username, PokemonCard card)
        {
            var user = GetUserElement(username);
            if (user == null) return new FetchReturnType(error: "User not found");

            var existingCard = user.Element("PokemonCards")
                                   ?.Elements("Card")
                                   .FirstOrDefault(c => c.Attribute("Id")?.Value == card.Id);

            if (existingCard != null) return new FetchReturnType(error: "Added card is already in you collection"); // Don't add duplicates

            user.Element("PokemonCards").Add(new XElement("Card",
                new XAttribute("Id", card.Id),
                new XAttribute("Name", card.Name),
                new XAttribute("Image", card.Image),
                new XAttribute("SetName", card.SetName)
            ));

            SaveChanges();
            return new FetchReturnType(success: "Card added successfully"); 
        }

        public List<PokemonCard> GetPokemonCards(string username)
        {
            var user = GetUserElement(username);
            var cards = new List<PokemonCard>();

            if (user != null)
            {
                var cardElements = user.Element("PokemonCards")?.Elements("Card");
                foreach (var card in cardElements)
                {
                    cards.Add(new PokemonCard(
                        card.Attribute("Id")?.Value,
                        card.Attribute("Name")?.Value,
                        card.Attribute("Image")?.Value,
                        card.Attribute("SetName")?.Value
                    ));
                }
            }

            return cards;
        }

        public FetchReturnType RemovePokemon(string username, string cardId)
        {
            var user = GetUserElement(username);
            if (user == null) return new FetchReturnType(error: "User not found");

            var card = user.Element("PokemonCards")
                           ?.Elements("Card")
                           .FirstOrDefault(c => c.Attribute("Id").Value == cardId);

            card.Remove();
            SaveChanges();
            return new FetchReturnType(success: "Card removed successfully");
        }

        private XElement GetUserElement(string username)
        {
            return doc.Root.Elements("User")
                     .FirstOrDefault(u => u.Attribute("username")?.Value == username);
        }

        public bool ValidateMember(string username, string password)
        {
            var user = GetUserElement(username);
            if (user == null) return false;
            string storedPassword = user.Attribute("password")?.Value;
            return EncryptionDecryption.Encrypt(password) == storedPassword;
        }
        }
}