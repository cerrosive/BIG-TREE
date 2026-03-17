using UnityEngine;

public class CloudBarrier : MonoBehaviour
{
    public Rigidbody2D rb;
    private void OnTriggerStay2D(Collider2D collision)
    {
  // it ain't sensing the player even though it's tagged
  // it works if it isn't trying to sense for the tag, but that could break stuff if not included
  // also we need to limit the upward speed it gives the player but I can't figure it out pls help
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("trigger");
            rb.linearVelocity += new Vector2(rb.linearVelocity.x, 1);
        }
    }
}
