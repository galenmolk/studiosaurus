using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class StudioControls : MonoBehaviour
    {
        [SerializeField] private Window studioControlMenu = null;
        [SerializeField] private Window objectDeletionWindow = null;
        [SerializeField] private DoItObject findablePrefab = null;

        private readonly List<DoItObject> allFindables = new List<DoItObject>();

        private CanvasGroup menuCanvasGroup;
        private CanvasGroup objectDeletionWindowCanvasGroup;
        private RectTransform studioControlMenuRectTransform;
        private RectTransform objectDeletionWindowRectTransform;

        private DoItObject objectToDelete = null;

        private void Awake()
        {
            studioControlMenuRectTransform = studioControlMenu.transform as RectTransform;
            objectDeletionWindowRectTransform = objectDeletionWindow.transform as RectTransform;
            objectDeletionWindowCanvasGroup = objectDeletionWindow.GetComponent<CanvasGroup>();
            menuCanvasGroup = studioControlMenu.GetComponent<CanvasGroup>();
        }

        public void OpenControlMenu(PointerEventData eventData)
        {
            studioControlMenuRectTransform.SetAsLastSibling();

            studioControlMenu.OpenWindow(studioControlMenuRectTransform);

            Vector2 size = studioControlMenuRectTransform.sizeDelta * StudioCanvas.Instance.ScaleFactor;
            Vector2 clickPos = eventData.position;

            float xOffset = Screen.width - clickPos.x > size.x ? size.x * 0.5f : size.x * -0.5f;
            float yOffset = clickPos.y > size.y ? size.y * -0.5f : size.y * 0.5f;

            Vector2 offset = new Vector2(xOffset, yOffset);

            studioControlMenuRectTransform.anchoredPosition = StudioCanvas.Instance.RectTransform.InverseTransformPoint(clickPos + offset);
            Utils.SetCanvasGroupEnabled(menuCanvasGroup, true);
        }

        public void CreateNewFindable()
        {
            studioControlMenu.Close();
            DoItObject findable = Instantiate(findablePrefab, StudioCanvas.Instance.RectTransform);
            allFindables.Add(findable);
            Debug.Log("Add Findable");
            findable.onConfirmDelete.AddListener(ConfirmDeleteObject);
        }

        public void ExportJson()
        {
            string json = FindablesJsonBuilder.CreateFindablesJsonArray(allFindables);
            Debug.Log(json);
            studioControlMenu.Close();
            Debug.Log("Export JSON");
        }

        public void ConfirmDeleteObject(DoItObject doItObject)
        {
            objectToDelete = doItObject;
            objectDeletionWindowRectTransform.SetAsLastSibling();
            objectDeletionWindow.OpenWindow(objectDeletionWindowRectTransform);
            Utils.SetCanvasGroupEnabled(objectDeletionWindowCanvasGroup, true);
        }

        public void DeleteObject()
        {
            allFindables.Remove(objectToDelete);
            Destroy(objectToDelete.gameObject);
            objectDeletionWindow.Close();
        }
    }
}
