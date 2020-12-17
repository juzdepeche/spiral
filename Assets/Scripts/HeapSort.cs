using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class HeapSort
{
	//TODO: make this function generic
	public static void SortListByIndex(List<Ball> list)
	{
		var length = list.Count;
		for (int i = length / 2 - 1; i >= 0; i--)
		{
			Heapify(list, length, i);
		}
		for (int i = length - 1; i >= 0; i--)
		{
			var temp = list[0];
			list[0] = list[i];
			list[i] = temp;
			Heapify(list, i, 0);
		}
	}

	static void Heapify(List<Ball> list, int length, int i)
	{
		int largest = i;
		int left = 2 * i + 1;
		int right = 2 * i + 2;
		if (left < length && list[left].Index < list[largest].Index)
		{
			largest = left;
		}
		if (right < length && list[right].Index < list[largest].Index)
		{
			largest = right;
		}
		if (largest != i)
		{
			var swap = list[i];
			list[i] = list[largest];
			list[largest] = swap;
			Heapify(list, length, largest);
		}
	}
}
