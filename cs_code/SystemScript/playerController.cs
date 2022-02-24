using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public GameObject animation;
    public GameObject model;
    public GameObject player;
    public GameObject battle;


    public AudioClip bgaudioClip;

    

    private CharacterController controller;
    private Vector3 dir;
    private Vector3 startPoint;
    private Vector3 nowPoint;
    private float moveVelocity;
    float time = 0;

    private float gravity=9.8f;
    private Vector3 velocity;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    public bool isGround;

    public GameObject bag;
    bool isOpen;

    void Start()
    {
        SavePos(-16.42f, 0.3800001f, -26.97f);
        startPoint = transform.position;
        controller = GetComponent<CharacterController>();
        time = 0;
        moveVelocity = 5;
        isOpen = false;

        
    }
    void OnTriggerEnter(Collider col)
    {
    if (col.tag == "Home")
    {
            SavePos(-16.42f, 0.3800001f, -26.97f);
            transform.localPosition = new Vector3(-100.67f, -1.28f, -44.45f);
        }

    if (col.tag == "PC")
    {
            
            SavePos(54.93f, 0.380000f, 136.64f);
            transform.localPosition = new Vector3(-202.73f, -4.56f, -34.73f);
        }

    if (col.tag == "Store")
    {
            SavePos(-13.45f, 0.380000f, 20.49f);
            transform.localPosition = new Vector3(-153.29f, 0.35f, -45.43f);
        }
        if (col.tag == "Main")
        {
            LoadPos();
        }
    }


    void SavePos(float posX,float posY,float posZ)
    {
    PlayerPrefs.SetFloat("PosX", posX);
    PlayerPrefs.SetFloat("PosY", posY);
    PlayerPrefs.SetFloat("PosZ", posZ);
    }
    //读取主角的位置
    void LoadPos()
    {
    float x = PlayerPrefs.GetFloat("PosX");
    float y = PlayerPrefs.GetFloat("PosY");
    float z = PlayerPrefs.GetFloat("PosZ");
    transform.localPosition = new Vector3(x,y,z);
    }

    // Update is called once per frame
    void Update()
    {
        OpneBag();
        float ad = moveVelocity * Time.deltaTime * Input.GetAxis("Horizontal");//x����������d�ƶ�����
        float ws = moveVelocity * Time.deltaTime * Input.GetAxis("Vertical");//z����������w�ƶ�����
        time += Time.deltaTime*3;
        dir = transform.forward * ws + transform.right * ad;

        Physics.autoSyncTransforms = true;
        controller.Move(dir);

        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);
        if (isGround && velocity.y <= 0)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }


        //��������
        if (ad != 0 || ws != 0)
        {
            int x = animation.GetComponent<Animator>().GetInteger("state");      
           // float y = model.transform.rotation.y;
            Quaternion q;
            if (ws > 0 && ad > 0)
            {
                q = Quaternion.Euler(0, 45, 0);
            }
            else if (ws > 0 && ad < 0)
            {
                q = Quaternion.Euler(0, 315, 0);
            }
            else if (ws < 0 && ad > 0)
            {
                q = Quaternion.Euler(0, 135, 0);
            }
            else if (ws < 0 && ad < 0)
            {
                q = Quaternion.Euler(0, 225, 0);
            }
            else if (ws > 0)
            {              
               q=Quaternion.Euler(0,0,0);
            }
            else if (ws<0)
            {
                q = Quaternion.Euler(0, 180, 0);
            }
            else if (ad > 0)
            {
                q = Quaternion.Euler(0, 90, 0);
            }
            else if (ad < 0)
            {
                q = Quaternion.Euler(0, 270, 0);
            }
            else
            {
                q= Quaternion.Euler(0, 270, 0);
            }
           // float y1 = q.y;          
        //    if (Mathf.Abs(y - y1) == 1)
       //     {
        //        Debug.Log(y);
        //        x = 2;
        //        animation.GetComponent<Animator>().SetInteger("state", x);
        //    }
        model.transform.rotation = q;
            if (Input.GetKey(KeyCode.LeftShift)) {
                moveVelocity = 10;
                x = 2;
            }
            else
            {
                moveVelocity = 5;
                x = 1;
            }
               
        animation.GetComponent<Animator>().SetInteger("state", x);
        }
        else
        {
            animation.GetComponent<Animator>().SetInteger("state", 0);
        }

    }

    void OpneBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (bag.activeSelf == true)
            {
                bag.SetActive(false);
            }
            else
            {
                bag.SetActive(true);
            }
        }
    }


}
