using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLevel : MonoBehaviour
{
    public LeaveZone begin;
    public LeaveZone end;

    public static UpdateLevel sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }
}
