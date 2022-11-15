using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject key1;
    public Animation hingeWall;
    public Vector3 spawnPoint;
    private void Start(){
        if(checkpoint1==null || checkpoint2==null || hingeWall==null || key1==null)
            return;
        
        spawnPoint = gameObject.transform.position;
    }
    private void Update(){
        if(gameObject.transform.position.y < -20f){
            gameObject.transform.position = spawnPoint;
            Debug.Log("Harusnya spawn kembali di : " + gameObject.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Checkpoint")){
            spawnPoint = checkpoint1.transform.position;
            Destroy(checkpoint1);
        }
        if(other.gameObject.CompareTag("Checkpoint2")){
            spawnPoint = checkpoint2.transform.position;
            Destroy(checkpoint2);
        }
        // if(other.gameObject.CompareTag("Checkpoint")){
        //     spawnPoint = checkpoint.transform.position;
        //     Destroy(checkpoint);
        // }
        if(other.gameObject.CompareTag("Key1")){
            spawnPoint = key1.transform.position;
            hingeWall.Play();
            Destroy(key1);
        }
    }
}
