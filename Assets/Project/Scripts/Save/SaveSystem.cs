using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    #region Save
    public static void Save()
    {
        SavePlayer(PlayerManager.Instance);
        SaveMap(MapManager.Instance.GetMapData());
        SaveDay(DayNightManager.Instance.GetDayData());
    }

    private static void SavePlayer(PlayerManager player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private static void SaveMap(MapData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/map.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private static void SaveDay(DayData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/day.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    #endregion

    #region Load
    public static async void Load()
    {
        MapData mapData = LoadMap();

        if (SceneManager.GetActiveScene().name != mapData.sceneName)
            await SceneManager.LoadSceneAsync(mapData.sceneName);
        else
            ScreenAspectRadio.Instance.isOpened = true;

        MapManager.Instance.Load(mapData);
        PlayerManager.Instance.Load(LoadPlayer());
        DayNightManager.Instance.Load(LoadDay());
    }

    private static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }

        return null;
    }

    private static MapData LoadMap()
    {
        string path = Application.persistentDataPath + "/map.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapData data = formatter.Deserialize(stream) as MapData;
            stream.Close();

            return data;
        }

        return null;
    }

    private static DayData LoadDay()
    {
        string path = Application.persistentDataPath + "/day.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DayData data = formatter.Deserialize(stream) as DayData;
            stream.Close();

            return data;
        }

        return null;
    }

    #endregion
}
