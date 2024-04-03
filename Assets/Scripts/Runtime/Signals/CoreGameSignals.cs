using System;
using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals : MonoSignleton<CoreGameSignals>
{
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public Func<GameObject> onGetEnemyTarget = delegate { return default; };
}