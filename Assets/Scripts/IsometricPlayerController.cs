using UnityEngine;
using System.Collections;

public class IsometricPlayerController : MonoBehaviour
{
    [SerializeField]
    private float turnSmoothing = 15f;
    [SerializeField]
    private float speedDampTime = 0.1f;
    private Rigidbody myRB;
    private Animator m_anim;
    private Vector3 movement;
    private float currentSpeed;
    [SerializeField]
    private float runningSpeed, walkingSpeed;
    private PlayerAnimatorController myAnimatorController;

    private void Start()
    {
        myAnimatorController = GetComponent<PlayerAnimatorController>();
        myRB = GetComponent<Rigidbody>();
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        MovementManagement(horizontal, vertical);
        AnimationsManagement(horizontal, vertical);
    }

    private void MovementManagement(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            Move(horizontal, vertical);
            Rotating(horizontal, vertical);
        }
    }

    private void AnimationsManagement(float horizontal, float vertical)
    {
        if ((horizontal != 0 || vertical != 0) && !myAnimatorController.IsRunning())
        {
            myAnimatorController.StartWalk();
        }
        else if((horizontal == 0 || vertical == 0))
        {
            myAnimatorController.StartIdle();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            myAnimatorController.StartRunning();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            myAnimatorController.StopRunningAnimation();
        }
    }

    private void Rotating(float horizontal, float vertical)
    {
        Vector3 targetDirection = new Vector3(horizontal, 0, vertical);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(myRB.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        myRB.MoveRotation(newRotation);
    }

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0, vertical);
        movement = movement.normalized * currentSpeed * Time.deltaTime;
        myRB.MovePosition(transform.position + movement);
    }
}

