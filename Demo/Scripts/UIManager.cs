using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private int totalScore;

    private void OnEnable()
    {
        EventBus<EnemyDiedEvent>.Subscribe(OnEnemyDied, this);
        EventBus<PlayerDamagedEvent>.Subscribe(OnPlayerDamaged, this);
        EventBus<LevelLoadedEvent>.SubscribeOnce(OnLevelLoaded, this);
    }

    private void OnEnemyDied(EnemyDiedEvent evt)
    {
        totalScore += evt.score;
        scoreText.text = totalScore.ToString();
    }

    private void OnPlayerDamaged(PlayerDamagedEvent evt) =>
        Debug.Log($"HP: {evt.remainingHealth}");

    private void OnLevelLoaded(LevelLoadedEvent evt) =>
        Debug.Log($"Level {evt.levelIndex} loaded");
}