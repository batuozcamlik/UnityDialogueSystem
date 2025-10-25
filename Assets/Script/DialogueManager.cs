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
    [Tooltip("T�m diyalog karakter ve replik bilgileri listesi")]
    public List<CharacterInformation> allDialogue;

    [Tooltip("Konu�malar�n hangi tu� ile ge�ilece�i")]
    public KeyCode skipDialoguesKey = KeyCode.Space;

    [Tooltip("Yaz�lar�n ekranda g�r�nme (yaz�lma) h�z�")]
    public float textTypingSpeed = 0.01f;

    [Tooltip("Belirli �zel karakterlerde ne kadar bekleme yap�laca��")]
    public KeyDuration[] allKey;

    [Tooltip("Animasyonlar�n oynatma h�z�")]
    [Range(0.1f, 5f)]
    public float animationSpeed = 1f;

    [Space(10)]
    [Header("Audio & State")]
    [Tooltip("Yaz� yazarken karakter reaksiyon sesi oynat�lacaksa kullan�lacak ses kayna��")]
    public AudioSource characterReactionSFX;

    [Tooltip("Yaz� kutusu �u anda a��k m�?")]
    [ReadOnly]public bool textBoxIsOpen = false;

    [Tooltip("Diyalog sistemi �u anda aktif mi?")]
    [ReadOnly] public bool isPlayDialogue = false;

    [Space(15)]
    [Header("UI References")]
    [Tooltip("Konu�ma kutusu GameObject'i")]
    public GameObject textBox;

    [Tooltip("Sa� tarafta konu�an karakterin portresi")]
    public Image rightImage;

    [Tooltip("Sol tarafta konu�an karakterin portresi")]
    public Image leftImage;

    [Tooltip("Diyalog metninin yazd�r�laca�� TextMeshPro alan�")]
    public TextMeshProUGUI text;

    [Tooltip("Konu�an karakterin ad�n�n yaz�laca�� TextMeshPro alan�")]
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
