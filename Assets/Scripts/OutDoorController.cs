using UnityEngine;

public class OutDoorController : MonoBehaviour
{
    [SerializeField]
    private ElevatorController[] elevatorControllers;

    public void PressUpButton(int floorNumber)
    {
        ElevatorController availableElevator = null;
        ElevatorController closestElevator = null;
        int closestDistance = int.MaxValue;

        // Check if an elevator is already on the requested floor
        foreach (ElevatorController elevator in elevatorControllers)
        {
            if (!elevator.isMoving && elevator.GetCurrentFloor() == floorNumber)
            {
                availableElevator = elevator;
                break;
            }
        }

        if (availableElevator != null)
        {
            availableElevator.MoveElevatorToFloor(floorNumber, true);
        }
        else
        {
            // Otherwise, find the closest elevator that is not moving
            foreach (ElevatorController elevator in elevatorControllers)
            {
                if (!elevator.isMoving)
                {
                    int distance = Mathf.Abs(elevator.GetCurrentFloor() - floorNumber);
                    if (distance < closestDistance)
                    {
                        closestElevator = elevator;
                        closestDistance = distance;
                    }
                }
            }

            if (closestElevator != null)
            {
                closestElevator.MoveElevatorToFloor(floorNumber, true);
            }
        }
    }

    public void PressDownButton(int floorNumber)
    {
        ElevatorController availableElevator = null;
        ElevatorController closestElevator = null;
        int closestDistance = int.MaxValue;

        // Check if an elevator is already on the requested floor
        foreach (ElevatorController elevator in elevatorControllers)
        {
            if (!elevator.isMoving && elevator.GetCurrentFloor() == floorNumber)
            {
                availableElevator = elevator;
                break;
            }
        }

        if (availableElevator != null)
        {
            availableElevator.MoveElevatorToFloor(floorNumber, false);
        }
        else
        {
            // Otherwise, find the closest elevator that is not moving
            foreach (ElevatorController elevator in elevatorControllers)
            {
                if (!elevator.isMoving)
                {
                    int distance = Mathf.Abs(elevator.GetCurrentFloor() - floorNumber);
                    if (distance < closestDistance)
                    {
                        closestElevator = elevator;
                        closestDistance = distance;
                    }
                }
            }

            if (closestElevator != null)
            {
                closestElevator.MoveElevatorToFloor(floorNumber, false);
            }
        }
    }
}
