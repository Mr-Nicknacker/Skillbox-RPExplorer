using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Canvases")]
    [SerializeField] private GameObject _victoryWindowCanvas;
    [SerializeField] private GameObject _defeatWindowCanvas;
    [SerializeField] private GameObject _pauseWindowCanvas;
    [SerializeField] private GameObject _gameHUD;
    [Header("UI Buttons")]
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeGame;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _toMainMenuButton;
    [Header("Delay to show a screen")]
    [SerializeField] private float _timeToShowVictory;
    [SerializeField] private float _timeToShowDefeat;

    private void Awake()
    {
        HandleButtonInput();
        ShowGameWindow(); 
    }
    private void Start()
    {
        DroneController.onLandingStateChange += Drone_onLandingStateChange;
        RetryButtonNotifier.OnRetryButtonClick += ResetGame;
    }

    private void Drone_onLandingStateChange(DroneController.LandingState state)
    {
        switch (state)
        {
            case DroneController.LandingState.Crashed:
                StartCoroutine(ShowDefeatWindow());
                break;
            case DroneController.LandingState.Landed:
                StartCoroutine(ShowVictoryWindow());
                break;
        }
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private IEnumerator ShowDefeatWindow()
    {
        _victoryWindowCanvas.SetActive(false);
        yield return new WaitForSeconds(_timeToShowDefeat);
        _defeatWindowCanvas.SetActive(true);
    }
    private IEnumerator ShowVictoryWindow()
    {
        _defeatWindowCanvas.SetActive(false);
        yield return new WaitForSeconds(_timeToShowVictory);
        _victoryWindowCanvas.SetActive(true);       
    }
    private void ShowGameWindow()
    {
        _gameHUD.SetActive(true);
        _victoryWindowCanvas.SetActive(false);
        _defeatWindowCanvas.gameObject.SetActive(false);
    }
    private void ResetGame()
    {
        PlayerScore.GetInstance().ResetScore();
        SceneManager.LoadScene(1);
        UnPauseGame();
    }
    private void PauseGame()
    {
        Time.timeScale = 0.0f;
        _gameHUD.SetActive(false);
        _victoryWindowCanvas.SetActive(false);
        _defeatWindowCanvas.gameObject.SetActive(false);
        _pauseWindowCanvas.SetActive(true);
    }
    private void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        _gameHUD.SetActive(true);
        _pauseWindowCanvas.SetActive(false);
    }
    private void HandleButtonInput()
    {
        _nextLevelButton.onClick.AddListener(ResetGame);
        _toMainMenuButton.onClick.AddListener(OpenMainMenu);
        _pauseButton.onClick.AddListener(PauseGame);
        _resumeGame.onClick.AddListener(UnPauseGame);

    }
    private void OnDestroy()
    {
        DroneController.onLandingStateChange -= Drone_onLandingStateChange;
        RetryButtonNotifier.OnRetryButtonClick -= ResetGame;
        StopAllCoroutines();
    }
}
