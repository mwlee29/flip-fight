using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnEndGameMenu : MonoBehaviour
{
    public GameObject playerEndGameMenuPrefab;
    public PlayerInput input;

    private void Awake()
    {
        var rootMenu = GameObject.Find("GameplayUI");
        if (rootMenu != null)
        {
            var menu = Instantiate(playerEndGameMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
        }
    }
}
