using UnityEngine;
using UnityEditor;

public class IslandCreator : EditorWindow
{
    private int x = 5;
    private int y = 5;

    private Biom[,] cells = new Biom[5, 5];

    public int X 
    {
        get { return x;} 
        set 
        {
            x = value;
            UpdateArray();    
        } 
    }
    public int Y
    {
        get { return y;} 
        set 
        {
            y = value;
            UpdateArray();    
        } 
    }
    
    
    [MenuItem("Window/Island")]
    static void OpenWindow()
    {
        IslandCreator window = (IslandCreator)GetWindow(typeof(IslandCreator));
        window.minSize = new Vector2(300, 300);
        window.Show();
    }

    private void OnGUI()
    {   
        try
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, 20));
            EditorGUILayout.BeginHorizontal();
            
            X = EditorGUILayout.IntField(X);
            Y = EditorGUILayout.IntField(Y);

            EditorGUILayout.EndHorizontal();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect (0, 20, Screen.width, Screen.height - 20));
            for(int k = 0; k < y; k++)
            {
                 EditorGUILayout.BeginHorizontal();
                 for(int i = 0; i < x; i++)
                 {
                     cells[i, k] = (Biom)EditorGUILayout.EnumPopup(cells[i, k]);
                 }
                 EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Create"))
            {
                GameObject newIsland = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Island"), Vector3.zero, Quaternion.identity);
                newIsland.GetComponent<Island>().Build(cells);
                newIsland.GetComponent<Island>().GenerateCollider(cells);
            }
            GUILayout.EndArea();
        }
        catch(System.Exception ex)
        {
             Debug.LogError(ex.Message + ex.StackTrace);
             Close();
        }
    }

    private void UpdateArray()
    {
        if (cells == null)
        {
            cells = new Biom[x, y];
            return;
        }

        Biom[,] newArray = new Biom[x, y];

        for(int i = 0; i < x && i < cells.GetLength(0); i++)
        {
            for(int k = 0; k < y && k < cells.GetLength(1); k++)
            {
                newArray[i, k] = cells[i, k];
            }
        }

        cells = newArray;
    }
}