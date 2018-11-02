using UnityEngine;

public class MouseRts : MonoBehaviour
{
    private const int LevelArea = 100;

    private const int ScrollArea = 25;
    private const int ScrollSpeed = 25;
    private const int DragSpeed = 100;

    private const int ZoomSpeed = 25;
    private const int ZoomMin = 25;
    private const int ZoomMax = 100;

    private const int PanSpeed = 50;
    private const int PanAngleMin = 50;
    private const int PanAngleMax = 80;

    public Camera Camera;

    // Update is called once per frame
    void Update()
    {
        // Init camera translation for this frame.
        var translation = Vector3.zero;

        //// Zoom in or out
        //var zoomDelta = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * Time.deltaTime;
        //if (zoomDelta != 0)
        //{
        //    translation -= Vector3.up * ZoomSpeed * zoomDelta;
        //}

        //// Start panning camera if zooming in close to the ground or if just zooming out.
        //var pan = Camera.transform.eulerAngles.x - zoomDelta * PanSpeed;
        //pan = Mathf.Clamp(pan, PanAngleMin, PanAngleMax);
        //if (zoomDelta < 0 || Camera.transform.position.y < (ZoomMax / 2))
        //{
        //    Camera.transform.eulerAngles = new Vector3(pan, 0, 0);
        //}

        // Move camera with arrow keys
        translation += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        // Move camera with mouse
        if (Input.GetMouseButton(2)) // MMB
        {
            // Hold button and drag camera around
            translation -= new Vector3(Input.GetAxis("Mouse X") * DragSpeed * Time.deltaTime,
                Input.GetAxis("Mouse Y") * DragSpeed * Time.deltaTime, 0);
        }
        else
        {
            // Move camera if mouse pointer reaches screen borders
            if (Input.mousePosition.x < ScrollArea)
            {
                translation += Vector3.right * -ScrollSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.x >= Screen.width - ScrollArea)
            {
                translation += Vector3.right * ScrollSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.y < ScrollArea)
            {
                translation += Vector3.up * -ScrollSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.y > Screen.height - ScrollArea)
            {
                translation += Vector3.up * ScrollSpeed * Time.deltaTime;
            }
        }

        // Keep camera within level and zoom area
        var desiredPosition = Camera.transform.position + translation;
        //if (desiredPosition.x < -LevelArea || LevelArea < desiredPosition.x)
        //{
        //    translation.x = 0;
        //}
        //if (desiredPosition.y < ZoomMin || ZoomMax < desiredPosition.y)
        //{
        //    translation.y = 0;
        //}
        //if (desiredPosition.z < -LevelArea || LevelArea < desiredPosition.z)
        //{
        //    translation.z = 0;
        //}

        // Finally move camera parallel to world axis
        Camera.transform.position += translation;
    }
}