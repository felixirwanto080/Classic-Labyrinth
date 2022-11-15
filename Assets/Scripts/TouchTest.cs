using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchTest : MonoBehaviour // IPointerDownHandler
{
    private void Update()
    {
        if(Input.touchCount==0)
            return;

        // Swipe
        var touch = Input.GetTouch(0);
        if(touch.deltaPosition.x>10){
            Debug.Log("Right");
        }
        else if(touch.deltaPosition.x<-10){
            Debug.Log("Left");
        }

        // Tap
        if(touch.tapCount>0){
            Debug.Log(touch.tapCount);
        }
    }
    private void OnDrawGizmos(){
        foreach (var touch in Input.touches)
        {
            var touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchWorldPos.z = 0;

            switch(touch.phase){
            case TouchPhase.Began:
                Gizmos.color = Color.green;
                Debug.Log("Began");
            break;
            case TouchPhase.Stationary:
                Gizmos.color = Color.grey;
                Debug.Log("Stationary");
            break;
            case TouchPhase.Moved:
                Gizmos.color = Color.yellow;
                Debug.Log("Moved");
            break;
            case TouchPhase.Ended:
                Gizmos.color = Color.red;
                Debug.Log("Ended");
            break;
            case TouchPhase.Canceled:
                Gizmos.color = Color.magenta;
                Debug.Log("Canceled");
            break;
            default:
                Debug.Log("Error");
            break;
        }  
            Gizmos.DrawSphere(touchWorldPos, 0.5f);
        }
    }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     throw new System.NotImplementedException();
    // }
}
