using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int curPoints;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject buttonsToToggleOnSettingsOpen;
    [SerializeField] TextMeshProUGUI highScoreCelebrationText;
    [SerializeField] Bullet playerBulletPF;
    [SerializeField] int playerBulletDamageIncrease = 5;
    int currentWave;
    int enemyCount;
    bool isPaused = false;
    bool isNewGame = false;

    private void Awake()
    {
        Instance = this;

        currentWave = PlayerPrefs.GetInt(Constants.ENEMY_WAVE_SAVE_STRING);
        curPoints = PlayerPrefs.GetInt(Constants.SCORE_SAVE_STRING);

        enemyCount = -1; // So we do not attempt AdvaneWave() on the Home Scene.

        ToggleCursor(true);
    }

    private void Start()
    {
        EventManager.Instance.AwardPointsEvent += AwardPoints;
        EventManager.Instance.ReduceEnemyCountEvent += ReduceEnemyCount;
        EventManager.Instance.PlayerDeathEvent += EndGame;
        InputReader.Instance.PauseEvent += TogglePause;
    }

    private void Update()
    {
        while (enemyCount != 0) return;
            
        AdvanceWave();
    }

    private void OnDestroy()
    {
        EventManager.Instance.AwardPointsEvent -= AwardPoints;
        EventManager.Instance.ReduceEnemyCountEvent -= ReduceEnemyCount;
        EventManager.Instance.PlayerDeathEvent -= EndGame;
        InputReader.Instance.PauseEvent -= TogglePause;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public int GetWave()
    {
        return currentWave;
    }

    public int GetScore()
    {
        return curPoints;
    }

    void ReduceEnemyCount()
    {
        enemyCount--;
    }

    void AwardPoints(int pointsAwarded)
    {
        curPoints += pointsAwarded;
        EventManager.Instance.InvokeUpdateScoreTextUIEvent(curPoints);
    }

    void AdvanceWave()
    {
        if (isPaused) TogglePause();

        if (isNewGame)
        {
            currentWave = 1;

            PlayerPrefs.SetInt(Constants.SCORE_SAVE_STRING, 0);
            PlayerPrefs.SetInt(Constants.ENEMY_WAVE_SAVE_STRING, currentWave);
            playerBulletPF.ResetDamage();

            isNewGame = false;
        }
        else
        {
            int playerHealth = GameObject.Find("Player").GetComponent<Health>().GetHealth();
            currentWave += 1;

            PlayerPrefs.SetInt(Constants.PLAYER_HEALTH_SAVE_STRING, playerHealth);
            PlayerPrefs.SetInt(Constants.SCORE_SAVE_STRING, curPoints);
            PlayerPrefs.SetInt(Constants.ENEMY_WAVE_SAVE_STRING, currentWave);

            if ((currentWave % 5) == 1)
            {
                playerBulletPF.UpdateDamage(playerBulletDamageIncrease);
            }
        }

        SceneManager.LoadScene(1);
    }

    public void SetEnemyCount(int count)
    {
        enemyCount = count;
        ToggleCursor(enemyCount < 0);
    }

    private void ToggleCursor(bool isCursorOn)
    {
        if (!isCursorOn)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    #region BUTTON_AND_SCENE_MANAGEMENT
    public void OpenSettings()
    {
        if (buttonsToToggleOnSettingsOpen != null) buttonsToToggleOnSettingsOpen.SetActive(false);
        settingsMenu.SetActive(true);
        settingsMenu.GetComponent<VolumeManager>().SetSliderValues();
    }

    public void TogglePause()
    {
        if (pauseMenu == null) return;

        isPaused = !isPaused;
        ToggleCursor(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void BackToGameBtn()
    {
        if (isPaused) TogglePause();
        if (settingsMenu.activeSelf) settingsMenu.SetActive(false);
        if (buttonsToToggleOnSettingsOpen != null) buttonsToToggleOnSettingsOpen.SetActive(true);
    }

    public void HomeBtn()
    {
        if (isPaused) TogglePause();

        SceneManager.LoadScene(0);
    }

    public void GameStartingFresh()
    {
        PlayerPrefs.SetInt(Constants.PLAYER_HEALTH_SAVE_STRING, 
            PlayerPrefs.GetInt(Constants.PLAYER_MAX_HEALTH_SAVE_STRING));

        isNewGame = true;
        AdvanceWave();
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

    void EndGame()
    {
        gameOverScreen.SetActive(true);

        ToggleCursor(true);

        int highScore = PlayerPrefs.GetInt(Constants.HIGH_SCORE_SAVE_STRING);
        if(curPoints > highScore)
        {
            PlayerPrefs.SetInt(Constants.HIGH_SCORE_SAVE_STRING, curPoints);
            highScoreCelebrationText.text = "*New High Score!*\n" + curPoints; 
        }
    }
    #endregion
}
