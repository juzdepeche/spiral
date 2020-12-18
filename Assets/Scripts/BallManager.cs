using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallManager : MonoBehaviour
{
	public static BallManager Instance;
	[SerializeField]
	private GameObject BallPrefab;
	[SerializeField]
	private int BallAmount = 3000;
	[SerializeField]
	private int MergeAmount = 100;
	private List<GameObject> ballObjects;
	private List<Ball> mergedBalls;
	private List<Ball> balls;
	private List<Ball> redBalls;
	private List<Ball> greenBalls;
	private List<Ball> blueBalls;
	public static event Action OnScoreChanged;

	private void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		balls = new List<Ball>();
		createBalls();
		regroupBalls();
		sortBalls();
		mergeBalls();
		createSpiral();
		OnScoreChanged?.Invoke();
	}

	private void Update()
	{
		HandleClick();
	}

	public int GetTotalScore()
	{
		return mergedBalls.Sum(b => b.Score);
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
		var colorIndex = UnityEngine.Random.Range(0, 3);
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
		return UnityEngine.Random.Range(0, 256);
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

	private void sortBalls()
	{
		Func<Ball, int> compareIndex = (ball) => ball.Index;
		HeapSort.SortBalls(redBalls, compareIndex);
		HeapSort.SortBalls(greenBalls, compareIndex);
		HeapSort.SortBalls(blueBalls, compareIndex);
	}

	private void mergeBalls()
	{
		mergedBalls = new List<Ball>();
		for (int i = 0; i < MergeAmount; i++)
		{
			int rValue = redBalls[i].Intensity;
			int gValue = greenBalls[i].Intensity;
			int bValue = blueBalls[i].Intensity;

			int score = redBalls[i].Index + greenBalls[i].Index + blueBalls[i].Index;
			Color color = new Color(rValue / 255f, gValue / 255f, bValue / 255f);
			Ball ball = new Ball(0, score, color);
			mergedBalls.Add(ball);
		}
	}

	private void createSpiral()
	{
		ballObjects = new List<GameObject>();
		sortMergedBalls();
		SpiralUtils.Create(0, 0, 10, 5, 100, 1f, spawnBall, MergeAmount);
	}

	private void spawnBall(int iteration, float x, float y)
	{
		var ball = mergedBalls[iteration];

		Vector3 position = new Vector3(x, 0, y);
		var newBallGameObject = Instantiate(BallPrefab, position, Quaternion.identity);

		newBallGameObject.GetComponent<BallObject>().Index = ball.Index;
		newBallGameObject.GetComponent<Renderer>().material.SetColor("_Color", ball.Color);

		ballObjects.Add(newBallGameObject);
	}

	private void updateSpiral()
	{
		if (ballObjects.Count == 0) return;

		SpiralUtils.Create(0, 0, 10, 5, 100, 1f, moveBall, ballObjects.Count);
	}

	private void moveBall(int iteration, float x, float y)
	{
		Vector3 position = new Vector3(x, 0, y);
		ballObjects[iteration].transform.localPosition = position;
	}

	private void sortMergedBalls()
	{
		Func<Ball, int> compareScore = (ball) => ball.Score;
		HeapSort.SortBalls(mergedBalls, compareScore);
		mergedBalls.Reverse();
	}

	private void HandleClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				SphereCollider sc = hit.collider as SphereCollider;
				if (sc != null)
				{
					OnBallClicked(sc.gameObject);
				}
			}
		}
	}

	private void OnBallClicked(GameObject ball)
	{
		var ballIndex = ball.GetComponent<BallObject>().Index;
		int index = mergedBalls.FindIndex(b => b.Index == ballIndex);
		mergedBalls.RemoveAt(index);

		var ballObjectIndex = ballObjects.FindIndex(b => b == ball);
		ballObjects.RemoveAt(ballObjectIndex);

		Destroy(ball);

		OnScoreChanged?.Invoke();
		updateSpiral();
	}
}
