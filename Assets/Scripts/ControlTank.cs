using UnityEngine;
using UnityEngine.UI;

public class ControlTank : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent agent;
    public float moveSpeed;
    public float rotationSpeed;
    public float damage;
    public float health;
    public AudioSource endGame;
    private bool dead = false;
    
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private float moveSpeedOri;
    [SerializeField]
    private float moveSpeedAcc;
    [SerializeField]
    private float dmgOri;
    [SerializeField]
    private float dmgUp;

    public float accCount;
    public float dmgCount;
    public float ironCount;
    public float timeCount;

    public AudioClip tankMove;
    public AudioClip originalBgm;
    public AudioClip ironBgm;
    public AudioClip endBgm;
    public AudioClip buffBgm;
    public AudioClip healBgm;
    public AudioSource backgroundMusic;
    public AudioSource tankState;
    public AudioSource buffSE;
    private float accPitch = 1.3f;

    public Material ironSkyColor;
    public Material normalSkyColor;


    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        health = 100;
    }

    void Update()
    {   
        // Update health to slider
        healthSlider.value = health;
        if (health <= 0)
        {
            //Destroy(gameObject);
            GameObject.Find("MainControl").GetComponent<MainControl>().endGame();
            backgroundMusic.Stop();
            tankState.Stop();
            if(!dead)
            {
                endGame.Play();
                dead = true;
            }
            //tankState.clip = endBgm;
            //tankState.Play();
        }
        // Acceleration Buff Counting
        if (accCount > 0)
        {
            moveSpeed = moveSpeedAcc;
            accCount -= Time.deltaTime;
            backgroundMusic.pitch = accCount/20*(accPitch - 1)+1;
        }
        else
        {
            moveSpeed = moveSpeedOri;
            accCount = 0;
            backgroundMusic.pitch = 1;
        }

        // DamageUp Buff Counting
        if (dmgCount > 0)
        {
            damage = dmgUp;
            dmgCount -= Time.deltaTime;
        }
        else
        {
            damage = dmgOri;
            dmgCount = 0;
        }

        // Iron Buff Counting
        if (ironCount > 0)
        {
            health = 100;
            ironCount -= Time.deltaTime;
        }
        else
        {
            ironCount = 0;
            if(backgroundMusic.clip != originalBgm){
                backgroundMusic.clip = originalBgm;
                backgroundMusic.Play();
            }
            RenderSettings.skybox = normalSkyColor;
        }
    
        float rh = Input.GetAxis("Horizontal");
        float rv = Input.GetAxis("Vertical");
        if (rh !=0 || rv != 0)
        {
            tankState.clip = tankMove;
            if (!tankState.isPlaying)
            {
                tankState.Play();
            }
           
            
        }
        else
        {
            tankState.Stop();
        }
        transform.Translate(new Vector3(0, 0, rv) * Time.deltaTime * moveSpeed);
        transform.Rotate(new Vector3(0, rh, 0) * Time.deltaTime * rotationSpeed);
        
    }


    // Buff System
    private void OnTriggerEnter(Collider other)
    {
        buffSE.clip = buffBgm;
        switch (other.tag)
        {
            case "Accelerate":
                accCount=20;
                backgroundMusic.pitch = accPitch;
                buffSE.Play();
                break;

            case "DamageUp":
                dmgCount=20;
                buffSE.Play();
                break;
            
            case "Iron":
                ironCount=20;
                backgroundMusic.clip = ironBgm;
                backgroundMusic.Play();
                RenderSettings.skybox = ironSkyColor;
                break;
            case "Time":
                GameObject.Find("MainControl").GetComponent<MainControl>().timeBuff(20);
                buffSE.Play();
                break;

            case "Healing":
                health = health + 50;
                buffSE.clip = healBgm;
                backgroundMusic.volume = 0.5f;
                buffSE.Play();
                backgroundMusic.volume = 1f;
                break;
        }
    }

    private void OnDeath()
    {
        Debug.Log("This tank is over.");
    }
}