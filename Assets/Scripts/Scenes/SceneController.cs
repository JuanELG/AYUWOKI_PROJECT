using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController singleton;

    public string MAIN_MENU = "USER_INTERFACE";
    public string GAME = "Level-1";

    private void Awake()
    {
        singleton = this;
    }
}
