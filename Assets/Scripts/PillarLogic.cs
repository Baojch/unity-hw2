using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarLogic : MonoBehaviour
{
    bool ismoving = false;
    bool inverseMoving = false;
    [SerializeField]
    float time = 0.0f;

    float currentPos = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position.y;
        StartCoroutine(Waitfor(time));
    }

    // Update is called once per frame
    void Update()
    {
        if(ismoving == true){
            transform.position += new Vector3(0, 2, 0) * Time.deltaTime;
            if(transform.position.y > currentPos + 4.0f){
                ismoving = false;
                StartCoroutine(Wait());

            }
        }
        if(inverseMoving == true){
            transform.position -= new Vector3(0, 2, 0) * Time.deltaTime;
            if(transform.position.y < currentPos){
                inverseMoving = false;
            }
        }

    }

    IEnumerator Waitfor(float time)
    {
        while (true)
        {
            ismoving = true;
            yield return new WaitForSeconds(time);
            
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        inverseMoving = true;
    }
}
