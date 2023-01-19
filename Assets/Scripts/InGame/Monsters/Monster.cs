using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    private NightTimer timer;

    [SerializeField]
    private float maxHP;
    private float HP;

    public NightTimer Timer 
    {
        set { timer = value; }
    }
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.RemainTime <= 0)
            Dead();
    }

    public float AddHP(float value)
    {
        HP += value;
        if (HP <= 0)
            Dead();
        if(HP > maxHP)
            HP = maxHP;
        return HP;
    }

    virtual protected void Dead() 
    { 
        Destroy(gameObject);
    }
}
