using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isGrounded = true;

    BoxCollider2D triggerBox;
    SpriteRenderer sr;

    PlayerConfiguration playerConfig;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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

    public void EnemyCheck(Vector2 pos)
    {
        Vector2 size = transform.lossyScale * triggerBox.size;

        Collider2D intersecting = Physics2D.OverlapBox(pos, size, 0.0f, LayerMask.GetMask("Players"), -1.0f, 1.0f);

        if (intersecting != null)
        {
            Debug.Log("ObjName = " + intersecting.gameObject.name);
            Destroy(intersecting.gameObject);
            var lc = FindObjectOfType<LevelController>();
            lc.ResetLevel(playerConfig.PlayerIndex);
        }
    }
}
