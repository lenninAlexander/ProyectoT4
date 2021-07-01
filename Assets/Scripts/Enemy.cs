using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform ground;
    public float speed;
    private bool movingr = false;
    public LayerMask groundLayer;
    public int points = 0;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundinfo = Physics2D.Raycast(ground.position, Vector2.down, 1f, groundLayer);
        RaycastHit2D groundinfo1 = Physics2D.Raycast(ground.position, Vector2.right, 0.1f, groundLayer);

        if (groundinfo.collider == false || groundinfo1 == true)
        {
            if (movingr == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingr = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingr = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float yOffset = 0.76031f;
            if (transform.position.y + yOffset < collision.transform.position.y)
            {
                EnemyController.sharedInstance.RemoveEnemy(this);
                Destroy(gameObject);
                GameManager.sharedInstance.CollectPoints(this.points);
            }
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            EnemyController.sharedInstance.RemoveEnemy(this);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.sharedInstance.CollectPoints(this.points);
        }

        if (collision.gameObject.CompareTag("Laser"))
        {
            EnemyController.sharedInstance.RemoveEnemy(this);
            Destroy(gameObject);
            GameManager.sharedInstance.CollectPoints(this.points);
        }

    }

}