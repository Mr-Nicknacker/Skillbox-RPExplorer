using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private DroneController _droneController;
    [SerializeField] private DroneFuel _droneFuel;

    private PlayerScore _playerScore;
    
    
    private void Awake()
    {
        _playerScore = new();
    }
    private void Start()
    {
        _droneController.onPointsPickup += AddPoints;
        _droneController.onFuelPickup += AddFuel;        
    }
    private void AddPoints(int points)
    {
        _playerScore.AddScore(points);
    }
    private void AddFuel (float fuel)
    {
        _droneFuel.AddFuel(fuel);
    }
    private void OnDisable()
    {
        _droneController.onPointsPickup -= AddPoints;
        _droneController.onFuelPickup -= AddFuel;
    }
}
