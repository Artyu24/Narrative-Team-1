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
    private string playerName;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void InitDialogue(DialogueData dialogue)
    {
        StartDialogue(GameManager.instance.TextDatabase.rows[dialogue.DialogueLine].columns[0]);
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
    }

    private void FixText(ref string texte)
    {
        //texte = texte.Replace("PLAYER", playerName).Replace("POKEPL", CombatManager.instance.playerPoke.data.name).Replace("POKEEN", CombatManager.instance.enemiePoke.data.name).Replace("ATKPL", CombatManager.instance.PlayerAttackName).Replace("ATKEN", CombatManager.instance.EnemieAttackName);
    }
}
