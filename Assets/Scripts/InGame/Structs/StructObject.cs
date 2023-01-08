using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructObject : MonoBehaviour, StructInterface
{
    private long cost;
    private int size;
    private Vector2 installGrid;
    public long Cost
    {
        get
        {
            return cost;
        }
    }

    public int Size
    {
        get 
        { 
            return size; 
        }
    }

    public Vector2 InstallGrid
    {
        get 
        {
            return installGrid;
        }
        set 
        {
            installGrid = value;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void CreateStruct(Transform parent, Vector3 position, Vector2 installGrid) 
    {
        GameObject newObj = Instantiate(gameObject);
        newObj.transform.position = position;
        newObj.transform.SetParent(parent);
        newObj.GetComponent<StructObject>().InstallGrid = installGrid;
    }
    public GameObject GetGameObject() 
    {
        return gameObject;
    }
}
