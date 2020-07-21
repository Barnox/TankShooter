using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreationUI : MonoBehaviour
{
    public PartCollection collectedParts;
    public GameObject mobilityHolder;
    public TMP_Dropdown mobilityDropdown;
    public GameObject bodyHolder;
    public TMP_Dropdown bodyDropdown;
    public GameObject[] partsHolder;
    public TMP_Dropdown[] partsDropdown;

    public Image uiPanel;

    public TankSpawn tankSpawn;

    public GameObject holderPrefab;

    TMP_Dropdown currentDropdown;

    int NumberOfConnectors;
    int partInDropdown;
    int numberOfTanks = 0;


    // Start is called before the first frame update
    void Awake()
    {
        uiPanel = GetComponentInChildren<Image>();

        this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        mobilityDropdown = mobilityHolder.GetComponentInChildren<TMP_Dropdown>();
        mobilityDropdown.ClearOptions();
        PopulateDropdown(mobilityDropdown, collectedParts.collectedMobility);

        bodyDropdown = bodyHolder.GetComponentInChildren<TMP_Dropdown>();
        bodyDropdown.ClearOptions();
        PopulateDropdown(bodyDropdown, collectedParts.collectedBody);

        CreatePartDropdowns();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopulateDropdown(TMP_Dropdown dropdown, TankyParts[] optionsArray)
    {
        List<string> options = new List<string>();
        foreach (var option in optionsArray)
        {
            options.Add(option.name); // Or whatever you want for a label
        }
        dropdown.AddOptions(options);
    }

    void AddIndividualDropdown(TMP_Dropdown dropdown, string optionToAdd)
    {
        List<string> options = new List<string>();
        options.Add(optionToAdd); // Or whatever you want for a label
        dropdown.AddOptions(options);
    }

    public void CreatePartDropdowns()
    {
        NumberOfConnectors = collectedParts.collectedBody[bodyDropdown.value].connectorAngleOffset.Length;
        partsHolder = new GameObject[NumberOfConnectors];
        for (int i = 0; i < NumberOfConnectors; i++)
        {
            partsHolder[i] = Instantiate(holderPrefab, uiPanel.transform);
            partsHolder[i].transform.localPosition = new Vector3(0, 40 - (40 * i), 0);
            currentDropdown = partsHolder[i].GetComponentInChildren<TMP_Dropdown>();
            currentDropdown.ClearOptions();
            AddIndividualDropdown(currentDropdown, "Nothing");
            PopulateDropdown(currentDropdown, collectedParts.collectedWeapon);
            PopulateDropdown(currentDropdown, collectedParts.collectedMisc);
        }


    }

    public void EnablePartDropdowns()
    {
        NumberOfConnectors = collectedParts.collectedBody[bodyDropdown.value].connectorAngleOffset.Length;
        foreach (GameObject Holder in partsHolder)
        { Holder.SetActive(false); }
        for (int i = 0; i < NumberOfConnectors; i++)
        {
            partsHolder[i].SetActive(true);
            partsHolder[i].transform.localPosition = new Vector3(0, 40 - (40 * i), 0);
            currentDropdown = partsHolder[i].GetComponentInChildren<TMP_Dropdown>();
            currentDropdown.ClearOptions();
            AddIndividualDropdown(currentDropdown, "Nothing");
            PopulateDropdown(currentDropdown, collectedParts.collectedWeapon);
            PopulateDropdown(currentDropdown, collectedParts.collectedMisc);
        }


    }

    public void InitiateGenerateTank()
    {
        TankyBody basePart = collectedParts.collectedBody[bodyDropdown.value];
        TankyMobility mobilityPart = collectedParts.collectedMobility[mobilityDropdown.value];
        TankyParts[] attachParts = new TankyParts[NumberOfConnectors];


        for (int i = 0; i < NumberOfConnectors; i++)
        {
            partInDropdown = partsHolder[i].GetComponentInChildren<TMP_Dropdown>().value;
            //Debug.Log("Part in Dropdown:" + partInDropdown);

            if (partInDropdown == 0)
            {
                //Debug.Log("It was 0");
                attachParts[i] = null;
            }
            else {
                if (partInDropdown <= collectedParts.collectedWeapon.Length)
                {
                    //Debug.Log("WeaponLookup:" + (partInDropdown - 1));
                    attachParts[i] = collectedParts.collectedWeapon[partInDropdown - 1];
                }
                else
                {
                    //Debug.Log("MiscLookup:" + (partInDropdown -1 - collectedParts.collectedWeapon.Length));
                    attachParts[i] = collectedParts.collectedMisc[partInDropdown - 1 - collectedParts.collectedWeapon.Length];
                }
            }
        }
        numberOfTanks++;
        tankSpawn.CreateTank(Vector3.zero + new Vector3((-3 * numberOfTanks),0,0), basePart, mobilityPart, attachParts);
        
    }

}
