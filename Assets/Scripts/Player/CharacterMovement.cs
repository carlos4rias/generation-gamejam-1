using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    [SerializeField]
    protected float movementSpeed = 1f;

    private Vector3 movementDelta;
    private RaycastHit2D movementHit;
    private BoxCollider2D myCollider;

    protected virtual void Awake() {
        myCollider = GetComponent<BoxCollider2D>();    
    }

    protected virtual void HandleMovement(float x, float y) {
        movementDelta = new Vector3(x, y, 0f) * movementSpeed;
        transform.Translate(movementDelta * Time.deltaTime);
    }

    public Vector3 GetMoveDelta() {
        return movementDelta;
    }
}
