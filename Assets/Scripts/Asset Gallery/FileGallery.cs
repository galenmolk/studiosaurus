using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileGallery : MonoBehaviour
{
    private static FileGallery sharedInstance;
    public static FileGallery Instance
    {
        get
        {
            if (sharedInstance == null)
                sharedInstance = FindObjectOfType<FileGallery>();

            return sharedInstance;
        }
    }

    [SerializeField] private FileSlot fileSlotPrefab = null;
    [SerializeField] private TMP_Text uploadPromptText = null;
    [SerializeField] private AssetDeletionWindow assetDeletionWindow = null;
    [SerializeField] private Button selectButton = null;
    [SerializeField] private TMP_Text selectButtonText = null;
    [SerializeField] private Transform fileSlotContainer = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private Dictionary<string, FileSlot> spriteSlotPairs = new Dictionary<string, FileSlot>();

    private DoItObject selectedDoItObject;
    private FileSlot selectedSlot;

    private const string SELECT_BUTTON_TEXT = "Select";
    private const string ASSET_NAME_TEXT_COLOR = "#c0c0c0ff";

    private void Awake()
    {
        sharedInstance = this;
        UpdateSelectButton();
    }

    public void Open(DoItObject doItObject)
    {
        Utils.SetCanvasGroupEnabled(canvasGroup, true);

        selectedDoItObject = doItObject;
    }

    public void Select()
    {
        Utils.SetCanvasGroupEnabled(canvasGroup, false);

        if (selectedSlot != null && selectedDoItObject != null)
        {
            selectedDoItObject.UpdateSpriteAsset(selectedSlot.SpriteAsset);
            selectedSlot.DeselectSlot();
        }

        selectedSlot = null;
        selectedDoItObject = null;
    }

    public void Cancel()
    {
        Utils.SetCanvasGroupEnabled(canvasGroup, false);

        if (selectedSlot != null)
            selectedSlot.DeselectSlot();

        selectedSlot = null;
        selectedDoItObject = null;
    }

    public void AddFile(SpriteAsset spriteAsset)
    {
        if (uploadPromptText.gameObject.activeInHierarchy)
            uploadPromptText.gameObject.SetActive(false);

        if (!spriteSlotPairs.TryGetValue(spriteAsset.path, out FileSlot fileSlot))
        {
            fileSlot = Instantiate(fileSlotPrefab, fileSlotContainer);
            spriteSlotPairs.Add(spriteAsset.path, fileSlot);
        }

        fileSlot.UpdateSpriteAsset(spriteAsset);
    }

    public void FileSelected(FileSlot fileSlot)
    {
        if (selectedSlot != null && selectedSlot != fileSlot)
            selectedSlot.DeselectSlot();

        selectedSlot = fileSlot;
        UpdateSelectButton();
    }

    public void ConfirmDeleteSlot(FileSlot fileSlot)
    {
        assetDeletionWindow.OpenWindow(fileSlot);
    }

    public void DeleteSlot(FileSlot fileSlot)
    {
        spriteSlotPairs.Remove(fileSlot.SpriteAsset.path);
        CreatorAssetLibrary.Instance.DeleteSprite(fileSlot.SpriteAsset);

        if (selectedSlot != null && selectedSlot == fileSlot)
            selectedSlot = null;

        UpdateSelectButton();

        if (spriteSlotPairs.Count == 0)
            uploadPromptText.gameObject.SetActive(true);
    }

    private void UpdateSelectButton()
    {
        selectButtonText.text = SELECT_BUTTON_TEXT;
        selectButton.interactable = selectedSlot != null;

        if (selectedSlot != null)
            selectButtonText.text = $"{selectButtonText.text}: <b><color={ASSET_NAME_TEXT_COLOR}>{selectedSlot.SpriteAsset.assetName}</color></b>";
    }
}
