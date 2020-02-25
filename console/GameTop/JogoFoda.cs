using System;
using GameTop.Interface;

namespace GameTop
{
    public class JogoFoda
    {
        private readonly IJogador Jogador1;

        private readonly IJogador Jogador2;

        public JogoFoda(IJogador jogador1, IJogador jogador2)
        {
            this.Jogador1 = jogador1;
            this.Jogador2 = jogador2;
        }
        public void IniciarJogo()
        {
            Console.Write(Jogador1.Corre());
            Console.Write(Jogador1.Passe());
            Console.Write(Jogador1.Chuta());

            Console.Write("\n proximo jogador \n");

            Console.Write(Jogador2.Corre());
            Console.Write(Jogador2.Passe());
            Console.Write(Jogador2.Chuta());
        }
    }
}