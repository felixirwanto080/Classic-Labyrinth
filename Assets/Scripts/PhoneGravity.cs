using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneGravity : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float gravityMagnitude;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip collisionClip;
    bool isTeleporting;


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
            inputDir.z,
            inputDir.y
        );

    }
    private void FixedUpdate(){
        rb.AddForce(gravityDir*gravityMagnitude, ForceMode.Acceleration);
    }
    public void SetGravityMagnitude(float gravity){
        gravityMagnitude = gravity;
    }
    public void OnDisabled(){
        if(SystemInfo.supportsGyroscope){
            Input.gyro.enabled = false;
            useGyro = false;
        }
    }
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Out"){
            StopAllCoroutines();
            StartCoroutine(DelayedTeleport());
            audioSource.PlayOneShot(collisionClip);
        }
    }
    IEnumerator DelayedTeleport(){
        isTeleporting=true;
        yield return new WaitForSeconds(2);
        rb.isKinematic=true;
        // this.transform.position = gm.lastCheckPointPos;
        isTeleporting=false;
    }
}
