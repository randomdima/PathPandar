using UnityEngine;
using UnityEngine.UI;
using GameEventBus;
using GameEventBus.Events;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    public Text label;
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
    }

    private void OnDisable()
    {
        Bus.Unsubscribe<CountSavedPandaEvent>(CountSavedPanda);
        Bus.Unsubscribe<CountFailedPandaEvent>(CountFailedPanda);

    }

    void Start()
    {
        if(label == null)
            label = transform.Find("SavedPandas").GetComponent<Text>();
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
        label.text = $"Score: {score}";
    }

    private void CountFailedPanda(CountFailedPandaEvent mEvent)
    {
        failed++;
    }


}

public class CountSavedPandaEvent : EventBase
{

}


public class CountFailedPandaEvent : EventBase
{

}

