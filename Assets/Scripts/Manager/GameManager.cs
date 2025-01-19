using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerControllerr playerController;
    private ScrollManager scrollManager;
    private MonsterSpawnManager monsterSpawnManager;

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

        inputHandle = GetComponent<KeyBoardInputHandle>();
    }

    private void Start()
    {
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        if (inputHandle != null) {
            playerController?.CustomUpdate(inputHandle.GetInput());
        }
    }

    IEnumerator GameStart()
    {
        scrollManager?.SetScrollSpeed(4f);
        playerController?.GameStart();
        monsterSpawnManager?.InitSpawnManager();
        yield return null;
    }
}
