using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21 {
	class Program : ParsedInputProgramStructure<Food> {

		Program() : base(Food.Parse) {
		}

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Food[] input) {
			List<string> foundAllergens = new List<string>();
			Dictionary<string, List<string>> allergens = new Dictionary<string, List<string>>();

			bool changed = true;
			while (changed) {
				changed = false;

				foreach(Food food in input) {
					foreach(string allergen in food.Allergens) {
						if (!allergens.ContainsKey(allergen)) {
							allergens[allergen] = new List<string>(food.Ingredients);
							changed = true;
						} else {
							List<string> newList = allergens[allergen].Intersect(food.Ingredients).ToList();
							changed |= (newList.Count != allergens[allergen].Count);
							allergens[allergen] = newList;
						}

						changed |= (allergens[allergen].RemoveAll(x => foundAllergens.Contains(x)) != 0);
						if (allergens[allergen].Count == 1 && !foundAllergens.Contains(allergens[allergen][0])) {
							foundAllergens.Add(allergens[allergen][0]);
							changed = true;
						}
					}
				}
			}

			//The "Except" function in linq is for some reason removing duplicate elements so we have to do it the ugly way instead.
			return input.SelectMany(x => x.Ingredients).Where(x => !foundAllergens.Contains(x)).Count().ToString(); 
		}

		protected override string CalculatePart2(Food[] input) {
			return "null";
		}
	}
}
