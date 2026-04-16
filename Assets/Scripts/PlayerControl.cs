using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] bool groundedPlayer;
    [SerializeField] float playerSpeed;
    [SerializeField] float gravityValue;
    [SerializeField] GameObject activeChar;
    [SerializeField] float moveHorizontal;
    [SerializeField] float moveVertical;
    [SerializeField] float speed = 4;
    [SerializeField] float rotateSpeed = 4;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] bool isJumping;

    void Start()
    {
        playerSpeed = 4;
        gravityValue = -20;
    }

    void Update()
    {
        // transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        // Vector3 forward = transform.TransformDirection(Vector3.forward);
        // float curSpeed = speed * Input.GetAxis("Vertical");
        // controller.SimpleMove(forward * curSpeed);

        // if (Input.GetButtonDown("Jump") && groundedPlayer)
        // {
        //     isJumping = true;
        //     activeChar.GetComponent<Animator>().Play("Jump");
        //     playerVelocity.y += 10;
        // }

        // playerVelocity.y += gravityValue * Time.deltaTime;
        // controller.Move(playerVelocity * Time.deltaTime);

        // if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        // {
        //     this.gameObject.GetComponent<CharacterController>().minMoveDistance = 0.001f;
        //     if(isJumping == false)
        //     {
        //         activeChar.GetComponent<Animator>().Play("Slow Run");
        //     }
        // }

        // else
        // {
        //     this.gameObject.GetComponent<CharacterController>().minMoveDistance = 0.901f;
        //     if (isJumping == false)
        //     {
        //         activeChar.GetComponent<Animator>().Play("Idle");
        //     }
        // }

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            isJumping = false;
        }
        
        float horizontal = 0;
        float vertical = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) horizontal = -1;
            if (Keyboard.current.dKey.isPressed) horizontal = 1;
            if (Keyboard.current.wKey.isPressed) vertical = 1;
            if (Keyboard.current.sKey.isPressed) vertical = -1;
        }

        transform.Rotate(0, horizontal * rotateSpeed * Time.deltaTime, 0);

        Vector3 forward = transform.forward;
        float curSpeed = speed * vertical;
        controller.SimpleMove(forward * curSpeed);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && groundedPlayer)
        {
            isJumping = true;
            activeChar.GetComponent<Animator>().Play("Jump");
            playerVelocity.y = 10f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        bool isMoving = vertical != 0 || horizontal != 0;

        if (isMoving)
        {
            controller.minMoveDistance = 0.001f;

            if (!isJumping)
            {
                activeChar.GetComponent<Animator>().Play("Slow Run");
            }
        }
        else
        {
            controller.minMoveDistance = 0.901f;

            if (!isJumping)
            {
                activeChar.GetComponent<Animator>().Play("Idle");
            }
        }
    }
}
