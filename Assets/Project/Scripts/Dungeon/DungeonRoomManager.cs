using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRoomManager : MonoBehaviour
{
    private Vector3Int centerPosition = Vector3Int.zero;
    private bool doorExit = false;
    private int size = 0;
    private int corridorHeight = 0;
    private List<Direction> doorDirections = new List<Direction>();

    private Tilemap wallMap = null;
    private Tilemap pitMap = null;

    [System.Serializable]
    public class InstantiateUtil
    {
        public GameObject prefab;
        [Range(0, 100)]
        public int chanceToDrop;
    }

    public InstantiateUtil[] objectsCenterRoom = null;
    [SerializeField]
    private InstantiateUtil[] objectsAsideDoor = null;
    [SerializeField]
    private InstantiateUtil[] objectsInRoom = null;

    [SerializeField]
    private IntRange numberOfEnemies = new IntRange(0, 2);
    [SerializeField]
    private InstantiateUtil[] enemiesInRoom = null;

    [Space(10)]
    [SerializeField]
    private GameObject exitPrefab = null;

    public void Init(int size, Tilemap wallMap, Tilemap pitMap, int corridorHeight, bool doorExit = false)
    {
        centerPosition = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, 0);
        this.doorExit = doorExit;
        this.size = size;
        this.corridorHeight = corridorHeight;

        this.wallMap = wallMap;
        this.pitMap = pitMap;
    }

    public void Populate()
    {
        GenerateObjectsCenterRoom();
        GenerateObjectsAsideDoor();
        GenerateObjectsAsideRoom();
        GenerateObjectsEnemies();
    }

    private void GetDoorPosition()
    {
        int halfSize = size / 2;
        int margin = 2;

        if (!wallMap.GetTile(centerPosition + new Vector3Int(0, halfSize + margin, 0)) && !pitMap.GetTile(centerPosition + new Vector3Int(halfSize + margin, 0, 0)))
            doorDirections.Add(Direction.East);

        if (!wallMap.GetTile(centerPosition - new Vector3Int(0, halfSize + margin, 0)) && !pitMap.GetTile(centerPosition - new Vector3Int(halfSize + margin, 0, 0)))
            doorDirections.Add(Direction.West);

        if (!wallMap.GetTile(centerPosition + new Vector3Int(0, halfSize + margin, 0)) && !pitMap.GetTile(centerPosition + new Vector3Int(0, halfSize + margin, 0)))
            doorDirections.Add(Direction.North);

        if (!wallMap.GetTile(centerPosition - new Vector3Int(0, halfSize + margin, 0)) && !pitMap.GetTile(centerPosition - new Vector3Int(0, halfSize + margin, 0)))
            doorDirections.Add(Direction.South);
    }

    #region Generate Objects
    private void GenerateObjectWithInstantiateUtil(InstantiateUtil[] objects, Vector3Int pos)
    {
        int valueSort = Random.Range(0, 100);

        foreach (var obj in objects)
        {
            bool dropped = ChanceToDrop(valueSort, obj, pos);

            if (dropped)
                break;
        }
    }

    private void GenerateObjectsCenterRoom()
    {
        if (doorExit)
            Instantiate(exitPrefab, centerPosition, Quaternion.identity, transform);
        else
            GenerateObjectWithInstantiateUtil(objectsCenterRoom, centerPosition);
    }

    private void GenerateObjectsAsideDoor()
    {
        GetDoorPosition();

        if (doorDirections.Count > 0)
        {
            int halfSize = size / 2;
            int marginRightAndLeft = 1;
            int marginTopAndBottom = 2;

            if (doorDirections.Contains(Direction.East))
            {
                Vector3Int doorMargin = centerPosition + new Vector3Int(halfSize - marginRightAndLeft, 0, 0);

                GenerateObjectWithInstantiateUtil(objectsAsideDoor, doorMargin + new Vector3Int(0, marginRightAndLeft + (corridorHeight / 3), 0));
                GenerateObjectWithInstantiateUtil(objectsAsideDoor, doorMargin + new Vector3Int(0, -marginRightAndLeft - (corridorHeight / 3), 0));
            }

            if (doorDirections.Contains(Direction.West))
            {
                Vector3Int doorMargin = centerPosition - new Vector3Int(halfSize - marginRightAndLeft, 0, 0);

                GenerateObjectWithInstantiateUtil(objectsAsideDoor, doorMargin + new Vector3Int(0, marginRightAndLeft + (corridorHeight / 3), 0));
                GenerateObjectWithInstantiateUtil(objectsAsideDoor, doorMargin + new Vector3Int(0, -marginRightAndLeft - (corridorHeight / 3), 0));
            }

            if (doorDirections.Contains(Direction.North))
            {
                Vector3Int doorMargin = centerPosition + new Vector3Int(0, halfSize - marginTopAndBottom, 0);

                GenerateObjectWithInstantiateUtil(objectsAsideDoor, doorMargin + new Vector3Int(marginTopAndBottom + (corridorHeight / 3), 0, 0));
                GenerateObjectWithInstantiateUtil(objectsAsideDoor, doorMargin + new Vector3Int(-marginTopAndBottom - (corridorHeight / 3), 0, 0));
            }

            if (doorDirections.Contains(Direction.South))
            {
                Vector3Int doorMargin = centerPosition - new Vector3Int(0, halfSize - marginTopAndBottom, 0);

                GenerateObjectWithInstantiateUtil(objectsAsideDoor, doorMargin + new Vector3Int(marginTopAndBottom + (corridorHeight / 3), 0, 0));
                GenerateObjectWithInstantiateUtil(objectsAsideDoor, doorMargin + new Vector3Int(-marginTopAndBottom - (corridorHeight / 3), 0, 0));
            }
        }
    }

    private void GenerateObjectsAsideRoom()
    {
        int halfOfHalf = size / 2 / 2;

        GenerateObjectWithInstantiateUtil(objectsInRoom, centerPosition + new Vector3Int(halfOfHalf, halfOfHalf, 0));
        GenerateObjectWithInstantiateUtil(objectsInRoom, centerPosition + new Vector3Int(-halfOfHalf, halfOfHalf, 0));

        GenerateObjectWithInstantiateUtil(objectsInRoom, centerPosition + new Vector3Int(halfOfHalf, -halfOfHalf, 0));
        GenerateObjectWithInstantiateUtil(objectsInRoom, centerPosition + new Vector3Int(-halfOfHalf, -halfOfHalf, 0));
    }

    private void GenerateObjectsEnemies()
    {
        int enemiesAmount = numberOfEnemies.Random;
        int halfOfHalf = size / 2 / 2;

        for (int i = 0; i < enemiesAmount; i++)
            GenerateObjectWithInstantiateUtil(enemiesInRoom, centerPosition + new Vector3Int(-halfOfHalf, halfOfHalf, 0));
    }
    #endregion

    private bool ChanceToDrop(int valueSort, InstantiateUtil instantiateUtil, Vector3 position)
    {
        if (valueSort >= instantiateUtil.chanceToDrop)
            return false;

        if (instantiateUtil.prefab != null)
            Instantiate(instantiateUtil.prefab, position, Quaternion.identity, transform);

        return true;
    }
}
