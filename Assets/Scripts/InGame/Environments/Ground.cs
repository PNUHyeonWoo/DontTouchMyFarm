using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public static Ground ground = null;
    [SerializeField]
    private Vector2 groundSize; //���� ���� ���� xz����
    [SerializeField]
    private Vector2 numCells; // �׸��� xz�� ����

    [SerializeField]
    private GameObject house;

    private Vector2 groundStartPos; //������ xz�� ���� ���� ����
    private Vector2 halfCellSize; //�� ���� xzũ���� ����
    private bool[,] installTable; //���� ��ġ���� ��ġ�� �׸��� �� ���̺�

    private void Awake()
    {
        ground = this;
    }

    private void Start()
    {
        installTable = new bool[(int)numCells.x, (int)numCells.y];
        for(int i = 0; i < numCells.x;i++)
            for(int j = 0; j < numCells.y; j++)
                installTable[i,j] = false;

        groundStartPos = new Vector2(transform.position.x - groundSize.x / 2, transform.position.z - groundSize.y / 2);
        halfCellSize = new Vector2(groundSize.x / numCells.x, groundSize.y / numCells.y) / 2;

        //house��ġ
        StructObject.SelectStruct = house;
        InstallSelectStruct(new Vector2(0, 0));
        StructObject.SelectStruct = null;

    }



    public bool InstallSelectStruct(Vector2 rayPos)
    {
        int[] startIndex = IfCanInstallSelectStructGetStartIndex(rayPos);
        if (startIndex[0] != -1)
            return InstallSelectStruct(startIndex);
        else return false;
    }

    public void DestroyStruct(StructObject target) // ������ ������Ʈ �ı� �� installGrid ����
    {
        SetInstallGrid(false, target.InstallGrid, target.Size);
        Destroy(target.gameObject);
    }

    public Vector4 GetInstallFloorPosition(Vector2 rayPos) // xyz�� installFloor ������, w�� ��ġ �����ϸ� 1, �Ұ��� �� 0 
    {
        int structsize = StructObject.GetStructComponent().Size;

        Vector4 result = new Vector4();

        int[] startIndex = DoubleGridIndex2StartIndex(World2DoubleGridIndex(rayPos), structsize);
        result = GetInstallPosition(startIndex);
        result.w = IsCanInstall(startIndex, structsize) ? 1 : 0;

        result.y += 0.05f;

        return result;
    }

    private bool InstallSelectStruct(int[] startIndex) //������ ������Ʈ ���� �� installGrid ����, ���� �� false����
    {
        int structsize = StructObject.GetStructComponent().Size;
        if (IsCanInstall(startIndex, structsize))
        {
            StructObject.CreateStruct(transform,
                GetInstallPosition(startIndex),
                startIndex
                );
            SetInstallGrid(true, startIndex, structsize);
            return true;
        }
        else
            return false;
    }

    private int[] IfCanInstallSelectStructGetStartIndex(Vector2 rayPos) //��ġ �� �� ������ ���� �ε�����, ������ -1�� ���� �迭�� ���� 
    {
        int structsize = StructObject.GetStructComponent().Size;
        int[] startIndex = DoubleGridIndex2StartIndex(World2DoubleGridIndex(rayPos), structsize);
        if (IsCanInstall(startIndex, structsize))
            return startIndex;
        else
            return new int[] { -1 };
    }

    private int[] World2DoubleGridIndex(Vector2 world) // ���� xz��ǥ���� �׸��� xz �ε��� * 2�� ��ȯ
    {
        Vector2 result = world - groundStartPos;
        return new int[] { (int)(result.x / halfCellSize.x), (int)(result.y / halfCellSize.y) };
    }

    private int[] DoubleGridIndex2StartIndex(int[] dgi, int structSize) // �׸��� xz �ε��� * 2���� ���� �׸��� xz �ε����� ��ȯ
    {
        int[] result;
        if (structSize % 2 == 1)
            result = new int[]
            { 
                dgi[0]/2 - structSize/2,
                dgi[1]/2 - structSize/2 
            };
        else
        {
            result = new int[]
            {
                dgi[0]/2 - structSize/2  + (dgi[0] % 2 == 0 ? 0 : 1),
                dgi[1]/2 - structSize/2  + (dgi[1] % 2 == 0 ? 0 : 1)
            };
        }

        return new int[] { (int)Mathf.Clamp(result[0], 0, numCells.x - structSize), (int)Mathf.Clamp(result[1], 0, numCells.y - structSize) };
    }

    private bool IsCanInstall(int[] startIndex, int size) // �ش� ���� �׸��� xz �ε������� sizeũ���� ��ġ���� ��ġ �������� ��ȯ
    {
        if (startIndex[0] < 0 || numCells.x - size < startIndex[0] ||
            startIndex[1] < 0 || numCells.y - size < startIndex[1])
            return false;

        for (int i = startIndex[0]; i < startIndex[0] + size; i++)
            for (int j = startIndex[1]; j < startIndex[1] + size; j++)
                if (installTable[i, j])
                    return false;

        return true;
    }

    private void SetInstallGrid(bool value,int[] startIndex, int size) // �ش� ���� isntallGrid�� ����
    {
        for (int i = startIndex[0]; i < startIndex[0] + size; i++)
            for (int j = startIndex[1]; j < startIndex[1] + size; j++)
                installTable[i, j] = value;
    }

    private Vector3 GetInstallPosition(int[] startIndex) //��ġ�� ��ġ ��ġ ����
    {
        int structsize = StructObject.GetStructComponent().Size;
        return new Vector3(
                    groundStartPos.x + (startIndex[0] + 0.5f) * halfCellSize.x * 2 + (structsize - 1) * halfCellSize.x,
                    transform.position.y + 0.5f,
                    groundStartPos.y + (startIndex[1] + 0.5f) * halfCellSize.y * 2 + (structsize - 1) * halfCellSize.y
                );
    }


}
