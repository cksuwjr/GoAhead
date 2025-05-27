using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private GameObject[] terrains;
    [SerializeField] private GameObject[] terrainObjects;
    [SerializeField] private GameObject[] edgeObjects;

    public void Init()
    {
        CreateTerrain();
    }

    private void CreateTerrain()
    {
        SpawnEdge();
        SpawnEdgeObjects();
    }

    private void SpawnEdgeObjects()
    {
        if (terrains.Length < 1) return;
        if (terrainObjects.Length < 1) return;

        Vector3 createPosition;
        int randIntValue = 0;
        float randFloatValue = 0f;

        Vector3 terrainScale;
        Vector3 terrainObjectScale;

        //1 / 1.5f  0.6
        // 1 / 5

        terrainScale = terrains[0].transform.localScale;
        terrainObjectScale = new Vector3(1 / terrainScale.x, 1 / terrainScale.y, 1 / terrainScale.z);
        for (float z = 0; z <= 50; z+=0.5f)
        {
            randIntValue = Random.Range(0, terrainObjects.Length);
            randFloatValue = Random.Range(-1.5f, 0f);

            createPosition.x = -5.5f + randFloatValue;
            createPosition.y = Random.Range(0f, 2f);
            createPosition.z = z;

            var spawnedObject = Instantiate(terrainObjects[randIntValue], createPosition, Quaternion.identity, terrains[0].transform);
            spawnedObject.transform.localScale = terrainObjectScale;

            createPosition.z = 100 - z;
            spawnedObject = Instantiate(terrainObjects[randIntValue], createPosition, Quaternion.identity, terrains[1].transform);
            spawnedObject.transform.localScale = terrainObjectScale;
        }

        for (float z = 0; z <= 50; z+= 0.5f)
        {
            randIntValue = Random.Range(0, terrainObjects.Length);
            randFloatValue = Random.Range(0, 1.5f);

            createPosition.x = 5.5f + randFloatValue;
            createPosition.y = Random.Range(0f, 2f);
            createPosition.z = z;

            var spawnedObject = Instantiate(terrainObjects[randIntValue], createPosition, Quaternion.identity, terrains[0].transform);
            spawnedObject.transform.localScale = terrainObjectScale;

            createPosition.z = 100 - z;
            spawnedObject = Instantiate(terrainObjects[randIntValue], createPosition, Quaternion.identity, terrains[1].transform);
            spawnedObject.transform.localScale = terrainObjectScale;
        }
    }

    private void SpawnEdge()
    {
        if (terrains.Length < 1) return;
        if (edgeObjects.Length < 1) return;

        Vector3 createPosition;
        int randIntValue = 0;
        float randFloatValue = 0f;
        GameObject nowTerrain;
        Vector3 nowTerrainScale;
        Vector3 terrainObjectScale;

        float edgeMul = 3.5f;

        nowTerrain = terrains[0];
        nowTerrainScale = nowTerrain.transform.localScale;
        terrainObjectScale = new Vector3(1 / nowTerrainScale.x, 1 / nowTerrainScale.y, 1 / nowTerrainScale.z);
        terrainObjectScale *= edgeMul;
        terrainObjectScale.y *= 5;
        for (float z = 0; z <= 50; z += edgeMul)
        {
            randIntValue = Random.Range(0, edgeObjects.Length);
            randFloatValue = Random.Range(-1.5f, 0f);

            createPosition.x = -10f + randFloatValue;
            createPosition.y = 0;
            createPosition.z = z;

            var spawnedObject = Instantiate(edgeObjects[randIntValue], createPosition, Quaternion.identity, terrains[0].transform);
            spawnedObject.transform.localScale = terrainObjectScale;

            createPosition.z = 100 - z;
            spawnedObject = Instantiate(edgeObjects[randIntValue], createPosition, Quaternion.identity, terrains[1].transform);
            spawnedObject.transform.localScale = terrainObjectScale;
        }

        for (float z = 0; z <= 50; z += edgeMul)
        {
            randIntValue = Random.Range(0, edgeObjects.Length);
            randFloatValue = Random.Range(0, 1.5f);

            createPosition.x = 10f + randFloatValue;
            createPosition.y = 0;
            createPosition.z = z;

            var spawnedObject = Instantiate(edgeObjects[randIntValue], createPosition, Quaternion.identity, terrains[0].transform);
            spawnedObject.transform.localScale = terrainObjectScale;

            createPosition.z = 100 - z;
            spawnedObject = Instantiate(edgeObjects[randIntValue], createPosition, Quaternion.identity, terrains[1].transform);
            spawnedObject.transform.localScale = terrainObjectScale;
        }
    }
}
