using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Linq;

namespace Day4 {
	class Passport {

		public string BirthYear { get; protected set; }
		public string IssueYear { get; protected set; }
		public string ExpirationYear { get; protected set; }
		public string Height { get; protected set; }
		public string HairColor { get; protected set; }
		public string EyeColor { get; protected set; }
		public string PassportID { get; protected set; }
		public string CountryID { get; protected set; }

		public bool IsValid { get; private set; } = false;

		public Passport(IEnumerable<string> keyValuePairs) {
			foreach(string element in keyValuePairs) {
				int index = element.IndexOf(':');
				string key = element.Substring(0, index);
				string value = element.Substring(index + 1);

				switch (key) {
					case "byr":
						BirthYear = value;
						break;
					case "iyr":
						IssueYear = value;
						break;
					case "eyr":
						ExpirationYear = value;
						break;
					case "hgt":
						Height = value;
						break;
					case "hcl":
						HairColor = value;
						break;
					case "ecl":
						EyeColor = value;
						break;
					case "pid":
						PassportID = value;
						break;
					case "cid":
						CountryID = value;
						break;
				}
			}
		}

		public virtual void Validate() {
			IsValid = (BirthYear != null)
				&& (IssueYear != null)
				&& (ExpirationYear != null)
				&& (Height != null)
				&& (HairColor != null)
				&& (EyeColor != null)
				&& (PassportID != null);
				//&& ((ignoreCID) ? true : (CountryID != null));

		}

	}
}
