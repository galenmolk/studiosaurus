using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class DoItObject : MonoBehaviour
    {
        [SerializeField] private ContextMenu contextMenuPrefab = null;

        [HideInInspector] public NegativeSizeWarning negativeSizeWarning;

        public ConfigSection[] configSections;
        public DoitObjectEvent onConfirmDelete = new DoitObjectEvent();

        [HideInInspector] public UnityEvent onNewSpriteAssigned = new UnityEvent();
        [HideInInspector] public UnityEvent fixNegativeSize = new UnityEvent();

        public DragHandle dragHandle;
        public ResizeHandles resizeHandles;

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
            CreateConfigComponents();
        }

        private void CacheReferences()
        {
            negativeSizeWarning = GetComponentInChildren<NegativeSizeWarning>(true);
        }

        private void CreateConfigComponents()
        {
            foreach (ConfigSection configSection in configSections)
            {
                foreach (ConfigComponent configComponent in configSection.configComponentPrefabs)
                {
                    configSection.configComponents.Add(Instantiate(configComponent, transform));
                }
            }
        }

        public void InspectObject(PointerEventData eventData)
        {
            if (negativeSizeWarning.gameObject.activeInHierarchy)
            {
                fixNegativeSize?.Invoke();
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
            contextMenu.Open(this, eventData.position);
        }

        public void SubscribeControls(UnityAction<Vector2> action)
        {
            dragHandle.onDrag.AddListener(action);
        }

        private void OnDestroy()
        {
            if (contextMenu != null)
                contextMenu.Close();
        }
    }
}
