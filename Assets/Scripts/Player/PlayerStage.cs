using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStage
{
    private GameObject[,] stageBlocks = null;


    public void GetStageBlock(List<GameObject> block)
    {
        var count = 0;
        for(var x = 0;x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                stageBlocks[x, y] = block[count];
                count++;
            }
        }
    }
}
