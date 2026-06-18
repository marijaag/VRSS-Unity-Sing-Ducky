using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class SimonSaysManager : MonoBehaviour
{
    public DuckController[] ducks;
    private int highScore = 0;

    [Header("Audio")]
    public AudioSource audioIzvor;
    public AudioClip gameOverZvuk;

     [Header("UI")]
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverRestartButton;
    public GameObject blurPanel;

    private List<int> sequence = new List<int>();
    private int playerIndex = 0;
    private bool playerTurn = false;
    private int xp = 0;
    private int roundCount = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
         StartCoroutine(GameStart());
    }

    void UpdateHighScoreUI()
    {
        highScoreText.text = "HighScore: " + highScore + " xp";
    }

    void CheckHighScore()
{
    if (xp > highScore)
    {
        highScore = xp;
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
        UpdateHighScoreUI();
    }
}

public void BonusPtica()
{
    int bonus = sequence.Count * 10; 
    xp += bonus;
    xpText.text = "" + xp + " xp";
    CheckHighScore();
    StartCoroutine(ShowBonusText(bonus));
}

IEnumerator ShowBonusText(int bonus)
{
    yield return StartCoroutine(FadeText("Bonus! +" + bonus + " xp"));
    yield return new WaitForSeconds(1.5f);
    yield return StartCoroutine(FadeOutText());
}

    IEnumerator GameStart()
    {
        if (gameOverRestartButton != null) gameOverRestartButton.SetActive(false);
        if (blurPanel != null) blurPanel.SetActive(false);

        SetAllClickable(false);
        roundText.text = "";
        xpText.text = "0 xp";

        yield return StartCoroutine(FadeText("3"));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeText("2"));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeText("1"));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOutText());

        yield return StartCoroutine(FadeText("Watch closely!"));
        yield return new WaitForSeconds(1.2f);
        yield return StartCoroutine(FadeOutText());
        
        StartCoroutine(StartNewRound());
    }

    IEnumerator StartNewRound()
    {
        roundCount = roundCount+1;
        playerTurn = false;
        SetAllClickable(false);
        yield return StartCoroutine(FadeText("Round " + roundCount + "!"));
        roundText.text = "Round " + roundCount;
        sequence.Add(Random.Range(0, 4));

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeOutText());

        yield return new WaitForSeconds(0.7f);

        foreach (int duckID in sequence)
        {
            yield return StartCoroutine(ducks[duckID].PlaySequenceAnimation());
            yield return new WaitForSeconds(0.2f);
        }

        yield return StartCoroutine(FadeText("Your turn!"));
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(FadeOutText());

        playerIndex = 0;
        playerTurn = true;
        SetAllClickable(true);
    }

    public void PlayerClickedDuck(int duckID)
    {
        if (!playerTurn) return;

        if (duckID == sequence[playerIndex])
        {
            playerIndex++;
            if (playerIndex >= sequence.Count)
            {
                playerTurn = false;
                SetAllClickable(false);
                xp += sequence.Count * 10;
                xpText.text = "" + xp + " xp";
                CheckHighScore();
                StartCoroutine(ShowBravoAndContinue());
            }
        }
        else
        {
            playerTurn = false;
            SetAllClickable(false);
            StartCoroutine(GameOver());
        }
    }

    IEnumerator ShowBravoAndContinue()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeText("Bravo! +" + sequence.Count * 10 + " XP"));
        yield return new WaitForSeconds(1.3f);
        yield return StartCoroutine(FadeOutText());
        StartCoroutine(StartNewRound());
    }

    IEnumerator GameOver()
    {
        CheckHighScore();
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(FadeText("Game Over!\n"));
        if (audioIzvor != null && gameOverZvuk != null)
        {
            audioIzvor.PlayOneShot(gameOverZvuk);
        }

        if (gameOverRestartButton != null) gameOverRestartButton.SetActive(true);
        if (blurPanel != null) blurPanel.SetActive(true);
        Time.timeScale = 0f;
        // yield return new WaitForSeconds(2f);
        // roundCount = 0;
        // sequence.Clear();
        // xp = 0;
        // xpText.text = "0 xp";
        // StartCoroutine(GameStart());
    }

    void SetAllClickable(bool state)
    {
        foreach (var duck in ducks)
            duck.SetClickable(state);
    }

    IEnumerator FadeText(string message, float duration = 0.2f)
{
    statusText.text = message;
    Color c = statusText.color;
    c.a = 0;
    statusText.color = c;

    float time = 0;
    while (time < duration)
    {
        time += Time.deltaTime;
        c.a = Mathf.Lerp(0, 1, time / duration);
        statusText.color = c;
        yield return null;
    }
    c.a = 1;
    statusText.color = c;
}

IEnumerator FadeOutText(float duration = 0.2f)
{
    Color c = statusText.color;
    float time = 0;
    while (time < duration)
    {
        time += Time.deltaTime;
        c.a = Mathf.Lerp(1, 0, time / duration);
        statusText.color = c;
        yield return null;
    }
    c.a = 0;
    statusText.color = c;
    statusText.text = "";
}

public void RestartGame()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

public void ExitGame()
{
    SceneManager.LoadScene(0);
}
}