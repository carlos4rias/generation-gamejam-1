using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement {
    private float moveX, moveY;

    private Animator animator;

    private int direction = 1;

    private PlayerToolManager playerToolManager;

    private AudioSource walkAudio;

    public GameObject deadTree;
    public GameObject aliveTree;

    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
        playerToolManager = GetComponent<PlayerToolManager>();
        walkAudio = GetComponent<AudioSource>();
    } 

    private void Update() {
        if (GameLogic.Instance.gameState != 2) return;
        if (Input.GetKeyDown(KeyCode.H) && playerToolManager.toolIndex == 0) {
            int harvestingResult = GameLogic.Instance.canHarvest(transform.position.x, transform.position.y);
            Debug.Log($"Haversting result {harvestingResult}");
        }
    }

    // Because we are using the method Translate
    private void FixedUpdate() {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if ((moveX!= 0 || moveY != 0)) {
            if (getDirection(moveX, moveY) != direction)
                direction = getDirection(moveX, moveY);
            HandlePlayerAnimation(moveX, moveY);    
            HandleMovement(moveX, moveY);
            walkAudio.Play();
        } else {
            walkAudio.Stop();
        }
    }

    private int getDirection(float x, float y) {
        int xi = Mathf.RoundToInt(x);
        int yi = Mathf.RoundToInt(y);
        if (xi == 0 && yi == -1) return 1;
        if (xi == 1 && yi == 0) return 3;
        if (xi == 0 && yi == 1) return 4;
        if (xi == -1 && yi == 0) return 2;
        return 1;
    }

    private void HandlePlayerAnimation(float x, float y) {
        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
        playerToolManager.ActivateTool(direction - 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.tag);
        if (other.tag == "AliveTree" && playerToolManager.toolIndex == 1) {
            Vector3 transformParent = other.transform.position;
            GameLogic.Instance.deforest(transformParent.x, transformParent.y);
            transformParent.y -= 0.4f;
            Destroy(other.gameObject);
            Instantiate(deadTree, transformParent,  Quaternion.identity);
        }
    }

}
