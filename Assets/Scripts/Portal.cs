using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject portal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (PortalController.sharedInstance.Count() == 2)
        {
            portal = PortalController.sharedInstance.portalObject(gameObject);

            if (portal.name == name)
            {
                PlayerController.sharedInstance.flip = !PlayerController.sharedInstance.flip;
            }
            StartCoroutine("newPosition");
        }
    }

    IEnumerator newPosition()
    {
        yield return new WaitForSeconds(0.2f);

        int absolute = portal.name == "Portal I" ? 1 : -1;
        Vector3 pos = portal.transform.position;
        PlayerController.sharedInstance.transform.position = new Vector3(pos.x + absolute * 0.7f, pos.y, pos.z);
    }
}
