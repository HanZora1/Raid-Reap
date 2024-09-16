using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject speechBubble;
    public Text speechBubbleText;
    public void OpenBlacksmithUI()
    {
        speechBubble.SetActive(true);
        speechBubbleText.text = "BlackSmith";
    }

    public void CloseBlacksmithUI()
    {
        speechBubble.SetActive(false);
        speechBubbleText.text = "";
    }
}
