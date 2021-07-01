using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPoint;
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        GenerateIntialBlocks();
        
    }

    public void AddLevelBlock()
    {
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);
        LevelBlock currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
        currentBlock.transform.SetParent(this.transform, false);

        Vector3 spawPosition = Vector3.zero;

        if (currentBlocks.Count == 0)
        {
            spawPosition = levelStartPoint.position;
        }
        else
        {
            spawPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        Vector3 correction = new Vector3(spawPosition.x - currentBlock.startPoint.position.x, spawPosition.y - currentBlock.startPoint.position.y, 0);
        currentBlock.transform.position = correction;
        currentBlocks.Add(currentBlock);
    }

    public void RemoveOldestLevelBlock()
    {
        LevelBlock oldestBlock = currentBlocks[0];
        currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);
    }

    public void RemoveAllTheBlocks()
    {
        while (currentBlocks.Count > 0)
        {
            RemoveOldestLevelBlock();
        }
    }

    public void InstanceWall()
    {
        UpdateLevel L = UpdateLevel.sharedInstance;
        L.begin.transform.position = new Vector3(currentBlocks[0].exitPoint.position.x, 0.0f);
        L.end.transform.position = new Vector3(currentBlocks[2].exitPoint.position.x, 0.0f);
    }

    public void UpdateBlock()
    {
        for (int i = 0; i < 2; i++)
            RemoveOldestLevelBlock();

        for (int i = 0; i < 2; i++)
            AddLevelBlock();

        InstanceWall();
        EnemyController.sharedInstance.GenerateEnemies();
        WeaponController.sharedInstance.GenerateWeapons();
    }

    public void GenerateIntialBlocks()
    {
        for (int i = 0; i < 4; i++)
            AddLevelBlock();

        InstanceWall();
        EnemyController.sharedInstance.GenerateEnemies();
        WeaponController.sharedInstance.GenerateWeapons();
    }
}
