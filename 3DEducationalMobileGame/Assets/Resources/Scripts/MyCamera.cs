using UnityEngine;

public class MyCamera : MonoBehaviour
{

    public float Yaxis;
    public float Xaxis;
    public float RotationSens = 8f;

    float RotationMin = -40f;
    float RotationMax = 80f;
    float smoothTime = 0.3f;

    public Transform target;
    Vector3 targetRotation;
    Vector3 currentVel;

    // Update is called once per frame
    void LateUpdate()
    {
        Yaxis += Input.GetAxis("Mouse X");
        Xaxis -= Input.GetAxis("Mouse Y");

        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);


        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * 2f;

    }
}
