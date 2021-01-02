using Common;
using System;

namespace Day25 {
	class Program : ParsedInputProgramStructure<PublicKey> {

		Program() : base(x => new PublicKey(x)) { }

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(PublicKey[] input) {
			PublicKey cardPublicKey = input[0];
			PublicKey doorPublicKey = input[1];

			int cardLoopSize = cardPublicKey.FindLoopSize(7);
			int doorLoopSize = doorPublicKey.FindLoopSize(7);

			long encryptionKey = PublicKey.Transform(doorPublicKey.Key, cardLoopSize);
			return encryptionKey.ToString();
		}

		protected override string CalculatePart2(PublicKey[] input) {
			return "null";
		}
	}
}
