using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isTree;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isTree && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Climb"))
        {
            isTree = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Climb"))
        {
            Debug.Log("triggered");
            isTree = false;
            isClimbing = false;
            rb.gravityScale = 5f;
        }
    }
}