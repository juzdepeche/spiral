using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpiralUtils
{
	public static void Create(int centerX, int centerY, float chord, int coils, int radius, float rotation, Action<int, float, float> predicate, int maxIteration)
	{
		int iteration = 0;
		float thetaMax = coils * 2 * Mathf.PI;
		float awayStep = radius / thetaMax;

		predicate(iteration, centerX, centerY);

		for (float theta = chord / awayStep; theta <= thetaMax;)
		{
			iteration++;
			if (iteration >= maxIteration) return;

			float away = awayStep * theta;
			float around = theta + rotation;

			float x = centerX + Mathf.Cos(around) * away;
			float y = centerY + Mathf.Sin(around) * away;
			predicate(iteration, x, y);

			theta += chord / away;
		}
	}
}
