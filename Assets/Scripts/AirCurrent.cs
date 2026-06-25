using UnityEngine;

public class AirCurrent : MonoBehaviour
{
    public enum WindDirection
    {
        Up,
        Right,
        Left,
        Down,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft
    }

    public WindDirection direction = WindDirection.Up;
    public float strength = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerFlight flight = other.GetComponent<PlayerFlight>();

        if (flight != null)
        {
            flight.EnterCurrent(GetDirectionVector(), strength);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerFlight flight = other.GetComponent<PlayerFlight>();

        if (flight != null)
        {
            flight.ExitCurrent();
        }
    }

    Vector2 GetDirectionVector()
    {
        switch (direction)
        {
            case WindDirection.Up:
                return Vector2.up;

            case WindDirection.Right:
                return Vector2.right;

            case WindDirection.Left:
                return Vector2.left;

            case WindDirection.Down:
                return Vector2.down;

            case WindDirection.UpRight:
                return new Vector2(1, 1).normalized;

            case WindDirection.UpLeft:
                return new Vector2(-1, 1).normalized;

            case WindDirection.DownRight:
                return new Vector2(1, -1).normalized;

            case WindDirection.DownLeft:
                return new Vector2(-1, -1).normalized;

            default:
                return Vector2.up;
        }
    }
}