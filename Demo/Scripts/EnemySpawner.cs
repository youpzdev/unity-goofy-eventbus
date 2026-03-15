using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Button spawnButton;
    [SerializeField] private Button killButton;

    private readonly List<Enemy> enemies = new();

    private void Start()
    {
        spawnButton.onClick.AddListener(Spawn);
        killButton.onClick.AddListener(KillLast);
    }

    private void Spawn()
    {
        var obj = Instantiate(enemyPrefab, Random.insideUnitSphere * 3f, Quaternion.identity);
        enemies.Add(obj.GetComponent<Enemy>());
    }

    private void KillLast()
    {
        if (enemies.Count == 0) return;
        enemies[^1].Die();
        enemies.RemoveAt(enemies.Count - 1);
    }
}