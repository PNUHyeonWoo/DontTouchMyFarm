using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform frontPosition;
    [SerializeField]
    private Transform backPosition;
    [SerializeField]
    private Transform rightPosition;
    [SerializeField]
    private Transform leftPosition;
    [Serializable]
    private struct NightSpawn 
    {
        [Serializable]
        public struct MonsterSpawn 
        {
            public GameObject monster;
            public int amount;
        }
        [NonReorderable]
        public MonsterSpawn[] front;
        [NonReorderable]
        public MonsterSpawn[] back;
        [NonReorderable]
        public MonsterSpawn[] right;
        [NonReorderable]
        public MonsterSpawn[] left;
        public bool isAllSideSpawnWithFront;

        public int GetTotalAmount() 
        {
            int result = 0;
            foreach (MonsterSpawn spawns in front)
                result += spawns.amount;

            if (isAllSideSpawnWithFront)
                return result * 4;

            foreach (MonsterSpawn spawns in back)
                result += spawns.amount;
            foreach (MonsterSpawn spawns in right)
                result += spawns.amount;
            foreach (MonsterSpawn spawns in left)
                result += spawns.amount;
            return result;
        }
    }
    [NonReorderable]
    [SerializeField]
    private NightSpawn[] nightSpawns;

    public void SpawnMonster(int day)
    {
        day--;
        day = day >= nightSpawns.Length ? nightSpawns.Length - 1 : day;

        NightSpawn todaySpawn = nightSpawns[day];

        Transform[] spawnPositions = { frontPosition, backPosition, rightPosition, leftPosition };
        NightSpawn.MonsterSpawn[][] sideSpawns;

        if (!todaySpawn.isAllSideSpawnWithFront)
            sideSpawns = new NightSpawn.MonsterSpawn[][]{ todaySpawn.front, todaySpawn.back, todaySpawn.right, todaySpawn.left };
        else
            sideSpawns = new NightSpawn.MonsterSpawn[][] { todaySpawn.front, todaySpawn.front, todaySpawn.front, todaySpawn.front };

        for(int side = 0; side < 4; side++)
            foreach (NightSpawn.MonsterSpawn spawns in sideSpawns[side])
                for (int i = 0; i < spawns.amount; i++)
                { 
                    GameObject spawnMonster = Instantiate(spawns.monster);
                    spawnMonster.transform.position = spawnPositions[side].position;
                }

        Monster.totalAmount = todaySpawn.GetTotalAmount();
    }

}
