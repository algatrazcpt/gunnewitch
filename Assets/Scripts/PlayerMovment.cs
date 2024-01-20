using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class PlayerMovment : MonoBehaviour
{
    public PopController popController;

    public CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin noise;

    public Image healthBar;
    public Image speelBar;
    public float speelCount;
    float currentSpeelCount;

    public float moveSpeed;
    Rigidbody2D rb;
    Vector2 moveDir;
    public Transform weapon;
    public float offset;
    public SpriteRenderer sp;
    public float Health;
    float currentHealt;
    public bool faceRight = true;

    public GameObject customUi;

    public int InteractId = -1;
    public GameObject currentInteractItem;


    private void Awake()
    {
        currentHealt = Health;
        currentSpeelCount = 0;
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        HealthUi();
        SpeelUi();
    }



    // Start is called before the first frame update
    void Start()
    {
        popController = PopController.instance;

        rb = GetComponent<Rigidbody2D>();
        sp=GetComponent<SpriteRenderer>();
    }

    void HealthUi()
    {
        healthBar.fillAmount = currentHealt / Health;
        
    }
    void SpeelUi()
    {
        speelBar.fillAmount = currentSpeelCount / speelCount;
    }



    public void TakeDamage(float damage)
    {
        currentHealt = Mathf.Clamp(currentHealt-damage,0,Health);
        DamagePop currentPop = popController.GetObjectFromPool().GetComponent<DamagePop>();
        if (damage <= 0)
        {
            currentPop.DamageCreate(transform.position, damage*-1);
            currentPop.DamageColor(Color.green);
        }
        else
        {
            currentPop.DamageCreate(transform.position, damage );
            currentPop.DamageColor(Color.red);
        }

        Shake(1,0.1f);
        HealthUi();

    }
    // Update is called once per frame
    void Update()
    {
        InputManager();
        KeyManager();
        weaponRotate();
    }
    private void FixedUpdate()
    {
        move();
    }

    void InputManager()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;


    }

    void KeyManager()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            InteractItem(InteractId);
        }
    }



    public void TakeSpeelDamage(float damage)
    {
        currentSpeelCount = Mathf.Clamp(currentSpeelCount - damage, 0, speelCount);
        DamagePop currentPop = popController.GetObjectFromPool().GetComponent<DamagePop>();
        if (damage <= 0)
        {
            currentPop.DamageCreate(transform.position, damage * -1);
            currentPop.DamageColor(Color.blue);
        }
        SpeelUi();
    }
    void move()
    {

        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);

        if (moveDir.x > 0 && !faceRight)
        {
            
            filipWeapon();
            filipWitch();
            //sp.flipX = true;



        }
       else if (moveDir.x < 0 && faceRight)
        {
           filipWeapon();
           filipWitch();
           


        }
    }

    void filipWitch()
    {
        Vector3 scale = gameObject.transform.localScale;

        scale.x *= -1;
        gameObject.transform.localScale = scale;
        faceRight = !faceRight;



    }
    void filipWeapon()
    {
        Vector3 scale = weapon.transform.localScale;

        scale.x *= -1;
        weapon.transform.localScale = scale;

    }
  
    void weaponRotate()
    {

        //Vector3 scaleWeapon = weapon.transform.localScale;
        Vector3 displayWeapon = weapon.position - ( Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float angles = Mathf.Atan2(displayWeapon.y, displayWeapon.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0f, 0f, angles+ offset);
        Vector3 localScales = weapon.transform.localScale;
        if (faceRight ==false)
        {
            
            localScales.x = -1f;
        }
        else if (faceRight == true)
        {
            localScales.x = 1f;

        }
        //weapon.eulerAngles = new Vector3(0, 0, angles);

        
        //Debug.Log(localScales);
        if(angles>90 || angles < -90)
        {
            localScales.y = 1f;
            


        }
        else
        {
            localScales.y = -1f;
            


        }
        weapon.transform.localScale = localScales;

    }


    public void Shake(float intensity, float duration)
    {
        StartCoroutine(ShakeCoroutine(intensity, duration));
    }

    private IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        noise.m_AmplitudeGain = intensity;

        yield return new WaitForSeconds(duration);

        noise.m_AmplitudeGain = 0f;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Random"))
        {
            customUi.SetActive(true);
            currentInteractItem = collision.gameObject;
            InteractId = collision.gameObject.GetComponent<KazanController>().GetRandomItem();
        }
        else if (collision.transform.CompareTag("Consumable"))
        {
            Debug.Log("Ýtem Consumbale");
            currentInteractItem = collision.gameObject;
            InteractId = 3;
            customUi.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentInteractItem = null;
        InteractId = -1;
        customUi.SetActive(false);
    }
    void InteractItem(int id)
    {
        if(id!=-1 &&id<3)
        {
                currentInteractItem.GetComponent<KazanController>().PlayerKazanDestroy();
                GetComponent<RandGun>().setWeapon(id);
                InteractId = -1;
                currentInteractItem = null;
        }
        else if (currentInteractItem!=null)
        {
            if (currentInteractItem.transform.CompareTag("Random"))
            {
                currentInteractItem.GetComponent<KazanController>().PlayerKazanDestroy();
                InteractId = -1;
                currentInteractItem = null;
            }
            else
            {
                float cHealth= currentInteractItem.GetComponent<ItemControl>().healt;
                float cSpeel= currentInteractItem.GetComponent<ItemControl>().speel;
                if(cHealth>0)
                {
                    TakeDamage(-cHealth);
                }
                if(cSpeel>0)
                {
                    TakeSpeelDamage(-cSpeel);
                }
                currentInteractItem.GetComponent<ItemControl>().Delete();
                InteractId = -1;
                currentInteractItem = null;
            }
        }

    }

}
