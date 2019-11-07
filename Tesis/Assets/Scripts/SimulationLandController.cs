﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationLandController : MonoBehaviour
{
    public TutorialController  tutoController;
    public GameObject simulationLand;

    private PhSimLandCOntroller phlc;
    private SimNutrientsLandController nlc;
    private FarmSimLandController flc;

    public bool endSim1 = false;
    public bool endSim2 = false;
    public bool endSim3 = false;

    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        phlc = simulationLand.AddComponent<PhSimLandCOntroller>() as PhSimLandCOntroller;
        nlc = simulationLand.AddComponent<SimNutrientsLandController>() as SimNutrientsLandController;
        flc = simulationLand.GetComponent<FarmSimLandController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (phlc.endSim1)
        {
            EndPhPhase();
        }
        if (nlc.endNutSim)
        {
            EndNutPhase();
        }
        if (flc.finish)
        {
            EndFarmPhase();
        }
    }


    public void initializePhLand()
    {
        phlc.materials = materials;

        GameObject text = new GameObject();
        text.tag = "PhText";
        text.transform.parent = simulationLand.transform;

        TextMeshPro t = text.AddComponent<TextMeshPro>();
        t.fontSize = 10;
        t.transform.localEulerAngles += new Vector3(180, 90, 180);
        t.transform.position += new Vector3(simulationLand.transform.position.x, simulationLand.transform.position.y - 1f, simulationLand.transform.position.z);
        text.GetComponent<RectTransform>().sizeDelta = new Vector2(3, 5);
    }

    private void EndPhPhase()
    {
        Destroy(simulationLand.GetComponent<PhLandController>());
        foreach (Transform child in simulationLand.transform)
        {
            if (child.tag != "Limit")
            {
                Destroy(child.gameObject);
            }
        }
        if (checkPhSafe())
        {
            endSim1 = true;
        }
        else
        {
            phlc = simulationLand.AddComponent<PhSimLandCOntroller>() as PhSimLandCOntroller;
            initializePhLand();
        }
    }
    public bool checkPhSafe()
    {
        if (simulationLand.tag == "SafePH")
        {
            return true;
        }
        return false;
    }
    public void initializeNutrients()
    {
        nlc.materials = materials;
        nlc.EnableText();
    }
    public void EndNutPhase()
    {
        Destroy(simulationLand.GetComponent<NutrientLandController>());
        foreach (Transform child in simulationLand.transform)
        {
            if (child.tag != "Limit")
            {
                Destroy(child.gameObject);
            }
        }
        if (checkNutrientsSafe())
        {
            endSim2 = true;
        }
        else
        {
            nlc = simulationLand.AddComponent<SimNutrientsLandController>() as SimNutrientsLandController;
            initializeNutrients();
        }
    }
    public bool checkNutrientsSafe()
    {
        if (simulationLand.tag == "SafeNutrients")
        {
            return true;
        }
        return false;
    }
    public void initializeFarm()
    {

    }
    private void EndFarmPhase()
    {
        Destroy(simulationLand.GetComponent<FarmSimLandController>());
        if (checkFarmSafe())
        {
            endSim3 = true;
        }
        else
        {
            flc = simulationLand.AddComponent<FarmSimLandController>() as FarmSimLandController;
        }
    }
    public bool checkFarmSafe()
    {
        if (simulationLand.tag == "SafeLand")
        {
            return true;
        }
        return false;
    }
}
