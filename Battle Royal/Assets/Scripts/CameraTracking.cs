using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class determines the size of the bounding box that follows all players,
//this allows the camera to feel dynamic so the players can get a better feel for the battle
public class CameraTracking : MonoBehaviour
{

    [SerializeField]
    public List<GameObject> targets = new List<GameObject>();
    public GameObject[] units;
    [SerializeField]
    float boundingBoxPadding = 2f;

    [SerializeField]
    float minimumOrthographicSize = 8f;

    [SerializeField]
    float zoomSpeed = 20f;

    Camera camera;
    
    
    void Update() {

        UpdateUnits();

        foreach (GameObject myUnit in units)  {
            
            Debug.Log("Adding unit.");
            Debug.Log("This unit is: " + myUnit.name);
            targets = new List<GameObject>();
            targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        }
    }

    void UpdateUnits() {
       units = GameObject.FindGameObjectsWithTag("Player");
    }


    void Awake()
    {
        camera = GetComponent<Camera>();
        camera.orthographic = true;
    }

    void LateUpdate()
    {
        Rect boundingBox = CalculateTargetsBoundingBox();
        transform.position = CalculateCameraPosition(boundingBox);
        camera.orthographicSize = CalculateOrthographicSize(boundingBox);
    }

    /// <summary>
    /// Calculates how large the camera view should be in comparsion to the players
    /// </summary>
    /// <returns>A Rect containing all the targets.</returns>
    Rect CalculateTargetsBoundingBox()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        if(targets.Count == 0)
        {
            camera.orthographicSize = 1;
        }

        foreach (GameObject target in targets)
        {
            Vector3 position = GameObject.Find("Player").transform.position;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxX = Mathf.Max(maxX, position.x);
            maxY = Mathf.Max(maxY, position.y);
        }

        return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
    }



    /// <summary>
    /// Calculates the center position of where all players will fit into the camera
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A Vector3 in the center of the bounding box.</returns>
    Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        Vector2 boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, camera.transform.position.z);
    }

    /// <summary>
    /// Calculates a new orthographic size for the camera based on the target bounding box.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A float for the orthographic size.</returns>
    float CalculateOrthographicSize(Rect boundingBox)
    {
        float orthographicSize = camera.orthographicSize;
        Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
        Vector3 topRightAsViewport = camera.WorldToViewportPoint(topRight);

        if (topRightAsViewport.x >= topRightAsViewport.y)
            orthographicSize = Mathf.Abs(boundingBox.width) / camera.aspect / 2f;
        else
            orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

        return Mathf.Clamp(Mathf.Lerp(camera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
    }

}