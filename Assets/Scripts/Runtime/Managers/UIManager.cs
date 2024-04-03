using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject JoystickPanel;
    private void Start()
    {
        OnPlayStart();
    }
    private void OnPlayStart()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
    }
    public void OnPlay()
    {
        JoystickPanel.SetActive(true);
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Score, 2);
        CoreGameSignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(1);
    }
}
