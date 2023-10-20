using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] LayerMask[] layerMask;
    [SerializeField] LayerMask layerMaskTarget;
    private Mesh mesh;

    private Vector3 origin;
    private float fov;
    private float startAngle;
    private GhostMovement ghostMovement;

    // Start is called before the first frame update
    private void Start() {
        fov = 60f;
        startAngle = 30f;

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    private void LateUpdate() {
        int rayCount = 50;
        float angle = startAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 4f;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        bool targetInView = false;
        for (int i = 0; i <= rayCount; i++)
        {
            RaycastHit2D[] raycastHit2D = new RaycastHit2D[layerMask.Length];
            for (int j = 0; j < layerMask.Length; j++) {
                raycastHit2D[j] = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask[j]);
            }

            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            for (int j = 0; j < raycastHit2D.Length; j++) {
                if (raycastHit2D[j].collider != null && Vector2.Distance(origin, raycastHit2D[j].point) < Vector2.Distance(origin, vertex)) {
                    vertex = raycastHit2D[j].point;
                }
            }

            RaycastHit2D raycastHitTarget = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMaskTarget);
            if (raycastHitTarget.collider != null && Vector2.Distance(origin, raycastHitTarget.point) < Vector2.Distance(origin, vertex)) {
                targetInView = true;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;                
            }

            vertexIndex++;
            angle -= angleIncrease;

        }

        ghostMovement.SetChase(targetInView);

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }

    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }

    public void SetDirection(Vector3 direction) {
        startAngle = GetAngleFromVectorFloat(direction) + fov / 2f;
    }

    public void SetGhostMovement(GhostMovement ghostMovement){
        this.ghostMovement = ghostMovement;
    }

    private static Vector3 GetVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
