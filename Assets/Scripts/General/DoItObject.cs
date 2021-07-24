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
        [SerializeField] private ConfigComponent[] configComponentPrefabs;

        [HideInInspector] public NegativeSizeWarning negativeSizeWarning;
        [HideInInspector] public List<ConfigComponent> configComponents = new List<ConfigComponent>();

        public Vector2Event onImageNativeSizeSet = new Vector2Event();
        public DragHandle dragHandle;
        public ResizeHandles resizeHandles;
        public float SizeRatio { get; set; }
        private Vector2 originalSize = Vector2.zero;

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
            CacheReferences();
            originalSize = RectTransform.sizeDelta;
            SizeRatio = originalSize.x / originalSize.y;
            CreateConfigComponents();
        }

        private void CacheReferences()
        {
            negativeSizeWarning = GetComponentInChildren<NegativeSizeWarning>(true);
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
            if (negativeSizeWarning.gameObject.activeInHierarchy)
            {
                resizeHandles.onSizeChanged.Invoke(originalSize);
                return;
            }

            OpenContextMenu(eventData);
        }

        private void OpenContextMenu(PointerEventData eventData)
        {
            if (contextMenu != null)
            {
                contextMenu.PositionMenu(eventData.position);
                return;
            }

            contextMenu = Instantiate(contextMenuPrefab, StudioCanvas.Instance.RectTransform);
            StartCoroutine(contextMenu.Open(this, eventData.position));
        }

        public void SubscribeControls(UnityAction<Vector2> action)
        {
            dragHandle.onDrag.AddListener(action);
        }
    }
}
