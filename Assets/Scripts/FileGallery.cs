using System.Collections;
using System.Collections.Generic;
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

    private List<FileSlot> openFileSlots = new List<FileSlot>();

    private FileSlot selectedSlot;

    private void Awake()
    {
        sharedInstance = this;
    }

    public void AddNewFile(SpriteAsset spriteAsset)
    {
        FileSlot newFileSlot = Instantiate(fileSlotPrefab, transform);
        newFileSlot.DisplayFile(spriteAsset);
    }

    public void FileSelected(FileSlot fileSlot)
    {
        if (selectedSlot != null)
            selectedSlot.DeselectSlot();

        selectedSlot = fileSlot;
    }
}
