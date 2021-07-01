using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController sharedInstance;

    public int cantidadMax;

    public List<Enemy> allTheEnemies = new List<Enemy>();
    public List<Enemy> currentEnemies = new List<Enemy>();

    void Awake()
    {
        sharedInstance = this;
    }

    private void Update()
    {
        if (currentEnemies.Count == 0)
        {
            UpdateLevel.sharedInstance.end.m_box.isTrigger = true;
            UpdateLevel.sharedInstance.end.walk.SetActive(false);
        }
    }

    public void GenerateEnemy()
    {
        int index = Random.Range(0, allTheEnemies.Count - 1);
        float x = Random.Range(UpdateLevel.sharedInstance.begin.transform.position.x, UpdateLevel.sharedInstance.end.transform.position.x - 2.0f);
        Debug.Log("Okay");
        Enemy currentEnemy = (Enemy)Instantiate(allTheEnemies[index], new Vector3(x + 2.0f, 3.0f, 0.0f), allTheEnemies[index].transform.rotation);
        currentEnemy.transform.SetParent(this.transform, false);
        currentEnemies.Add(currentEnemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        currentEnemies.Remove(enemy);
    }

    public void GenerateEnemies()
    {
        int index = Random.Range(1, cantidadMax);

        for (int i = 0; i < index; i++)
        {
            GenerateEnemy();
        }
    }
}
