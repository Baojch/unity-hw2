using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField]
    Delphox m_delphox;

    [SerializeField]
    TextMeshProUGUI m_coinText;

    [SerializeField]
    TextMeshProUGUI m_TimeText;

    [SerializeField]
    RawImage m_TipImage;

    [SerializeField]
    RawImage m_TipImage2;

    [SerializeField]
    RawImage m_scoreImage;

    [SerializeField]
    TextMeshProUGUI m_score;

    PlayerLogic m_playerLogic;

    int coinCount = 0;

    bool isend = false;

    public AudioClip[] musicClips;
    private AudioSource audioSource;
    private int currentClipIndex = 0;

    float Last_time;
    int Final_score;
    int times;

    bool addnum;


    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        m_playerLogic = FindObjectOfType<PlayerLogic>();
        m_TipImage.gameObject.SetActive(false);
        m_TipImage2.gameObject.SetActive(false);
        m_scoreImage.gameObject.SetActive(false);

        audioSource = GetComponentInChildren<AudioSource>();
        StartCoroutine(PlayNextClip());
        times = 0;
        addnum = false;
    }

    IEnumerator PlayNextClip()
    {
        while (!isend){
            if (audioSource.isPlaying)
            {
                yield return new WaitForSeconds(1.0f);
            }
            else{
                audioSource.clip = musicClips[currentClipIndex];
                audioSource.Play();
                yield return new WaitForSeconds(0.1f);
                currentClipIndex++;

                if (currentClipIndex >= musicClips.Length)
                {
                    currentClipIndex = 0;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isend){
            m_coinText.text = "Coins: "+ coinCount.ToString();
            m_TimeText.text = "Time: " + Time.time.ToString("F2");
            Last_time = Time.time;
        }
        if(isend && addnum){
            if(times >= 200){
                m_score.text = Final_score.ToString();
            }else{
                times++;
                m_score.text = ((int)(Final_score/200*times)).ToString();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);
        minusCoin();
        m_delphox.SetInactive();
        m_playerLogic.CanMove();
    }
    public void Load()
    {
        if(m_delphox == null){
            Debug.Log("Delphox is null");
        }
        m_delphox.SetTriggerDie();
        m_playerLogic.Load();
        StartCoroutine(Wait());
        
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
    
    public void minusCoin()
    {
        if(coinCount >= 1)
        {
            coinCount--;
        }
    }
    public void ShowTipImage(){
        StartCoroutine(ShowTip());
    }
    IEnumerator ShowTip(){
        Debug.Log("Show Tip");
        m_TipImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        m_TipImage.gameObject.SetActive(false);
    }
    public void ShowTipImage2(){
        StartCoroutine(ShowTip2());
    }
    IEnumerator ShowTip2(){
        Debug.Log("Show Tip");
        m_TipImage2.gameObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        m_TipImage2.gameObject.SetActive(false);
    }

    public void EndGame(){
        isend = true;
        StartCoroutine(EndWait());
    }
    IEnumerator EndWait()
    {
        yield return new WaitForSeconds(5.0f);
        audioSource.Stop();
        //EndUI
        m_scoreImage.gameObject.SetActive(true);
        Final_score = coinCount * 100 + (int)(10000 - Last_time * 10);
        m_score.gameObject.SetActive(true);
        addnum = true;
    }
    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
