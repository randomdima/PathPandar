using UnityEngine;
using UnityEngine.UI;
using GameEventBus;
using GameEventBus.Events;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    public Text pandaCount;
    public Text bambooCount;
    public Text bombCount;
    public GameObject levelParent;
    public GameObject panel;

    private int score = 0;
    private int failed = 0;
    private bool isPaused = true;

    public static EventBus Bus = new EventBus();

    public void RestartOnClick()
    {
        isPaused = false;
        panel.SetActive(isPaused);
        Time.timeScale = 1.0f;
        Reload();
    }

    public void Level1OnClick()
    {
        isPaused = false;
        panel.SetActive(isPaused);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Level1");
    }

    public void Level2OnClick()
    {
        isPaused = false;
        panel.SetActive(isPaused);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Level2");
    }


    private void OnEnable()
    {
        Bus = new EventBus();
        Bus.Subscribe<CountSavedPandaEvent>(CountSavedPanda);
        Bus.Subscribe<CountFailedPandaEvent>(CountFailedPanda);
        Bus.Subscribe<BombCountEvent>(BombCount);
        Bus.Subscribe<BambooCountEvent>(BambooCount);
        Bus.Subscribe<GameEndedEvent>(GameEnded);
    }

    private void OnDisable()
    {
        Bus.Unsubscribe<CountSavedPandaEvent>(CountSavedPanda);
        Bus.Unsubscribe<CountFailedPandaEvent>(CountFailedPanda);
        Bus.Unsubscribe<BombCountEvent>(BombCount);
        Bus.Unsubscribe<BambooCountEvent>(BambooCount);
        Bus.Unsubscribe<GameEndedEvent>(GameEnded);

    }

    void OnDestroy()
    {
        Bus.Unsubscribe<CountSavedPandaEvent>(CountSavedPanda);
        Bus.Unsubscribe<CountFailedPandaEvent>(CountFailedPanda);
        Bus.Unsubscribe<BombCountEvent>(BombCount);
        Bus.Unsubscribe<BambooCountEvent>(BambooCount);
        Bus.Unsubscribe<GameEndedEvent>(GameEnded);
    }

    void Start()
    {
        if(pandaCount == null)
            pandaCount = transform.Find("SavedPandas").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            panel.SetActive(isPaused);
            if(isPaused)
            {
                Time.timeScale = 0.0f;
            } else
            {
                Time.timeScale = 1.0f;
            }
        }
    }

    private void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void CountSavedPanda(CountSavedPandaEvent mEvent)
    {
        score++;
        pandaCount.text = $"Score: {score}";
    }

    private void CountFailedPanda(CountFailedPandaEvent mEvent)
    {
        failed++;
    }

    private void BombCount(BombCountEvent mEvent)
    {
        bombCount.text = $"{mEvent.count}";
    }

    private void BambooCount(BambooCountEvent mEvent)
    {
        bambooCount.text = $"{mEvent.count}";
    }

    private void GameEnded(GameEndedEvent mEvent)
    {
        Debug.Log($"Game ended, score {score}");
        isPaused = true;
        panel.SetActive(isPaused);
        Time.timeScale = 0.0f;
    }
}

public class CountSavedPandaEvent : EventBase
{

}


public class CountFailedPandaEvent : EventBase
{

}


public class BombCountEvent : EventBase
{
    public int count = 0;
    public BombCountEvent(int count)
    {
        this.count = count;
    }
}

public class BambooCountEvent : EventBase
{
    public int count = 0;
    public BambooCountEvent(int count)
    {
        this.count = count;
    }
}

public class AllPandasSpawnedEvent : EventBase { }

public class GameEndedEvent : EventBase { }

