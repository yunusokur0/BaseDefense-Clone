using UnityEngine.Events;

public class PlayerSignals : MonoSignleton<PlayerSignals>
{
    public UnityAction<bool> onPlayerConditionChanged = delegate { };
    public UnityAction<bool> onMoveConditionChanged = delegate { };
}
