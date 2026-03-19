using UnityEngine;

public class CloudBarrier : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<PlayerMovement>().CloudJump();
        }
    }
}
