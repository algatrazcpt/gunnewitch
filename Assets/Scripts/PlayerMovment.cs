using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class PlayerMovment : MonoBehaviour
{
    public PopController popController;

    public bool yurumeKontrol = false;

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
    public asaAttack asa;
    public int InteractId = -1;
    public GameObject currentInteractItem;

    public Image healtMaxBarImage;
    float currentMaxHealtBarCount=0;
    public Animator anims;

    bool rageAnim = true;


    public void MaxHealtBarEffect(float exp)
    {
        Debug.Log(exp);
        currentMaxHealtBarCount+= exp;
        healtMaxBarImage.fillAmount=Mathf.Clamp(currentMaxHealtBarCount / LevelBalance.Instance.playerMaxHealthBar, 0, 1);
      //  Heal

        if(currentMaxHealtBarCount>=LevelBalance.Instance.playerMaxHealthBar)
        {
            currentMaxHealtBarCount = 0;
            Health += LevelBalance.Instance.playerHealtAdd;
            LevelBalance.Instance.PlayerHealtLevelUp();
        }

        healtImagCaseMat.SetFloat("_FlashCount", healtMaxBarImage.fillAmount * 5);
        //healtMaxBarImage.fillAmount
    }




    RangePower rangeSaldýr;

    public GameObject Rangeobj;

    [Range(1, 10)]
    public float healtEffectSpeed;
    [Range(10, 1)]
    public float healtEffectSpeedTime;
    [Range(1, 10)]
    public float speelEffectSpeed;
    [Range(10, 1)]
    public float speelEffectSpeedTime;


    public Material healtBarCaseMat;
    public Material speelBarCaseMat;
    public Material healtImagCaseMat;

    private void Awake()
    {
        currentHealt = 20;
        currentSpeelCount = 0;
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        HealthUi();
        SpeelUi();
        
    }


    IEnumerator speel;
    // Start is called before the first frame update
    void Start()
    {
        popController = PopController.instance;

        rb = GetComponent<Rigidbody2D>();
        sp=GetComponent<SpriteRenderer>();
        anims = GetComponent<Animator>();
        rangeSaldýr = GetComponent<RangePower>();

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
        StartCoroutine("HealthShow");
        if (currentHealt-damage>0)
        {
            if (defHealthTargetValue !=0)
            {
                StartCoroutine("HealthEffect");
            }
            else
            {
                currentHealt = Mathf.Clamp(currentHealt + damage, 0, Health);
                HealthUi();
            }
        }
        else
        {
            currentHealt = Mathf.Clamp(currentHealt + damage, 0, Health);
            HealthUi();
            //
            Debug.Log("GameOver");
        }
        
        
        DamagePop currentPop = popController.GetObjectFromPool().GetComponent<DamagePop>();
        if (damage > 0)
        {
            currentPop.DamageCreate(transform.position, damage);
            currentPop.DamageColor(Color.green);
        }
        else
        {
            currentPop.DamageCreate(transform.position, damage*-1 );
            currentPop.DamageColor(Color.red);
        }

        Shake(1,0.1f);
        HealthUi();
    }
    float defHealthTargetValue = 0;

    IEnumerator HealthEffect()
    {
        float vValue = (healtEffectSpeed / Health) * 100;
        if (defHealthTargetValue > 0)
        {
            while (defHealthTargetValue != 0)
            {
                defHealthTargetValue -= vValue;
                currentHealt = Mathf.Clamp(currentHealt + vValue, 0, Health);
                if (defSpelTargetValue < 0)
                {
                    defHealthTargetValue = 0;
                    StopCoroutine("HealthEffect");
                    break;
                }
                HealthUi();
                yield return new WaitForSeconds(healtEffectSpeedTime/100);
            }
        }
        else if (defHealthTargetValue < 0)
        {
            while (defHealthTargetValue != 0)
            {
                defHealthTargetValue += vValue;
                currentHealt = Mathf.Clamp(currentHealt - vValue, 0, Health);
                if (defHealthTargetValue > 0)
                {
                    defHealthTargetValue = 0;
                    StopCoroutine("HealthEffect");
                    break;
                }
                HealthUi();
                yield return new WaitForSeconds(healtEffectSpeedTime/100);
            }
        }
    }

   IEnumerator SpeelShow()
    {
        speelBarCaseMat.SetFloat("_FlashCount", 5f);
        yield return new WaitForSeconds(2f);
        speelBarCaseMat.SetFloat("_FlashCount", 0f);
    }
    IEnumerator HealthShow()
    {
        healtBarCaseMat.SetFloat("_FlashCount", 7f);
        yield return new WaitForSeconds(2f);
        healtBarCaseMat.SetFloat("_FlashCount", 0f);
    }



    //IEnumerator SpeelEffect
    IEnumerator SpeelEffect()
    {
        float vValue = (speelEffectSpeed / speelCount) * 100;
        //defSpelTargetValue;
        if (defSpelTargetValue > 0)
        {
            while (defSpelTargetValue != 0)
            {
                defSpelTargetValue -= vValue;
                currentSpeelCount = Mathf.Clamp(currentSpeelCount + vValue, 0, speelCount);
                if (defSpelTargetValue<0)
                {
                    defSpelTargetValue = 0;
                    StopCoroutine("SpeelEffect");
                    break;
                }
                SpeelUi();
                yield return new WaitForSeconds(speelEffectSpeedTime/ 100);
            }
        }
        else if (defSpelTargetValue < 0)
        {
            while (defSpelTargetValue != 0)
            {
                defSpelTargetValue += vValue;
                currentSpeelCount = Mathf.Clamp(currentSpeelCount - vValue, 0, speelCount);
                if (defSpelTargetValue > 0)
                {
                    defSpelTargetValue = 0;
                    StopCoroutine("SpeelEffect");
                    break;
                }
                SpeelUi();
                yield return new WaitForSeconds(speelEffectSpeedTime/ 100);
            }
        }
    }

    void SpeelEffect(bool effectState)
    {
        if (effectState)
        {
            speelBarCaseMat.SetFloat("_FlashCount", 5);
        }
        else
        {
            speelBarCaseMat.SetFloat("_FlashCount", 0);
        }
    }



    IEnumerator WitchAnim()
    {
        rageAnim = false;
        weapon.GetComponent<WeaponList>().currentWeapon.SetActive(false);
        yield return new WaitForSeconds(1f);
        weapon.GetComponent<WeaponList>().currentWeapon.SetActive(true);
        rageAnim = true;
    }

    IEnumerator RangeSalsýrýtime()
    {
        rageAnim = false;
        rangeSaldýr.range = 5f;
        rangeSaldýr.alaniçiObjebul();
        
        foreach(GameObject enmy in rangeSaldýr.alanaGirenler)
        {
            enmy.GetComponent<EnemyAttack>().attackMoveSpeed = 1;
        }
        yield return new WaitForSeconds(5f);
        rangeSaldýr.range = 0f;
        foreach (GameObject enmy in rangeSaldýr.alanaGirenler)
        {
            enmy.GetComponent<EnemyAttack>().attackMoveSpeed = 6;
        }
        rangeSaldýr.alantemizle();
        rageAnim = true;


    }
    float defSpelTargetValue = 0;
    // Update is called once per frame
    void Update()
    {
        InputManager();
        KeyManager();
        weaponRotate();

        if (yurumeKontrol == true)
        {
            anims.SetBool("yuruyor", true);
        }
        else if (yurumeKontrol == false)
        {
            anims.SetBool("yuruyor", false);
        }


        if (Input.GetKeyDown(KeyCode.Space) && rageAnim)
        {
            if(weapon.GetComponent<WeaponList>().currentWeapon.name== "asa_yilanli1_")
            {
                anims.SetTrigger("range");
                StartCoroutine("WitchAnim");
                //StartCoroutine("RangeSalsýrýtime");
                Rangeobj.SetActive(true);
                Rangeobj.GetComponent<RangeField>().OzelAktifmi = true;
                asa.canAttack = true;
            }

            else if(weapon.GetComponent<WeaponList>().currentWeapon.name == "lava_gun4")
            {
                anims.SetTrigger("range");
                StartCoroutine("WitchAnim");
                weapon.GetComponent<WeaponList>().currentWeapon.GetComponent<FlameGun>().ozelkullan = true;


            }


        }
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

        if(moveX!=0.0f || moveY != 0.0f)
        {
            yurumeKontrol = true;
        }
        else
        {
            yurumeKontrol = false;
        }
    }

    void KeyManager()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            InteractItem(InteractId);
        }
    }
    void TakeSpeelDamageFast(float damage)
    {
        currentSpeelCount = Mathf.Clamp(currentSpeelCount + damage, 0, speelCount);
        SpeelUi();
    }


    public void TakeSpeelDamage(float damage)
    {
        StartCoroutine("SpeelShow");
        Debug.Log(damage);
        if(defSpelTargetValue!=0)
        {
            TakeSpeelDamageFast(defSpelTargetValue);
        }
        defSpelTargetValue = damage;
        StartCoroutine("SpeelEffect");

        //currentSpeelCount = Mathf.Clamp(currentSpeelCount - damage, 0, speelCount);
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

        if (moveDir.x < 0 && !faceRight)
        {
            
            //filipWeapon();
            //filipWitch();
            sp.flipX = true;
            faceRight = !faceRight;



        }
       else if (moveDir.x > 0 && faceRight)
        {
           //filipWeapon();
            //filipWitch();
            sp.flipX = false;
            faceRight = !faceRight;



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


         // Eski kod yapýsý
        //if (faceRight ==false)
        //{

        //    localScales.x = -1f;
        //}
        //else if (faceRight == true)
        //{
        //    localScales.x = 1f;

        //}
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
                    TakeDamage(cHealth);
                }
                if(cSpeel>0)
                {
                    TakeSpeelDamage(cSpeel);
                }
                currentInteractItem.GetComponent<ItemControl>().Delete();
                InteractId = -1;
                currentInteractItem = null;
            }
        }

    }

}
