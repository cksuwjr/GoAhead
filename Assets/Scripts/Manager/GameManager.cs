using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerControllerr playerController;
    private ScrollManager scrollManager;
    private MonsterSpawnManager monsterSpawnManager;
    private ItemBoxManager itemBoxManager;
    private UIManager uiManager;
    private TerrainManager terrainManager;

    private IInputHandle inputHandle;


    private static GameManager instance;

    public static GameManager Instance => instance;

    public GameObject player;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject); else instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        player?.TryGetComponent<PlayerControllerr>(out playerController);
        GameObject.Find("ScrollManager")?.TryGetComponent<ScrollManager>(out scrollManager);
        GameObject.Find("MonsterSpawnManager")?.TryGetComponent<MonsterSpawnManager>(out monsterSpawnManager);
        GameObject.Find("ItemBoxManager")?.TryGetComponent<ItemBoxManager>(out itemBoxManager);
        GameObject.Find("UIManager")?.TryGetComponent<UIManager>(out uiManager);
        GameObject.Find("TerrainManager")?.TryGetComponent<TerrainManager>(out terrainManager);

        inputHandle = GetComponent<KeyBoardInputHandle>();
    }

    private void Start()
    {
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        if (inputHandle == null) return;

        playerController?.CustomUpdate(inputHandle.GetInput());
    }

    IEnumerator GameStart()
    {
        uiManager?.Init();

        terrainManager?.Init();
        scrollManager?.SetScrollSpeed(4f);
        playerController?.GameStart();

        monsterSpawnManager?.SetSpawnTerm(5);
        monsterSpawnManager?.StartSpawn();
        itemBoxManager?.SetProbabilityFromMonster(1f);
        itemBoxManager?.StartSpawn();
        yield return YieldInstructionCache.WaitForSeconds(60f);

        monsterSpawnManager?.SetSpawnTerm(3f);
        itemBoxManager?.SetProbabilityFromMonster(0.25f);
        //itemBoxManager?.StartSpawn();

        yield return YieldInstructionCache.WaitForSeconds(240f);

        itemBoxManager?.SetProbabilityFromMonster(0.15f);
        monsterSpawnManager?.SetSpawnTerm(1f);

    }

    public void SetGameSpeed(float speed)
    {
        scrollManager?.SetScrollSpeed(speed);
        itemBoxManager?.SetItemSpeed(speed);
    }
}
