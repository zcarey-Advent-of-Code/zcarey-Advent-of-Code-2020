using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day23 {
	class Cups : IEnumerable<int> {

		public int Count { get => cups.Count; }
		public LinkedListNode<int> First { get => cups.First; }

		private LinkedList<int> cups;
		private LinkedListNode<int>[] lookup;

		public Cups(string input) {
			cups = new LinkedList<int>(input.Select(x => int.Parse(x.ToString())));
			createNodeLookup();
		}

		public Cups(Cups copy, IEnumerable<int> additional) {
			cups = new LinkedList<int>(copy.cups.Concat(additional));
			createNodeLookup();
		}

		private void createNodeLookup() {
			//Create a look-up for every number;
			lookup = new LinkedListNode<int>[cups.Count];
			foreach(LinkedListNode<int> node in cups.First.AsEnumerable()) {
				lookup[node.Value - 1] = node;
			}
/*
			LinkedListNode<int> currentNode = cups.First;
			while (currentNode != null) {
				lookup[currentNode.Value - 1] = currentNode;
				currentNode = currentNode.Next;
			}*/
		}

		public LinkedListNode<int> this[int CupLabel] {
			get => this.lookup[CupLabel - 1];
		}

/*		public IEnumerable<int> RemoveAfter(LinkedListNode<int> node, int count) {
			while(count-- > 0) {
				LinkedListNode<int> next = node.Next;
				if (next == null) next = cups.First;
				cups.Remove(next);
				yield return next.Value;
			}
		}
*/
		public IEnumerator<int> GetEnumerator() {
			return cups.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return cups.GetEnumerator();
		}
	}
}
