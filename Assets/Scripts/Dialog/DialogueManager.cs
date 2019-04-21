/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
	#region Variables
	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text dialogueText;
	[SerializeField]
	private Animator dialogueAnimator;
	[SerializeField]
	private Animator responceAnimator;
	private Queue<string> sentences;
	[SerializeField]
	private Button continueButton;
	#endregion

	void Start()
	{
		sentences = new Queue<string>();
	}
	public void StartDialogue(Dialogue dialogue)
	{
		dialogueAnimator.SetBool("isOpen", true);
		responceAnimator.SetBool("isOpen", true);

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
		StopAllCoroutines();        //stops animation if the user clicks past text box before finished animating
		StartCoroutine(TypeSentence(sentence));
	}
	public void DisplaySentenceAnswer1()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();        //stops animation if the user clicks past text box before finished animating
		StartCoroutine(TypeSentence(sentence));
		removeResponceBox();
		continueButton.gameObject.SetActive(true);
	}
	public void DisplaySentenceAnswer2()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		sentence = sentences.Dequeue();
		StopAllCoroutines();        //stops animation if the user clicks past text box before finished animating
		StartCoroutine(TypeSentence(sentence));
		removeResponceBox();
		continueButton.gameObject.SetActive(true);
	}
	public void DisplaySentenceAnswer3()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		sentence = sentences.Dequeue();
		sentence = sentences.Dequeue();
		StopAllCoroutines();        //stops animation if the user clicks past text box before finished animating
		StartCoroutine(TypeSentence(sentence));
		removeResponceBox();
		continueButton.gameObject.SetActive(true);
	}
	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())     //ToCharArray puts string into charArray
		{
			dialogueText.text += letter;
			yield return null;      //one frame delay
		}
	}
	private void removeResponceBox()
	{
		responceAnimator.SetBool("isOpen", false);
	}
	public void EndDialogue()
	{
		dialogueAnimator.SetBool("isOpen", false);
		continueButton.gameObject.SetActive(false);
	}
}
