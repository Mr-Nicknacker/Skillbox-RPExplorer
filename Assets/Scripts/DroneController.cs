using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneController : MonoBehaviour
{
    [SerializeField] private float _upForce;
    [SerializeField] private float _sideForce;
    [SerializeField] private float _landingSpeedThreshold;
    [SerializeField] private float _landingAngleThreshold;


    private enum DroneState
    {
        WatingToStart,
        Normal,
        GameOver
    }
    public enum LandingState
    {
        Crashed,
        Landed
    }

    private Rigidbody _droneRigidbody;
    private DroneFuel _droneFuel;
    private DroneDestructor _droneDestructor;
    private DroneState _droneState;
    private LandingState _landingState;

    public static event Action<int> onPointsPickup;
    public static event Action<float> onFuelPickup;
    public static event Action<LandingState> onLandingStateChange;

    private void Awake()
    {
        _droneRigidbody = GetComponent<Rigidbody>();
        _droneRigidbody.sleepThreshold = 0f;
        _droneRigidbody.useGravity = false;

        _droneFuel = GetComponent<DroneFuel>();
        SetDroneState(DroneState.WatingToStart);

        _droneDestructor = GetComponent<DroneDestructor>();
    }
    private void FixedUpdate()
    {
        switch (_droneState)
        {
            case DroneState.WatingToStart:
                if ((Keyboard.current.wKey.isPressed) ||
                    Keyboard.current.aKey.isPressed ||
                    Keyboard.current.dKey.isPressed)
                {
                    _droneRigidbody.useGravity = true;
                    SetDroneState(DroneState.Normal);
                }
                break;
            case DroneState.Normal:

                if (_droneFuel.GetCurrentFuel() <= 0)
                {
                    return;
                }
                if ((Keyboard.current.wKey.isPressed) ||
                    Keyboard.current.aKey.isPressed ||
                    Keyboard.current.dKey.isPressed)
                {
                    _droneFuel.ConsumeFuel();
                }

                if (Keyboard.current.wKey.isPressed)
                {
                    ApplyUpForce();
                }
                if (Keyboard.current.aKey.isPressed)
                {
                    ApplyLeftYaw();

                }
                if (Keyboard.current.dKey.isPressed)
                {
                    ApplyRightYaw();
                }
                break;
            case DroneState.GameOver:
                break;
        }

    }

    private void ApplyUpForce()
    {
        _droneRigidbody.AddForce(transform.up * _upForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
    private void ApplyRightYaw()
    {
        _droneRigidbody.AddTorque(Vector3.forward * -_sideForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
    private void ApplyLeftYaw()
    {
        _droneRigidbody.AddTorque(Vector3.forward * _sideForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
    private void OnCollisionEnter(Collision collision)
    {
        float landingSpeed = collision.relativeVelocity.magnitude;
        var landingAngleCoef = Vector3.Dot(Vector3.up, transform.up);

        if (landingSpeed > _landingSpeedThreshold || landingAngleCoef < _landingAngleThreshold)
        {
            SetDroneState(DroneState.GameOver);
            SetLandingState(LandingState.Crashed);
            _droneDestructor.Detonate();
            return;
        }

        if (collision.gameObject.TryGetComponent<LandingPad>(out LandingPad landingPad))
        {
            SetDroneState(DroneState.GameOver);
            SetLandingState(LandingState.Landed);
        }
        else
        {
            SetLandingState(LandingState.Crashed);
            _droneDestructor.Detonate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PointsPickup>(out PointsPickup pointsPickup))
        {
            int pointsReceived = pointsPickup.GetPoints();
            onPointsPickup?.Invoke(pointsReceived);
            pointsPickup.DestroySelf();
        }
        if (other.TryGetComponent<FuelPickup>(out FuelPickup fuelPickup))
        {
            float fuelReceived = fuelPickup.GetFuel();
            onFuelPickup?.Invoke(fuelReceived);
            fuelPickup.DestroySelf();
        }
    }
    private void SetDroneState(DroneState newState)
    {
        _droneState = newState;
    }
    private void SetLandingState(LandingState newState)
    {
        _landingState = newState;
        onLandingStateChange?.Invoke(newState);
    }
}
