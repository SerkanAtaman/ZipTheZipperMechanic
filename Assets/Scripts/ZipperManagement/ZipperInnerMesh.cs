using UnityEngine;

namespace ZipTheZipper.ZipperManagement
{
    public class ZipperInnerMesh : MonoBehaviour
    {
        private MeshFilter _filter;

        private Vector3[] _vertices;
        private int[] _triangles;

        private int _triangleCount;
        private int _verticesCount;

        private void Awake()
        {
            _filter = GetComponent<MeshFilter>();
        }

        public void GenerateInnerMesh(Vector3[] leftSidePositions, Vector3[] rightSidePositions)
        {
            int vertices = leftSidePositions.Length + rightSidePositions.Length;
            _vertices = new Vector3[vertices];
            _verticesCount = vertices;

            for(int i = 0; i < vertices; i++)
            {
                if (i % 2 == 0) _vertices[i] = leftSidePositions[i / 2];
                else _vertices[i] = rightSidePositions[i / 2];
            }

            _triangleCount = (vertices - 2) * 3;
            _triangles = new int[_triangleCount];

            int triangleID = 0;
            for (int i = 0; i < vertices - 3; i += 2)
            {
                _triangles[triangleID] = i;
                triangleID++;
                _triangles[triangleID] = i + 2;
                triangleID++;
                _triangles[triangleID] = i + 1;
                triangleID++;
                _triangles[triangleID] = i + 2;
                triangleID++;
                _triangles[triangleID] = i + 3;
                triangleID++;
                _triangles[triangleID] = i + 1;
                triangleID++;
            }

            Vector2[] uvs = new Vector2[vertices];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(_vertices[i].x, _vertices[i].z);
            }

            Mesh mesh = _filter.mesh;
            mesh.vertices = _vertices;
            mesh.triangles = _triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            _filter.mesh = mesh;
        }

        public void UpdateInnerMesh(Vector3[] leftSidePositions, Vector3[] rightSidePositions)
        {
            for (int i = 0; i < _verticesCount; i++)
            {
                if (i % 2 == 0) _vertices[i] = leftSidePositions[i / 2];
                else _vertices[i] = rightSidePositions[i / 2];
            }

            int triangleID = 0;
            for (int i = 0; i < _verticesCount - 3; i += 2)
            {
                _triangles[triangleID] = i;
                triangleID++;
                _triangles[triangleID] = i + 2;
                triangleID++;
                _triangles[triangleID] = i + 1;
                triangleID++;
                _triangles[triangleID] = i + 2;
                triangleID++;
                _triangles[triangleID] = i + 3;
                triangleID++;
                _triangles[triangleID] = i + 1;
                triangleID++;
            }

            Vector2[] uvs = new Vector2[_verticesCount];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(_vertices[i].x, _vertices[i].z);
            }

            Mesh mesh = _filter.mesh;
            mesh.vertices = _vertices;
            mesh.triangles = _triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            _filter.mesh = mesh;
        }
    }
}