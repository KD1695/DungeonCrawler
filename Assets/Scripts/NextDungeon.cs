using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDungeon : MonoBehaviour
{
    private bool loadingNextScene = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(loadingNextScene)
                return;
            loadingNextScene = true;
            GameManager.Instance.LevelUp();
            SceneManager.LoadSceneAsync(0);
        }
    }
}
