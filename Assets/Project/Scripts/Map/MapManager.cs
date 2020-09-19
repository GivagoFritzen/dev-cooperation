using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public List<PickupItem> items = new List<PickupItem>();
    public List<Merchant> merchants = new List<Merchant>();
    public List<SimpleEnemy> enemies = new List<SimpleEnemy>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    #region Get Map Info
    public void GetMapInfo()
    {
        items.Clear();
        foreach (PickupItem item in FindObjectsOfType(typeof(PickupItem)))
            items.Add(item);

        merchants.Clear();
        foreach (Merchant merchant in FindObjectsOfType(typeof(Merchant)))
            merchants.Add(merchant);

        enemies.Clear();
        foreach (SimpleEnemy enemy in FindObjectsOfType(typeof(SimpleEnemy)))
            enemies.Add(enemy);
    }

    public MapData GetMapData()
    {
        return new MapData(SceneManager.GetActiveScene().name, GetItems(), GetMerchants(), GetEnemies());
    }

    private ItemData[] GetItems()
    {
        ItemData[] itemsData = new ItemData[items.Count];

        for (int index = 0; index < items.Count; index++)
        {
            if (items[index] != null)
            {
                Vector3 position = items[index].transform.position;
                float[] transform = new float[3] { position.x, position.y, position.z };
                itemsData[index] = new ItemData(items[index].GetItem(), transform);
            }
        }

        return itemsData;
    }

    private MerchantData[] GetMerchants()
    {
        MerchantData[] merchantsData = new MerchantData[merchants.Count];

        for (int index = 0; index < merchants.Count; index++)
        {
            if (merchants[index] != null)
            {
                Vector3 position = merchants[index].transform.position;
                float[] transform = new float[3] { position.x, position.y, position.z };
                merchantsData[index] = new MerchantData(merchants[index].GetName(), merchants[index].GetItemsData(), transform);
            }
        }

        return merchantsData;
    }

    private EnemyData[] GetEnemies()
    {
        EnemyData[] enemiesData = new EnemyData[enemies.Count];

        for (int index = 0; index < enemies.Count; index++)
        {
            if (enemies[index] != null)
            {
                Vector3 position = enemies[index].transform.position;
                float[] transform = new float[3] { position.x, position.y, position.z };
                enemiesData[index] = new EnemyData(enemies[index].name, enemies[index].life, transform);
            }
        }

        return enemiesData;
    }
    #endregion

    #region Load
    public void Load(MapData data)
    {
        ResetScene();
        CreateItems(data.items);
        CreateMerchant(data.merchants);
        CreateEnemies(data.enemies);

        GetMapInfo();
    }

    private void CreateItems(ItemData[] items)
    {
        foreach (var item in items)
        {
            if (item != null)
            {
                GameObject itemObject = (GameObject)Resources.Load($"{RouteUtil.GetPrefabsItems()}ItemPrefab", typeof(GameObject));
                itemObject.transform.position = new Vector3(item.position[0], item.position[1], item.position[2]);

                Debug.Log(item.name);
                Item newItem = (Item)Resources.Load(RouteUtil.GetPrefabsItems() + StringUtil.RemoveWhitespace(item.name), typeof(Item));
                Debug.Log(newItem.icon);
                itemObject.GetComponent<PickupItem>().SetItem(newItem);
                itemObject.GetComponent<SpriteRenderer>().sprite = newItem.icon;

                Instantiate(itemObject);
            }
        }
    }

    private void CreateMerchant(MerchantData[] merchants)
    {
        foreach (var merchant in merchants)
        {
            if (merchant != null)
            {
                GameObject merchantObject = (GameObject)Resources.Load($"{RouteUtil.GetPrefabsNPC()}Merchant", typeof(GameObject));
                merchantObject.transform.position = new Vector3(merchant.position[0], merchant.position[1], merchant.position[2]);

                merchantObject.GetComponent<Merchant>().SetName(merchant.name);
                merchantObject.GetComponent<Merchant>().SetItemsDataToListItems(merchant.items);
                merchantObject.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load($"{RouteUtil.GetAnimatorsNPC()}/Animator-{merchant.name}", typeof(RuntimeAnimatorController));
                merchantObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load($"{RouteUtil.GetPrefabsNPC()}Merchant/{merchant.name}", typeof(Sprite));

                Instantiate(merchantObject);
            }
        }
    }

    private void CreateEnemies(EnemyData[] enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                GameObject enemyObject = (GameObject)Resources.Load($"{RouteUtil.GetPrefabsEnemy()}{enemy.name}", typeof(GameObject));
                enemyObject.transform.position = new Vector3(enemy.position[0], enemy.position[1], enemy.position[2]);

                enemyObject.GetComponent<SimpleEnemy>().life = enemy.life;

                Instantiate(enemyObject);
            }
        }
    }

    private void ResetScene()
    {
        foreach (PickupItem item in items)
            if (item != null)
                DestroyImmediate(item.gameObject);
        items.Clear();

        foreach (Merchant merchant in merchants)
            if (merchant != null)
                DestroyImmediate(merchant.gameObject);
        merchants.Clear();

        foreach (SimpleEnemy enemy in enemies)
            if (enemy != null)
                DestroyImmediate(enemy.gameObject);
        enemies.Clear();
    }
    #endregion
}
