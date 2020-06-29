[System.Serializable]
public class MapData
{
    public string sceneName = "";
    public ItemData[] items = null;
    public MerchantData[] merchants = null;
    public EnemyData[] enemies = null;

    public MapData(string sceneName, ItemData[] items, MerchantData[] merchants, EnemyData[] enemies)
    {
        this.sceneName = sceneName;
        this.items = items;
        this.merchants = merchants;
        this.enemies = enemies;
    }
}
