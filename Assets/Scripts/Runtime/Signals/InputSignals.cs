using UnityEngine.Events;

public class InputSignals : MonoSignleton<InputSignals>
{
    public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
    public UnityAction<bool> onPKInput = delegate { };
}