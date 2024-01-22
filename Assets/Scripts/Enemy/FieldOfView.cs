using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private Mesh mesh;
    private float fov; 
    private Vector3 origin = Vector3.zero;
    private float startingAngle;

    [SerializeField] private Vector3 vertex;
    [SerializeField] private Vector3 enemyPosition;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 90f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(origin, vertex);
    }

    private void LateUpdate()
    {
        int raycount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / raycount;
        float viewDistance = 3;

        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= raycount; i++)
        {
            vertex = origin + GetVectorFromAngle(angle) * viewDistance;

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;

            angle -= angleIncrease;

            //RaycastHit2D raycastHit2D = Physics2D.Raycast(enemyPosition, GetVectorFromAngle(angle), viewDistance, layerMask);

            //if (raycastHit2D.collider != null)
            //{
            //    GameManager.Instance.GameOver();
            //}
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (n < 0) n += 360;

        return n;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetEnemyPosition(Vector3 enemyPosition)
    {
        this.enemyPosition = enemyPosition;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }
}


