using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [SerializeField] private Vector2 spawnXRange = new Vector2(-4.5f, 4.5f);

    [SerializeField] private float spawnTime = 1f;

    private void SpawnMonster()
    {
        var time = (int)Time.time;

        var monster = PoolManager.Instance.monsterPool.GetPoolObject();

        monster.transform.position = new Vector3(Random.Range(spawnXRange.x, spawnXRange.y), 0, 40f);
        monster.transform.rotation = Quaternion.Euler(0, 180f, 0);
        monster.SetActive(true);

        if (!monster.TryGetComponent<Monster>(out var mob)) return;


        mob.SetAct(true);
        if (time < 60)
        {
            mob.SetSpeed(4f);
            mob.InitMonster(time / 2, MonsterState.Move01);
        }
        else if(time < 120)
        {
            mob.SetSpeed(5f);
            mob.InitMonster(time / 2, MonsterState.Move01);
        }
        else if(time < 180)
        {
            GameManager.Instance.SetGameSpeed(5f);
            mob.SetSpeed(6f);
            mob.InitMonster(time / 2, MonsterState.Move02);
        }
        else if(time < 300)
        {
            GameManager.Instance.SetGameSpeed(7f);
            mob.SetSpeed(8f);
            var mul = time / 60;
            mob.InitMonster(time + 60 * mul, MonsterState.Move03);
        }
    }

    public void SetSpawnTerm(float time)
    {
        spawnTime = time;
    }

    public void StartSpawn()
    {
        StartCoroutine("StartSpawnC");
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartSpawnC()
    {
        while (true)
        {
            yield return YieldInstructionCache.WaitForSeconds(spawnTime);
            SpawnMonster();
        }
    }
}