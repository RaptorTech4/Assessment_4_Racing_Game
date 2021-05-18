using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RoadSegment : MonoBehaviour
{

    [SerializeField] Mesh2D _shape2D;

    [Range(2, 32)]
    [SerializeField] int _edgeRingCount = 8;

    Mesh _mesh;

    [Range(0, 1)]
    [SerializeField] float _t = 0f;
    [SerializeField] Transform[] _ControlPoints = new Transform[4];
    Vector3 _GetPos(int i) => _ControlPoints[i].position;



    private void Awake()
    {
        _mesh = new Mesh();
        _mesh.name = "Segment";
        GetComponent<MeshFilter>().sharedMesh = _mesh;
    }

    private void Update() => GenerateMesh();

    void GenerateMesh()
    {
        _mesh.Clear();

        List<Vector3> verts = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        for (int ring = 0; ring < _edgeRingCount; ring++)
        {
            float t = ring / (_edgeRingCount - 1f);
            OrientedPoint op = GetBezierOP(t);
            for (int i = 0; i < _shape2D.VertexCount; i++)
            {
                verts.Add(op.LocalToWorldpos(_shape2D.vertices[i].points));
                normals.Add(op.LocalToWorldVec(_shape2D.vertices[i].normal));
            }
        }

        List<int> triIndices = new List<int>();
        for (int ring = 0; ring < _edgeRingCount - 1; ring++)
        {
            int rootIndex = ring * _shape2D.VertexCount;
            int rootIndexNext = (ring + 1) * _shape2D.VertexCount;

            for (int Line = 0; Line < _shape2D.LineCount; Line += 2)
            {
                int linIndexA = _shape2D.lineIndices[Line];
                int linIndexB = Line + 1;

                int currentA = rootIndex + linIndexA;
                int currentB = rootIndex + linIndexB;
                int nextA = rootIndexNext + linIndexA;
                int nextB = rootIndexNext + linIndexB;

                triIndices.Add(currentA);
                triIndices.Add(nextA);
                triIndices.Add(nextB);

                triIndices.Add(currentA);
                triIndices.Add(nextB);
                triIndices.Add(currentB);
            }
        }

        _mesh.SetVertices(verts);
        _mesh.SetNormals(normals);
        _mesh.SetTriangles(triIndices, 0);
    }

    public void OnDrawGizmos()
    {
        /*
        for (int i = 0; i < _ControlPoints.Length; i++)
        {
            Gizmos.DrawSphere(_GetPos(i), 0.1f);
        }

        Handles.DrawBezier(
            _GetPos(0),
            _GetPos(3),
            _GetPos(1),
            _GetPos(2),
            Color.white, EditorGUIUtility.whiteTexture, 1f);

        OrientedPoint testPoint = GetBezierOP(_t);

        Handles.PositionHandle(testPoint.pos, testPoint.rot);

        Vector3[] verts = _shape2D.vertices.Select(v => testPoint.LocalToWorldpos(v.points)).ToArray();
        for (int i = 0; i < _shape2D.lineIndices.Length; i += 2)
        {
            Vector3 a = verts[_shape2D.lineIndices[i]];
            Vector3 b = verts[_shape2D.lineIndices[i + 1]];
            Gizmos.DrawLine(a, b);
        }
        */
    }

    OrientedPoint GetBezierOP(float t)
    {

        Vector3 p0 = _GetPos(0);
        Vector3 p1 = _GetPos(1);
        Vector3 p2 = _GetPos(2);
        Vector3 p3 = _GetPos(3);

        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 pos = Vector3.Lerp(d, e, t);
        Vector3 tangent = (e - d).normalized;

        return new OrientedPoint(pos, tangent);
    }
}
