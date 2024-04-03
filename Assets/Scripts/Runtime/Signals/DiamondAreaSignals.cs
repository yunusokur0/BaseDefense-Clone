using System;
using UnityEngine;
using UnityEngine.Events;

public class DiamondAreaSignals : MonoSignleton<DiamondAreaSignals>
{
    public Func<GameObject> onGetMiner = delegate { return null; };
    public UnityAction onSetMiner = delegate { };
}
