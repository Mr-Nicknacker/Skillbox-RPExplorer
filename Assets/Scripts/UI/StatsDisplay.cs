using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statsNumbers;
    [SerializeField] private Image _fuelBar;
    
    void Start()
    {
        DroneFuel.onFuelChangeNormalized += DisplayFuel;
        PlayerScore.onScoreChange += DisplayPoints;
    }

    private void DisplayFuel(float fuel)
    {
        _fuelBar.fillAmount = fuel;
    }
    private void DisplayPoints(int points)
    {
        _statsNumbers.text =$"{points}";
    }
    private void OnDisable()
    {
        DroneFuel.onFuelChangeNormalized -= DisplayFuel;
        PlayerScore.onScoreChange -= DisplayPoints;
    }
}
