using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Canvases")]
    [SerializeField] private GameObject _victoryWindowCanvas;
    [SerializeField] private GameObject _defeatWindowCanvas;
    [SerializeField] private GameObject _gameHUD;
    [Header("UI Buttons")]
    [SerializeField] private Button _resetGameButton;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _nextLevelButton;

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
        //DroneFuel.ResetFuel();
        SceneManager.LoadScene(1);
    }
    private void HandleButtonInput()
    {
        _resetGameButton.onClick.AddListener(ResetGame);
        _retryButton.onClick.AddListener(ResetGame);
        _nextLevelButton.onClick.AddListener(ResetGame);
    }
    private void OnDestroy()
    {
        DroneController.onLandingStateChange -= Drone_onLandingStateChange;
        StopAllCoroutines();
    }
}
