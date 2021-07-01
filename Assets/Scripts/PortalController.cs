using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public static PortalController sharedInstance;

    public GameObject portal_I;
    public GameObject portal_D;

    public List<GameObject> portalInstance = new List<GameObject>(); 

    private void Awake()
    {
        sharedInstance = this;
    }

    public int Count() { return portalInstance.Count; }

    public GameObject portalObject(GameObject portalInstance)
    {
        if (portalInstance.transform.position == this.portalInstance[0].transform.position)
            return this.portalInstance[1];

        return this.portalInstance[0];
    }

    public void RemoveAllPortal()
    {
        while (this.portalInstance.Count > 0)
        { 
            GameObject oldestPortal = this.portalInstance[0];
            this.portalInstance.Remove(oldestPortal);
            Destroy(oldestPortal.gameObject);
        }
    }
    

}
