using System;
using UnityEngine;
using UnityEngine.Events;

public class PoolSignals : MonoSignleton<PoolSignals>
{
    public Func<byte,GameObject> onGetPoolObject = delegate { return default; };
    public UnityAction<GameObject,byte> onReturnToPool = delegate { };
    public Func<GameObject> onGetBulletBox = delegate { return default; };
}
