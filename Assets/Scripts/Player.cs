using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class WinEvent : UnityEvent<PlayerConfiguration> { }

public class Player : MonoBehaviour
{
    public bool isGrounded = true;

    //Testing UnityEvents, currently a WIP
    public WinEvent endTheGame;

    BoxCollider2D triggerBox;

    PlayerConfiguration playerConfig;

    private void Start()
    {
        triggerBox = transform.GetComponent<BoxCollider2D>();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    //EnemyCheck is used to detect other players when flipping,
    //called from the PlayerMovement script
    public void EnemyCheck(Vector2 pos)
    {
        Vector2 size = transform.lossyScale * triggerBox.size;

        Collider2D intersecting = Physics2D.OverlapBox(pos, size, 0.0f, LayerMask.GetMask("Players"), -1.0f, 1.0f);

        if (intersecting != null)
        {
            Debug.Log("ObjName = " + intersecting.gameObject.name);
            Destroy(intersecting.gameObject);
            endTheGame.Invoke(playerConfig);
        }
    }
}
