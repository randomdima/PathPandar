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

    private void OnEnable()
    {
        Bus.Subscribe<CountSavedPandaEvent>(CountSavedPanda);
        Bus.Subscribe<CountFailedPandaEvent>(CountFailedPanda);
        Bus.Subscribe<BombCountEvent>(BombCount);
        Bus.Subscribe<BambooCountEvent>(BambooCount);
    }

    private void OnDisable()
    {
        Bus.Unsubscribe<CountSavedPandaEvent>(CountSavedPanda);
        Bus.Unsubscribe<CountFailedPandaEvent>(CountFailedPanda);
        Bus.Unsubscribe<BombCountEvent>(BombCount);
        Bus.Unsubscribe<BambooCountEvent>(BambooCount);

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
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
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

