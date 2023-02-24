using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InDoorController : MonoBehaviour
{
    [SerializeField] private ElevatorController elevatorController;
    public GameObject inDoorPopup;

    [SerializeField] private List<Button> buttons = new List<Button>();

    [SerializeField]
    private int currentFloor = 0;
    [SerializeField]
    private bool shouldShowPopup = false;

    private void Update()
    {
        if (shouldShowPopup)
        {
            inDoorPopup.SetActive(true);
        }
        else
        {
            inDoorPopup.SetActive(false);
        }
    }

    private void OnEnable()
    {
        elevatorController.OnElevatorReachedFloor += HandleElevatorReachedFloor;
    }

    private void OnDisable()
    {
        elevatorController.OnElevatorReachedFloor -= HandleElevatorReachedFloor;
    }

    private void HandleElevatorReachedFloor(int floorNumber)
    {
        // update the current floor number
        currentFloor = floorNumber;

        // enable all buttons
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        // disable the button for the current floor, unless it's the ground floor
        if (currentFloor >= 0)
        {
            buttons[currentFloor].interactable = false;
        }

        // show or hide the popup based on whether the current floor button is interactable or not
        if (buttons[currentFloor].interactable)
        {
            shouldShowPopup = false;
            inDoorPopup.SetActive(false);
        }
        else
        {
            shouldShowPopup = true;
        }

        // disable the button for the floor we just left, unless it's the ground floor
        if (floorNumber > 0)
        {
            buttons[floorNumber].interactable = false;
        }

        
    }






    public void OnButtonClicked(int floorNumber)
    {
        // move the elevator to the selected floor
        elevatorController.MoveElevatorToFloor(floorNumber);

        // hide the popup if the selected floor is not the current floor
        if (floorNumber != currentFloor)
        {
            shouldShowPopup = false;
            inDoorPopup.SetActive(false);
        }
    }

    public void HideInDoorPopup()
    {
        // enable all buttons
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }
}
