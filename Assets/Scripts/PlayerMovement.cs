using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float runningSpeed, walkingSpeed;
    private PlayerAnimatorController myAnimatorController;
    private Vector3 playerMovement = Vector3.zero;
    public float RotateSpeed = 30f;

    private void Start()
    {
        myAnimatorController = GetComponent<PlayerAnimatorController>();
        currentSpeed = walkingSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runningSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkingSpeed;
        }
    }

    private void FixedUpdate()
    {
        //Move();
    }


    /*private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Debug.Log("Vertical: " + vertical);
        Debug.Log("Horizontal: " + horizontal);

        if (vertical != 0 && horizontal == 0 && !myAnimatorController.IsRunning())
        {
            myAnimatorController.StartWalk();
        }

        if (horizontal < 0 && !myAnimatorController.IsRunning())
        {
            myAnimatorController.StartLeftWalk();
        }
        else if (horizontal > 0 && !myAnimatorController.IsRunning())
        {
            myAnimatorController.StartRightWalk();
        }

        if (horizontal == 0 && !myAnimatorController.IsRunning())
        {
            myAnimatorController.StopHorizontalMovement();
        }

        if (vertical == 0)
        {
            myAnimatorController.StopWalkAnimation();
        }

        if (vertical == 0 && horizontal == 0)
        {
            myAnimatorController.PlayIdleAnimation();
            myAnimatorController.StopAllAnimations();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myAnimatorController.PlayRunningAnimation();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            myAnimatorController.StopRunningAnimation();
        }
        playerMovement = new Vector3(horizontal, 0, vertical) * currentSpeed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }*/
}
