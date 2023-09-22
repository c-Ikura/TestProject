using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using Unity.Burst.Intrinsics;

public enum PlayerState
{
    UP, DOWN, LEFT, RIGHT, NONE, MOVE, DASH
}
public class PlayerController : MonoBehaviour
{
    private PhysicCheck physicCheck;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSp;

    public PlayerState state;
    public float jumpForce;
    public float moveSpeed;

    public float dashColdTime;
    public float nextDashTime;

    public int Hp;

    private void Awake()
    {
        physicCheck = GetComponent<PhysicCheck>();
        playerRb = GetComponent<Rigidbody2D>();
        playerSp = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        state = PlayerState.NONE;
        dashColdTime = nextDashTime;
    }
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (physicCheck.isUp)
        {
            if (input == Vector2.down)
            {
                state = PlayerState.DOWN;
            }
            if (input.x != 0)
            {
                playerRb.velocity = Vector2.right * input.x * moveSpeed;
                state = PlayerState.MOVE;
            }
            else
            {
                playerRb.velocity = Vector2.zero;
            }

        }
        else if (physicCheck.isDonw)
        {
            if (input == Vector2.up)
            {
                state = PlayerState.UP;
            }
            if (input.x != 0)
            {
                playerRb.velocity = Vector2.right * input.x * moveSpeed;
                state = PlayerState.MOVE;
            }
            else
            {
                playerRb.velocity = Vector2.zero;
            }
        }
        else if (physicCheck.isLeft)
        {
            if (input == Vector2.right)
            {
                state = PlayerState.RIGHT;
            }
            if (input.y != 0)
            {
                playerRb.velocity = Vector2.up * input.y * moveSpeed;
                state = PlayerState.MOVE;
            }
            else
            {
                playerRb.velocity = Vector2.zero;
            }


        }
        else if (physicCheck.isRight)
        {
            if (input == Vector2.left)
            {
                state = PlayerState.LEFT;
            }
            if (input.y != 0)
            {
                playerRb.velocity = Vector2.up * input.y * moveSpeed;
                state = PlayerState.MOVE;
            }
            else
            {
                playerRb.velocity = Vector2.zero;
            }
        }
        else//第二次按
        {
            dashColdTime -= Time.deltaTime;

            if (dashColdTime <= 0 && state != PlayerState.DASH)
            {
                if (input == Vector2.up)
                {
                    state = PlayerState.DASH;
                    Jump(Vector2.up);
                }
                if (input == Vector2.down)
                {
                    state = PlayerState.DASH;
                    Jump(Vector2.down);
                }
                if (input == Vector2.left)
                {
                    state = PlayerState.DASH;
                    Jump(Vector2.left);
                }
                if (input == Vector2.right)
                {
                    state = PlayerState.DASH;
                    Jump(Vector2.right);
                }
            }


        }


        if (physicCheck.onWall)
        {
            dashColdTime = nextDashTime;
            if (playerSp.color == Color.blue)
            {
                playerSp.color = Color.white;

                state = PlayerState.NONE;
            }
        }
        switch (state)
        {
            case PlayerState.UP:
                playerRb.velocity = Vector2.up * jumpForce;
                break;
            case PlayerState.DOWN:
                playerRb.velocity = Vector2.down * jumpForce;
                break;
            case PlayerState.LEFT:
                playerRb.velocity = Vector2.left * jumpForce;
                break;
            case PlayerState.RIGHT:
                playerRb.velocity = Vector2.right * jumpForce;
                break;
        }

    }

    public void Jump(Vector2 dir)
    {
        playerSp.color = Color.blue;
        playerRb.velocity = dir * jumpForce * 3f;
        dashColdTime = nextDashTime;
    }
    public void Move(Vector2 dir)
    {
        print(2);
        playerRb.velocity = dir * moveSpeed;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            Hp--;
            if (Hp <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("PlayScene");
            }
        }
        if (other.CompareTag("Gameover"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PlayScene");
        }
    }
}
