using UnityEngine;

public class BulletTracer : MonoBehaviour
{
    int frame = 0;
    float framerate = .016f;
    float timer;

    public Material weaponTracerMaterial;
    public Vector3 targetPosition;
    public Vector3 fromPosition;
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer.SetPosition(0, fromPosition + Vector3.back * 5);
        lineRenderer.SetPosition(1, targetPosition + Vector3.back * 5);

        //var dir = (targetPosition - fromPosition).normalized;
        //var eulerZ = Vector3.Angle(dir, transform.forward) - 90;
        //var distance = Vector3.Distance(fromPosition, targetPosition);
        //var tracerSpawnPosition = fromPosition + dir * distance * .5f;


        //var tmpWeaponTracerMaterial = new Material(weaponTracerMaterial);
        //tmpWeaponTracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance / 256f));
        //World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPosition, eulerZ, 6f, distance, tmpWeaponTracerMaterial, null, 10000);

        
        timer = framerate;

        //worldMesh.SetUVCoords(new World_Mesh.UVCoords(0, 0, 16, 256));
        
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
                //Destroy(gameObject);
                
            }
            else
            {
                //worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame, 0, 16, 256));
            }
        }
    }
}
