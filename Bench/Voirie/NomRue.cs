
namespace Bench
{
    public class NomRue
    {
        public NomRue() { }

        public NomRue(string type, string nom)
        {
            Type = type;
            Nom = nom;
        }

        public string Type { get; set; }
        public string Nom { get; set; }

        public override string ToString()
        {
            return string.Concat(Type, ' ', Nom).Trim();
        }
    }
}
