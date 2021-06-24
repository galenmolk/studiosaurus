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

    private List<FileSlot> openFileSlots = new List<FileSlot>();
    private FileSlot selectedSlot;

    private const string SELECT_BUTTON_TEXT = "Select";
    private const string ASSET_NAME_TEXT_COLOR = "#c0c0c0ff";

    private void Awake()
    {
        sharedInstance = this;
        UpdateSelectButton();
    }

    public void AddNewFile(SpriteAsset spriteAsset)
    {
        if (uploadPromptText.gameObject.activeInHierarchy)
            uploadPromptText.gameObject.SetActive(false);

        FileSlot newFileSlot = Instantiate(fileSlotPrefab, transform);
        newFileSlot.DisplayFile(spriteAsset);
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
        CreatorAssetLibrary.Instance.DeleteSprite(fileSlot.SpriteAsset);

        if (selectedSlot != null && selectedSlot == fileSlot)
            selectedSlot = null;

        UpdateSelectButton();
        Destroy(fileSlot.gameObject);
    }

    private void UpdateSelectButton()
    {
        selectButtonText.text = SELECT_BUTTON_TEXT;
        selectButton.interactable = selectedSlot != null;

        if (selectedSlot != null)
            selectButtonText.text = $"{selectButtonText.text}: <b><color={ASSET_NAME_TEXT_COLOR}>{selectedSlot.SpriteAsset.assetName}</color></b>";
    }
}
