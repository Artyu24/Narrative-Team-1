using System;
using System.Collections;
using System.Collections.Generic;
using TeamOne;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private SpriteRenderer dialogueBox;
    public SpriteRenderer DialogueBox => dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    public TextMeshProUGUI DialogueText => dialogueText;

    //Cree une liste ranger dans l ordre d apparition les objets present
    public Queue<string> sentences = new Queue<string>();
    private string actualSentence;
    public int langageID;

    [SerializeField] private GameObject choice;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void InitDialogue(DialogueData dialogue)
    {
        StartDialogue(GameManager.instance.TextDatabase.rows[dialogue.DialogueLine].columns[langageID]);
    }

    public void StartDialogue(string dialogue)
    {
        sentences.Clear();

        dialogueBox.gameObject.SetActive(true);

        sentences.Enqueue(dialogue);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            dialogueBox.gameObject.SetActive(false);
            return;
        }

        actualSentence = sentences.Dequeue();
        FixText(ref actualSentence);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(actualSentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        choice.SetActive(true);
        choice.GetComponent<Sword>().InitChoice();
    }

    public void FixText(ref string texte)
    {
        texte = texte.Replace("[PF]", GameManager.instance.PnjDatabase.pnjDatabase[1].PnjName).Replace("[FF]", GameManager.instance.PnjDatabase.pnjDatabase[0].PnjName).Replace("[PR]", GameManager.instance.PnjDatabase.pnjDatabase[2].PnjName).Replace("[VF]", GameManager.instance.PnjDatabase.pnjDatabase[3].PnjName);
    }
}
