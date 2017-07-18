using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Island : DestinationPoint
{
    protected override string info
    {
        get
        {
            return "It's an island\nThat's all\nYou can go now";
        }
    }

    const float ONE = 0.1f;

    [SerializeField]Biom[,] cells;
    [SerializeField]Vector2[] randomizePoints;

    GameObject[] cellsObj;

    public void Randomize()
    {
        foreach(var point in randomizePoints)
        {
            try
            {
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x, (int)point.y] = Biom.sand;
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x - 1, (int)point.y] = Biom.sand;
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x + 1, (int)point.y] = Biom.sand;
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x, (int)point.y - 1] = Biom.sand;
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x, (int)point.y + 1] = Biom.sand;
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x - 1, (int)point.y - 1] = Biom.sand;
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x - 1, (int)point.y + 1] = Biom.sand;
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x + 1, (int)point.y - 1] = Biom.sand;
                if (Random.Range(0, 100) <= 33)
                    cells[(int) point.x + 1, (int)point.y + 1] = Biom.sand;
            }
            catch(System.IndexOutOfRangeException)
            {
                Debug.LogError("Randomize points of " + name + " are not set properly!");
            }
        }
    }

    public void Build(Biom[,] cells)
    {
        this.cells = cells;
        Build();
    }
   
    [ContextMenu("Build")]
    public void Build()
    {
        int centerX = cells.GetLength(0) / 2;
        int centerY = cells.GetLength(1) / 2;
        GameObject sand = Resources.Load<GameObject>("Prefabs/Sand");
   
        List<GameObject> newCells = new List<GameObject>();
   
        for(int i = 0; i < cells.GetLength(0); i++)
        {
            for(int k = 0; k < cells.GetLength(1); k++)
            {
                if (cells[i, k] != Biom.empty)
                {
                    newCells.Add((GameObject)Instantiate(sand, transform.position - Vector3.right * ((centerX - i) * ONE) + Vector3.up * ((centerY - k) * ONE), Quaternion.identity, transform));
                }
            }
        }
   
        cellsObj = newCells.ToArray();
    }

    public void GenerateCollider(Biom[,] cells)
    {
        List<Block> blocks = new List<Block>();
        List<Vector2> points = new List<Vector2>();
        int xLen = cells.GetLength(0);
        int yLen = cells.GetLength(1);

        for(int i = 0; i < xLen; i++)
        {
            for(int k = 0; k < yLen; k++)
            {
                if (cells[i, k] != Biom.empty && CheckForBoards(i, k))
                {
                    blocks.Add(new Block(i, k));
                }
            }
        }

        while(blocks.Any(b => !b.wasChecked))
        {
            var block = blocks.Find(b => !b.wasChecked);

            while(!block.wasChecked)
            {
                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));
                block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);

                //if (block.x - 1 < 0 || cells[block.x - 1, block.y] == Biom.empty) //Left
                //{
                //    points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE / 2 + ONE / 2));
                //}
                //else
                //{
                //    if (blocks.Any(b => b.x == block.x - 1 && b.y == block.y))
                //    {
                //        var nextBlock = blocks.Find(b => b.x == block.x - 1 && b.y == block.y);
                //        
                //        if(!nextBlock.wasChecked)
                //        {
                //            block = nextBlock;
                //        }
                //    }
                //}
                //
                //if (block.y - 1 < 0 || cells[block.x, block.y - 1] == Biom.empty)  // Up
                //{
                //    points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE / 2 + ONE / 2));
                //}
                //else
                //{
                //    if (blocks.Any(b => b.x == block.x && b.y == block.y - 1))
                //    {
                //        var nextBlock = blocks.Find(b => b.x == block.x && b.y == block.y - 1);
                //        
                //        if(!nextBlock.wasChecked)
                //        {
                //            block = nextBlock;
                //        }
                //    }
                //}
                //
                //if (block.x + 1 > xLen || cells[block.x + 1, block.y] == Biom.empty)  //Right
                //{
                //    points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE / 2 - ONE / 2));
                //}
                //else
                //{
                //    if (blocks.Any(b => b.x == block.x + 1 && b.y == block.y))
                //    {
                //        var nextBlock = blocks.Find(b => b.x == block.x + 1 && b.y == block.y);
                //        
                //        if(!nextBlock.wasChecked)
                //        {
                //            block = nextBlock;
                //        }
                //    }
                //}
                //
                //if (block.y + 1 > yLen || cells[block.x, block.y + 1] == Biom.empty)  // Down
                //{
                //    points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE / 2 - ONE / 2));
                //}
                //else
                //{
                //    if (blocks.Any(b => b.x == block.x && b.y == block.y + 1))
                //    {
                //        var nextBlock = blocks.Find(b => b.x == block.x && b.y == block.y + 1);
                //        
                //        if(!nextBlock.wasChecked)
                //        {
                //            block = nextBlock;
                //        }
                //    }
                //}
            }
        }

        PolygonCollider2D coll = gameObject.AddComponent<PolygonCollider2D>();
        coll.points = points.ToArray();
    }

    private bool CheckForBoards(int x, int y)
    {
        if(x - 1 < 0 || cells[x - 1, y] == Biom.empty)  return true;
        if(y - 1 < 0 || cells[x, y - 1] == Biom.empty)  return true;
        if(x + 1 > cells.GetLength(0) || cells[x + 1, y] == Biom.empty)   return true;
        if(y + 1 > cells.GetLength(1) || cells[x, y + 1] == Biom.empty)   return true;

        return false;
    }

    private class Block
    {
        public int  x;
        public int  y;
        public bool wasChecked;

        public Block(int x, int y)
        {
             this.x = x;   
             this.y = y;
             wasChecked = false;
        }

        public bool Check(ref Block block, List<Block> blocks, List<Vector2> points, Biom[,] cells, int x, int y)
        {
            if (blocks.Any(b => b.x == x && b.y == y))
            {
                var nextBlock = blocks.Find(b => b.x == x && b.y == y);
                
                if(!nextBlock.wasChecked)
                {
                    block = nextBlock;
                }
                return true;
            }
            return false;
        }

        public void CheckFromLeft(ref Block block, List<Block> blocks, List<Vector2> points, Biom[,] cells, int xLen, int yLen)
        {
            if (block.wasChecked) return;
            wasChecked = true;

            if (block.x - 1 < 0 || !Check(ref block, blocks, points, cells, block.x - 1, block.y))                                  //Left
            {
                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE + ONE / 2));

                if(block.x - 1 < 0 || block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x - 1, block.y - 1))        //LeftUp
                {
                    if (block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x, block.y - 1))                          //Up
                    {
                        points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE + ONE / 2));

                        if (block.x + 1 > xLen || block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x + 1, block.y - 1))    //UpRight
                        {
                            if (block.x + 1 > xLen || !Check(ref block, blocks, points, cells, block.x + 1, block.y))              //Right
                            {
                                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));

                                if (block.x + 1 > xLen || block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x + 1, block.y + 1)) //RightDown
                                {
                                    if (block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x, block.y + 1))       //Down
                                    {
                                        points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));

                                        if (!(block.x - 1 < 0 || block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x - 1, block.y + 1)))//LeftDown
                                        {
                                            block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
                                        }
                                    }
                                    else
                                    {
                                        block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
                                    }
                                }
                                else
                                {
                                    block.CheckFromUp(ref block, blocks, points, cells, xLen, yLen);
                                }   
                            }
                            else
                            {
                                block.CheckFromUp(ref block, blocks, points, cells, xLen, yLen);
                            }
                        }
                        else
                        {
                            block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);
                        }
                    }
                    else
                    {
                        block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);
                    }
                }
                else
                {
                    block.CheckFromDown(ref block, blocks, points, cells, xLen, yLen);
                }
            }
            else
            {
                block.CheckFromDown(ref block, blocks, points, cells, xLen, yLen);
            }
        }

        public void CheckFromUp(ref Block block, List<Block> blocks, List<Vector2> points, Biom[,] cells, int xLen, int yLen)
        {
            if (block.wasChecked) return;
            wasChecked = true;

            if (block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x, block.y - 1))                          //Up
            {
                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE + ONE / 2));
            
                if (block.x + 1 > xLen || block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x + 1, block.y - 1))    //UpRight
                {
                    if (block.x + 1 > xLen || !Check(ref block, blocks, points, cells, block.x + 1, block.y))              //Right
                    {
                        points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));
            
                        if (block.x + 1 > xLen || block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x + 1, block.y + 1)) //RightDown
                        {
                            if (block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x, block.y + 1))       //Down
                            {
                                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));
            
                                if (!(block.x - 1 < 0 || block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x - 1, block.y + 1)))//LeftDown
                                {
                                    block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);

                                    if (block.x - 1 < 0 || !Check(ref block, blocks, points, cells, block.x - 1, block.y))                                  //Left
                                    {
                                        points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE + ONE / 2));

                                        if (!(block.x - 1 < 0 || block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x - 1, block.y - 1)))    //LeftUp
                                        {
                                            block.CheckFromDown(ref block, blocks, points, cells, xLen, yLen);
                                        }
                                    }
                                    else
                                    {
                                        block.CheckFromDown(ref block, blocks, points, cells, xLen, yLen);
                                    }
                                }
                                else
                                {
                                    block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
                                }
                            }
                            else
                            {
                                block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
                            }
                        }
                        else
                        {
                            block.CheckFromUp(ref block, blocks, points, cells, xLen, yLen);
                        }   
                    }
                    else
                    {
                        block.CheckFromUp(ref block, blocks, points, cells, xLen, yLen);
                    }
                }
                else
                {
                    block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);
                }
            }
            else
            {
                block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);
            }
        }

        public void CheckFromRight(ref Block block, List<Block> blocks, List<Vector2> points, Biom[,] cells, int xLen, int yLen)
        {
            if (block.wasChecked) return;
            wasChecked = true;

            if (block.x + 1 > xLen || !Check(ref block, blocks, points, cells, block.x + 1, block.y))              //Right
            {
                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));
            
                if (block.x + 1 > xLen || block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x + 1, block.y + 1)) //RightDown
                {
                    if (block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x, block.y + 1))       //Down
                    {
                        points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));
            
                        if ((block.x - 1 < 0 || block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x - 1, block.y + 1)))//LeftDown
                        {
                            if (block.x - 1 < 0 || !Check(ref block, blocks, points, cells, block.x - 1, block.y))                                  //Left
                            {
                                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE + ONE / 2));
            
                                if(block.x - 1 < 0 || block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x - 1, block.y - 1))        //LeftUp
                                {
                                    if (block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x, block.y - 1))                          //Up
                                    {
                                        points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE + ONE / 2));
                                        
                                        if (!(block.x + 1 > xLen || block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x + 1, block.y - 1)))    //UpRight
                                        {
                                            block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);
                                        }
                                    }
                                    else
                                    {
                                        block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);
                                    }
                                }
                                else
                                {
                                    block.CheckFromDown(ref block, blocks, points, cells, xLen, yLen);
                                }
                            }
                            else
                            {
                                block.CheckFromDown(ref block, blocks, points, cells, xLen, yLen);
                            }
                        }
                        else
                        {
                            block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
                        }
                    }
                    else
                    {
                        block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
                    }
                }
                else
                {
                    block.CheckFromUp(ref block, blocks, points, cells, xLen, yLen);
                }   
            }
            else
            {
                block.CheckFromUp(ref block, blocks, points, cells, xLen, yLen);
            }                          
        }

        public void CheckFromDown(ref Block block, List<Block> blocks, List<Vector2> points, Biom[,] cells, int xLen, int yLen)
        {
            if (block.wasChecked) return;
            wasChecked = true;

            if (block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x, block.y + 1))       //Down
            {
                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));
            
                if (!(block.x - 1 < 0 || block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x - 1, block.y + 1)))//LeftDown
                {
                    block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
            
                    if (block.x - 1 < 0 || !Check(ref block, blocks, points, cells, block.x - 1, block.y))                                  //Left
                    {
                        points.Add(new Vector2(-(xLen / 2 - block.x) * ONE - ONE /2, (yLen / 2 - block.y) * ONE + ONE / 2));
            
                        if(block.x - 1 < 0 || block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x - 1, block.y - 1))        //LeftUp
                        {
                            if (block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x, block.y - 1))                          //Up
                            {
                                points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE + ONE / 2));
                                
                                if (!(block.x + 1 > xLen || block.y - 1 < 0 || !Check(ref block, blocks, points, cells, block.x + 1, block.y - 1)))    //UpRight
                                {
                                    block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);

                                    if (block.x + 1 > xLen || !Check(ref block, blocks, points, cells, block.x + 1, block.y))              //Right
                                    {
                                        points.Add(new Vector2(-(xLen / 2 - block.x) * ONE + ONE /2, (yLen / 2 - block.y) * ONE - ONE / 2));
                                    
                                        if (!(block.x + 1 > xLen || block.y + 1 > yLen || !Check(ref block, blocks, points, cells, block.x + 1, block.y + 1))) //RightDown
                                        {
                                            block.CheckFromUp(ref block, blocks, points, cells, xLen, yLen);
                                        }
                                    }
                                    else
                                    {
                                        block.CheckFromUp(ref block, blocks, points, cells, xLen, yLen);
                                    }
                                }
                                else
                                {
                                    block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);
                                }
                            }
                            else
                            {
                                block.CheckFromLeft(ref block, blocks, points, cells, xLen, yLen);
                            }
                        }
                        else
                        {
                            block.CheckFromDown(ref block, blocks, points, cells, xLen, yLen);
                        }
                    }
                    else
                    {
                        block.CheckFromDown(ref block, blocks, points, cells, xLen, yLen);
                    }
                }
                else
                {
                    block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
                }
            }
            else
            {
                block.CheckFromRight(ref block, blocks, points, cells, xLen, yLen);
            }                            
        }
    }
   
   // private void OnDrawGizmos()
   // {
   //     int centerX = cells.GetLength(0) / 2;
   //     int centerY = cells.GetLength(1) / 2;
   //
   //     List<GameObject> newCells = new List<GameObject>();
   //
   //     for(int i = 0; i < cells.GetLength(0); i++)
   //     {
   //         for(int k = 0; k < cells.GetLength(1); i++)
   //         {
   //             if (cells[i, k] != Biom.empty)
   //             {
   //                 Gizmos.DrawCube(transform.position - Vector3.right * (centerX - i * 0.1f) - Vector3.up * (centerY - k * 0.01f), Vector3.one * 0.1f);
   //             }
   //         }
   //     }
   // }
}