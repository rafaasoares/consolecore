using GameTop.Interface;

namespace GameTop.Lib
{
    public class Jogador3 : IJogador
    {
        public string Chuta()
        {
            return "teste chuta \n";
        }

        public string Corre()
        {
            return "teste corre \n";
        }

        public string Passe()
        {
            return "teste passe \n";
        }
    }
}