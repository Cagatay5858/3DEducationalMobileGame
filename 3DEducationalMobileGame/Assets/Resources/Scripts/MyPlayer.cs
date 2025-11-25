using UnityEngine;

public class MyPlayer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;

        transform.Translate(transform.forward * (5f * inputDir.magnitude) * Time.deltaTime, Space.World);
    }
}
