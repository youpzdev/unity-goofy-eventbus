using UnityEngine;

public struct EnemyDiedEvent
{
    public Vector3 position;
    public int score;
}

public struct PlayerDamagedEvent
{
    public int damage;
    public int remainingHealth;
}

public struct LevelLoadedEvent
{
    public int levelIndex;
}