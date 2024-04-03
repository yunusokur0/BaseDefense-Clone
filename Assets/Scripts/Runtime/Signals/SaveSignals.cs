using UnityEngine.Events;

public class SaveSignals : MonoSignleton<SaveSignals>
{
    public UnityAction onSaveGameData = delegate { };
}
