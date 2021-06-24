using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private List<FileSlot> openFileSlots = new List<FileSlot>();

    private FileSlot selectedSlot;

    private void Awake()
    {
        sharedInstance = this;
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
    }

    public void ConfirmDeleteSlot(FileSlot fileSlot)
    {
        assetDeletionWindow.OpenWindow(fileSlot);
    }

    public void DeleteSlot(FileSlot fileSlot)
    {
        CreatorAssetLibrary.Instance.DeleteSprite(fileSlot.SpriteAsset);
        Destroy(fileSlot.gameObject);
    }
}
