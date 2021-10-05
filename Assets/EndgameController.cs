using System;
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
		Debug.Log("endgame enable");
		UILogic.Bus.Subscribe<AllPandasSpawnedEvent>(AllPandasSpawned);
		//UILogic.Bus.Subscribe<CountSavedPandaEvent>(PandaSaved);
	}

	private void OnDisable()
	{
		Debug.Log("endgame disable");
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
		var timeRemainder = Time.time - ((int)(Math.Floor(Time.time)/5))*5;
		if (timeRemainder < 0.012)
		{
			Debug.Log($"no pandas left on level {noPandasLeftOnLevel}");
		}

		if (noPandasLeftOnLevel && allPandasSpawned && !gameEnded)
		{
			Debug.Log("publishing game ended");
			gameEnded = true;
			UILogic.Bus.Publish(new GameEndedEvent());
		}
	}

	private void AllPandasSpawned(AllPandasSpawnedEvent anEvent)
	{
		Debug.Log("All pandas spawned registered");
		allPandasSpawned = true;
	}

	private void PandaSaved(CountSavedPandaEvent anEvent)
	{
		savedPandas++;
	}
}
