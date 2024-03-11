using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement {
    private float moveX, moveY;

    private Animator animator;

    private int direction = 1;

    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
    } 

    // Because we are using the method Translate
    private void FixedUpdate() {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if ((moveX!= 0 || moveY != 0) && getDirection(moveX, moveY) != direction) {
            HandlePlayerAnimation(moveX, moveY);    
            direction = getDirection(moveX, moveY);
        }
        HandleMovement(moveX, moveY);
    }

    private int getDirection(float x, float y) {
        int xi = Mathf.RoundToInt(x);
        int yi = Mathf.RoundToInt(y);
        if (xi == 0 && yi == -1) return 1;
        if (xi == 1 && yi == 0) return 2;
        if (xi == 0 && yi == 1) return 3;
        if (xi == -1 && yi == 0) return 4;
        return 1;
    }

    private void HandlePlayerAnimation(float x, float y) {
        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
}
