using UnityEngine;
using UnityEngine.UI;
using GameEventBus;
using GameEventBus.Events;

public class UILogic : MonoBehaviour
{
    public Text label;

    private int score = 0;

    public static EventBus Bus = new EventBus();

    private void OnEnable()
    {
        Bus.Subscribe<CountPandaEvent>(CountPanda);
    }

    private void OnDisable()
    {
        Bus.Unsubscribe<CountPandaEvent>(CountPanda);
    }

    void Start()
    {
        label = transform.Find("ScoreLabel").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CountPanda(CountPandaEvent mEvent)
    {
        score++;
        label.text = $"Score: {score}";
    }


}

public class CountPandaEvent : EventBase
{

}

