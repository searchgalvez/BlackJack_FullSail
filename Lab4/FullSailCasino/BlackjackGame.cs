using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackjackClassLibrary;
using PG2Input;

namespace FullSailCasino
{
    public class BlackjackGame
    {
        BlackjackHand _dealer = new BlackjackHand(true);
        BlackjackHand _player = new BlackjackHand();
        BlackjackDeck _deck = new BlackjackDeck();
        int _playerWins = 0;
        int _dealerWins = 0;

        public void PlayRound()
        {
            _dealer = new BlackjackHand(true);
            _player = new BlackjackHand();
            _deck = new BlackjackDeck();

            Console.Clear();

            DealInitialCard();
            DrawTable();
            if (_player.Score < 21 || _dealer.Score < 21)
            {
                PlayersTurn();
                DealersTurn();
            }
            DeclareWinner();
        }
        public void DrawTable(bool reveal = false)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Players Hand");
            Console.ResetColor();
            _player.HandPrint(0, 2);

            Console.SetCursorPosition(0, 6);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Dealers Hand");
            Console.ResetColor();
            if (reveal)
            {
                _dealer.Reveal(0, 8);
            }
            else
            {
                _dealer.HandPrint(6, 8);
            }
            DrawWins();
        }
        public void DealInitialCard()
        {
            _deck.Shuffle();

            for (int i = 0; i < 2; i++)
            {
                _player.AddCard(_deck.Deal());
                _dealer.AddCard(_deck.Deal());
            }
        }
        public void PlayersTurn()
        {

            while (_player.Score < 21)
            {
                DrawTable();
                string[] Selection = new string[] { "1. Hit", "2. stand" };

                Console.SetCursorPosition(0, 15);
                Input.GetMenuChoice("\nHit or Stand? ", Selection, out int variable);

                if (variable == 1)
                {

                    _player.AddCard(_deck.Deal());
                    DrawTable();
                }
                else
                    break;

            }
        }
        public void DealersTurn()
        {
            DrawTable(true);
            if (_dealer.Score < 17)
            {

                _dealer.AddCard(_deck.Deal());
                DrawTable(true);


            }
        }
        public void DeclareWinner()
        {
            if (_player.Score > 21)
            {
                _dealerWins++;
                Console.SetCursorPosition(35, 6);
                Console.Write("Player Bust, Dealer Wins!");



            }
            else if (_dealer.Score > 21)
            {
                _playerWins++;
                Console.SetCursorPosition(35, 6);
                Console.Write("Dealer Bust, Player Wins!");


            }
            else if (_player.Score == _dealer.Score)
            {
                Console.SetCursorPosition(35, 6);
                Console.Write("Its a draw");

            }
            else if (_player.Score > _dealer.Score)
            {
                _playerWins++;
                Console.SetCursorPosition(35, 6);
                Console.Write("Player Wins!");

            }
            else if (_dealer.Score > _player.Score)
            {
                _dealerWins++;
                Console.SetCursorPosition(35, 6);
                Console.Write("Dealer Wins!");

            }
        }
        public void DrawWins()
        {
            Console.SetCursorPosition(20, 0);
            Console.Write($"Player Wins: {_playerWins}");
            Console.SetCursorPosition(40, 0);
            Console.Write($"Dealer Wins: {_dealerWins}");
        }
    }
}
