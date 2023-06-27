using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShoot : MonoBehaviour
{
    [SerializeField]
    Transform m_fireballSpawn;

    [SerializeField]
    GameObject m_fireballObject;

    Vector3 m_spawnPos;
    
    // Start is called before the first frame update
    void Start()
    {
        m_spawnPos = transform.position;
        StartCoroutine(ReleaseFireballCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ReleaseFireball()
    {
        // Debug.Log("Release Fireball");
        Instantiate(m_fireballObject, m_fireballSpawn.transform.position, transform.rotation);
    }
    IEnumerator ReleaseFireballCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            ReleaseFireball();
        }
    }
}
