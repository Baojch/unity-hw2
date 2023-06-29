using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delphox : MonoBehaviour
{
    GameObject m_delphox;

    [SerializeField]
    GameObject Stick;

    // float fadeDuration = 1.0f;
    float targetAngle = 175.0f;
    float currentAngle = 0.0f;

    float currentPos = 0.0f;

    bool isRotating = false;

    bool inverseRotating = false;

    bool ismoving = false;
    bool inverseMoving = false;

    AudioSource m_audioSource;

    [SerializeField]
    AudioClip Magic;
    // private Renderer[] childRenderers;
    // private Coroutine fadeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        m_delphox = GameObject.FindWithTag("Delphox");
        currentAngle = Stick.transform.eulerAngles.y;
        // childRenderers = m_delphox.GetComponentsInChildren<Renderer>();
        currentPos = m_delphox.transform.position.y;
        m_delphox.SetActive(false);
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ismoving == true){
            m_delphox.transform.position += new Vector3(0, 3, 0) * Time.deltaTime;
            if(m_delphox.transform.position.y > currentPos + 5.0f){
                ismoving = false;
            }
            if(m_delphox.transform.position.y > currentPos + 3.0f){
                isRotating = true;
            }
        }
        if(inverseMoving == true){
            m_delphox.transform.position -= new Vector3(0, 2, 0) * Time.deltaTime;
            if(m_delphox.transform.position.y < currentPos){
                inverseMoving = false;
            }
        }
        if (isRotating && Stick != null)
        {
            //sound
            Stick.transform.Rotate(Vector3.up, 120f * Time.deltaTime);
            if(Stick.transform.eulerAngles.y  > targetAngle)
            {
                m_audioSource.PlayOneShot(Magic);
                isRotating = false;
                Debug.Log("Stick rotation stopped.");
                inverseRotating = true;
            }
        }
        if(inverseRotating && Stick != null)
        {
            Stick.transform.Rotate(Vector3.up, -120f * Time.deltaTime);
            if(Stick.transform.eulerAngles.y < currentAngle)
            {
                inverseRotating = false;
                inverseMoving = true;
            }
        }
        if(!inverseMoving && !inverseRotating && !ismoving && !isRotating){
            m_delphox.SetActive(false);
        }
    }
    public void SetTriggerDie(){
        //Debug.Log("Delphox is triggered.");
        // m_delphox.SetActive(true);
        if (m_delphox != null)
        {
            m_delphox.SetActive(true);
        }
        ismoving = true;
    }

    public void SetInactive()
    {
        if (m_delphox != null)
        {
            // m_delphox.SetActive(false);
        }
    }

    // Some part of the body doesn't fade.



    // private void SetAlpha(float alpha)
    // {
    //     foreach (Renderer renderer in childRenderers)
    //     {
    //         Color color = renderer.material.color;
    //         color.a = alpha;
    //         renderer.material.color = color;
    //     }
    // }

    // private void FadeIn()
    // {
    //     StopFadeCoroutine();
    //     fadeCoroutine = StartCoroutine(FadeToAlpha(1f));
    // }

    // private void FadeOut()
    // {
    //     StopFadeCoroutine();
    //     fadeCoroutine = StartCoroutine(FadeToAlpha(0f));
    // }

    // private void StopFadeCoroutine()
    // {
    //     if (fadeCoroutine != null)
    //     {
    //         StopCoroutine(fadeCoroutine);
    //     }
    // }

    // private IEnumerator FadeToAlpha(float targetAlpha)
    // {
    //     float elapsedTime = 0f;
    //     float startAlpha = childRenderers[0].material.color.a;

    //     while (elapsedTime < fadeDuration)
    //     {
    //         float t = elapsedTime / fadeDuration;
    //         float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
    //         SetAlpha(currentAlpha);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     SetAlpha(targetAlpha);
    // }
    
}
