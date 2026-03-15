using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;

    public void Die()
    {
        EventBus<EnemyDiedEvent>.Raise(new EnemyDiedEvent
        {
            position = transform.position,
            score = scoreValue
        });

        Destroy(gameObject);
    }
}