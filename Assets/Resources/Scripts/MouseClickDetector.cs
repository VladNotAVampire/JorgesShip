using UnityEngine;

public class MouseClickDetector : MonoBehaviour
{
    public static event System.Action onMissClick;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(World.GetMousePosition(), Vector3.forward, 1);
            
            if (hit)
            {
                var subscribed = hit.transform.GetComponents<MonoBehaviour>();
                
                foreach(var component in subscribed)
                {
                    if (component is IOnClickSubscribed)
                    {
                        print(component.gameObject);
                        (component as IOnClickSubscribed).OnClick();
                    }
                }

                return;
            } 
            
            if (onMissClick != null)    onMissClick();
        }
    }
}
