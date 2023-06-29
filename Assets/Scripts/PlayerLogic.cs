using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    const float GRAVITY = -1.0f;
    float MOVEMENT_SPEED = 5.0f;
    const float JUMPFORCE = 5.0f;
    
    //moving
    float m_horizontalInput;
    float m_verticalInput;
    //jumping
    int m_isJumping;
    int m_falling;
    int changing_m_isjumping;//first jump height
    bool secondJump_Lock;

    //knocking
    float knockbackForce = 200.0f;
    int is_Knocked = 0;

    int floor_num = 0;

    bool controller_enable = true;


    CharacterController m_characterController;
    GameObject m_player;

    Vector3 m_movement;
    Vector3 knock_direction;
    Vector3 initial_rotation;
    [SerializeField]
    GameObject rocket1;
    Animator m_animator;


    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_player = GameObject.FindWithTag("Player");
        changing_m_isjumping = 4;
        initial_rotation = transform.rotation.eulerAngles;


        // Vector3 pos = new Vector3(-8.1f,3.4f,601f);
        // Instantiate(rocket1, pos, Quaternion.identity);
        // Vector3 pos1 = new Vector3(106.5f,3.4f,601f);
        // Instantiate(rocket1, pos1, Quaternion.identity);
        // Vector3 pos2 = new Vector3(-111.4f,3.4f,601f);
        // Instantiate(rocket1, pos2, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if(is_Knocked != 0)
        {
            return;
        }
        //stick to object
        if(controller_enable == false && (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Z")) )
        {
            controller_enable = true;
            m_characterController.enabled = true;
            m_player.transform.parent = null;
            transform.rotation = Quaternion.Euler(initial_rotation);
        }
        else if(controller_enable == true && Input.GetKeyDown(KeyCode.Z))
        {
            controller_enable = false;
            //setparent code in hit
        }

        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        
        m_animator.SetFloat("Horizontal", m_horizontalInput);
        m_animator.SetFloat("Vertical", m_verticalInput);

        if((Input.GetKeyDown(KeyCode.Space)  || Input.GetButtonDown("Jump") ) && m_characterController.isGrounded){
            m_isJumping += changing_m_isjumping;
            Debug.Log("m_isJumping");
            secondJump_Lock = false;
        }else
        { //on the ground
            m_movement.y =+ GRAVITY * Time.deltaTime;   
        }
        // judge second jump
        if ((Input.GetKeyDown(KeyCode.Space)  || Input.GetButtonDown("Jump") )&& !m_characterController.isGrounded && !secondJump_Lock)
        {
            Debug.Log("Second Jump!!!"); 
            m_isJumping += changing_m_isjumping - 1;
            secondJump_Lock = true;
        }
    }

    void FixedUpdate()
    {
        if(is_Knocked != 0){
            //Debug.Log("knocked");
            m_movement = knock_direction * knockbackForce * Time.deltaTime;
            is_Knocked--;
        }
        else{
            m_movement.x = m_horizontalInput * MOVEMENT_SPEED * Time.deltaTime;
            m_movement.z = m_verticalInput * MOVEMENT_SPEED * Time.deltaTime;
        }

        //jumping logic starts here
        if (m_isJumping != 0)
        {
            m_movement.y += JUMPFORCE * m_isJumping * Time.deltaTime;
            m_isJumping--;

            if(m_isJumping == 1)
            {
                m_falling = 99;
            }
        }
        else{
            // falling
            if (m_falling != 0 && !m_characterController.isGrounded)
            {
                m_movement.y += GRAVITY * Time.deltaTime * (100 - m_falling) / 2;
                m_falling--;
            }
            else // On ground
            {
                m_falling = 100;
                m_movement.y += GRAVITY * Time.deltaTime;
            }
        }
        //jumping logic ends here
        m_characterController.Move(m_movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("Character collided with " + hit.gameObject.name);
        if(hit.gameObject.CompareTag("followObj") && m_characterController.isGrounded)
        {
            if(controller_enable == false){
                m_player.transform.parent = hit.transform;
                m_characterController.enabled = false;
            }
        }
        else if(hit.gameObject.CompareTag("ShakingPendulum"))
        {
            is_Knocked = 5;
            knock_direction = transform.position - hit.point;
            knock_direction.y = 0;
            //Debug.Log("knocked_direction: " + knock_direction.x+" "+knock_direction.y+" "+knock_direction.z);
        }
        
        //slowerFloor
        if(hit.gameObject.CompareTag("SlowerFloor") && m_characterController.isGrounded){
            //Debug.Log("SlowerFloor");
            MOVEMENT_SPEED = 3.0f;
        }
        //Floor and save
        if(hit.gameObject.CompareTag("Floor1") && floor_num<1 && m_characterController.isGrounded){
            Save();
            floor_num++;
            Debug.Log("floor_num: "+floor_num);
        }
        else if(hit.gameObject.CompareTag("Floor2") && floor_num<2 && m_characterController.isGrounded){
            Save();
            floor_num++;
            Debug.Log("floor_num: "+floor_num);
        }
        else if(hit.gameObject.CompareTag("Floor3") && floor_num<3 && m_characterController.isGrounded){
            Save();
            floor_num++;
            Debug.Log("floor_num: "+floor_num);
        }
        else if(hit.gameObject.CompareTag("Floor4") && floor_num<4 && m_characterController.isGrounded){
            Save();
            floor_num++;
            Debug.Log("floor_num: "+floor_num);
        }
        else if(hit.gameObject.CompareTag("Floor5") && floor_num<5 && m_characterController.isGrounded){
            Save();
            floor_num++;
            Debug.Log("floor_num: "+floor_num);
        }
        else if(hit.gameObject.CompareTag("Floor6") && floor_num<6 && m_characterController.isGrounded){
            Save();
            floor_num++;
            Debug.Log("floor_num: "+floor_num);
            MOVEMENT_SPEED = 5.0f;
            changing_m_isjumping = 4;
        }
        else if(hit.gameObject.CompareTag("Floor7") && floor_num<7 && m_characterController.isGrounded){
            Save();
            floor_num++;
            Debug.Log("floor_num: "+floor_num);
            MOVEMENT_SPEED = 5.0f;
            changing_m_isjumping = 4;
        }
        else if(hit.gameObject.CompareTag("Floor8") && m_characterController.isGrounded){
            Save();
            floor_num++;
            Debug.Log("floor_num: "+floor_num);
            MOVEMENT_SPEED = 5.0f;
            changing_m_isjumping = 4;

            Vector3 pos = new Vector3(-8.1f,3.4f,601f);
            Instantiate(rocket1, pos, Quaternion.identity);
            Vector3 pos1 = new Vector3(106.5f,3.4f,601f);
            Instantiate(rocket1, pos1, Quaternion.identity);
            Vector3 pos2 = new Vector3(-111.4f,3.4f,601f);
            Instantiate(rocket1, pos2, Quaternion.identity);
            //End UI
            m_characterController.enabled = false;
            SaveManager.Instance.EndGame();
        }
                
        if(hit.gameObject.CompareTag("TipBox")){
            // UI tips for QE camera button
            SaveManager.Instance.ShowTipImage();
        }
        else if(hit.gameObject.CompareTag("TipBox2")){
            // UI tips for Z button
            SaveManager.Instance.ShowTipImage2();
        }

    }
    
    public void Save()
    {
        // Position
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);

        // Rotation
        PlayerPrefs.SetFloat("PlayerRotX", transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("PlayerRotY", transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("PlayerRotZ", transform.rotation.eulerAngles.z);
    }

    public void Load()
    {
        // Position
        float posX = PlayerPrefs.GetFloat("PlayerPosX");
        float posY = PlayerPrefs.GetFloat("PlayerPosY");
        float posZ = PlayerPrefs.GetFloat("PlayerPosZ");

        // Rotation
        float rotX = PlayerPrefs.GetFloat("PlayerRotX");
        float rotY = PlayerPrefs.GetFloat("PlayerRotY");
        float rotZ = PlayerPrefs.GetFloat("PlayerRotZ");

        MOVEMENT_SPEED = 5.0f;

        // Set Position
        m_characterController.enabled = false;
        transform.position = new Vector3(posX, posY, posZ);
        m_characterController.enabled = true;

        // Set Rotation
        transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

        m_characterController.enabled = false;
        controller_enable = false;
    }


    public void Die(){
        SaveManager.Instance.Load();
    }
    public void CanMove(){
        m_characterController.enabled = true;
        controller_enable = true;
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("End");
        //End UI
    }

}
