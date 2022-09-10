using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    private float healthScore = 100;

    // Start is called before the first frame update
    void Start()
    {
        SetHealth(healthScore);
    }

    private void SetHealth(float score)
    {
        healthSlider.value = score;
    }

    public void Damage(float amount)
    {
        healthScore -= amount;
        if(healthScore <= 0)
        {
            OnDeath();
            healthScore = 0;
        }
    }

    private void OnDeath()
    {
        Debug.Log("This tank is over.");
    }
}
