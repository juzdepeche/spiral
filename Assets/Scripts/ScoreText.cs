using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
	Text _text;

	private void Awake()
	{
		_text = GetComponent<Text>();
	}

	private void updateScore()
	{
		var newScore = BallManager.Instance.GetTotalScore();
		_text.text = "Scope: " + newScore;
	}

	private void OnEnable()
	{
		BallManager.OnScoreChanged += updateScore;
	}

	private void OnDisable()
	{
		BallManager.OnScoreChanged -= updateScore;
	}
}
