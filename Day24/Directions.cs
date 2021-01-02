using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Day24 {
	class Directions /*: IEnumerable<Point>*/ {

		private Point[] offsets;

		public Directions(string input) {
			offsets = convert(input).ToArray();
		}

		private IEnumerable<Point> convert(string input) {
			IEnumerator<char> chars = input.GetEnumerator();
			while (chars.MoveNext()) {
				switch (chars.Current) {
					case 'w': 
						yield return new Point(-1, 0);
						break;
					case 'e':
						yield return new Point(1, 0);
						break;
					case 'n':
						if (!chars.MoveNext()) throw new ArgumentOutOfRangeException("input", "Bad input, out of characters.");
						yield return new Point((chars.Current == 'e') ? 1 : 0, -1);
						break;
					case 's':
						if (!chars.MoveNext()) throw new ArgumentOutOfRangeException("input", "Bad input, out of characters.");
						yield return new Point((chars.Current == 'w') ? -1 : 0, 1);
						break;
					default:
						throw new ArgumentOutOfRangeException("input", "Bad input.");
				}
			}
		}

		public Point GetAbsoluteOffset(Point origin = new Point()) {
			foreach(Point offset in offsets) {
				origin.Offset(offset);
			}
			return origin;
		}
		/*
		public IEnumerator<Point> GetEnumerator() {
			return this.offsets.GetEnumerator<Point>(); //new CustomEnumerator(offsets);
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		*/
		public static Directions Parse(string input) {
			return new Directions(input);
		}
/*
		private class CustomEnumerator : IEnumerator<(int q, int r)> {
			public (int q, int r) Current => array[index];
			object IEnumerator.Current => this.Current;

			private (int q, int r)[] array;
			int index = -1;

			public CustomEnumerator((int q, int r)[] array) {
				this.array = array;
			}

			public void Dispose() {
				this.array = null;
			}

			public bool MoveNext() {
				index++;
				return index < array.Length;
			}

			public void Reset() {
				this.index = -1;
			}
		}
*/	}
}
