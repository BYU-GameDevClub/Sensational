using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewConeScript : MonoBehaviour
{
    [SerializeField] Material viewConeMaterialPrefab;
    [SerializeField] GameObject viewConeObject;
    Material viewConeMaterial;
    float viewConeDistance = 5;
    float halfConeAngle = 45;

    public MeshFilter meshFilter;
    Mesh viewMesh;

    // Start is called before the first frame update
    void Start()
    {
        viewConeMaterial = new Material(viewConeMaterialPrefab);
        viewConeObject.GetComponent<Renderer>().material = viewConeMaterial;
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        meshFilter.mesh = viewMesh;
    }

    void ShowViewCone()
    {
        int rayNum = Mathf.RoundToInt(halfConeAngle) * 2;
        List<Vector3> viewPoints = new List<Vector3>();
        Vector3[] vertices = new Vector3[rayNum + 2];
        int[] triangles = new int[(rayNum - 1) * 3];
        vertices[0] = Vector3.zero;
        for(int i = 0; i <= rayNum; i++)
        {
            viewPoints.Add(ViewCast(i - halfConeAngle));
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if(i < rayNum - 1)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    Vector3 ViewCast(float angle)
    {
        float angleToZero = Mathf.Acos(Vector3.Dot(Vector3.forward, transform.forward) / (Vector3.forward.magnitude * transform.forward.magnitude));
        if(transform.forward.x >= 0)
        {
            angle += angleToZero * Mathf.Rad2Deg;
        }
        else
        {
            angle -= angleToZero * Mathf.Rad2Deg;
        }
        Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        direction = direction.normalized;
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, viewConeDistance);
        if(hit.point != Vector3.zero)
        {
            return hit.point;
        }
        else
        {
            Vector3 endPoint = transform.position + direction * viewConeDistance;
            return endPoint;
        }
    }
}
