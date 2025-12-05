using UnityEngine;
using UnityEngine.Assertions.Must;

public class MyPlayer : MonoBehaviour
{
    float currentVelocity;
    public float MoveSpeed = 3f;
    public float smoothRotationtime = 0.25f;

    float currentSpeed;
    float speedVelocity;

    Transform cameraTransform;

    public bool enableMobileInputs = false;
    public FixedJoystick joystick;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        if (enableMobileInputs)
        {
            input = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        else
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref currentVelocity, smoothRotationtime );

        }
        float targetSpeed = MoveSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, 0.1f);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

    }
}
