using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class determines the size of the bounding box that follows all players,
//this allows the camera to feel dynamic so the players can get a better feel for the battle
public class CameraTracking : MonoBehaviour
{

    [SerializeField]
    public GameObject[] targets = new GameObject[1];
    [SerializeField]
    float boundingBoxPadding =6f;

    [SerializeField]
    float minimumOrthographicSize = 12f;

    [SerializeField]
    float zoomSpeed = 20f;

    Camera camera;
    int counter = 0;

    void Update()  {
        UpdateUnits();
        //when there is a gameObject  myUnits in the game then  it goes through this loop
        foreach (GameObject myUnit in targets)  {
            //adds 1 to counter, which keeps track of how many players there are
            counter +=1;
            //then it recreates targets of [counter] GameObjects
            targets = new GameObject[counter];
            //afterwards it adds all the GameObjects with the tag Player into the array
            targets = GameObject.FindGameObjectsWithTag("Player");
        }
    }

    //this method checks every frame for an object called Player
    void UpdateUnits()  {
        targets = GameObject.FindGameObjectsWithTag("Player");
    }


    void Awake() {
        camera = GetComponent<Camera>();
        camera.orthographic = true;
    }

    void LateUpdate() {
        Rect boundingBox = CalculateTargetsBoundingBox();
        transform.position = CalculateCameraPosition(boundingBox);
        camera.orthographicSize = CalculateOrthographicSize(boundingBox);
    }

    /// <summary>
    /// Calculates how large the camera view should be in comparsion to the players
    /// </summary>
    /// <returns>A Rect containing all the targets.</returns>
    Rect CalculateTargetsBoundingBox() {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        //if there currently isn't any players on the board it will update once players join the game
        if (counter == 0){
            Vector3 position = GameObject.Find("Player").transform.position;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxX = Mathf.Max(maxX, position.x);
            maxY = Mathf.Max(maxY, position.y);
        }

        foreach (GameObject target in targets){
            Vector3 position = target.transform.position;

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