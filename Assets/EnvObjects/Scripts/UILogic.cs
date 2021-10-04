using UnityEngine;
using UnityEngine.UI;
using GameEventBus;
using GameEventBus.Events;

public class UILogic : MonoBehaviour
{
    public Text label;

    private int score = 0;
    private int failed = 0;

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

