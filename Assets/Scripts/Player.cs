using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float slowFallSpeed = 1f;
    [SerializeField] float fastFallSpeed = 5f;
    [SerializeField] float bounceForce = 10f;

    [SerializeField] Sprite[] playerSpriteArray;
    [SerializeField] Color[] playerTrailArray;

    public bool canFall;
    bool inGame;

    SpriteRenderer mySpriteRenderer;
    TrailRenderer myTrail;
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Game"))
        {
            inGame = true;
        }
        else { inGame = false; }

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myTrail = GetComponent<TrailRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();

        canFall = true;

        SetPlayerSprite();
        SetTrailColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            CanPlayerFall();
            FallFast();
        }
    }

    private void CanPlayerFall()
    {
        if (myRigidBody.velocity.y < 0f)
        {
            canFall = true;
        }
        else
        {
            canFall = false;
        }
    }

    private void FallFast()
    {
        if (canFall == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, -fastFallSpeed);
            }
            else
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, -slowFallSpeed);
            }
        }
        else if (canFall == false)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y);
        }
    }

    public void BounceBack()
    {
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, bounceForce);
    }

    private void SetPlayerSprite()
    {
        mySpriteRenderer.sprite = playerSpriteArray[PlayerPrefs.GetInt("PlayerSprite")];
    }

    private void SetTrailColor()
    {
        myTrail.startColor = playerTrailArray[PlayerPrefs.GetInt("TrailColor")];
    }
}
