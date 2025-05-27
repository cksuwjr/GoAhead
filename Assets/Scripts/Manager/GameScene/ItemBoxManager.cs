using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// => ItemWeightedValue / Sum(ItemWeightedValue)
public enum ItemWeightedValue
{
    ArrowPower = 10,
    ArrowSpeed = 10,
    ArrowCount = 1,
    ShootSpeed = 5,
}

// =>  ItemValanceValue / 100
public enum ItemBalanceValue
{
    ArrowPower = 100,
    ArrowSpeed = 1,
    ArrowCount = 10,
    ShootSpeed = 100,
}

public class ItemBoxManager : MonoBehaviour
{
    private float probabilityFromMonster = 0.25f;
    private float itemBox_naturalSpawnTime = 10f;

    private float itemfloorSpeed = 4f;

    private void OnEnable()
    {
        Monster.onDead += MonsterDeadItemSpawn;
    }

    private void OnDisable()
    {
        Monster.onDead -= MonsterDeadItemSpawn;
    }

    private StatusType GetRandomItemType()
    {
        var sumValue = 0;
        sumValue += (int)ItemWeightedValue.ArrowPower;
        sumValue += (int)ItemWeightedValue.ArrowSpeed;
        sumValue += (int)ItemWeightedValue.ArrowCount;
        sumValue += (int)ItemWeightedValue.ShootSpeed;

        var randomValue = Random.Range(0f, sumValue);
        var value = (int)ItemWeightedValue.ArrowPower;

        if (randomValue < value) return StatusType.ArrowPower;
        value += (int)ItemWeightedValue.ArrowSpeed;
        if(randomValue < value) return StatusType.ArrowSpeed;
        value += (int)ItemWeightedValue.ArrowCount;
        if (randomValue < value) return StatusType.ArrowCount;
        value += (int)ItemWeightedValue.ShootSpeed;
        if (randomValue < value) return StatusType.ShootSpeed;

        Debug.Log("가중치 값을 벗어남");
        return StatusType.ArrowPower;
    }

    private float GetBalanceValue(StatusType itemType, float value)
    {
        float returnValue = 0;
        switch (itemType)
        {
            case StatusType.ArrowPower:
                returnValue = Mathf.FloorToInt(((int)(ItemBalanceValue.ArrowPower) / 100f) * value);
                break;
            case StatusType.ArrowSpeed:
                returnValue = ((int)ItemBalanceValue.ArrowSpeed / 100f) * value;
                returnValue = Mathf.Floor(returnValue * 100f) / 100f;
                break;
            case StatusType.ArrowCount:
                returnValue = (int)(((int)ItemBalanceValue.ArrowCount / 100f) * value);
                returnValue += 1;
                break;
            case StatusType.ShootSpeed:
                returnValue = Mathf.FloorToInt(((int)ItemBalanceValue.ShootSpeed / 100f) * value);
                break;
            default:
                break;
        }

        return returnValue;
    }

    private void MonsterDeadItemSpawn(Monster monster)
    {
        var randValue = Random.Range(0f, 100f) / 100f;
        if (randValue > probabilityFromMonster) return;
        var spawnPos = monster.transform.position;
        spawnPos.y = 1;

        ItemSpawnRandom(spawnPos);
    }

    public void SetProbabilityFromMonster(float percent)
    {
        probabilityFromMonster = percent;
    }

    public void ItemSpawn(Vector3 spawnPosition, StatusType itemType, float value)
    {
        var item = PoolManager.Instance.itemBoxPool.GetPoolObject();
        if (item.TryGetComponent<ItemBox>(out var itembox))
        {
            itembox.Init(itemType, value, itemfloorSpeed);
            item.transform.position = spawnPosition;
            item.SetActive(true);
        }
    }

    public void ItemSpawnRandom(Vector3 spawnPosition)
    {
        var item = PoolManager.Instance.itemBoxPool.GetPoolObject();
        var value = 0f;
        var valueMax = 10 + ((int)Time.time / 100);
        StatusType itemType;
        if (item.TryGetComponent<ItemBox>(out var itembox))
        {
            
            value = Random.Range(1, valueMax);

            itemType = GetRandomItemType();
            value = GetBalanceValue(itemType, value);

            itembox.Init(itemType, value, itemfloorSpeed);
            item.transform.position = spawnPosition;
            item.SetActive(true);
        }
    }

    public void DoubleItemBoxSpawn(Vector3 spawnPosition, Dictionary<StatusType, float> items)
    {
        var item = PoolManager.Instance.doubleItemBoxPool.GetPoolObject();
        

        if (item.TryGetComponent<DoubleItemBox>(out var itembox))
        {
            itembox.Init(items, itemfloorSpeed);
            item.transform.position = spawnPosition;
            item.SetActive(true);
        }
    }

    public void DoubleItemRandomBoxSpawn(Vector3 spawnPosition)
    {
        var item = PoolManager.Instance.doubleItemBoxPool.GetPoolObject();
        Dictionary<StatusType, float> items = new Dictionary<StatusType, float>();

        var randType = GetRandomItemType();
        var randType2 = GetRandomItemType();

        var valueMax = 30 + ((int)Time.time / 100);

        items.Add(randType, GetBalanceValue(randType, Random.Range(10, valueMax)));
        while(randType == randType2)
            randType2 = GetRandomItemType();

        items.Add(randType2, -GetBalanceValue(randType2, Random.Range(10, valueMax)));

        if (item.TryGetComponent<DoubleItemBox>(out var itembox))
        {
            itembox.Init(items, itemfloorSpeed);
            item.transform.position = spawnPosition;
            item.SetActive(true);
        }
    }

    public void SetItemSpeed(float speed)
    {
        itemfloorSpeed = speed;
    }

    public void StartSpawn()
    {
        StartCoroutine("ItemSpawnNatural");
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }



    private IEnumerator ItemSpawnNatural()
    {
        while (true)
        {
            yield return YieldInstructionCache.WaitForSeconds(itemBox_naturalSpawnTime);
            if (Random.Range(0f, 1f) < 0.5f)
                ItemSpawnRandom(new Vector3(0, 1, 35));
            else
                DoubleItemRandomBoxSpawn(new Vector3(0, 1, 35));
        }
    }
}
