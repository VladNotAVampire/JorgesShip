using UnityEngine;

public class ShipController : MonoBehaviour 
{
    private GameObject _point;
    private Vector3    _destination;
    private bool       go;
    private bool       _markOn;
    private MouseClickDetector detector;

    [SerializeField]private GameObject islandWindow;
    [SerializeField]private Inventory  inventory;

    public Vector3 destination
    {
        get 
        {
            return _destination;
        }
    }

    public bool markOn
    {
        get
        {
            return _markOn;
        }
    }

    public string destinationInfo {get; set;}

    private void Start()
    {
        _point = Instantiate(Resources.Load<GameObject>("Prefabs/Point"), new Vector3(1000, 1000), Quaternion.identity) as GameObject;
        detector = FindObjectOfType<MouseClickDetector>();
        MouseClickDetector.onMissClick += () => SetDestination(World.GetMousePosition(), true);
        World.ship = this;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        //{
        //    _point.transform.position = World.GetMousePosition();
        //    _destination = _point.transform.position;
        //    go = true;
        //    transform.rotation = World.LookAngle(transform.position, _point.transform.position);
        //}

        if (Vector2.Distance(transform.position, destination) < 0.1f)   go = false;
    }

    private void FixedUpdate()
    {
        if (go)
        {
            transform.position += transform.up * inventory.ship.speed * Time.fixedDeltaTime;    
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UnsetDestination();

        Island island = collision.gameObject.GetComponent<Island>();

        if (island)
        {
            OpenIslandWindow();
        }
    }

    private void OnGUI()
    {
        if (_markOn)
        {
            float x = Screen.width - 300;
            float y = Screen.height - 300;

            GUI.Box(new Rect (x, y, Screen.width - x, Screen.height - y), Vector2.Distance(transform.position, _destination).ToString() + "\n" + destinationInfo);
        }
    }

    private void UnsetDestination()
    {
        go = false;
        _markOn = false;
        _point.SetActive(false);
    }

    private void OpenIslandWindow()
    {
        islandWindow.SetActive(true);
        detector.enabled = false;
    }

    public void CloseIslandWindow()
    {
        islandWindow.SetActive(false);
        detector.enabled = true;
    }

    public void SetDestination(Vector3 destination, bool placePoint)
    {
        SetDestination(destination, placePoint, string.Empty);
    }

    public void SetDestination(Vector3 destination, bool placePoint, string destinationInfo)
    {
        _destination = destination;
        _point.SetActive(placePoint);
        _markOn              = true;
        this.destinationInfo = destinationInfo;
        
        if (placePoint)
        {
            _point.transform.position = destination;
        }
    }

    public void MoveToDestination()
    {
        _markOn = false;
        go      = true;
        _point.SetActive(false);
        transform.rotation = World.LookAngle(transform.position, destination);
    }
}