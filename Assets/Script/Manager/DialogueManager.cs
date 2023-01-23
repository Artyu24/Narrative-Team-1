using System;
using System.Collections;
using System.Collections.Generic;
using TeamOne;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialogueBox;
    public GameObject DialogueBox => dialogueBox;
    [SerializeField] private GameObject interactionImage;
    [SerializeField] private Text dialogueText;

    public delegate void DialogueDelegate();
    private DialogueDelegate actualDialogueDelegate = null;

    //Cree une liste ranger dans l ordre d apparition les objets present
    public Queue<string> sentences = new Queue<string>();
    private string actualSentence;
    private string playerName;

    private DialogueState actualDialogueState;
    [SerializeField] private float cdAction, cdDialogue;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void InitDialogue(DialogueData[] dialogue)
    {
        actualDialogueState = DialogueState.INTERACTION;

        //readGoogleSheet.GetTextString(dialogue);
    }

    public void StartDialogue(string[] dialogue)
    {
        sentences.Clear();

        dialogueBox.SetActive(true);

        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        interactionImage.SetActive(false);
        
        if(sentences.Count == 0)
        {
            switch (actualDialogueState)
            {
                #region Draw
                case DialogueState.DRAW:

                    break;
                #endregion

                #region Draw & Action
                case DialogueState.DRAW_ACTION:

                    if (actualDialogueDelegate != null)
                    {
                        actualDialogueDelegate();
                        actualDialogueDelegate = null;
                    }

                    break;
                #endregion

                #region Interaction
                case DialogueState.INTERACTION:
                    dialogueBox.SetActive(false);

                    break;
                #endregion

                #region Interaction & Action
                case DialogueState.INTERACTION_ACTION:
                    dialogueBox.SetActive(false);

                    if (actualDialogueDelegate != null)
                    {
                        actualDialogueDelegate();
                        actualDialogueDelegate = null;
                    }
                    break;
                #endregion
            }

            return;
        }

        actualSentence = sentences.Dequeue();

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

        //TEXTE FINIS D ETRE ECRIS

        if (actualDialogueState == DialogueState.INTERACTION || actualDialogueState == DialogueState.INTERACTION_ACTION)
        {
            interactionImage.SetActive(true);
        }
        else
            StartCoroutine(CoolDownNextSentence());
    }

    private IEnumerator CoolDownNextSentence()
    {
        if (sentences.Count == 0 && actualDialogueState == DialogueState.DRAW_ACTION)
            yield return new WaitForSeconds(cdAction);
        else
            yield return new WaitForSeconds(cdDialogue);

        DisplayNextSentence();
    }

    public void DisplayTextInstant()
    {
        if (actualDialogueState == DialogueState.INTERACTION || actualDialogueState == DialogueState.INTERACTION_ACTION)
        {
            StopAllCoroutines();
            dialogueText.text = actualSentence;
            interactionImage.SetActive(true);
        }
    }
}
