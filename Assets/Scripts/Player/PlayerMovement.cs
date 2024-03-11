using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement {
    private float moveX, moveY;

    // Because we are using the method Translate
    private void FixedUpdate() {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        HandleMovement(moveX, moveY);
    }
}
