using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    #region Grapple Components
    private Rigidbody2D rb;
    private LineRenderer grappleLine;
    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private Vector2 grappleDir;
    private RaycastHit2D hit;
    #endregion

    #region Grapple Variables
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    public float chainPullSpeed = 105f;
    private bool grappleReady = true;
    public float grappleAngle = 45f;
    public float grappleSpeed = 10f;
    public float grappleCooldown = 1f;
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
        if (grappleReady && Input.GetKeyDown(KeyCode.LeftShift))
        {
            grappleReady = false;
            _Grapple();
        }

        // Reset grapple cooldown
        if (!grappleReady)
        {
            grappleCooldown -= Time.deltaTime;
            if (grappleCooldown <= 0) {
                grappleReady = true;
                grappleCooldown = 1f;
            }
        }   

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            joint.enabled = false;
            grappleLine.enabled = false;
        }

        if (grappleLine.enabled)
        {
            grappleLine.SetPosition(0, transform.position);
        }
    }

    void _Grapple()
    {
        // Grapple Direction based on the grappleAngle and player facing direction
        if (transform.localScale.x > 0)
        {
            grappleDir = Quaternion.AngleAxis(grappleAngle, Vector3.forward) * Vector2.right;
        }
        else
        {
            grappleDir = Quaternion.AngleAxis(-grappleAngle, Vector3.forward) * Vector2.left;
        }

        hit = Physics2D.Raycast(transform.position, grappleDir, Mathf.Infinity, grappleLayer);

        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            grapplePoint.z = 0;
            joint.connectedAnchor = grapplePoint;
            joint.enabled = true;
            joint.distance = Vector2.Distance(transform.position, grapplePoint) - Time.deltaTime * chainPullSpeed;

            // Add horizontal movement to grapple
            rb.velocity = new Vector2(0, Mathf.Clamp(rb.velocity.y, -5f, 5f));
            rb.AddForce(grappleDir * grappleSpeed, ForceMode2D.Impulse);

            // Set grapple line from player to hit point
            grappleLine.SetPosition(0, transform.position);
            grappleLine.SetPosition(1, grapplePoint);
            grappleLine.enabled = true;
        }
    }
}