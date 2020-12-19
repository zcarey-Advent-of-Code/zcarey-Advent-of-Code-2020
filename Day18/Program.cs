using Common;
using System.Linq;
using System.Numerics;

namespace Day18 {
	class Program : ParsedInputProgramStructure<Expression> {


		Program() : base(Expression.Parse) {
		}

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example26.txt");
			//new Program().Run("Example51.txt");
			//new Program().Run("Example71.txt");
			//new Program().Run("Example437.txt");
			//new Program().Run("Example12240.txt");
			//new Program().Run("Example13632.txt");
		}

		protected override string CalculatePart1(Expression[] input) {
			BigInteger result = BigInteger.Zero;
			foreach(BigInteger answer in input.Select(x => x.Calculate(false))) {
				result += answer;
			}
			return result.ToString();
		}

		protected override string CalculatePart2(Expression[] input) {
			BigInteger result = BigInteger.Zero;
			foreach (BigInteger answer in input.Select(x => x.Calculate(true))) {
				result += answer;
			}
			return result.ToString();
		}
	}
}
