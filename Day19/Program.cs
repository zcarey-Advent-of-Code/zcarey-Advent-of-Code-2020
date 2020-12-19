using Common;
using System.Linq;

namespace Day19 {
	class Program : FullParsedInputProgramStructure<Input> {
		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Input input) {
			return input.Messages.Where(x => input.Rule0.Match(input.Rules, x)).Count().ToString();
		}

		protected override string CalculatePart2(Input input) {
			return "null";
		}
	}
}
