using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float speed = 1.0f; // the speed at which the elevator moves
    public Transform[] floors; // an array of Transform objects representing the floors
    //public Transform door; // the Transform object representing the elevator door

    public int currentFloor = 0; // the current floor of the elevator
    public bool isMoving = false; // a flag indicating whether the elevator is currently moving
    private bool isGoingUp = true; // a flag indicating the direction in which the elevator is currently moving
    private int targetFloor = 0; // the target floor that the elevator needs to reach
    [SerializeField] private TMP_Text[] currentFloorCounter;

    // define the OnElevatorReachedFloor event
    public event Action<int> OnElevatorReachedFloor;

    public int GetCurrentFloor()
    {
        return currentFloor;
    }


    private void Start()
    {
        // set the initial position of the elevator to the first floor
        transform.position = floors[currentFloor].position;
    }

    private void Update()
    {
        if (isMoving)
        {
            // move the elevator towards the target floor
            Vector3 targetFloorPosition = floors[targetFloor].position;
            transform.position = Vector3.MoveTowards(transform.position, targetFloorPosition, speed * Time.deltaTime);

            // check if the elevator has passed a floor
            int previousFloor = currentFloor;
            for (int i = 0; i < floors.Length; i++)
            {
                float distanceToFloor = Mathf.Abs(transform.position.y - floors[i].position.y);
                if (distanceToFloor < 0.1f) // adjust the threshold as needed
                {
                    currentFloor = i;
                }
            }

            // update the UI text if the current floor has changed
            if (previousFloor != currentFloor)
            {
                foreach (TMP_Text text in currentFloorCounter)
                {
                    text.text = currentFloor.ToString();
                }
            }

            // check if the elevator has reached the target floor
            if (transform.position == targetFloorPosition)
            {
                currentFloor = targetFloor;
                isMoving = false;

                // raise the OnElevatorReachedFloor event
                OnElevatorReachedFloor?.Invoke(currentFloor);
            }
        }
    }


    public void MoveElevatorToFloor(int floorNumber, bool isGoingUp = true)
    {
        if (isMoving)
        {
            return; // ignore the request if the elevator is currently moving
        }

        int targetFloorNumber = Mathf.Clamp(floorNumber, 0, floors.Length - 1);
        if (targetFloorNumber == currentFloor)
        {
            return; // ignore the request if the elevator is already on the target floor
        }

        this.targetFloor = targetFloorNumber;
        this.isGoingUp = (targetFloorNumber > currentFloor);
        isMoving = true;
    }
}
