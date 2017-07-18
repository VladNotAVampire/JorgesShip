using UnityEngine;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    #region ilsandInstances
    public static readonly Biom[,] smallIsland =   {
                                                         {Biom.empty, Biom.empty, Biom.empty, Biom.empty, Biom.empty},
                                                         {Biom.empty, Biom.sand, Biom.sand, Biom.sand, Biom.empty},
                                                         {Biom.empty, Biom.sand, Biom.sand, Biom.sand, Biom.empty},
                                                         {Biom.empty, Biom.sand, Biom.sand, Biom.sand, Biom.empty},
                                                         {Biom.empty, Biom.empty, Biom.empty, Biom.empty, Biom.empty} 
                                                   };

    #endregion

    #region randPoints
    public Dictionary<Biom[,], Vector2[]> randomizePoints = new Dictionary<Biom[,], Vector2[]> {

                                                               {smallIsland, new Vector2[] { Vector2.one, new Vector2(1, 3), new Vector3(3, 3), new Vector3(3, 1) }}
                                                          };
    #endregion
}

public enum Biom : byte
{
    empty,
    sand,
    grass,
    forest,
    stone,
    village,
    town
}