using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;

    public Vector3 Offset = new Vector3(0, 2f, 10f);

    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate is called once per frame AFTER all the Updates
    // Source: https://gamedev.stackexchange.com/questions/130621/turning-the-camera-when-the-player-turns
    void LateUpdate()
    {
        if (target == null) return;

        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + Offset.y;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * Offset.z;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(target);
    }
}
