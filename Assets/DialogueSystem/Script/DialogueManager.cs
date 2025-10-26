using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

#region Data Classes
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
#endregion

public class DialogueManager : MonoBehaviour
{
    #region Variables

    public static DialogueManager Instance;
    public bool playOnAwake;

    [Header("Dialogue Settings")]
    [Tooltip("Tüm diyalog karakter ve replik bilgileri listesi")]
    public List<CharacterInformation> allDialogues;

    [Tooltip("Konuþmalarýn hangi tuþ ile geçileceði")]
    public KeyCode skipDialoguesKey = KeyCode.Space;

    [Tooltip("Yazýlarýn ekranda görünme (yazýlma) hýzý")]
    public float textTypingSpeed = 0.01f;

    [Tooltip("Belirli özel karakterlerde ne kadar bekleme yapýlacaðý")]
    public KeyDuration[] allWaitKey;

    [Tooltip("Animasyonlarýn oynatma hýzý")]
    [Range(0.1f, 5f)]
    public float animationSpeed = 1f;

    [Space(10)]
    [Header("Audio & State")]
    [Tooltip("Yazý yazarken karakter reaksiyon sesi oynatýlacaksa kullanýlacak ses kaynaðý")]
    public AudioSource characterReactionSFX;
    [Tooltip("Yazý yazarken oynatýlacak ses var ise oynatýlacak ses")]
    public AudioSource typingSFX;

    [Tooltip("Yazý kutusu þu anda açýk mý?")]
    [ReadOnly] public bool textBoxIsOpen = false;

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

    [Tooltip("Oyuncunun yazý tamamlanmadan skip butona basarsa yazýyý tamamlayýp tamamlamayacaðý")]
    public bool canSkipDialogue = true;

    private bool pressSkipButton = false;

    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (playOnAwake)
        {
            StartDialogue();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(skipDialoguesKey))
        {
            pressSkipButton = true;
        }
    }
    #endregion

    #region Dialogue Control
    public void StartDialogue()
    {
        if (isPlayDialogue == false)
        {
            isPlayDialogue = true;
            StartCoroutine(StartDialogueIE());
        }
    }

    public void AddDialogueAndStart(List<CharacterInfo> newDialogues)
    {
        if (isPlayDialogue == false)
        {
            isPlayDialogue = true;
            allDialogues.Clear();
            allDialogues.AddRange(newDialogues);
            StartCoroutine(StartDialogueIE());
        }

    }
    #endregion

    #region Image & Animation
    void ChangeImage(bool right, Sprite currentSprite, bool instant = false)
    {
        float tFade = instant ? 0.25f : 0.5f / animationSpeed;
        float tScale = instant ? 0.5f : 1f / animationSpeed;

        if (right)
        {
            if (rightImage.sprite != currentSprite)
                rightImage.sprite = currentSprite;

            rightImage.gameObject.SetActive(rightImage.sprite != null);
            leftImage.gameObject.SetActive(leftImage.sprite != null);

            leftImage.DOFade(0.5f, tFade);
            leftImage.transform.DOScale(0.97f, tScale);

            rightImage.DOFade(1f, tFade);
            rightImage.transform.DOScale(1f, tScale);
        }
        else
        {
            if (leftImage.sprite != currentSprite)
                leftImage.sprite = currentSprite;

            rightImage.gameObject.SetActive(rightImage.sprite != null);
            leftImage.gameObject.SetActive(leftImage.sprite != null);

            leftImage.DOFade(1f, tFade);
            leftImage.transform.DOScale(1f, tScale);

            rightImage.DOFade(0.5f, tFade);
            rightImage.transform.DOScale(0.97f, tScale);
        }
    }
    #endregion

    #region UI Animation
    IEnumerator OpenTextBox(bool isOpen)
    {
        var boxImg = textBox.GetComponent<Image>();
        boxImg.DOKill();
        textBox.transform.DOKill();

        if (isOpen)
        {
            textBox.SetActive(true);

            characterNameText.DOFade(1f, 0f);

            leftImage.gameObject.SetActive(leftImage.sprite != null);
            rightImage.gameObject.SetActive(rightImage.sprite != null);

            boxImg.DOFade(1f, 0.25f).From(0.85f);
            textBox.transform.DOScale(1f, 0.25f).From(0.85f);
            yield return new WaitForSeconds(0.25f);
        }
        else
        {
            isPlayDialogue = false;

            boxImg.DOFade(0f, 0.25f);
            textBox.transform.DOScale(0.85f, 0.25f).From(1f);

            leftImage.DOFade(0f, 0.25f);
            rightImage.DOFade(0f, 0.25f);

            yield return new WaitForSeconds(0.25f);

            textBox.SetActive(false);
            text.text = string.Empty;
            characterNameText.text = string.Empty;

            rightImage.sprite = null;
            rightImage.gameObject.SetActive(false);
            leftImage.sprite = null;
            leftImage.gameObject.SetActive(false);
        }
        textBoxIsOpen = isOpen;
    }
    #endregion

    #region Dialogue Coroutine
    IEnumerator StartDialogueIE()
    {
        #region StartReset
        leftImage.DOKill(); rightImage.DOKill();
        leftImage.transform.DOKill(); rightImage.transform.DOKill();

        leftImage.sprite = null;
        rightImage.sprite = null;

        var lc = leftImage.color; lc.a = 0f; leftImage.color = lc;
        var rc = rightImage.color; rc.a = 0f; rightImage.color = rc;

        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
        #endregion

        Sprite oldSpriteRight = null, oldSpriteLeft = null;
        text.text = "";

        for (int i = 0; i < allDialogues.Count; i++)
        {
            pressSkipButton = false;
            characterNameText.text = allDialogues[i].name;

            if (!textBoxIsOpen)
            {
                yield return OpenTextBox(true);
            }

            text.text = string.Empty;

            if (allDialogues[i].isRight)
            {
                if (oldSpriteRight != allDialogues[i].sprite)
                {
                    rightImage.DOFade(0f, 0.25f);
                    rightImage.transform.DOScale(0.95f, 0.25f);
                }
            }
            else
            {
                if (oldSpriteLeft != allDialogues[i].sprite)
                {
                    leftImage.DOFade(0f, 0.25f);
                    leftImage.transform.DOScale(0.95f, 0.25f);
                }
            }
            yield return new WaitForSeconds(0.25f);

            bool instant = true;
            ChangeImage(allDialogues[i].isRight, allDialogues[i].sprite, instant);

            if (allDialogues[i].isRight) oldSpriteRight = allDialogues[i].sprite;
            else oldSpriteLeft = allDialogues[i].sprite;

            if (allDialogues[i].reactionSFX != null)
            {
                characterReactionSFX.clip = allDialogues[i].reactionSFX;
                characterReactionSFX.Play();
            }

            #region WriteText
            string currentText = allDialogues[i].text;
            foreach (char letter in currentText)
            {
                text.text += letter;

                float waitTime = textTypingSpeed;

                for (int j = 0; j < allWaitKey.Length; j++)
                {
                    if (letter.ToString() == allWaitKey[j].key)
                    {
                        waitTime = allWaitKey[j].duration;
                        break;
                    }
                }

                if (pressSkipButton)
                {

                    text.text = allDialogues[i].text;
                    pressSkipButton = false;
                    break;
                }

                if (typingSFX.clip != null)
                {
                    typingSFX.Play();
                }

                yield return new WaitForSeconds(waitTime);
            }
            #endregion

            yield return new WaitUntil(() => Input.GetKeyDown(skipDialoguesKey));

            if (allDialogues[i].finishEvent != null) allDialogues[i].finishEvent.Invoke();


        }

        yield return OpenTextBox(false);
        isPlayDialogue = false;
    }
    #endregion
}
