using System.Collections;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class grappleScript : MonoBehaviour
{
    //references
    public LayerMask grapplePointLayer; //the actual point the grapple hooks onto
    public Transform grappleAim; //cursor that points where the grapple is
    public Transform playerPos;

    //settings
    public float maxDistance = 10f;
    public float grappleSpeed = 15f;
    public float pullSpeed = 10f;
    public float grapplePointSnapRadius = 0.5f;

    //states
    private bool isGrappling = false;
    private bool isShooting = false;
    private Vector3 grappleTarget;
    private Transform hookedPoint;

    //rendering
    public LineRenderer grappleLine;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (grappleLine != null)
        {
            grappleLine.enabled = false;
        }

        if (grappleAim != null)
        {
            grappleAim.position = playerPos.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !isGrappling && !isShooting)
        {
            grapple();
        }

        if (isGrappling)
        {
            PullPlayerToGrapplePoint();
        }
    }


    private void grapple()
    {
        //mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - playerPos.position).normalized;
        float distance = Vector2.Distance(playerPos.position, mousePosition);

        //cap distance
        Vector3 targetPosition;

        if (distance > maxDistance)
        {
            targetPosition = playerPos.position + (Vector3)direction * maxDistance;
        }
        else
        {
            targetPosition = mousePosition;
        }

        grappleTarget = targetPosition;


        //check for grapple point
        Collider2D[] nearbyPoints = Physics2D.OverlapCircleAll(grappleTarget, grapplePointSnapRadius, grapplePointLayer);

        if (nearbyPoints.Length > 0)
        {
            Transform closestPoint = null;
            float closestDistance = float.MaxValue;

            foreach (var point in  nearbyPoints)
            {
                float dist = Vector2.Distance(targetPosition, point.transform.position);

                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestPoint = point.transform;
                }
            }


            hookedPoint = closestPoint;
            grappleTarget = closestPoint.position;
            grappleAim.position = grappleTarget;
            isGrappling = true;


            //line renderer
            if (grappleLine != null)
            {
                grappleLine.enabled = true;
                grappleLine.SetPosition(0, playerPos.position);
                grappleLine.SetPosition(1, grappleTarget);
            }
        }
        else
        {
            grappleTarget = targetPosition;
            StartCoroutine(ShootAndRetract());
        }
    }


    IEnumerator ShootAndRetract()
    {
        isShooting = true;
        float t = 0;
        Vector3 startPos = playerPos.position;


        if (grappleLine != null)
        {
            grappleLine.enabled = true;
        }


        //shoot
        while (t < 1)
        {
            t += Time.deltaTime * grappleSpeed;
            grappleAim.position = Vector3.Lerp(startPos, grappleTarget, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        //retract
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * grappleSpeed * 1.5f;
            grappleAim.position = Vector3.Lerp(grappleTarget, playerPos.position, t);
            yield return null;
        }


        grappleAim.position = playerPos.position;
        isShooting = false;

        if (grappleLine != null)
        {
            grappleLine.enabled = false;
        }
    }


    void PullPlayerToGrapplePoint()
    {
        playerPos.position = Vector3.MoveTowards(
            playerPos.position,
            grappleTarget,
            pullSpeed * Time.deltaTime
        );

        if (Vector3.Distance(playerPos.position, grappleTarget) < 0.1f)
        {
            StopGrappling();
        }
    }

    void StopGrappling()
    {
        isGrappling = false;
        hookedPoint = null;
        grappleAim.position = playerPos.position;

        if (grappleLine != null)
        {
            grappleLine.enabled = false;
        }    
    }
}
