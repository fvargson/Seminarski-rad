using Poke.Data.Model;
using PokeApiNet;

namespace Poke.Data.Interface
{
    public interface IPokemonRepository
    {
        public IEnumerable<MyPokemon> GetPokemon(int limit, int offset);
        public MyPokemon GetPokemonById(int id);
        public MyPokemon GetPokemonById(string name);
        public IEnumerable<string> GetPokemonType(int id);
        public IEnumerable<string> GetPokemonType(string name);
        public int GetPokemonGeneration(int id);
        public int GetPokemonGeneration(string name);
        public bool IsStarter(int id, int gen);
        public bool IsLegendary(int id);
        public bool IsMythical(int id);
        public bool IsBaby(int id);
        public IEnumerable<string> GetPokemonTypes();
        public PokemonSpecies GetPokeInfo(int id);
        public string LighterColor(string color);
    }
}