using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [Header("Quest Settings")]
    [SerializeField] private int totalItemsNeeded = 5;

    private int itemsCollected = 0;

    public bool IsQuestStarted { get; private set; }
    public bool IsQuestCompleted { get; private set; }
    public int ItemsCollected => itemsCollected;
    public int TotalItemsNeeded => totalItemsNeeded;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartQuest()
    {
        IsQuestStarted = true;
        Debug.Log($"Quest Dimulai! Kumpulkan {totalItemsNeeded} barang.");
    }

    public void CollectItem(string itemName, Sprite itemSprite)
    {
        if (!IsQuestStarted) return;

        itemsCollected++;

        if (itemsCollected >= totalItemsNeeded)
        {
            IsQuestCompleted = true;
        }

        // Tampilkan pop up item bersinar di tengah layar
        string progresMsg = $"Barang {itemsCollected} dari {totalItemsNeeded} berhasil dikumpulkan!";
        if (ItemPopupUI.Instance != null)
        {
            ItemPopupUI.Instance.ShowItemPopup(itemSprite, itemName, progresMsg);
        }
    }
}