using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string PLAYER_PREF_HIGHSCORE = "Highscore";

    [Header("Game Elements")]
    public GameObject blade;

    [Header("Score Elements")]
    public int score;
    public TMPro.TextMeshProUGUI scoreText;

    [Header("Highscore Elements")]
    public int highScore = 0;
    public TMPro.TextMeshProUGUI highScoreText;

    [Header("GameOver Elements")]
    public GameObject gameOverPanel;

    [Header("Sounds")]
    public AudioClip[] sliceSounds;
    public AudioClip bombSound;

    private AudioSource audioSource;

    private void Awake()
    {
        this.gameOverPanel.SetActive(false);
        this.audioSource = GetComponent<AudioSource>();
        UpdateHighscore();
    }

    public void IncreaseScore(int num)
    { 
        this.score += num; 
        this.scoreText.text = this.score.ToString();
        PlayRandomSliceSound();

        if (this.score > this.highScore)
        {
            PlayerPrefs.SetInt(PLAYER_PREF_HIGHSCORE, this.score);
            UpdateHighscore();
        }
    }

    public void OnBombHit()
    {
        PlayBombSound();
        Time.timeScale = 0; // das ganze Spiel wird angehalten
        this.gameOverPanel.SetActive(true);

        foreach (GameObject interactableElement in GameObject.FindGameObjectsWithTag("InteractableElement"))
        {
            Destroy(interactableElement);
        }

        this.blade.SetActive(false);
    }

    public void RestartGame()
    {
        Debug.Log("Restart game");
        UpdateHighscore();

        this.score = 0;
        this.scoreText.text = "0";

        Time.timeScale = 1;
        this.gameOverPanel.SetActive(false);

        this.blade.SetActive(true);
    }

    private void UpdateHighscore()
    {
        this.highScore = PlayerPrefs.GetInt(PLAYER_PREF_HIGHSCORE);
        this.highScoreText.text = "Highscore: " + this.highScore.ToString();
    }

    public void PlayRandomSliceSound()
    {
        AudioClip audioClip = this.sliceSounds[Random.Range(0, this.sliceSounds.Length)];
        this.audioSource.PlayOneShot(audioClip);
    }

    public void PlayBombSound()
    {
        this.audioSource.PlayOneShot(this.bombSound);
    }
}
