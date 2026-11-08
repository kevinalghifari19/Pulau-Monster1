using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ItemPopupUI : MonoBehaviour
{
    public static ItemPopupUI Instance { get; private set; }

    [Header("UI Overlay References")]
    [SerializeField] private GameObject popupPanel;      // Panel background hitam transparan
    [SerializeField] private Image itemIconImage;         // Gambar item di tengah
    [SerializeField] private TextMeshProUGUI itemTitleText;// Teks pas dapat barangnya "Kamu mendapatkan [Nama]!"
    [SerializeField] private TextMeshProUGUI itemDescText; // Teks deskripsi atau progres

    public bool IsPopupActive { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        HidePopup();
    }

    private void Update()
    {
        if (!IsPopupActive) return;

        // Tekan F atau Klik Mouse buat tutup pop up barang
        if ((Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame) ||
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame))
        {
            HidePopup();
        }
    }

    public void ShowItemPopup(Sprite itemIcon, string itemName, string description)
    {
        IsPopupActive = true;
        popupPanel.SetActive(true);

        if (itemIconImage != null && itemIcon != null)
            itemIconImage.sprite = itemIcon;

        if (itemTitleText != null)
            itemTitleText.text = $"Kamu mendapatkan {itemName}!";

        if (itemDescText != null)
            itemDescText.text = description;
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
        IsPopupActive = false;
    }
}