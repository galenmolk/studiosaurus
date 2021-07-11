using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class DoItObject : MonoBehaviour
    {
        [SerializeField] private ContextMenu contextMenuPrefab = null;

        public DragHandle dragHandle;
        public ResizeHandles resizeHandles;

        [SerializeField] private ConfigComponent[] configComponentPrefabs;
        [HideInInspector] public List<ConfigComponent> configComponents = new List<ConfigComponent>();

        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = transform as RectTransform;

                return rectTransform;
            }
        }

        private RectTransform rectTransform;
        private ContextMenu contextMenu;

        private void Awake()
        {
            CreateConfigComponents();
        }

        private void CreateConfigComponents()
        {
            for (int i = 0, length = configComponentPrefabs.Length; i < length; i++)
            {
                configComponents.Add(Instantiate(configComponentPrefabs[i], transform));
            }
        }

        public void InspectObject(PointerEventData eventData)
        {
            StartCoroutine(OpenContextMenu(eventData));
        }

        private IEnumerator OpenContextMenu(PointerEventData eventData)
        {
            if (contextMenu != null)
            {
                //contextMenu.transform.SetParent(transform);
                contextMenu.PositionMenu(eventData.position);
                yield break;
            }

            contextMenu = Instantiate(contextMenuPrefab, StudioCanvas.Instance.RectTransform);

            yield return StartCoroutine(contextMenu.Open(this));
            contextMenu.PositionMenu(eventData.position);
        }

        public void SubscribeControls(UnityAction<Vector2> action)
        {
            dragHandle.onDrag.AddListener(action);
        }

        public void DisplayDebug()
        {
            Debug.Log("I'm not null");
        }
    }
}
