using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public static ShootController sharedInstance;
    [SerializeField] List<GameObject> shootInstance = new List<GameObject>();

    private void Awake()
    {
        sharedInstance = this;
    }

    public GameObject at(int index)
    {
        return shootInstance[index];
    }

}
