using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20 {
	class Tile : FullInputParser {

		public int ID { get; private set; }
		public int Size { get; private set; }

		private bool[] boarder;

		public void Parse(string[] input) {
			ID = int.Parse(input[0].Split()[1].TrimEnd(':')); //Ex. "Tile 3079:"
			Size = input.Length - 1;
			boarder = new bool[Size * 4];

			int index = 0;
			//Top
			for(int x = 0; x < Size; x++) {
				boarder[index++] = (input[1][x] == '#');
			}

			//Right
			for (int y = 0; y < Size; y++) {
				boarder[index++] = (input[y + 1][Size - 1] == '#');
			}

			//Bottom
			for (int x = 0; x < Size; x++) {
				boarder[index++] = (input[Size][x] == '#');
			}

			//Left
			for (int y = 0; y < Size; y++) {
				boarder[index++] = (input[y + 1][0] == '#');
			}
		}

		private IEnumerable<bool> top { get => boarder.Take(Size); }
		private IEnumerable<bool> bottom { get => boarder.Skip(2 * Size).Take(Size); }
		private IEnumerable<bool> left { get => boarder.Skip(3 * Size).Take(Size); }
		private IEnumerable<bool> right { get => boarder.Skip(Size).Take(Size); }

		public IEnumerable<bool> Top(Operation op) { return Top(op.Transpose, op.FlipVertical, op.FlipHorizontal); }
		public IEnumerable<bool> Top(bool Transpose, bool FlipVertical, bool FlipHorizontal) {
			IEnumerable<bool> source;
			if (Transpose) {
				source = (FlipVertical ? right : left);
			} else {
				source = (FlipVertical ? bottom : top);
			}
			return FlipHorizontal ? source.Reverse() : source;
		}

		public IEnumerable<bool> Bottom(Operation op) { return Bottom(op.Transpose, op.FlipVertical, op.FlipHorizontal); }
		public IEnumerable<bool> Bottom(bool Transpose, bool FlipVertical, bool FlipHorizontal) {
			IEnumerable<bool> source;
			if (Transpose) {
				source = (FlipVertical ? left : right);
			} else {
				source = (FlipVertical ? top : bottom);
			}
			return FlipHorizontal ? source.Reverse() : source;
		}

		public IEnumerable<bool> Left(Operation op) { return Left(op.Transpose, op.FlipVertical, op.FlipHorizontal); }
		public IEnumerable<bool> Left(bool Transpose, bool FlipVertical, bool FlipHorizontal) {
			IEnumerable<bool> source;
			if (Transpose) {
				source = (FlipHorizontal ? bottom : top);
			} else {
				source = (FlipHorizontal ? right : left);
			}
			return FlipVertical ? source.Reverse() : source;
		}

		public IEnumerable<bool> Right(Operation op) { return Right(op.Transpose, op.FlipVertical, op.FlipHorizontal); }
		public IEnumerable<bool> Right(bool Transpose, bool FlipVertical, bool FlipHorizontal) {
			IEnumerable<bool> source;
			if (Transpose) {
				source = (FlipHorizontal ? top : bottom);
			} else {
				source = (FlipHorizontal ? left : right);
			}
			return FlipVertical ? source.Reverse() : source;
		}

	}
}
