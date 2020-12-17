using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public int Index { get; set; }
	public Color Color { get; private set; }
	public int Intensity { get; private set; }
	public int Score { get; private set; }

	public Ball(int index, Color color, int intensity)
	{
		Index = index;
		Color = color;
		Intensity = intensity;
	}

	public Ball(int index, int score, Color color)
	{
		Index = index;
		Score = score;
		Color = color;
	}
}
