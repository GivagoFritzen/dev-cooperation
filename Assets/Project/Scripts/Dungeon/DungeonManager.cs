using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager : MonoBehaviour
{
    [Header("Dungeon Manager")]
    [SerializeField]
    private int maxRoutes = 20;
    private const float PI = 3.1415926535f;
    [SerializeField]
    private IntRange roomSize = new IntRange(1, 100);
    public Vector2Int centerOfFirstRoom { get; private set; } = Vector2Int.zero;

    [Header("Corridor")]
    [SerializeField]
    private IntRange amountPossiblesCorridors = new IntRange(1, 2);
    [SerializeField]
    private int corridorWidth = 0;
    [SerializeField]
    private int corridorHeight = 0;

    [Header("Tiles")]
    [SerializeField]
    private Tile groundTile = null;
    [SerializeField]
    private Tile topWallTile = null;
    [SerializeField]
    private Tile botWallTile = null;
    [SerializeField]
    private Tile pitTile = null;

    [Header("Tile Maps")]
    [SerializeField]
    private Tilemap groundMap = null;
    [SerializeField]
    private Tilemap wallMap = null;
    [SerializeField]
    private Tilemap pitMap = null;

    private void Start()
    {
        NewRoute(0, 0, amountPossiblesCorridors.Random, RandomDirection(), 0);
        FillWalls();
    }

    private void NewRoute(int x, int y, int routeLength, Direction lastDirection, int routeCount = 0)
    {
        if (routeCount >= maxRoutes)
            return;

        routeCount++;
        int currentRoomSize;

        int xOffset;
        int yOffset;

        Direction direction;
        List<Direction> allLastDirections = new List<Direction>();
        allLastDirections.Add(lastDirection);

        do
        {
            currentRoomSize = roomSize.Random;

            direction = GetNextDirection(allLastDirections);
            allLastDirections.Add(direction);

            xOffset = GetXOffSet(x, direction, currentRoomSize);
            yOffset = GetYOffSet(y, direction, currentRoomSize);

            if (routeCount == 1)
                centerOfFirstRoom = new Vector2Int(xOffset, yOffset);

            GenerateRoom(xOffset, yOffset, currentRoomSize);
            NewRoute(xOffset, yOffset, amountPossiblesCorridors.Random, direction, routeCount);

            if (routeCount > 1)
                NextCorridor(x, y, xOffset, yOffset, direction);

            routeLength--;
        } while (routeLength > 0);
    }

    private int GetXOffSet(int x, Direction direction, int currentRoomSize)
    {
        int xOffset = x;

        switch (direction)
        {
            case Direction.East:
                xOffset = x + corridorWidth + currentRoomSize;
                break;
            case Direction.West:
                xOffset = x - corridorWidth - currentRoomSize;
                break;
        }

        return xOffset;
    }

    private int GetYOffSet(int y, Direction direction, int currentRoomSize)
    {
        int yOffset = y;

        switch (direction)
        {
            case Direction.North:
                yOffset = y + corridorWidth + currentRoomSize;
                break;
            case Direction.South:
                yOffset = y - corridorWidth - currentRoomSize;
                break;
        }

        return yOffset;
    }

    #region Directions Controller
    private Direction GetNextDirection(List<Direction> lastsDirections)
    {
        List<Direction> nextDirection = System.Enum.GetValues(typeof(Direction))
                                            .Cast<Direction>()
                                            .ToList();

        if (lastsDirections.Contains(Direction.East))
            nextDirection.Remove(Direction.West);
        if (lastsDirections.Contains(Direction.West))
            nextDirection.Remove(Direction.East);
        if (lastsDirections.Contains(Direction.South))
            nextDirection.Remove(Direction.North);
        if (lastsDirections.Contains(Direction.North))
            nextDirection.Remove(Direction.South);

        return nextDirection[Random.Range(0, nextDirection.Count - 1)];
    }

    private Direction RandomDirection()
    {
        List<Direction> allDirections = System.Enum.GetValues(typeof(Direction))
                                    .Cast<Direction>()
                                    .ToList();

        return allDirections[Random.Range(0, allDirections.Count - 1)];
    }
    #endregion

    private void NextCorridor(int x, int y, int xOffset, int yOffset, Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                GenerateCorridorNorth(x, y, yOffset);
                break;
            case Direction.South:
                GenerateCorridorSouth(x, y, yOffset);
                break;
            case Direction.East:
                GenerateCorridorEast(x, y, xOffset);
                break;
            case Direction.West:
                GenerateCorridorWest(x, y, xOffset);
                break;
        }
    }

    #region Generate Rooms
    private void GenerateRoom(int x, int y, int radius)
    {
        int randomRoom = Random.Range(0, 4);
        switch (randomRoom)
        {
            case 0:
            default:
                GenerateSquare(x, y, radius);
                break;
            case 1:
                GenerateCircle(x, y, radius);
                break;
            case 2:
                GenerateCross(x, y, radius);
                break;
            case 3:
                GenerateSquareWithCorridors(x, y, radius);
                break;
        }
    }

    private void GenerateSquare(int x, int y, int radius)
    {
        for (int tileX = x - radius / 2; tileX <= x + radius / 2; tileX++)
            for (int tileY = y - radius / 2; tileY <= y + radius / 2; tileY++)
                SetTileGround(tileX, tileY);
    }

    private void GenerateCircle(int x, int y, int radius)
    {
        float currentX, currentY, angle;

        for (int r = radius / 2; r > 0; r--)
        {
            for (int i = 0; i < 360; i++)
            {
                angle = i;
                currentX = r * Mathf.Cos(angle * PI / 180) + x;
                currentY = r * Mathf.Sin(angle * PI / 180) + y;
                SetTileGround((int)currentX, (int)currentY);
            }
        }
    }

    private void GenerateCross(int x, int y, int radius)
    {
        int minSize = radius / 2;
        int maxSize = radius;

        for (int tileX = x - minSize / 2; tileX <= x + minSize / 2; tileX++)
            for (int tileY = y - maxSize / 2; tileY <= y + maxSize / 2; tileY++)
                SetTileGround(tileX, tileY);

        for (int tileX = x - maxSize / 2; tileX <= x + maxSize / 2; tileX++)
            for (int tileY = y - minSize / 2; tileY <= y + minSize / 2; tileY++)
                SetTileGround(tileX, tileY);
    }

    private void GenerateSquareWithCorridors(int x, int y, int radius)
    {
        bool generateMiniCorridorVertical = Random.value > 0.5f;
        int miniCorridorWidth, miniCorridorHeight;

        GenerateSquare(x, y, radius);

        if (generateMiniCorridorVertical)
        {
            miniCorridorWidth = radius + radius / 3;
            miniCorridorHeight = radius / 2;

            for (int tileX = x - miniCorridorHeight / 2; tileX <= x + miniCorridorHeight / 2; tileX++)
                for (int tileY = y - miniCorridorWidth / 2; tileY <= y + miniCorridorWidth / 2; tileY++)
                    SetTileGround(tileX, tileY);
        }
        else
        {
            miniCorridorHeight = radius + radius / 3;
            miniCorridorWidth = radius / 2;

            for (int tileX = x - miniCorridorHeight / 2; tileX <= x + miniCorridorHeight / 2; tileX++)
                for (int tileY = y - miniCorridorWidth / 2; tileY <= y + miniCorridorWidth / 2; tileY++)
                    SetTileGround(tileX, tileY);
        }
    }

    private void GenerateCorridorNorth(int x, int y, int yOffset)
    {
        int width = corridorHeight;

        for (int tileX = x - width / 2; tileX <= x + width / 2; tileX++)
            for (int tileY = y; tileY <= yOffset; tileY++)
                SetTileGround(tileX, tileY);
    }

    private void GenerateCorridorSouth(int x, int y, int yOffset)
    {
        int width = corridorHeight;

        for (int tileX = x - width / 2; tileX <= x + width / 2; tileX++)
            for (int tileY = y; tileY >= yOffset; tileY--)
                SetTileGround(tileX, tileY);
    }

    private void GenerateCorridorEast(int x, int y, int xOffset)
    {
        int height = corridorHeight;

        for (int tileX = x; tileX <= xOffset; tileX++)
            for (int tileY = y - height / 2; tileY <= y + height / 2; tileY++)
                SetTileGround(tileX, tileY);
    }

    private void GenerateCorridorWest(int x, int y, int xOffset)
    {
        int height = corridorHeight;

        for (int tileX = x; tileX >= xOffset; tileX--)
            for (int tileY = y - height / 2; tileY <= y + height / 2; tileY++)
                SetTileGround(tileX, tileY);
    }
    #endregion

    private void SetTileGround(int x, int y)
    {
        Vector3Int tilePos = new Vector3Int(x, y, 0);
        groundMap.SetTile(tilePos, groundTile);
    }

    private void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);

                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);

                if (tile == null)
                {
                    if (tileBelow != null)
                        wallMap.SetTile(pos, topWallTile);
                    else if (tileAbove != null)
                        wallMap.SetTile(pos, botWallTile);
                    else
                        pitMap.SetTile(pos, pitTile);
                }
            }
        }
    }
}
