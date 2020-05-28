    using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float turnSmoothing = 15f;
    [SerializeField]
    private float speedDampTime = 0.1f;
    private Animator m_anim;
    private Vector3 movement;
    private float currentSpeed;
    [SerializeField]
    private float runningSpeed, walkingSpeed;
    private PlayerAnimatorController myAnimatorController;

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

        if (Input.GetKey(KeyCode.LeftShift) && (horizontal != 0 || vertical != 0))
        {
            myAnimatorController.StartRunning();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            myAnimatorController.StopRunningAnimation();
        }
    }

    private void Move(float horizontal, float vertical)
    {
        Vector3 playerMovement = new Vector3(horizontal, 0, vertical) * currentSpeed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}

