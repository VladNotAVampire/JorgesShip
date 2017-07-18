using UnityEngine;
using System.Collections.Generic;

public class GroundGenerator : MonoBehaviour 
{
    public int heigh;
    public int wight;

    const float ONE = 0.16f;

    [ContextMenu("GenerateGrid")]
    public void GenerateGrid()
    {
        var block = (GameObject)Resources.Load("Prefabs/BattleScene/Block");

        for (int x = 0; x < wight; x++)
        {
            for(int y = 0; y < heigh; y++)
            {
                Instantiate(block, new Vector3(x * ONE, y * ONE), Quaternion.identity);
            }
        }
    }
}
