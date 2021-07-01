using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    bullet,
    laser
}

[RequireComponent(typeof(Rigidbody2D))]

public class Shoot : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_spriteRenderer;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer.flipX = !PlayerController.sharedInstance.flip;

        Vector2 direction = PlayerController.sharedInstance.flip ? Vector2.right : Vector2.left;

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_rigidbody.velocity = direction * 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        Destroy(gameObject);

        if (gameObject.CompareTag("Laser")) 
        {            
            Transform T = gameObject.transform;
            List<GameObject> I = PortalController.sharedInstance.portalInstance;

            int C = I.Count;

            if (C == 0) 
            {
                if (m_spriteRenderer.flipX)
                {
                    I.Add(Instantiate(PortalController.sharedInstance.portal_I, T.position, T.rotation));
                    I[0].name = "Portal I";
                }
                else
                {
                    I.Add(Instantiate(PortalController.sharedInstance.portal_D, T.position, T.rotation));
                    I[0].name = "Portal D";
                }
            }

            if (C == 1) 
            {
                if (m_spriteRenderer.flipX)
                {
                    I.Add(Instantiate(PortalController.sharedInstance.portal_I, T.position, T.rotation));
                    I[1].name = "Portal I";
                }
                else
                {
                    I.Add(Instantiate(PortalController.sharedInstance.portal_D, T.position, T.rotation));
                    I[1].name = "Portal D";
                }
            }

            if (C == 2)
            {
                Destroy(I[0]);
                I[0] = I[1];

                if (m_spriteRenderer.flipX)
                {
                    I[1] = Instantiate(PortalController.sharedInstance.portal_I, T.position, T.rotation);
                    I[1].name = "Portal I";
                }
                else
                {
                    I[1] = Instantiate(PortalController.sharedInstance.portal_D, T.position, T.rotation);
                    I[1].name = "Portal D";
                }

            }
            
        }

    }

    

}
