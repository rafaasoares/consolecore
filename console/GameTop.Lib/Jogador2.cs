using GameTop.Interface;

namespace GameTop.Lib
{
    public class Jogador2 : IJogador
    {
        public readonly string Nome;

        public Jogador2(string nome)
        {
            Nome = nome;
        }

        public string Chuta()
        {
            return "Maradona estas chutando \n";
        }

        public string Corre()
        {
            return "Maradona estas correndo \n";
        }

        public string Passe()
        {
            return "Maradona estas passando \n";
        }
    }
}