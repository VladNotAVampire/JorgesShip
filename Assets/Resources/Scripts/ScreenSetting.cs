using UnityEngine;
using UnityEngine.UI;

public class ScreenSetting : MonoBehaviour 
{
    [SerializeField]private RectTransform[] divide32;
    
    [ContextMenu("Set")]
    private void Set()
    {
        float x = Screen.width / 3;
        float y = Screen.height / 2;

        foreach(var obj in divide32)
        {
            obj.sizeDelta = new Vector2(x, y);
        }
    }
}
