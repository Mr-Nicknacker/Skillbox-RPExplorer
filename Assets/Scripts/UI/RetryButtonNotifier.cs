using System;
using UnityEngine;
using UnityEngine.UI;

public class RetryButtonNotifier : MonoBehaviour
{
    private Button _retryButton;
    public static event Action OnRetryButtonClick;

    void Start()
    {
        _retryButton= GetComponent<Button>();
        _retryButton.onClick.AddListener(SendRetryEvent);
    }

    private void SendRetryEvent()
    {
        OnRetryButtonClick?.Invoke();
    }


}
