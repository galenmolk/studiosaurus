using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class StringEvent : UnityEvent<string> { }

    [Serializable]
    public class PointerDataEvent : UnityEvent<PointerEventData> { }

    public class AssetEvent<T> : UnityEvent<T> where T : GenericAsset<T> { }
}
