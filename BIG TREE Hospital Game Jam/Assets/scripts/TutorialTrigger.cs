using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    public bool HideTutorial = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HideTutorial != true)
        text.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.gameObject.SetActive(false);
    }

    public void hideTutorial()
    {
        HideTutorial = true;
    }
}
