using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private DroneFuel _droneFuel;

    private void Start()
    {
        DroneController.onPointsPickup += AddPoints;
        DroneController.onFuelPickup += AddFuel;        
    }
    private void AddPoints(int points)
    {
        PlayerScore.GetInstance().AddScore(points);
    }
    private void AddFuel (float fuel)
    {
        _droneFuel.AddFuel(fuel);
    }
    private void OnDisable()
    {
        DroneController.onPointsPickup -= AddPoints;
        DroneController.onFuelPickup -= AddFuel;
    }
}
