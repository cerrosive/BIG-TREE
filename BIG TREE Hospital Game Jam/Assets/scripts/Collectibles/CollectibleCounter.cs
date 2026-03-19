using UnityEngine;

public class CollectibleCounter : MonoBehaviour
{
    private int counter = 0;

    public void CounterIncrease()
    {
        counter += 1;
        Debug.Log("Counter: " + counter);
    }
}
