using UnityEngine;

public class WrapAroundCloud : MonoBehaviour
{
    public string direction;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        if (direction == "left")
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            if (transform.position.x < -30)
            {

                transform.position = new Vector2(30, transform.position.y);
            }
        }
        if (direction == "right")
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); ;
            if (transform.position.x > 30)
            {
                transform.position = new Vector2(-30, transform.position.y);
            }
        }

    }
}
