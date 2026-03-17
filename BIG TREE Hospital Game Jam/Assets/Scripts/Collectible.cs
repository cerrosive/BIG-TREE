using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject counterObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            counterObject.GetComponent<CollectibleCounter>().CounterIncrease();
            Destroy(gameObject);
        }
    }
}
