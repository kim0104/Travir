using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_typing : MonoBehaviour
{
    public Canvas chatCanvas;
    public GameObject dialogueCanvas;
    public Text t_text;
    public string[] tutorialDialogue;
    public string[] dialogues;
    public int talkNum;

    void Start()
    {
        StartTalk(tutorialDialogue);
    }

    public void StartTalk(string[] talks)
    {
        dialogues = talks;
        StartCoroutine(Typing(dialogues[talkNum]));
    }

    public void NextTalk()
    {
        t_text.text = null;
        talkNum++;

        if (talkNum == dialogues.Length)
        {
            EndTalk();
            return;
        }
        else
        {
            StartCoroutine(Typing(dialogues[talkNum]));
        }
    }

    public void EndTalk()
    {
        talkNum = 0;
        chatCanvas.enabled = true;
        dialogueCanvas.SetActive(false);
    }

    IEnumerator Typing(string talk)
    {
        t_text.text = null;
        if (talk.Contains("  ")) talk = talk.Replace("  ", "\n");

        for (int i = 0; i < talk.Length; i++)
        {
            t_text.text += talk[i];
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.8f);
        NextTalk();
    }
}
