using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D[] spawnZones;
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private ItemScriptable[] itemPool;
    [SerializeField]
    private GameObject itemHolderPrefab;
    [SerializeField]
    private int enemyCount = 3;
    [SerializeField]
    private Transform unitParent;
    public List<Unit> Enemyes { get; private set; }

    private void Start()
    {
        SpawnEnemy();
        LoadOnStart();
    }

    private static void LoadOnStart()
    {
        SaveData result = SaveLoadManager.Load();
        if(result != null && result.Inventory != null)
            FindAnyObjectByType<Player>().Inventory = result.Inventory;
    }

    public static SaveData GetProgress()
    {
        SaveData result = new SaveData();
        Player player = FindAnyObjectByType<Player>();
        if (player != null)
            result.Inventory = player.Inventory;
        return result;
    }

    private void SpawnEnemy()
    {
        Enemyes = new List<Unit>(enemyCount);
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = Instantiate(SomeEnemy(), unitParent);
            Unit newUnit = newEnemy.GetComponent<Unit>();
            newUnit.SetManager(this);
            Enemyes.Add(newUnit);
            newEnemy.GetComponent<RectTransform>().anchoredPosition = GetRandomPosition();
        }
    }

    public Sprite GetSprite(int ID)
    {
        return itemPool.Where(i => i.ID == ID).First().Image;
    }

    private Vector2 GetRandomPosition()
    {
        BoxCollider2D c = spawnZones[Random.Range(minInclusive: 0, maxExclusive: spawnZones.Length)].GetComponent<BoxCollider2D>();

        float x = Random.Range(c.offset.x - c.size.x / 2, c.offset.x + c.size.x / 2);
        float y = Random.Range(c.offset.y - c.size.y / 2, c.offset.y + c.size.y / 2);

        return new Vector2(x, y);
    }

    private GameObject SomeEnemy()
    {
        return enemyPrefabs[Random.Range(minInclusive: 0, maxExclusive: enemyPrefabs.Length)];
    }

    public void SpawnDrop(ItemScriptable item, Vector3 position)
    {
        var newItemHolder = Instantiate(itemHolderPrefab, position, Quaternion.identity, unitParent);
        newItemHolder.GetComponent<ItemHolder>().SetItem(item);
    }

    public void UnitDeath(Unit unit)
    {
        if(Enemyes.Contains(unit))
            Enemyes.Remove(unit);

        if (unit.gameObject.tag == "Player")
            Debug.LogError("GAME OVER!");
    }
}
