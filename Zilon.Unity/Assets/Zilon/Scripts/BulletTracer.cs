using UnityEngine;

public class BulletTracer : MonoBehaviour
{
    int frame = 0;
    float framerate = .016f;
    float timer;

    public Vector3 targetPosition;
    public Vector3 fromPosition;
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer.SetPosition(0, fromPosition + Vector3.back * 5);
        lineRenderer.SetPosition(1, targetPosition + Vector3.back * 5);

        timer = framerate;
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            frame++;
            timer += framerate;
            if (frame >= 4)
            {
                Destroy(gameObject);
                
            }
            else
            {
                //worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame, 0, 16, 256));
            }
        }
    }
}
