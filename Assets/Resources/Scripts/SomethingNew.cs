using UnityEngine;
using UnityEngine.UI;

public class SomethingNew : MonoBehaviour
{
    [SerializeField]private float someValue;
    [SerializeField]private GameObject someRef;
    [SerializeField]private Image someIU;

    private void Start()
    {
        someValue = 5;
        someRef = GameObject.FindObjectWithTag("Untaged");
    }

    private void Update()
    {
        transform.position += Vector3.up * someValue * Time.deltaTime;
        someRef.transform.position += Vector3.down * someValue * Time.deltaTime;
    }
}