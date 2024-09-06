using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Block> my_blocks;
    private int blocksCount;
    private LevelManager levelManager;
    private void Start()
    {
        levelManager = LevelManager.Instance;
        foreach (Block block in my_blocks) 
        {
            block.parentLevel = this;
        }
        blocksCount = my_blocks.Count;
    }
    public void ChangeCount()
    {
        blocksCount -= 1;
        if (blocksCount <= 0) levelManager.ChangeLevel();
    }
}
