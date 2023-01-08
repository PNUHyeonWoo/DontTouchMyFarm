using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StructInterface
{
    private static GameObject selectStruct;

    public static GameObject SelectStruct
    {
        get { return selectStruct; }
        set { selectStruct = value; }
    }

    public long Cost 
    {
        get;
    }

    public int Size
    {
        get;
    }

    public Vector2 InstallGrid 
    {
        get;
        set;
    }

    public StructInterface GetStructInterface() 
    {
        return selectStruct.GetComponent<StructInterface>();
    }

    public abstract void CreateStruct(Transform parent, Vector3 position, Vector2 installGrid);
    public abstract GameObject GetGameObject();

}
