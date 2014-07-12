
namespace Bench
{
    public class Commune : Agregat<Commune>
    {
        public CodeCommune Code {get; set;}
        public string Nom { get; set; }
    }
}
