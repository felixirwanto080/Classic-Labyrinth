using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGravityMM : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float gravityMagnitude;
    bool useGyro;
    Vector3 gravityDir;
    // Start is called before the first frame update
    void Start()
    {
        if(SystemInfo.supportsGyroscope){
            useGyro = true;
            Input.gyro.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var inputDir = useGyro ? Input.gyro.gravity : Input.acceleration;

        gravityDir = new Vector3(
            inputDir.x,
            inputDir.y,
            inputDir.z
        );

    }
    private void FixedUpdate(){
        rb.AddForce(gravityDir*gravityMagnitude, ForceMode.Acceleration);
    }
}
