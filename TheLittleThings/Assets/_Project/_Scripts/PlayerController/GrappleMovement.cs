using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{
    // Start is called before the first frame update

    #region Grapple Components
    private Rigidbody2D rb;
    private LineRenderer grappleLine;
    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private Vector2 grappleDir;
    #endregion

    #region Grapple Variables
    [SerializeField] private LayerMask grappleLayer;
    public float grappleSpeed = 10f;
    public float grappleAngle = 45f;
    public float chainPullSpeed = 105f;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grappleLine = GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartGrapple();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopGrapple();
        }

        if (joint.enabled)
        {
            PullPlayer();
        }

        if (grappleLine.enabled)
        {
            grappleLine.SetPosition(0, transform.position);
        }
    }
    void StartGrapple()
    {   // Grapple Direction based on an angle and player facing direction
        if (transform.localScale.x > 0)
        {
            grappleDir = Quaternion.AngleAxis(grappleAngle, Vector3.forward) * Vector2.right;
        }
        else
        {
            grappleDir = Quaternion.AngleAxis(-grappleAngle, Vector3.forward) * Vector2.left;
        }
        // Grapple Upwards instead of at an angle
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            grappleDir = Vector2.up;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, grappleDir, Mathf.Infinity, grappleLayer);

        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            grapplePoint.z = 0;
            joint.connectedAnchor = grapplePoint;
            joint.enabled = true;
            joint.distance = Vector2.Distance(transform.position, grapplePoint);

            // Set grapple line from player to hit point
            grappleLine.SetPosition(0, transform.position);
            grappleLine.SetPosition(1, grapplePoint);
            grappleLine.enabled = true;

            rb.AddForce(grappleDir * grappleSpeed, ForceMode2D.Force);
        }
    }


    void PullPlayer()
    {
        // Gradually decrease the joint's distance to pull the player
        joint.distance -= Time.deltaTime * chainPullSpeed;

        // Stop the grapple if the player is close enough to the grapple point
        if (joint.distance <= 1f)
        {
            StopGrapple();
        }
    }

    void StopGrapple()
    {
        joint.enabled = false;
        grappleLine.enabled = false;
    }


}
