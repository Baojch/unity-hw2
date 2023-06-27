using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
     public static SaveManager Instance;

    PlayerLogic m_playerLogic;

    int coinCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        m_playerLogic = FindObjectOfType<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        m_playerLogic.Save();

        PlayerPrefs.Save();
    }

    public void Load()
    {
        m_playerLogic.Load();

    }
    public void AddCoin()
    {
        coinCount++;
        // Debug.Log("Coin Count: " + coinCount);
    }
    public int GetCoinCount()
    {
        return coinCount;
    }
    
    public bool minusCoin()
    {
        if(coinCount >= 1)
        {
            coinCount--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
