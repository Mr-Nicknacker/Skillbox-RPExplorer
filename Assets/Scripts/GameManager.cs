using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _victoryWindowCanvas;
    [SerializeField] private GameObject _defeatWindowCanvas;
    [SerializeField] private GameObject _gameHUD;

    [SerializeField] private Button _resetGameButton;
    [SerializeField] private Button _nextLevelButton;
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
        Debug.Log(state);
        switch (state)
        {
            case DroneController.LandingState.Crashed:
                ShowDefeatWindow();
                break;
            case DroneController.LandingState.Landed:
                ShowVictoryWindow();
                break;
        }
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void ShowDefeatWindow()
    {
        _victoryWindowCanvas.SetActive(false);
        _defeatWindowCanvas.SetActive(true);
    }
    private void ShowVictoryWindow()
    {
        _victoryWindowCanvas.SetActive(true);
        _defeatWindowCanvas.SetActive(false);

    }
    private void ShowGameWindow()
    {
        _gameHUD.SetActive(true);
        _victoryWindowCanvas.SetActive(false);
        _defeatWindowCanvas.SetActive(false);
    }
    private void ResetGame()
    {
        //PlayerScore.ResetScore();
        //DroneFuel.ResetFuel();
        SceneManager.LoadScene(1);
    }
    private void HandleButtonInput()
    {
        _resetGameButton.onClick.AddListener(ResetGame);
    }
}
