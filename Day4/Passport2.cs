using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day4 {
	class Passport2 : Passport {

		private static readonly string[] EyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

		public Passport2(IEnumerable<string> keyValuePairs) : base(keyValuePairs) {

		}

		public override void Validate() {
			BirthYear = parseIntRange(BirthYear, 1920, 2002);
			IssueYear = parseIntRange(IssueYear, 2010, 2020);
			ExpirationYear = parseIntRange(ExpirationYear, 2020, 2030);
			Height = parseHeight(Height);
			HairColor = parseHairColor(HairColor);
			EyeColor = parseEyeColor(EyeColor);
			PassportID = parsePID(PassportID);
			base.Validate();
		}

		private string parseIntRange(string input, int min, int max) {
			int result;
			if(input != null && int.TryParse(input, out result)) {
				if(result >= min && result <= max) {
					return input;
				}
			}
			return null;
		}

		private string parseHeight(string input) {
			int result;
			if (input == null || !int.TryParse(input.Substring(0, input.Length - 2), out result)) {
				return null;
			}

			if (input.EndsWith("cm")) {
				return (result >= 150 && result <= 193) ? input : null;
			} else if (input.EndsWith("in")) {
				return (result >= 59 && result <= 76) ? input : null;
			} else {
				return null;
			}
		}

		private string parseHairColor(string input) {
			if(input != null && input.Length == 7 && input.StartsWith("#")) {
				for(int i = 1; i < input.Length; i++) {
					char c = input[i];
					if(!((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f'))) {
						return null;
					}
				}
				return input;
			}
			return null;
		}

		private string parseEyeColor(string input) {
			if (input != null && EyeColors.Contains(input)) {
				return input;
			} else {
				return null;
			}
		}

		private string parsePID(string input) {
			int id;
			if(input != null && input.Length == 9 && int.TryParse(input, out id)) {
				return input;
			} else {
				return null;
			}
		}

	}
}
