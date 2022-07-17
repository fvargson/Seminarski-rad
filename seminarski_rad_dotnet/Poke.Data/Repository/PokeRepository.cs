using Poke.Data.Interface;
using Poke.Data.Model;
using PokeApiNet;

namespace Poke.Data.Repository
{
    public class PokeRepository : IPokemonRepository
    {
        private static PokeApiClient _poke_client;
        public PokeRepository()
        {
            if(_poke_client == null)
            {
                _poke_client = new PokeApiClient();
            }
        }
        public IEnumerable<MyPokemon> GetPokemon(int limit = 20, int offset = 0)
        {
            
            var poke = _poke_client.GetNamedResourcePageAsync<Pokemon>(limit, offset);

            List<MyPokemon> pokes = 
                poke
                    .Result.Results
                    .Select(
                        p => new MyPokemon(p.Name, p.Url)
                    )
                    .ToList();
            
            return pokes;
        }

        public MyPokemon GetPokemonById(int id)
        {
            var poke = _poke_client.GetResourceAsync<Pokemon>(id).Result;

            return new MyPokemon() {
                Id = id,
                Name = poke.Name,
                Url_fd = poke.Sprites.FrontDefault,
                Url_fd_shiny = poke.Sprites.FrontShiny,
                Url_full_image = poke
                    .Sprites
                    .FrontDefault
                    .Replace("/sprites/pokemon/", "/sprites/pokemon/other/official-artwork/")
            };
        }

        public MyPokemon GetPokemonById(string name)
        {
            var poke = _poke_client.GetResourceAsync<Pokemon>(name).Result;

            return new MyPokemon() {
                Id = poke.Id,
                Name = poke.Name,
                Url_fd = poke.Sprites.FrontDefault,
                Url_fd_shiny = poke.Sprites.FrontShiny,
                Url_full_image = poke
                    .Sprites
                    .FrontDefault
                    .Replace("/sprites/pokemon/", "/sprites/pokemon/other/official-artwork/")
            };
        }

        public IEnumerable<string> GetPokemonTypes()
        {
            var types = _poke_client
                            .GetNamedResourcePageAsync<PokeApiNet.Type>()
                            .Result.Results
                            .Select(
                                t => t.Name
                            ).Where(
                                t => t != "unknown" && t != "shadow"
                            );
            
            return types;
        }

        public int GetPokemonGeneration(int id)
        {
            var gen = _poke_client.GetResourceAsync<PokemonSpecies>(id).Result.Generation.Name.Substring(11);
            
            int genToInt;
            if(gen == "iv")
            {
                genToInt = 4;
            } else if(gen[0] == 'i')
            {
                genToInt = gen.Length;
            } else
            {
                genToInt = gen.Length + 4;
            }
            return genToInt;
        }

        public int GetPokemonGeneration(string name)
        {
            var gen = _poke_client.GetResourceAsync<PokemonSpecies>(name).Result.Generation.Name.Substring(11);

            int genToInt;
            if(gen == "iv")
            {
                genToInt = 4;
            } else if(gen[0] == 'i')
            {
                genToInt = gen.Length;
            } else
            {
                genToInt = gen.Length + 4;
            }

            return genToInt;
        }

        public IEnumerable<string> GetPokemonType(int id)
        {
            var poke = _poke_client.GetResourceAsync<Pokemon>(id).Result;
            
            return poke.Types.Select(t => t.Type.Name).ToList();
        }

        public IEnumerable<string> GetPokemonType(string name)
        {
            var poke = _poke_client.GetResourceAsync<Pokemon>(name).Result;
            
            return poke.Types.Select(t => t.Type.Name).ToList();
        }

        public bool IsStarter(int id, int gen)
        {
            var poke = _poke_client.GetResourceAsync<PokemonSpecies>(id).Result;
            if(poke.PokedexNumbers[1].EntryNumber <= 9)
            {
                if(gen == 5)
                {
                    if(poke.PokedexNumbers[1].EntryNumber == 0)
                        return false;
                }
                return true;
            }
            return false;
        }

        public bool IsLegendary(int id)
        {
            var poke = _poke_client.GetResourceAsync<PokemonSpecies>(id).Result;
            
            return poke.IsLegendary;
        }

        public bool IsMythical(int id)
        {
            var poke = _poke_client.GetResourceAsync<PokemonSpecies>(id).Result;

            return poke.IsMythical;
        }

        public bool IsBaby(int id)
        {
            var poke = _poke_client.GetResourceAsync<PokemonSpecies>(id).Result;

            return poke.IsBaby;
        }

        public PokemonSpecies GetPokeInfo(int id)
        {
            var pokeInfo = _poke_client.GetResourceAsync<PokemonSpecies>(id).Result;
            
            return pokeInfo;
        }

        public string LighterColor(string color)
        {
            switch(color)
            {
                case "black": return "darkgray";
                case "brown": return "#EEDD82";
                case "pink": return "mistyrose";
                case "purple": return "#C5BADB";
                case "red": return "#FFD1D1";
                case "white": return "white";
                default: return "light" + color;
            }
        }
    }
}