using UnityEngine;

public class FuelPickup : MonoBehaviour, IPickup
{
    [SerializeField] private float _fuelToGive;
    public float GetFuel()
    {
        return _fuelToGive;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
