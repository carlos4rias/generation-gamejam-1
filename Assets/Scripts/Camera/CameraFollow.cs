using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    private Transform target;
    [SerializeField]
    private float boundX = 0.3f;
    [SerializeField]
    private float boundY = 0.15f;

    private Vector3 deltaPos;
    private float deltaX, deltaY;

    private void Start() {
        target = GameObject.FindWithTag("Player").transform;    
    }

    private void LateUpdate() {
        if (!target) return;
        
        deltaPos = Vector3.zero;
        
        // Y offset
        deltaX = target.position.x - transform.position.x;

        if (deltaX > boundX || deltaX < -boundX) {
            if (transform.position.x < target.position.x)
                deltaPos.x = deltaX - boundX;
            else
                deltaPos.x = deltaX + boundX;                    
        }

        // Y offset
        deltaY = target.position.y - transform.position.y;

        if (deltaY > boundY || deltaY < -boundY) {
            if (transform.position.y < target.position.y)
                deltaPos.y = deltaY - boundY;
            else
                deltaPos.y = deltaY + boundY;                    
        }

        deltaPos.z = 0;

        transform.position += deltaPos;

    }
}
