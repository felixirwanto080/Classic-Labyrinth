using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Accelerometer: " + SystemInfo.supportsAccelerometer);
        Debug.Log("Gyroscope: " + SystemInfo.supportsGyroscope);
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.rotation = Input.gyro.attitude;
        // this.transform.Rotate(0,0,180,Space.Self);
        // this.transform.Rotate(90,180,0,Space.World);

        // this.transform.rotation = new Quaternion(0.5f, 0.5f, -0.5f, -0.5f)
        //     *Input.gyro.attitude
        //     *new Quaternion(0,0,1,0);
        this.transform.rotation = Quaternion.Euler(90f, 90f, 0f)
            *Input.gyro.attitude
            *new Quaternion(0,0,1,0);
    }
}
