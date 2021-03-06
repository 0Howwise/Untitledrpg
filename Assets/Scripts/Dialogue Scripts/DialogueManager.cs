﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public bool StillPrinting = false;
    public Image npcImageSprite;
    public Animator animator;
    public GameObject currentDialogueTrigger;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, GameObject inDialogueTrigger)
    {
        currentDialogueTrigger = inDialogueTrigger;

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            StillPrinting = true;
            dialogueText.text += letter;
            yield return null;
            
        }
        StillPrinting = true;
    }

    public void EndDialogue()
    { 
        animator.SetBool("IsOpen", false);
        //FindObjectOfType<DialogueTrigger>().TriggerDialogueIsRunning = false;
        currentDialogueTrigger.GetComponent<DialogueTrigger>().TriggerDialogueIsRunning = false;
    }

}
