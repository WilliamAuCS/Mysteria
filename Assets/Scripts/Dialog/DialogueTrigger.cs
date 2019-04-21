using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool dialogStarted;
    [SerializeField]
    private Image image;
	[SerializeField]
	private Canvas knightCanvas;

	private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.E))
        {
			knightCanvas.gameObject.SetActive(true);
            TriggerDialogue();
		}
    }
    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
	private void OnTriggerExit2D(Collider2D collision)
	{
		FindObjectOfType<DialogueManager>().EndDialogue();
	}
}
