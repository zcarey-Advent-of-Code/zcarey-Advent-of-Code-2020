using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Parsing {
	public class StringReader : IReader<string> {
		override internal string Parse(StreamReader input) {
			return input.ReadToEnd();
		}
	}
}
