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
        growth += seasonGrowth[(int)Day.GetSeason(day)];
        if (growth >= maxGrowth)
        {
            PlusMoney(saleCost);
            Dead();
        }
    }

    new void Start()
    {
        base.Start();
        growth = 0;
    }
}
