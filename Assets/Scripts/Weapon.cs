using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider2D m_box;
    private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_spriteRenderer;

    public float hurt;
    public int cantidad;

    private void Start()
    {
        m_box = GetComponent<BoxCollider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!m_box.enabled)
        {
            m_spriteRenderer.flipX = !PlayerController.sharedInstance.flip;
        }

        if (transform.position.y < -5.0f) Destroy(gameObject); 
    }

    public void RemoveWeapon()
    {
        GameObject weapon = PlayerController.sharedInstance.weapon;
        GameObject player = PlayerController.sharedInstance.gameObject;

        if (weapon != null)
        {
            Weapon component = weapon.GetComponent<Weapon>();
            component.m_box.enabled = true;

            weapon.SetActive(true);
            weapon.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2);

            Vector2 direction = PlayerController.sharedInstance.flip ? Vector2.left : Vector2.right;

            component.m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
            component.m_rigidbody.AddForce(Vector2.up * 2 + direction * 2, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController.sharedInstance.RemoveEnemy(collision.gameObject.GetComponent<Enemy>()); 
            Destroy(gameObject);
        }

        if (!collision.gameObject.CompareTag("Player")) return;

        RemoveWeapon();
        int absolute = PlayerController.sharedInstance.flip ? -1 : 1;

        gameObject.transform.position = new Vector3(0.1f * absolute, 0.05f);
        gameObject.SetActive(false);

        m_rigidbody.bodyType = RigidbodyType2D.Static;
        PlayerController.sharedInstance.weapon = gameObject;
        m_box.enabled = false;
    }
}
