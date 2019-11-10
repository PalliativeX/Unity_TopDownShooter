using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{

    public Image fade;
    public GameObject gameOverUI;

	public RectTransform newWaveBanner;
	public Text newWaveTitle;
	public Text newWaveEnemyCount;
	public Text scoreUI;
	public Text gameOverScoreUI;
	public RectTransform healthBar;

	Spawner spawner;
	Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
		player.OnDeath += OnGameOver;
	}

	void Awake()
	{
		spawner = FindObjectOfType<Spawner>();
		spawner.OnNewWave += OnNewWave;
	}

	private void Update()
	{
		scoreUI.text = ScoreKeeper.score.ToString("D6");
		float healthPercent = 0;
		if (player != null)
		{
			healthPercent = player.health / player.startingHealth;
		}
		healthBar.localScale = new Vector3(healthPercent, 1, 1);
	}

	void OnNewWave(int waveNumber)
	{
		string[] numbers = { "One", "Two", "Three", "Four", "Five" };
		newWaveTitle.text = "- Wave " + numbers[waveNumber - 1] + " -";
		string enemyCountString = (spawner.waves[waveNumber - 1].infinite) ? "Infinite" : spawner.waves[waveNumber - 1].enemiesCount + "";
		newWaveEnemyCount.text = "Enemies: " + enemyCountString;

		StopCoroutine("AnimateNewWaveBanner");
		StartCoroutine("AnimateNewWaveBanner");
	}

	IEnumerator AnimateNewWaveBanner()
	{
		float delayTime = 1f;
		float speed = 3f;
		float animationPercent = 0;
		int dir = 1;

		float endDelayTime = Time.time + 1 / speed + delayTime;

		while (animationPercent >= 0)
		{
			animationPercent += Time.deltaTime * speed * dir;

			if (animationPercent >= 1)
			{
				animationPercent = 1;
				if (Time.time > endDelayTime)
				{
					dir = -1;
				}
			}
			newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-700, 400, animationPercent);
			yield return null;
		}

	}

	void OnGameOver()
	{
		Cursor.visible = true;
		StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, .95f), 1));
		gameOverScoreUI.text = scoreUI.text;
		scoreUI.gameObject.SetActive(false);
		healthBar.transform.parent.gameObject.SetActive(false);
		gameOverUI.SetActive(true);
	}

	IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            fade.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("Menu");
	}

}