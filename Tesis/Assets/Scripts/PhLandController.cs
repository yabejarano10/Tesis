﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhLandController : MonoBehaviour
{

	public float optimalMinimunLevel = 35;
    public float optimalMaxLevel = 55;

    private float currentPhLevel;
    private TextMeshPro phText;

	public Material[] materials;

	public bool isPhOK = false;
		
    void Start()
    {
		currentPhLevel = Random.Range(1, 15);
        phText = GetComponentInChildren<TextMeshPro>();
        phText.text = currentPhLevel + "Ph";
    }


    void FixedUpdate() {

        if (currentPhLevel > optimalMaxLevel)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.sharedMaterial = materials[2];
            gameObject.tag = "DamagedLand";
        }
        else if (currentPhLevel >= optimalMinimunLevel && currentPhLevel <= optimalMaxLevel)
        {
            gameObject.tag = "SafePH";
            Renderer rend = GetComponent<Renderer>();
            rend.sharedMaterial = materials[1];
        }
    }

	private void setPhState(bool state)
	{
		isPhOK = state;
	}

	public void increasePHLevel()
	{
		currentPhLevel += 0.01f*Random.Range(1,25);
        phText.text = currentPhLevel + "Ph";
        
	}
}
