using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerControllerr playerController;
    private ScrollManager scrollManager;

    IInputHandle inputHandle;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player")?.TryGetComponent<PlayerControllerr>(out playerController);
        GameObject.Find("ScrollManager")?.TryGetComponent<ScrollManager>(out scrollManager);

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
        yield return null;
    }
}
