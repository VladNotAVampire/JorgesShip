using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.Battle;

//namespace Assets.Resources.Scripts.Battle
//{
    public class BattleManager : MonoBehaviour
    {
        const string unitPrefabPath = "Prefabs/BattleScene/Unit";

        [SerializeField]private UnitToSpott[] firstTeamUnits;
        [SerializeField]private UnitToSpott[] secondTeamUnits;
        [SerializeField]private float firstTeamSpawnStart;
        [SerializeField]private float secondTeamSpawnStart;

        public IUnit[] FirstTeam { get; private set; }
        public IUnit[] SecondTeam { get; private set; }
        public IUnit[] AnotherTeam(IUnit unit)
        {
            if (FirstTeam.Contains(unit))
                return SecondTeam;
                
            return FirstTeam;    
        }
        
        private void Start()
        {
            FirstTeam = SpottTeam(firstTeamUnits, firstTeamSpawnStart, World.ONE);
            SecondTeam = SpottTeam(secondTeamUnits, secondTeamSpawnStart, -World.ONE);
        }

        private IUnit[] SpottTeam(UnitToSpott[] teamToSpott, float startSpawnPosition, float step)
        {
            var count = teamToSpott.Length;
            var team = new IUnit[count];
            var spawner = new Spawner(startSpawnPosition, step);
            var prefab = (GameObject)Resources.Load(unitPrefabPath);

            for(int i = 0; i < count; i++)
            {
                var obj = spawner.Spawn(prefab);

                if (obj == null)
                    continue;

                var unit = obj.GetComponent<Unit>();
                unit.Stats = new UnitStats(teamToSpott[i].name, teamToSpott[i].hitPoinsts, teamToSpott[i].damage, teamToSpott[i].speed);
                unit.Weapon = new Sword();
                team[i] = unit;
            }
            
            return team;
        }
    }

    [System.Serializable]
    public class UnitToSpott
    {
        public string name;
        public short  hitPoinsts;
        public short  damage;
        public float  speed;
    }

    public struct Spawner : ISpawner
    {
        private float SpawnPosition { get; set; }
        private float Step { get; set; }

        public Spawner(float startSpawnPosition) : this(startSpawnPosition, World.ONE)
        {
            //SpawnPosition = startSpawnPosition;
        }

        public Spawner(float startSpawnPosition, float step)
        {
            SpawnPosition = startSpawnPosition;
            Step          = step;
        }

        public GameObject Spawn(GameObject prefab)
        {
            Vector3 spawnPosition;
            
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(SpawnPosition, 10), Vector2.down);
            if(hit)
            {
                spawnPosition = hit.point;
            }
            else
            {
                Debug.Log("One of units has fallen down");
                return null;
            }

            var obj = (GameObject)Object.Instantiate(prefab, spawnPosition, Quaternion.identity);

            SpawnPosition += Step;

            return obj;
        }
    }
//}
