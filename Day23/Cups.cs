using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23 {
	class Cups : CircularArray<int> {

		public Cups(string input) : base(input.Length, input.Select(x => int.Parse(x.ToString()))) {
		}

	}
}
