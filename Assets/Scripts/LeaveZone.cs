using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{
    public BoxCollider2D m_box;
    public GameObject walk;

    private void Start()
    {
        m_box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (transform.position.x < PlayerController.sharedInstance.transform.position.x)
        {
            LevelGenerator.sharedInstance.UpdateBlock();
            PortalController.sharedInstance.RemoveAllPortal();

            walk.SetActive(true);
            m_box.isTrigger = false;
        }
    }

}
