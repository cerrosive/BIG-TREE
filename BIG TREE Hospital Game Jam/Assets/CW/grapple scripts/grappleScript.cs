using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class grappleScript : MonoBehaviour
{
    //variables
    public GameObject grapplePoint; //the actual point the grapple hooks onto
    public GameObject grappleAim; //cursor that points where the grapple is

    private Vector3 playerPos;
    private Vector3 aimPos;
    private bool hitTrigger;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aimPos = playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        //get player's current position
        playerPos = transform.position;

        //set grapple position
        grappleAim.transform.position = aimPos;


        if (Input.GetMouseButton(0))
        {
            grapple();
        }
    }


    private void grapple()
    {
        //mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        aimPos = mousePosition;
    }
}
