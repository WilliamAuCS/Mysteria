using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressEtoEnter : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        text.gameObject.SetActive(false);
    }

}
