using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleCounter : MonoBehaviour
{
    private int counter = 0;

    public TMP_Text counterText;

    public void CounterIncrease()
    {
        counter += 1;
        Debug.Log("Counter: " + counter);
        counterText.text = "Cats: " + counter + "/9";
    }
}
