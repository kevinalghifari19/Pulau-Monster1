using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// Struct ditaruh di atas class DialogueManager biar bisa dibaca di Inspector n script lain
[System.Serializable]
public struct DialogueLine
{
    public string speakerName; // Nama pembicara contohnya kaya "Tetua Desa" atau "Fathir")
    [TextArea(2, 4)]
    public string sentence;    // Kalimat dialog
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI nameText;     // Slot UI untuk nama pembicara
    [SerializeField] private TextMeshProUGUI dialogueText; // Slot UI untuk isi teks dialog

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.03f;

    // Queue menampung DialogueLine bukan string biasa lagi ini
    private Queue<DialogueLine> sentences = new Queue<DialogueLine>();
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private DialogueLine currentLine;

    public bool IsDialogueActive { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        HideDialogue();
    }

    private void Update()
    {
        if (!IsDialogueActive) return;

        // Tekan F atau Klik Kiri Mouse buat lanjut atau mempercepat teks
        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame))
        {
            if (isTyping)
            {
                // nampilin kalimat utuh jika pemain tekan tombol pas mengetik
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentLine.sentence;
                isTyping = false;
            }
            else
            {
                // Lanjut ke dialog atau pembicara berikutnya
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(DialogueLine[] lines)
    {
        IsDialogueActive = true;
        dialogueBox.SetActive(true);

        sentences.Clear();
        foreach (DialogueLine line in lines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            HideDialogue();
            return;
        }

        currentLine = sentences.Dequeue();

        // Ganti nama pembicara sesuai data elemen saat ini
        if (nameText != null)
        {
            nameText.text = currentLine.speakerName;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // Ketik kalimat huruf per huruf
        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void HideDialogue()
    {
        dialogueBox.SetActive(false);
        IsDialogueActive = false;
        isTyping = false;
        sentences.Clear();
    }
}