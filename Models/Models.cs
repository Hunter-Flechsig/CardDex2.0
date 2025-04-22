using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeFind.Models
{
    // class for representing and parsing pokmon card data recieved from PokeFind Service
    [Serializable]
    public class PokemonCard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string SetName { get; set; }

        public PokemonCard() { }

        public PokemonCard(string id, string name, string image, string setName)
        {
            Id = id;
            Name = name;
            Image = image;
            SetName = setName;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - {SetName}";
        }
    }

    public class FetchReturnType
    {
        public string error;
        public string success;

        public FetchReturnType(string error = null, string success = null)
        {
            this.error = error;
            this.success = success;
        }
    }
}