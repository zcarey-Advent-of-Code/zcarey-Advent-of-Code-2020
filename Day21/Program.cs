using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21 {
	class Program : ParsedInputProgramStructure<Food> {

		Program() : base(Food.Parse) {
		}

		static void Main(string[] args) {
			//new Program().Run("input.txt");
			new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Food[] input) {
			//Get a list of all possible allergens
			List<string> allergens = input.SelectMany(x => x.Allergens).Distinct().ToList();

			//The initial state each ingredient can be any of the possible allergens
			Dictionary<string, List<string>> possibleAllergens = new Dictionary<string, List<string>>();
			foreach(string ingredient in input.SelectMany(x => x.Ingredients).Distinct()) {
				possibleAllergens[ingredient] = new List<string>(allergens);
			}

			//Now using our input, elimate allergens that arent possible for some ingredients
			foreach(Food food in input) {
				List<string> missingAllergens = allergens.Except(food.Allergens).ToList();
				foreach(string ingredient in food.Ingredients) {
					foreach (string allergen in missingAllergens) {
						possibleAllergens[ingredient].Remove(allergen); //It can't be an allergen that isn't in this food's allergen list
					}
				}
			}

			//Get a list of all ingredients with no allergens and count how many of them appear in our input
			List<string> noAllergens = possibleAllergens.Where(pair => pair.Value.Count == 0).Select(pair => pair.Key).ToList();
			return input.SelectMany(x => x.Ingredients).Where(x => noAllergens.Contains(x)).Count().ToString();
		}

		protected override string CalculatePart2(Food[] input) {
			return "null";
		}
	}
}
