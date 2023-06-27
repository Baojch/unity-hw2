using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinState
{
    Inactive,
    Active
}

public class CoinLogic : MonoBehaviour
{
    const float ROTATION_SPEED = 100.0f;

    CoinState m_coinState = CoinState.Active;

    MeshRenderer m_meshRenderer;
    Collider m_collider;

    void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_collider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.right, Time.deltaTime * ROTATION_SPEED);    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SaveManager.Instance.AddCoin();
            SetCoinState(CoinState.Inactive);

        }
    }

    void SetCoinState(CoinState coinState)
    {
        m_coinState = coinState;

        m_meshRenderer.enabled = m_coinState == CoinState.Active;
        m_collider.enabled = m_coinState == CoinState.Active;
    }

}
