using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day20 {
	//These should really be readable-only, but I couldn't bother to make two separate classes so here you go.
	class Tile : SquareImage<bool> {

		public int ID { get; private set; }

		/*public Operation Operation { get; set; } = Operation.Original;
		public IEnumerable<bool> Top { get => getTop(Operation.Transpose, Operation.FlipVertical, Operation.FlipHorizontal); }
		public IEnumerable<bool> Bottom { get => getBottom(Operation.Transpose, Operation.FlipVertical, Operation.FlipHorizontal); }
		public IEnumerable<bool> Left { get => getLeft(Operation.Transpose, Operation.FlipVertical, Operation.FlipHorizontal); }
		public IEnumerable<bool> Right { get => getRight(Operation.Transpose, Operation.FlipVertical, Operation.FlipHorizontal); }*/

		//private bool[] boarder;
		//private bool[] all;

		public Tile(string[] input) : base(input.Length - 1) {
			ID = int.Parse(input[0].Split()[1].TrimEnd(':')); //Ex. "Tile 3079:"
			IEnumerable<bool> elements = input.Skip(1).SelectMany(x => x.Select(y => y == '#'));
			IEnumerator<bool> enumerator = elements.GetEnumerator();
			for (int y = 0; y < Size; y++) {
				for(int x = 0; x < Size; x++) {
					if (!enumerator.MoveNext()) throw new IndexOutOfRangeException("Read outside of input bounds.");
					this[x, y] = enumerator.Current;
				}
			}
			/*all = input.Skip(1).SelectMany(x => x.Select(y => y == '#')).ToArray();
			boarder = new bool[Size * 4];

			int index = 0;
			//Top
			for (int x = 0; x < Size; x++) {
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
			}*/
		}

		public IEnumerable<bool> Top { get => this.GetData(new Rectangle(0, 0, Size, 1)); }
		public IEnumerable<bool> Bottom { get => this.GetData(new Rectangle(0, Size - 1, Size, 1)); }
		public IEnumerable<bool> Left { get => this.GetData(new Rectangle(0, 0, 1, Size)); }
		public IEnumerable<bool> Right { get => this.GetData(new Rectangle(Size - 1, 0, 1, Size)); }

		/*public bool this[int x, int y] {
			get {
				Point p = new Point(x, y);
				if (this.Operation.Transpose) {
					p = new Point(p.Y, p.X);
				}
				if (this.Operation.FlipVertical) {
					p = new Point(p.X, Size - p.Y - 1);
				}
				if (this.Operation.FlipHorizontal) {
					p = new Point(Size - p.X - 1, p.Y);
				}
				return all[p.Y * Size + p.X];
			}
		}*/
		/*
				private IEnumerable<bool> top { get => boarder.Take(Size); }
				private IEnumerable<bool> bottom { get => boarder.Skip(2 * Size).Take(Size); }
				private IEnumerable<bool> left { get => boarder.Skip(3 * Size).Take(Size); }
				private IEnumerable<bool> right { get => boarder.Skip(Size).Take(Size); }

				//private IEnumerable<bool> getTop() => top;
				//private IEnumerable<bool> getTop(Operation op) { return Top(op.Transpose, op.FlipVertical, op.FlipHorizontal); }
				private IEnumerable<bool> getTop(bool Transpose, bool FlipVertical, bool FlipHorizontal) {
					IEnumerable<bool> source;
					if (Transpose) {
						source = (FlipVertical ? right : left);
					} else {
						source = (FlipVertical ? bottom : top);
					}
					return FlipHorizontal ? source.Reverse() : source;
				}

				//private IEnumerable<bool> getBottom() => bottom;
				//private IEnumerable<bool> getBottom(Operation op) { return Bottom(op.Transpose, op.FlipVertical, op.FlipHorizontal); }
				private IEnumerable<bool> getBottom(bool Transpose, bool FlipVertical, bool FlipHorizontal) {
					IEnumerable<bool> source;
					if (Transpose) {
						source = (FlipVertical ? left : right);
					} else {
						source = (FlipVertical ? top : bottom);
					}
					return FlipHorizontal ? source.Reverse() : source;
				}

				//private IEnumerable<bool> getLeft() => left;
				//private IEnumerable<bool> getLeft(Operation op) { return Left(op.Transpose, op.FlipVertical, op.FlipHorizontal); }
				private IEnumerable<bool> getLeft(bool Transpose, bool FlipVertical, bool FlipHorizontal) {
					IEnumerable<bool> source;
					if (Transpose) {
						source = (FlipHorizontal ? bottom : top);
					} else {
						source = (FlipHorizontal ? right : left);
					}
					return FlipVertical ? source.Reverse() : source;
				}

				//private IEnumerable<bool> getRight() => right;
				//private IEnumerable<bool> getRight(Operation op) { return Right(op.Transpose, op.FlipVertical, op.FlipHorizontal); }
				private IEnumerable<bool> getRight(bool Transpose, bool FlipVertical, bool FlipHorizontal) {
					IEnumerable<bool> source;
					if (Transpose) {
						source = (FlipHorizontal ? top : bottom);
					} else {
						source = (FlipHorizontal ? left : right);
					}
					return FlipVertical ? source.Reverse() : source;
				}
		*/
	}
}
