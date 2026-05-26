using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private Button _startGameButton;
    private void Awake()
    {
        HandleButtonInput();
    }
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }
    private void HandleButtonInput()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _quitGameButton.onClick.AddListener(QuitGame);
    }
}
