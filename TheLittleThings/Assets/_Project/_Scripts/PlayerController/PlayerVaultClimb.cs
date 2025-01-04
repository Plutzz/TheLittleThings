using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerVaultClimb : State
{
    [SerializeField] private float raycastYOffset, raycastForwardOffset;
    [SerializeField] private float step, finishRadius;
    [SerializeField] private Collider playerCollider;
    private Vector3 targetPos, lerpPos;
    private bool xCheck, yCheck, zCheck;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.isKinematic = true;
        playerCollider.enabled = false;
        targetPos = rb.position;
        lerpPos = targetPos;
        Physics.Raycast(transform.position + raycastYOffset * Vector3.up + raycastForwardOffset * transform.forward, Vector3.down, out RaycastHit hit);
        targetPos = hit.point;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        
        // Lerp player from side of ledge to standing on ledge
        lerpPos = Vector3.Lerp(lerpPos, targetPos, step * Time.deltaTime);
        rb.transform.position = lerpPos;
        
        Debug.DrawRay(targetPos, Vector3.up * 10f, Color.green);
        
        // Check if the player has arrived within a certain radius to the target position
        xCheck = Mathf.Abs(rb.position.x) >= Mathf.Abs(targetPos.x) - finishRadius &&
                      Mathf.Abs(rb.position.x) <= Mathf.Abs(targetPos.x) + finishRadius;
        yCheck = Mathf.Abs(rb.position.y) >= Mathf.Abs(targetPos.y) - finishRadius &&
                      Mathf.Abs(rb.position.y) <= Mathf.Abs(targetPos.y) + finishRadius;
        zCheck = Mathf.Abs(rb.position.z) >= Mathf.Abs(targetPos.z) - finishRadius &&
                      Mathf.Abs(rb.position.z) <= Mathf.Abs(targetPos.z) + finishRadius;
        
        if (xCheck && yCheck && zCheck)
        {
            isComplete = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        rb.isKinematic = false;
        playerCollider.enabled = true;
    }
}
