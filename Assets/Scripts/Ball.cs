using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
	public int Index { get; private set; }
	public Color Color { get; private set; }
	public int Intensity { get; private set; }

	public Ball(int index, Color color, int intensity)
	{
		Index = index;
		Color = color;
		Intensity = intensity;
	}
}
