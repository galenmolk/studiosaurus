using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class StringEvent : UnityEvent<string> { }

    [Serializable]
    public class PointerDataEvent : UnityEvent<PointerEventData> { }

    public class AssetEvent<T> : UnityEvent<T> where T : GenericAsset<T> { }

    [Serializable]
    public class Vector2Event : UnityEvent<Vector2> { }

    public class FloatEvent : UnityEvent<float> { }

    [Serializable]
    public class HandleDeltaEvent : UnityEvent<HandleDelta> { }
}
