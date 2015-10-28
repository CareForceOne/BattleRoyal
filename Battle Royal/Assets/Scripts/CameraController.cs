using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform Player;

    public BoxCollider2D bounds;

    public Vector2
        Margin,
        smoothing;

    private Vector3
        _min,
        _max;

    public bool IsFollowing { get; set;}


	void Start () {
        _min = bounds.bounds.min;
        _max = bounds.bounds.max;
        IsFollowing = true;
    }
	
	// Update is called once per frame
	void Update () {
        var x = transform.position.x;
        var y = transform.position.y;

        if (IsFollowing)
        {
            if (Mathf.Abs(x - Player.position.x) > Margin.x)
                x = Mathf.Lerp(x, Player.position.x, smoothing.x * Time.deltaTime);
        
        if (Mathf.Abs(y - Player.position.y) > Margin.y)
            y = Mathf.Lerp(y, Player.position.y, smoothing.y * Time.deltaTime);
    }

        var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, _min.y + cameraHalfWidth, _max.y - GetComponent<Camera>().orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
