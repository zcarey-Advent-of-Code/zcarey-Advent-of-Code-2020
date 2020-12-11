using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day11 {
	class Map : FullInputParser {

		public int Width { get; private set; }
		public int Height { get; private set; }

		private int[,] map;

		public void Parse(string[] input) {
			Width = input[0].Length;
			Height = input.Length;
			map = new int[Width, Height];
			for(int y = 0; y < Height; y++) {
				for(int x = 0; x < Width; x++) {
					char c = input[y][x];
					if(c == '.') {
						map[x, y] |= (int)SeatState.Floor;
					}else if(c == 'L') {
						map[x, y] |= (int)SeatState.Empty;
					}else if(c == '#') {
						map[x, y] |= (int)SeatState.Occupied;
					} else {
						throw new Exception("Unknown seat character.");
					}
				}
			}
		}

		public void Simulate() {
			while (Update()) ;
		}

		//Return true if at least one seat state updated
		public bool Update() {
			updateOccupiedCount();
			return updateSeatState();
		}

		public int CountOccupiedSeats() {
			int count = 0;
			for(int y = 0; y < Height; y++) {
				for(int x = 0; x < Width; x++) {
					if(isOccupied(x, y)) {
						count++;
					}
				}
			}
			return count;
		}

		private bool isValidLocation(int x, int y) {
			return (y >= 0) && (y < Height) && (x >= 0) && (x < Width);
		}

		private bool isSeat(int x, int y) {
			return isValidLocation(x, y) && ((map[x, y] & (int)(SeatState.Empty | SeatState.Occupied)) != 0);
		}

		private bool isOccupied(int x, int y) {
			return isValidLocation(x, y) && ((map[x, y] & (int)SeatState.Occupied) != 0);
		}

		private void setOccupied(int x, int y, bool occupied) {
			map[x, y] = (int)(occupied ? SeatState.Occupied : SeatState.Empty) | (map[x, y] & 0x0F);
		}

		private void setSeatCount(int x, int y, int count) {
			map[x, y] = (map[x, y] & (int)(SeatState.Empty | SeatState.Occupied)) | (count & 0x0F);
		}

		private int getSeatCount(int x, int y) {
			return map[x, y] & 0x0F;
		}

		private void updateOccupiedCount() {
			for(int y = 0; y < Height; y++) {
				for (int x = 0; x < Width; x++) {
					if (isSeat(x, y)) {
						//Count surrounding seats
						int count = 0;
						for (int j = -1; j <= 1; j++) {
							for (int i = -1; i <= 1; i++) {
								if (i == 0 && j == 0) continue;
								if (isOccupied(x + i, y + j)) {
									count++;
								}
							}
						}
						setSeatCount(x, y, count);
					}
				}
			}
		}

		//Returns true if at least one seat state updated
		private bool updateSeatState() {
			bool updated = false;
			for (int y = 0; y < Height; y++) {
				for (int x = 0; x < Width; x++) {
					if (isSeat(x, y)) {
						int count = getSeatCount(x, y);
						if (isOccupied(x, y)) {
							if(count >= 4) {
								setOccupied(x, y, false);
								updated = true;
							}
						} else {
							if(count == 0) {
								setOccupied(x, y, true);
								updated = true;
							}
						}
					}
				}
			}
			return updated;
		}

	}
}
