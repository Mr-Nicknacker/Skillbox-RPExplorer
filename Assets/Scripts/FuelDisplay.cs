using UnityEngine;

public class FuelDisplay : MonoBehaviour
{
    private void Start()
    {
        DroneFuel.onFuelChangeNormalized += DisplayFuel;
    }
    private void DisplayFuel(float newFuel)
    {
        Debug.Log($"new fuel amount is {newFuel}");
    }
}
