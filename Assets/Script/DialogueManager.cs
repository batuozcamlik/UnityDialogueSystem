using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



[System.Serializable]
public class CharacterInformation
{
    [Header("Character")]
    public string name;
    public Sprite sprite;
    [Header("Dialogue")]
    [TextArea]
    public string text;
    public bool isRight = false;
    [Space(5)]
    public AudioClip reactionSFX;
    public UnityEvent finishEvent;
    
}
[System.Serializable]
public class KeyDuration
{
    public string key;
    public float duration;
}

public class DialogueManager : MonoBehaviour
{
    #region Variable

    public static DialogueManager Instance;
    public bool playOnAwake;

    [Header("Dialogue Settings")]
    [Tooltip("Tüm diyalog karakter ve replik bilgileri listesi")]
    public List<CharacterInformation> allDialogue;

    [Tooltip("Konuþmalarýn hangi tuþ ile geçileceði")]
    public KeyCode skipDialoguesKey = KeyCode.Space;

    [Tooltip("Yazýlarýn ekranda görünme (yazýlma) hýzý")]
    public float textTypingSpeed = 0.01f;

    [Tooltip("Belirli özel karakterlerde ne kadar bekleme yapýlacaðý")]
    public KeyDuration[] allKey;

    [Tooltip("Animasyonlarýn oynatma hýzý")]
    [Range(0.1f, 5f)]
    public float animationSpeed = 1f;

    [Space(10)]
    [Header("Audio & State")]
    [Tooltip("Yazý yazarken karakter reaksiyon sesi oynatýlacaksa kullanýlacak ses kaynaðý")]
    public AudioSource characterReactionSFX;

    [Tooltip("Yazý kutusu þu anda açýk mý?")]
    [ReadOnly]public bool textBoxIsOpen = false;

    [Tooltip("Diyalog sistemi þu anda aktif mi?")]
    [ReadOnly] public bool isPlayDialogue = false;

    [Space(15)]
    [Header("UI References")]
    [Tooltip("Konuþma kutusu GameObject'i")]
    public GameObject textBox;

    [Tooltip("Sað tarafta konuþan karakterin portresi")]
    public Image rightImage;

    [Tooltip("Sol tarafta konuþan karakterin portresi")]
    public Image leftImage;

    [Tooltip("Diyalog metninin yazdýrýlacaðý TextMeshPro alaný")]
    public TextMeshProUGUI text;

    [Tooltip("Konuþan karakterin adýnýn yazýlacaðý TextMeshPro alaný")]
    public TextMeshProUGUI characterNameText;

    #endregion
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
