using System.Collections;
using UnityEngine;

namespace Studiosaurus
{  
    public class TweenSizeComponent : SizeComponent
    {
        [SerializeField] private SizeComponent sizeComponent;

        private void Start()
        {
            sizeComponent = transform.parent.GetComponentInChildren<SizeComponent>();
        }

        [UnityEngine.ContextMenu("Scale")]
        public override string GetComponentAsJSON()
        {
            Vector2 startingSize = sizeComponent.currentVector.Value;
            return JsonSerializer.GetTweenSizeScale(this, startingSize);
        }
    }
}
