using System;
using System.Collections.Generic;
using System.Text;

namespace Day23 {
	public static class LinkedListNodeExtensions {

		public static LinkedListNode<T> CircularNext<T>(this LinkedListNode<T> node) {
			if (node.Next == null) return node.List.First;
			else return node.Next;
		}

		public static LinkedListNode<T> CircularPrevious<T>(this LinkedListNode<T> node) {
			if (node.Previous == null) return node.List.Last;
			else return node.Previous;
		}

		public static IEnumerable<LinkedListNode<T>> AsEnumerable<T>(this LinkedListNode<T> Node) {
			LinkedListNode<T> currentNode = Node;
			do {
				yield return currentNode;
				currentNode = currentNode.CircularNext();
			} while (currentNode != Node);
		}

		public static void AddAfter<T>(this LinkedListNode<T> node, IEnumerable<LinkedListNode<T>> elements) {
			LinkedList<T> list = node.List;
			LinkedListNode<T> currentNode = node;
			foreach(LinkedListNode<T> element in elements) {
				list.AddAfter(currentNode, element);
				currentNode = element;
			}
		}

		public static IEnumerable<LinkedListNode<T>> RemoveAfter<T>(this LinkedListNode<T> node, int count) {
			LinkedList<T> list = node.List;
			while (count-- > 0) {
				LinkedListNode<T> next = node.CircularNext();
				list.Remove(next);
				yield return next;
			}
		}

	}
}
