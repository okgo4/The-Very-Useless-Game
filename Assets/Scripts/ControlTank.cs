using UnityEngine;
using UnityEngine.UI;

public class ControlTank : MonoBehaviour
{

    public GameObject wpManager;
    UnityEngine.AI.NavMeshAgent agent;
    public float moveSpeed;
    public float rotationSpeed;
    public float damage;
    public float health;

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

    public AudioClip tankMove;

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
        }
        // Acceleration Buff Counting
        if (accCount > 0)
        {
            moveSpeed = moveSpeedAcc;
            accCount -= Time.deltaTime;
        }
        else
        {
            moveSpeed = moveSpeedOri;
            accCount = 0;
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
        }

        float rh = Input.GetAxis("Horizontal");
        float rv = Input.GetAxis("Vertical");
        if (rh !=null || rv != null)
        {
            AudioSource.PlayClipAtPoint(tankMove, transform.position);
        }
        transform.Translate(new Vector3(0, 0, rv) * Time.deltaTime * moveSpeed);
        transform.Rotate(new Vector3(0, rh, 0) * Time.deltaTime * rotationSpeed);
        
    }


    // Buff System
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Accelerate":
                accCount=20;
                break;

            case "DamageUp":
                dmgCount=20;
                break;
            
            case "Iron":
                ironCount=20;
                break;
            
            case "Healing":
                health = health + 50;
                break;
        }
    }

    private void OnDeath()
    {
        Debug.Log("This tank is over.");
    }
}