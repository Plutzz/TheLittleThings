using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public PlayerInput playerInput;
    [SerializeField] private float rotationSpeed;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // rotate orientation
        Vector3 _viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = _viewDir.normalized;

        // rotate player object
        Vector2 _inputVector = new Vector2(playerInput.xInput, playerInput.yInput);
        Vector3 _inputDir = orientation.forward * _inputVector.y + orientation.right * _inputVector.x;

        if (_inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, _inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        

        //if(Input.GetKeyDown(KeyCode.Escape)) 
        //{
        //    Cursor.lockState = CursorLockMode.Confined;
        //}
    }
}
