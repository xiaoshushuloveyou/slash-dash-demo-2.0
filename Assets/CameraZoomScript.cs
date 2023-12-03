using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomScript : MonoBehaviour
{

    private float zoom;
    private float zoomMultiplier=4f;
    private float minZoom = 10f;
    private float maxZoom = 12f;
    private float velocity = 0f;
    private float smoothTimeZoom = 0.25f;


    [SerializeField] private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        zoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            zoom -= Time.deltaTime*zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTimeZoom);
        }
        else
        {
            zoom += Time.deltaTime * zoomMultiplier *1.5f;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTimeZoom);
        }
    }
}
