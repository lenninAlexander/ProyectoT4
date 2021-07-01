using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    money
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money;
    public int value = 0;

    void Collect()
    {
        switch (this.type)
        {
            case CollectableType.money:
                GameManager.sharedInstance.CollectMoney(value);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Collect();
        Destroy(gameObject);
    }

}
