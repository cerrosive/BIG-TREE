using System.Security.Cryptography;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public float moveLimit;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < moveLimit && player.transform.position.x > -moveLimit)
        {
            transform.position = new Vector2(player.transform.position.x, transform.position.y);
        }
        transform.position = new Vector2(transform.position.x, player.transform.position.y+2);
    }
}
