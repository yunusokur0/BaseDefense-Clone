using System;
using UnityEngine;
using UnityEngine.Events;

public class StackSignals : MonoSignleton<StackSignals>
{
    public UnityAction<GameObject> onPlayerCollectableStack = delegate { };
    public UnityAction<GameObject> onAmmonPKStack = delegate { };
    public UnityAction<GameObject> onRemoveEnemyFromList = delegate { };
    public Func<GameObject> onGetFirstMoney = delegate { return null; };
    public UnityAction<GameObject> onAddMoney = delegate { };
}
