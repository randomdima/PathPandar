using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerController : MonoBehaviour
{
	public float angerLevel = 0.0f;
	public float angerPerSecond = 0.1f;

	public PandaController pandaController;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		angerLevel += angerPerSecond*Time.deltaTime;
		if (angerLevel >= 1.0f && pandaController.mode != PandaController.PandaMode.Angry)
		{
			//angry now
			pandaController.MakeAngry();
		}

		angerLevel = Mathf.Min(angerLevel, 1.0f);
	}

}
