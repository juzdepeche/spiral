using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
	public int BallAmount = 3000;
	private List<Ball> balls;
	private List<Ball> redBalls;
	private List<Ball> greenBalls;
	private List<Ball> blueBalls;

	void Start()
	{
		balls = new List<Ball>();
		createBalls();
		regroupBalls();
	}

	private void createBalls()
	{
		for (int i = 0; i < BallAmount; i++)
		{
			Ball ball = new Ball(i, getRandomColor(), getRandomIntensity());
			balls.Add(ball);
		}

		var redCount = balls.FindAll(b => b.Color == Color.red).Count;
		var greenCount = balls.FindAll(b => b.Color == Color.green).Count;
		var blueCount = balls.FindAll(b => b.Color == Color.blue).Count;

		if (redCount < 100 || greenCount < 100 || blueCount < 100)
		{
			createBalls();
		}
	}

	private Color getRandomColor()
	{
		var colorIndex = Random.Range(0, 3);
		Color color = Color.white;

		switch (colorIndex)
		{
			case 0:
				color = Color.red;
				break;
			case 1:
				color = Color.green;
				break;
			case 2:
				color = Color.blue;
				break;
		}

		return color;
	}

	private int getRandomIntensity()
	{
		return Random.Range(0, 256);
	}

	private void regroupBalls()
	{
		redBalls = new List<Ball>();
		greenBalls = new List<Ball>();
		blueBalls = new List<Ball>();

		foreach (var ball in balls)
		{
			if (ball.Color == Color.red)
			{
				redBalls.Add(ball);
			}
			else if (ball.Color == Color.green)
			{
				greenBalls.Add(ball);
			}
			else if (ball.Color == Color.blue)
			{
				blueBalls.Add(ball);
			}
		}
	}
}
