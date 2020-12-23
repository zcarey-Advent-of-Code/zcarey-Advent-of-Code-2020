using Common;
using System.Linq;

namespace Day19 {
	class Program : FullParsedInputProgramStructure<Input> {
		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
			//new Program().Run("Example2.txt", true);
		}

		protected override string CalculatePart1(Input input) {
			return input.Messages.Where(x => input.Rule0.Match(input.Rules, x)).Count().ToString();
		}

		protected override string CalculatePart2(Input input) {
			/*input.Rules[8] =*/ input.ParseRule("8: 42 | 42 8"); //Rule.Parse("8: 42 | 42 8", 0);
			/*input.Rules[11] =*/ input.ParseRule("11: 42 31 | 42 11 31"); //Rule.Parse("11: 42 31 | 42 11 31", 0);
			//Note to self: Rules 31 and 42 are the same size (length of 5)
			return input.Messages.Where(x => input.Rule0.MatchLooping(input.Rules, x)).Count().ToString();
		}
	}
}
