using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoSignleton<UISignals>
    {
        public UnityAction<byte> onSetDiamondValue = delegate { };
    }
