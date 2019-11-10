using System.Collections;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
	public static int score { get; private set; }
	float lastEnemyKilledTime;
	int streakCount;
	float streakExpiryTime = 1.5f;

	void Start()
	{
		Enemy.OnDeathStatic += onEnemyKilled;
		FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
		;
	}

	void onEnemyKilled()
	{
		if (Time.time < lastEnemyKilledTime + streakExpiryTime)
			streakCount++;
		else
			streakCount = 0;

		lastEnemyKilledTime = Time.time;

		score += 5 + (int)Mathf.Pow(2, streakCount);
	}

	void OnPlayerDeath()
	{
		Enemy.OnDeathStatic -= onEnemyKilled;
	}

}
