using UnityEngine;

public class PointsPickup : MonoBehaviour, IPickup
{
    [SerializeField] private int _pointsToGive;
    public int GetPoints()
    {
        return _pointsToGive;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
