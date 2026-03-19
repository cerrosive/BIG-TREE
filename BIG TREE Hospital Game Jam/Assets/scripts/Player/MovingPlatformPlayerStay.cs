using UnityEngine;

public class MovingPlatformPlayerStay : MonoBehaviour
{

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}



