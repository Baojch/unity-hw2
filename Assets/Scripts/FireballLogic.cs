using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLogic : MonoBehaviour
{
    const float SPEED = 7.0f;
    Rigidbody m_rigidBody;


    [SerializeField]
    GameObject m_explosionObject;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.velocity = transform.forward * SPEED;
        StartCoroutine(DestroyMyself());

    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerLogic playerLogic = other.GetComponent<PlayerLogic>();
            if(playerLogic)
            {
                playerLogic.Die();

                Instantiate(m_explosionObject, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyMyself()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
        
    }
}
