using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomManager : MonoBehaviour
{
    private RoomTag typeOfRoom = RoomTag.Square;
    private int size = 0;


    [System.Serializable]
    public class InstantiateUtil
    {
        public GameObject prefab;
        [Range(0, 100)]
        public int chanceToDrop;
    }

    public InstantiateUtil[] objectsCenterRoom = null;
    /*
    [SerializeField]
    private InstantiateUtil[] objectsAsideDoor = null;
    [SerializeField]
    private InstantiateUtil[] objectsAsideWall = null;
    [SerializeField]
    private InstantiateUtil[] objectsInRoom = null;
    [SerializeField]
    private InstantiateUtil[] enemiesInRoom = null;
    */

    public void Init(int size, RoomTag typeOfRoom)
    {
        this.size = size;
        this.typeOfRoom = typeOfRoom;
    }

    public void Populate()
    {
        GenerateObjectsAsideDoor();
        GenerateCenterObjects();
    }

    #region Generate Objects
    private void GenerateCenterObjects()
    {
        int valueSort = Random.Range(0, 100);

        foreach (var obj in objectsCenterRoom)
        {
            bool dropped = ChanceToDrop(valueSort, obj);

            if (dropped)
                break;
        }
    }

    private void GenerateObjectsAsideDoor()
    {
        Debug.Log(transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit.collider != null)
        {
            Debug.Log("down");
            Debug.Log(hit.collider.name);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.up);

        if (hit.collider != null)
        {
            Debug.Log("up");
            Debug.Log(hit.collider.name);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.left);

        if (hit.collider != null)
        {
            Debug.Log("left");
            Debug.Log(hit.collider.name);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right);

        if (hit.collider != null)
        {
            Debug.Log("right");
            Debug.Log(hit.collider.name);
        }
    }
    #endregion

    private bool ChanceToDrop(int valueSort, InstantiateUtil instantiateUtil)
    {
        return ChanceToDrop(valueSort, instantiateUtil, transform.position);
    }

    private bool ChanceToDrop(int valueSort, InstantiateUtil instantiateUtil, Vector3 position)
    {
        if (valueSort >= instantiateUtil.chanceToDrop)
            return false;

        if (instantiateUtil.prefab != null)
            Instantiate(instantiateUtil.prefab, position, Quaternion.identity, transform);

        return true;
    }
}
