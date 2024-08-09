using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
	private int previousScore = 0;

	public TextMeshProUGUI[] spScore;

	public int score = 0;

	private void Update()
	{
		spScore[0].text = "Score : " + score;
		spScore[1].text = "Score : " + score;
		previousScore = score;
	}
}