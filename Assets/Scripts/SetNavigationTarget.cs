using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class SetNavigationTarget : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown navigationTargetDropDown;
    [SerializeField] private List<Target> navigationTargetObjects = new List<Target>();
    [SerializeField] private Slider navigationYOffset;
    private int currentFloor = 1; // Initialize it with a default value -- self added by me:) 

    private NavMeshPath path;
    private LineRenderer line;
    private Vector3 targetPosition = Vector3.zero;

    private bool lineToggle = false;

    private void Start()
    {
        path = new NavMeshPath();
        line = GetComponent<LineRenderer>();
        line.enabled = lineToggle;
    }

    private void Update()
    {
        if (lineToggle && targetPosition != Vector3.zero)
        {
            CalculateAndDrawPath();
        }
    }

    private Vector3[] AddLineOffset()
    {
        if (navigationYOffset.value == 0)
        {
            return path.corners;
        }

        Vector3[] calculatedLine = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++)
        {
            calculatedLine[i] = path.corners[i] + new Vector3(0, navigationYOffset.value, 0);
        }
        return calculatedLine;
    }

    private void CalculateAndDrawPath()
    {
        NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
        line.positionCount = path.corners.Length;
        Vector3[] calculatedPathAndOffset = AddLineOffset();
        line.SetPositions(calculatedPathAndOffset);
    }

    public void SetCurrentNavigationTarget(int selectedValue)
    {
        targetPosition = Vector3.zero;
        string selectedText = navigationTargetDropDown.options[selectedValue].text;
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.ToLower().Equals(selectedText.ToLower()));
        if (currentTarget != null)
        {
            // if (lineToggle)
            // {
            //     CalculateAndDrawPath();
            // }
            if (!line.enabled)
            {
                ToggleVisibility();
            }
            targetPosition = currentTarget.PositionObject.transform.position;
        }
    }

    public void ToggleVisibility()
    {
        lineToggle = !lineToggle;
        line.enabled = lineToggle;
        // if (!lineToggle)
        // {
        //     line.positionCount = 0;
        // }
        // else if (targetPosition != Vector3.zero)
        // {
        //     CalculateAndDrawPath();
        // }
    }

    public void ChangeActiveFloor(int floorNumber)
    {
        currentFloor = floorNumber;
        SetNavigationTargetDropdownOptions(floorNumber);
    }

    private void SetNavigationTargetDropdownOptions(int floorNumber)
    {
        navigationTargetDropDown.ClearOptions();
        navigationTargetDropDown.value = 0;

        if (line.enabled)
        {
            ToggleVisibility();
        }

         if (floorNumber == 1)
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("MajlisCube"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("RoomCube"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("KitchenCube"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("BedRoomCube"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("SALA"));

        }
        //there was else if i removed 'else' word from it
          if (floorNumber == 2)
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("entranceDoor"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("seepDoor"));
        }
    }
}
