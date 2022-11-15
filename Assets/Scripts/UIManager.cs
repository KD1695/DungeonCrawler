using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    [SerializeField] private TextMeshProUGUI soulsText;
    [SerializeField] private TextMeshProUGUI levelText;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(this.gameObject);
    }

    private void Start()
    {
        GameManager.Instance.updatedSoulsCount += SetSoulsUI;
        GameManager.Instance.updatedLevelCount += SetLevelUI;
    }

    private void SetLevelUI(int level)
    {
        levelText.text = "Level : " + level;
    }

    private void SetSoulsUI(int souls)
    {
        soulsText.text = "Souls : " + souls;
    }
}
