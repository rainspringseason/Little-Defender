using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;

    [Header("Timer Settings")]
    public float gameDuration = 120f;

    [Header("UI Timer")]
    public TMP_Text timerText;

    private Tower tower;
    private bool isPaused = false;
    private bool gameEnded = false;
    private float timer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        tower = FindObjectOfType<Tower>();
    }

    private void Start()
    {
        timer = gameDuration;
        Time.timeScale = 1;

        AudioManager.Instance.Play("BackgroundMusic");
    }

    private void Update()
    {
        if (gameEnded) return;

        HandlePause();
        if (isPaused) return;

        TimerCountdown();
        UpdateTimerUI();
        CheckGameOver();
    }

    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void TimerCountdown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Win();
        }
    }

    private void CheckGameOver()
    {
        if (tower == null || tower.IsDead())
        {
            Lose();
        }
    }

    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Win()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 0;
        winMenu.SetActive(true);

        AudioManager.Instance.Stop("BackgroundMusic");
        AudioManager.Instance.Play("Win");
    }

    private void Lose()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 0;
        loseMenu.SetActive(true);

        AudioManager.Instance.Stop("BackgroundMusic");
        AudioManager.Instance.Play("Lose");
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"{Mathf.Floor(timer / 60):00}:{Mathf.Floor(timer % 60):00}";
        }
    }

    public float GetRemainingTime()
    {
        return timer;
    }

    public bool GetPause()
    {
        return isPaused;
    }
}
