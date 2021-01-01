using Common;
using System.Linq;

namespace Day22 {
	class Program : BlockParsedInputProgramStructure<Deck> {

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Deck[] input) {
			while (!input[0].Empty && !input[1].Empty) {
				Play(input);
			}
			Deck winner = (!input[0].Empty) ? input[0] : input[1];
			return winner.Cards.Reverse().WithIndex().Select(pair => pair.Element * (pair.Index + 1)).Sum().ToString();
		}

		static void Play(Deck[] decks) {
			int card1 = decks[0].Draw();
			int card2 = decks[1].Draw();

			Deck winner = (card1 > card2) ? decks[0] : decks[1];
			winner.PushBack(new int[] { card1, card2 });
		}

		protected override string CalculatePart2(Deck[] input) {
			return "null";
		}
	}
}
