using UnityEngine;

public class SpaceJump : MonoBehaviour
{
    public Rigidbody2D body;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "space")
        {
            body.gravityScale = 3;
        }
    }
}
