using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float dampingTime = 0.2f;
    public Vector3 velocity = Vector3.zero;

    private void Start()
    {
        offset = GetComponent<Vector3>();
    }
    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition(){
        MoveCamera(false);
    }

    void MoveCamera(bool smooth){
        offset = new Vector3(-2.35f, 2.0f, -10f);
        Vector3 destination = new Vector3(
                            target.position.x - offset.x,
                            target.position.y - offset.y,
                            offset.z);
        if(smooth){
            this.transform.position = Vector3.SmoothDamp(
                                    this.transform.position,
                                    destination,
                                    ref velocity,
                                    dampingTime);
        } else{
            this.transform.position = destination;
        }                  
    }
}
