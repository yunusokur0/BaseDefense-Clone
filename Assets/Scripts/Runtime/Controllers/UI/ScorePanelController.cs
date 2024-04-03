using TMPro;
using UnityEngine;

public class ScorePanelController : MonoBehaviour
{
    public TextMeshProUGUI diamondText;
    private int valıes;
    public void OnSetDiamondValue(byte value)
    {
        valıes+=value;
        diamondText.text = valıes.ToString();       
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        UISignals.Instance.onSetDiamondValue += OnSetDiamondValue;
    }
    private void UnSubscribeEvents()
    {
        UISignals.Instance.onSetDiamondValue -= OnSetDiamondValue;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
