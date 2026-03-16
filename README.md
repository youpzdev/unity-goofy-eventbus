# 📡 Goofy EventBus

> Decouple everything. Zero dependencies. One file.

**by [youpzdev](https://github.com/youpzdev)**

---

## What is this

A type-safe event bus for Unity. No strings, no UnityEvents, no direct references between systems. Objects talk to each other without knowing each other exist.

```csharp
// Enemy just yells into the void
EventBus<EnemyDiedEvent>.Raise(new EnemyDiedEvent { score = 100 });

// UI, Audio, GameManager listen independently
EventBus<EnemyDiedEvent>.Subscribe(OnEnemyDied, this);
```

---

## Install

Import `goofy-eventbus.unitypackage` into your project or copy `EventBus.cs` into your `Assets/` folder. Done.

---

## Usage

```csharp
// ── Define an event ────────────────────────────────────────
public struct EnemyDiedEvent
{
    public Vector3 position;
    public int score;
}

// ── Subscribe ──────────────────────────────────────────────
EventBus<EnemyDiedEvent>.Subscribe(OnEnemyDied, this); // auto-unsubscribes when this is destroyed
EventBus<EnemyDiedEvent>.Subscribe(OnEnemyDied);       // no target — lives forever

// ── Subscribe once ─────────────────────────────────────────
EventBus<LevelLoadedEvent>.SubscribeOnce(OnLevelLoaded, this); // fires once, then gone

// ── Raise ──────────────────────────────────────────────────
EventBus<EnemyDiedEvent>.Raise(new EnemyDiedEvent { position = transform.position, score = 100 });

// ── Unsubscribe manually ───────────────────────────────────
EventBus<EnemyDiedEvent>.Unsubscribe(OnEnemyDied);

// ── Check before raising ───────────────────────────────────
if (EventBus<EnemyDiedEvent>.HasSubscribers)
    EventBus<EnemyDiedEvent>.Raise(evt);

// ── Clear all subscribers ──────────────────────────────────
EventBus<EnemyDiedEvent>.Clear();
```

---

## Real example

```csharp
// Enemy.cs — raises event, knows nothing about UI or audio
public class Enemy : MonoBehaviour
{
    [SerializeField] private int scoreValue = 100;

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

// UIManager.cs — listens, knows nothing about Enemy
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int totalScore;

    private void OnEnable()
    {
        EventBus<EnemyDiedEvent>.Subscribe(OnEnemyDied, this);
    }

    private void OnEnemyDied(EnemyDiedEvent evt)
    {
        totalScore += evt.score;
        scoreText.text = totalScore.ToString();
    }
}
```

No `UIManager` reference in `Enemy`. No `Enemy` reference in `UIManager`. Add new listeners anytime without touching existing code.

---

## How it works

| | |
|---|---|
| 🧬 **Generic** | Separate subscriber list per event type — zero lookup overhead |
| 🛡️ **Null-safety** | `WeakReference` on target — auto-cleans dead subscribers on next `Raise` |
| 🚫 **No duplicates** | Duplicate guard on `Subscribe` and `SubscribeOnce` |
| 🔁 **Safe iteration** | Iterates in reverse — safe to remove during iteration |
| 🔒 **Resilience** | `try/catch` per callback — one broken listener doesn't kill the rest |

---

## Part of the Goofy Tools collection

| | |
|---|---|
| [**goofy-pooling**](https://github.com/youpzdev/unity-goofy-pooling) | 🐟 Zero-config object pooling |
| [**goofy-timers**](https://github.com/youpzdev/unity-goofy-timers) | ⏱️ No-coroutine timer system |
| **goofy-eventbus** | 📡 You are here |
| [**goofy-save**](https://github.com/youpzdev/unity-goofy-saves) | 💾 AES-256 encrypted save system |

---

## License

MIT — do whatever you want.
