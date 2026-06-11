using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputListener : MonoBehaviour
{
    private DroneInputActions _inputActions;

    public static event Action onPausePressed;
    private static PlayerInputListener _instance;
    //Можно сделать ивенты на каждую кнопку: вверх, влево, вправо. Это позволит также включать партиклы.
    //А как будем обрабатывать, чтоб партиклы шли и из основного двигателя, и из бокового?

    public static PlayerInputListener GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PlayerInputListener();
        }
        return _instance;
    }
    private void Awake()
    {
        _inputActions = new DroneInputActions();
        _inputActions.Enable();
        _inputActions.Player.Pause.performed += context => onPausePressed?.Invoke();
    }

    public bool IsUpActionPressed() {
        return _inputActions.Player.GoUp.IsPressed(); 
    }
    public bool IsLeftActionPressed() {
        return _inputActions.Player.GoLeft.IsPressed();
    }
    public bool IsRightActionPressed() {
        return _inputActions.Player.GoRight.IsPressed();
    }
    
    private void OnDestroy()
    {
        _inputActions.Disable();
    }
}
