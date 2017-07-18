using UnityEngine;

public class DestinationPoint : MonoBehaviour, IOnClickSubscribed
{
    public void OnClick()
    {
        if (World.ship.markOn && World.ship.destination == transform.position)
        {
             World.ship.MoveToDestination();
             return;
        }

        World.ship.SetDestination(transform.position, false, info);
    }

    protected virtual string info
    {
        get 
        {
            return string.Empty;
        }
    }
}
