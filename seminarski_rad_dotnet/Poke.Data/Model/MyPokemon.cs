namespace Poke.Data.Model
{
    public class MyPokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url_fd { get; set; }
        public string Url_fd_shiny { get; set; }
        public string Url_full_image { get; set; }

        public MyPokemon() {}

        public MyPokemon(string name, string url)
        {
            Name = name;
            Id = Int32.Parse(url.Split("/").Last(p => p != ""));
            Url_fd = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + Id + ".png";
            Url_fd_shiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/" + Id + ".png";
            Url_full_image = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + Id + ".png";
        }
    }
}