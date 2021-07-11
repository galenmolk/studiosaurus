using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studiosaurus
{
    [RequireComponent(typeof(Image))]
    public class DoItObject : MonoBehaviour
    {
        [SerializeField] private ContextMenu contextMenuPrefab = null;

        [SerializeField] private Image image = null;
        [SerializeField] private AudioSource audioSource = null;

        public DragHandle dragHandle = null;
        public ResizeHandles resizeHandles = null;

        public ConfigComponent[] configComponents = null;

        private ContextMenu contextMenu;

        private void Awake()
        {
            CreateConfigComponents();
        }

        private void CreateConfigComponents()
        {
            for (int i = 0, length = configComponents.Length; i < length; i++)
            {
                Instantiate(configComponents[i], transform);
            }
        }

        public void OpenContextMenu(PointerEventData eventData)
        {
            if (contextMenu != null)
            {
                contextMenu.transform.SetParent(transform);
                contextMenu.PositionMenu(eventData.position);
                return;
            }

            contextMenu = Instantiate(contextMenuPrefab, transform);
            StartCoroutine(contextMenu.Open(this, eventData.position));
        }
    }
}
