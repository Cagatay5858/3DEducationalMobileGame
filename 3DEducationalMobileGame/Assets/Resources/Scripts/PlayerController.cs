using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference moveAction;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float gravity = -20f;
    private float yVelocity;
    private CharacterController cc;

    [Header("Camera")]
    public Camera camera;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        moveAction?.action?.Enable();
    }

    void OnDisable()
    {
        moveAction?.action?.Disable();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // 1. GÜVENLÝK KONTROLÜ: CharacterController yoksa veya kapalýysa iþlem yapma
        if (cc == null || !cc.enabled) return;

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        // --- IMPORTANT ---
        // Always read direction from actual main camera
        Transform cam = camera.transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * moveInput.y + right * moveInput.x;

        // Rotate player toward camera direction
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                10f * Time.deltaTime);
        }

        // gravity
        if (cc.isGrounded)
            yVelocity = -1f;
        else
            yVelocity += gravity * Time.deltaTime;

        Vector3 move = moveDir * moveSpeed;
        move.y = yVelocity;

        // 2. Hareket komutunu ver
        cc.Move(move * Time.deltaTime);
    }
}
