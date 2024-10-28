using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform PlayerTransform;
    public Transform playerObj;
    public Player Player;
    public PlayerInput playerInput;
    [SerializeField, Range(0, 10)]private float sensitivity = 5f;
    [SerializeField] private float rotationSpeed;
    private CinemachineFreeLook freeLookCam;
    private void Awake()
    {
        freeLookCam = GetComponent<CinemachineFreeLook>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        freeLookCam.m_YAxis.m_MaxSpeed = sensitivity;
        freeLookCam.m_XAxis.m_MaxSpeed = sensitivity * 100f;

        // rotate orientation
        Vector3 _viewDir = PlayerTransform.position - new Vector3(transform.position.x, PlayerTransform.position.y, transform.position.z);
        orientation.forward = _viewDir.normalized;

        // rotate player object
        Vector2 _inputVector = new Vector2(playerInput.xInput, playerInput.yInput);
        Vector3 _inputDir = orientation.forward * _inputVector.y + orientation.right * _inputVector.x;

        if (_inputDir != Vector3.zero && Player.stateMachine.currentState is not PlayerChooseAttack && Player.stateMachine.currentState is not PlayerRoll)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, _inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        

        //if(Input.GetKeyDown(KeyCode.Escape)) 
        //{
        //    Cursor.lockState = CursorLockMode.Confined;
        //}
    }
}
