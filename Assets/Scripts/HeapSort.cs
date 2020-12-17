using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class HeapSort
{
	//TODO: make this function generic
	public static void SortBalls(List<Ball> list, Func<Ball, int> valueToCompare)
	{
		var length = list.Count;
		for (int i = length / 2 - 1; i >= 0; i--)
		{
			Heapify(list, valueToCompare, length, i);
		}
		for (int i = length - 1; i >= 0; i--)
		{
			var temp = list[0];
			list[0] = list[i];
			list[i] = temp;
			Heapify(list, valueToCompare, i, 0);
		}
	}

	static void Heapify(List<Ball> list, Func<Ball, int> valueToCompare, int length, int i)
	{
		int largest = i;
		int left = 2 * i + 1;
		int right = 2 * i + 2;
		if (left < length && valueToCompare(list[left]) < valueToCompare(list[largest]))
		{
			largest = left;
		}
		if (right < length && valueToCompare(list[right]) < valueToCompare(list[largest]))
		{
			largest = right;
		}
		if (largest != i)
		{
			var swap = list[i];
			list[i] = list[largest];
			list[largest] = swap;
			Heapify(list, valueToCompare, length, largest);
		}
	}
}
