using System.Collections;
using Unity.VisualScripting;
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

    public Rigidbody2D rb;
    private bool turnOnGravity;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (grappleLine != null)
        {
            grappleLine.enabled = false;
            grappleLine.textureMode = LineTextureMode.Tile;
            grappleLine.alignment = LineAlignment.View;
        }

        if (grappleAim != null)
        {
            grappleAim.position = playerPos.position;
        }

        grappleAim.GetComponent<SpriteRenderer>().enabled = false;
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
            rb.gravityScale = 0f;
        }
        else if (!isGrappling && turnOnGravity)
        {
            rb.gravityScale = 5f;
            turnOnGravity = false;
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
        if (distance > maxDistance)
        {
            grappleTarget = playerPos.position + (Vector3)direction * maxDistance;
        }
        else
        {
            grappleTarget = mousePosition;
        }

        StartCoroutine(ShootAndCheckForHooks());
    }


    IEnumerator ShootAndCheckForHooks()
    {
        isShooting = true;
        float t = 0;
        Vector3 startPos = playerPos.position;
        bool hooked = false;


        if (grappleLine != null)
        {
            grappleLine.enabled = true;
        }


        //shoot
        while (t < 1)
        {
            grappleAim.GetComponent<SpriteRenderer>().enabled = true;


            t += Time.deltaTime * grappleSpeed;
            grappleAim.position = Vector3.Lerp(startPos, grappleTarget, t);


            Vector2 moveDirection = (grappleAim.position - playerPos.position).normalized;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            grappleAim.rotation = Quaternion.Euler(0, 0, angle);


            if (grappleLine != null)
            {
                grappleLine.SetPosition(0, playerPos.position);
                grappleLine.SetPosition(1, grappleAim.position);
            }

            Collider2D[] nearbyPoints = Physics2D.OverlapCircleAll(grappleAim.position, grapplePointSnapRadius, grapplePointLayer);

            if (nearbyPoints.Length > 0)
            {
                Transform closestPoint = null;
                float closestDistance = float.MaxValue;

                foreach (var point in nearbyPoints)
                {
                    float dist = Vector2.Distance(grappleAim.position, point.transform.position);
                    if (dist < closestDistance)
                    {
                        closestDistance = dist;
                        closestPoint = point.transform;
                    }
                }

                if (closestPoint != null)
                {
                    hooked = true;
                    hookedPoint = closestPoint;
                    grappleTarget = closestPoint.position;
                    grappleAim.position = grappleTarget;
                    isGrappling = true;
                    isShooting = false;

                    if (grappleLine != null)
                    {
                        grappleLine.SetPosition(1, grappleTarget);
                    }

                    yield break;
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);


        if (!hooked)
        {
            yield return new WaitForSeconds(0.1f);

            //retract
            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * grappleSpeed * 1.5f;
                grappleAim.position = Vector3.Lerp(grappleTarget, playerPos.position, t);

                if (grappleLine != null)
                {
                    grappleLine.SetPosition(1, grappleAim.position);
                }

                yield return null;
            }


            grappleAim.position = playerPos.position;
            isShooting = false;

            if (grappleLine != null)
            {
                grappleLine.enabled = false;
            }

            grappleAim.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    void PullPlayerToGrapplePoint()
    {
        playerPos.position = Vector3.MoveTowards(
            playerPos.position,
            grappleTarget,
            pullSpeed * Time.deltaTime
        );

        if (grappleLine != null )
        {
            grappleLine.SetPosition(0, playerPos.position);
            grappleLine.SetPosition(1, grappleTarget);
        }


        if (Vector3.Distance(playerPos.position, grappleTarget) < 0.1f)
        {
            StopGrappling();
        }
    }

    void StopGrappling()
    {
        isGrappling = false;
        turnOnGravity = true;
        hookedPoint = null;
        grappleAim.position = playerPos.position;

        grappleAim.GetComponent<SpriteRenderer>().enabled = false;

        if (grappleLine != null)
        {
            grappleLine.enabled = false;
        }
    }
}
