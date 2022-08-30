using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{

    public void Chat(string text, float time)
    {
        StopCoroutine ("Begin"); // Stop if existing
        StartCoroutine(Begin(text, time));
    }
    public void Chat(string text)
    {
        StopCoroutine ("Begin"); // Stop if existing
        StartCoroutine(Begin(text, 2)); // Default seconds will be 2 if not specified
    }
    IEnumerator Begin(string text, float time)
    {
        canvasChat.DOFade(0, 0); // Make alpha 0
        canvasChat.text = text;
        canvasChat.DOFade(1, 1);
        yield return new WaitForSeconds(time + 1); // The one is time for fade
        canvasChat.DOFade(0, 1);
    }
    public void OpenBook(Sprite[] pages)
    {
        bookTransform.DOLocalMoveY(20, 0.7f); // 20 is the height
        status.Interrupt();
        BookInterface bookInterface = bookTransform.GetComponentInChildren<BookInterface>();
        bookInterface.bookPages = pages;
        bookInterface.Start();
        soundManager.PlayAudio("bookopen");

        MoveInventory(150);
    }
    
    public void CloseBook()
    {
        soundManager.PlayAudio("bookclose");
        bookTransform.DOLocalMoveY(-500, 0.7f);
        status.UnInterrupt();

        MoveInventory(0);
    }
    void MoveInventory(float y)
    {
        inventoryTransform.DOKill();
        inventoryTransform.DOLocalMoveY(y, 0.5f);
    }
    StatusManager status;
    Transform bookTransform;
    public TMP_Text canvasChat;
    SoundManager soundManager;
    Transform inventoryTransform;
    void Start()
    {
        inventoryTransform = GameObject.FindWithTag("Inventory Transform").transform;
        status = FindObjectOfType<StatusManager>();
        bookTransform = GameObject.FindWithTag("Book Transform").transform;
        soundManager = FindObjectOfType<SoundManager>();
    }
}