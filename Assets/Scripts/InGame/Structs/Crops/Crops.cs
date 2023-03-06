using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : StructObject
{
    [SerializeField]
    private float maxGrowth;
    private float growth;
    [SerializeField]
    private float[] seasonGrowth = new float[4];
    [SerializeField]
    private long saleCost;

    GameObject crop0, crop1, crop2;

    public float MaxGrowth 
    { 
        get { return maxGrowth; }
    }

    public float[] SeasonGrowth
    {
        get { return seasonGrowth; }
    }

    public long SaleCost 
    {
        get { return saleCost; }
    }
    public override StructType GetStructType()
    {
        return StructType.Crops;
    }

    public override void UpdateDay(int day)
    {
        base.UpdateDay(day);
        growth += seasonGrowth[Day.day.Season];
        if (growth >= maxGrowth)
        {
            UISound.uiSound.PlaySound(1);
            PlusMoney(saleCost);
            Dead();
        }
        Grow(growth);
    }

    void Grow(float growth)
    {
        float lvl1 = maxGrowth / 3;
        float lvl2 = lvl1 * 2;
        
        if (0 <= growth && growth < lvl1)
        {
            crop0.SetActive(true);
            crop1.SetActive(false);
            crop2.SetActive(false);
        }
        else if (growth < lvl2)
        {
            crop0.SetActive(false);
            crop1.SetActive(true);
        }
        else
        {
            crop1.SetActive(false);
            crop2.SetActive(true);
        }
    }

    new void Start()
    {
        base.Start();
        growth = 0;
        crop0 = transform.GetChild(1).gameObject;
        crop1 = transform.GetChild(2).gameObject;
        crop2 = transform.GetChild(3).gameObject;
    }
}
