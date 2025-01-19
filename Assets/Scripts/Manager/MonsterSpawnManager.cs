using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;

    [SerializeField] private Vector2 spawnXRange = new Vector2(-4.5f, 4.5f);

    [SerializeField] private float spawnTime = 1f;


    private void SpawnMonster()
    {
        var monster = PoolManager.Instance.monsterPool.GetPoolObject();
        monster.transform.position = new Vector3(Random.Range(spawnXRange.x, spawnXRange.y), 0, 45f);
        monster.transform.rotation = Quaternion.Euler(0, 180f, 0);
        monster.SetActive(true);

        if (monster.TryGetComponent<Monster>(out var mob))
        {
            mob.SetSpeed(4f);
            mob.SetAct(true);
            mob.InitMonster();
        }
    }

    public void InitSpawnManager()
    {
        StartCoroutine("StartSpawn");
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }



    private IEnumerator StartSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnMonster();
        }
    }
}