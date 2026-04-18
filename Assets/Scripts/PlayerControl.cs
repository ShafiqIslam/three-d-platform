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
    [SerializeField] float rotateSpeed = 64;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] bool isJumping;

    void Start()
    {
        playerSpeed = 4;
        gravityValue = -20;
    }

    void Update()
    {
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

        if (Keyboard.current.spaceKey.isPressed && groundedPlayer)
        {
            isJumping = true;
            activeChar.GetComponent<Animator>().Play("Jump");
            playerVelocity.y += 10;
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
