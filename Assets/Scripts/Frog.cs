using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] Joystick joystick;
    [SerializeField] Source source;
    [SerializeField, Range(0,2)] float speed;
    enum Source{
        Keyboard,
        Joystick,
        Accelerometer,
        Gyroscope
    }
    private void Start(){
        Debug.Log("Accelerometer: " + SystemInfo.supportsAccelerometer);
        Debug.Log("Gyroscope: " + SystemInfo.supportsGyroscope);
        Input.gyro.enabled = true;
    }
    void Update()
    {
        Vector2 moveDir = Vector2.zero;
        switch (source)
        {
            case Source.Keyboard:
                moveDir = new Vector2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical")
                );
                break;
            case Source.Joystick:
                moveDir = joystick.Direction;
                // joystick.Vertical, joystick.DeadZone
                break;
            case Source.Accelerometer:
                moveDir = (Vector2)Input.acceleration;
                // Debug.Log(Input.acceleration);
                break;
            case Source.Gyroscope:
                moveDir = Input.gyro.gravity;
                // Debug.Log(Input.gyro.gravity);
                // Debug.Log(Input.gyro.rotationRate);
                break;
            default:
                // Vector2.zero;
                break;
        }        

        if(Input.gyro.rotationRate.magnitude > 10)
            Debug.Log("Shake");

        this.transform.Translate(moveDir*Time.deltaTime*speed);

        if(moveDir.x>0)
            spriteRenderer.flipX = false;
        else if(moveDir.x<0)
            spriteRenderer.flipX = true;
        
        animator.SetBool("IsMoving", moveDir!=Vector2.zero);
        
    }
}
