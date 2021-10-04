using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndgameController : MonoBehaviour
{
	public bool allPandasSpawned = false;
	public int savedPandas = 0;
	public bool gameEnded = false;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	private void OnEnable()
	{
		UILogic.Bus.Subscribe<AllPandasSpawnedEvent>(AllPandasSpawned);
		//UILogic.Bus.Subscribe<CountSavedPandaEvent>(PandaSaved);
	}

	private void OnDisable()
	{
		UILogic.Bus.Unsubscribe<AllPandasSpawnedEvent>(AllPandasSpawned);
		//UILogic.Bus.Unsubscribe<CountSavedPandaEvent>(PandaSaved);
	}

	// Update is called once per frame
	void Update()
	{
		var pandaController = GameObject.FindObjectsOfType<PandaController>();
		var noPandasLeftOnLevel = pandaController == null || pandaController.Length == 0;

		/*
		if(noPandasLeftOnLevel)
		{
			Debug.Log($"pandas not found {allPandasSpawned} {savedPandas}");
		}
		else
		{
			Debug.Log($"panda controller found {pandaController[0].name}");
		}
		*/

		if (noPandasLeftOnLevel && allPandasSpawned && !gameEnded)
		{
			gameEnded = true;
			UILogic.Bus.Publish(new GameEndedEvent());
		}
	}

	private void AllPandasSpawned(AllPandasSpawnedEvent anEvent)
	{
		allPandasSpawned = true;
	}

	private void PandaSaved(CountSavedPandaEvent anEvent)
	{
		savedPandas++;
	}
}
