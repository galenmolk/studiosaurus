using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssetSelector : MonoBehaviour
{
    private static AssetSelector sharedInstance;
    public static AssetSelector Instance
    {
        get
        {
            if (sharedInstance == null)
                sharedInstance = FindObjectOfType<AssetSelector>();

            return sharedInstance;
        }
    }

    [SerializeField] private AssetSlot assetSlotPrefab = null;
    [SerializeField] private TMP_Text uploadPromptText = null;
    [SerializeField] private AssetDeletionWindow assetDeletionWindow = null;
    [SerializeField] private Button chooseButton = null;
    [SerializeField] private TMP_Text chooseButtonText = null;
    [SerializeField] private Transform assetSlotContainer = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private AssetSlotGallery gallery;

    private DoItObject selectedDoItObject;
    private AssetSlot selectedSlot;

    private const string CHOOSE_BUTTON_TEXT = "Choose";
    private const string ASSET_NAME_TEXT_COLOR = "#c0c0c0ff";

    private void Awake()
    {
        sharedInstance = this;
        UpdateChooseButton();
        CloseGallery();
    }

    public void Open(DoItObject doItObject, AssetSlotGallery gallery)
    {
        OpenGallery(gallery);
        Utils.SetCanvasGroupEnabled(canvasGroup, true);
        selectedDoItObject = doItObject;
        transform.SetAsLastSibling();
    }

    private void OpenGallery(AssetSlotGallery gallery)
    {
        if (this.gallery == gallery)
            return;

        while (assetSlotContainer.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);

        this.gallery = gallery;

        for (int i = 0, count = gallery.SlotCount; i < count; i++)
        {
            Instantiate(gallery.Slots[i], assetSlotContainer);
        }
    }

    public void ChooseSlot()
    {
        if (selectedSlot != null && selectedDoItObject != null)
            selectedDoItObject.UpdateAsset(selectedSlot.Asset);

        CloseGallery();
    }

    public void CloseGallery()
    {
        Utils.SetCanvasGroupEnabled(canvasGroup, false);
        selectedDoItObject = null;
    }

    public void AddFile(SpriteAsset spriteAsset)
    {
        if (uploadPromptText.gameObject.activeInHierarchy)
            uploadPromptText.gameObject.SetActive(false);
        
        if (!gallery.Contains(spriteAsset, out AssetSlot slot))
        {
            slot = Instantiate(assetSlotPrefab, assetSlotContainer);
            gallery.AddSlot(spriteAsset, slot);
        }

        slot.UpdateAsset(spriteAsset);
        slot.SelectSlot();
    }

    public void SlotSelected(AssetSlot fileSlot)
    {
        if (selectedSlot != null && selectedSlot != fileSlot)
            selectedSlot.DeselectSlot();

        selectedSlot = fileSlot;
        UpdateChooseButton();
    }

    public void ConfirmDeleteSlot(AssetSlot fileSlot)
    {
        assetDeletionWindow.OpenWindow(fileSlot);
    }

    public void DeleteSlot(AssetSlot assetSlot)
    {
        AssetSlot adjacentSlot = gallery.GetAdjacentSlot(assetSlot);
        gallery.DeleteSlot(assetSlot);
        CreatorAssetLibrary.Instance.DeleteAsset(assetSlot.Asset);

        if (selectedSlot != null && selectedSlot == assetSlot)
        {
            selectedSlot = null;
            UpdateChooseButton();
        }

        if (adjacentSlot != null)
            adjacentSlot.SelectSlot();

        if (gallery.SlotCount == 0)
            uploadPromptText.gameObject.SetActive(true);
    }

    private void UpdateChooseButton()
    {
        chooseButtonText.text = CHOOSE_BUTTON_TEXT;
        chooseButton.interactable = selectedSlot != null;

        if (selectedSlot != null)
        {
            Debug.Log("selectedSlot: " + selectedSlot);
            Debug.Log("selectedSlot.Asset: " + selectedSlot.Asset);

            chooseButtonText.text = $"{chooseButtonText.text}: <b><color={ASSET_NAME_TEXT_COLOR}>{selectedSlot.Asset.assetName}</color></b>";
        }
    }
}
