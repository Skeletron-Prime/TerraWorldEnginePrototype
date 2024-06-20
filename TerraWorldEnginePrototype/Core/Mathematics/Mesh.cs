using Assimp;
using System.Numerics;
namespace TerraWorldEnginePrototype.Core.Mathematics
{
    public class Mesh
    {
        public readonly bool IsWriteable = true;
        public readonly bool IsReadable = true;

        private Vector3[]? vertices;
        private Vector3[]? normals;
        private Color[]? colors;
        private uint[]? indices;

        public Vector3[] Vertices
        {
            get => vertices ?? [];
            set
            {
                if (!IsWriteable)
                    return;

                var needChange = vertices == null || vertices.Length != value.Length;
                vertices = value;

                if (needChange)
                {
                    colors = null;
                }
            }
        }

        public Vector3[] Normals
        {
            get => ReadVertexData(normals ?? []);
            set => WriteVertexData(ref normals, value, value.Length);
        }

        public Color[] Colors
        {
            get => ReadVertexData(colors ?? []);
            set => WriteVertexData(ref colors, value, value.Length);
        }

        public uint[] Indices
        {
            get => ReadVertexData(indices ?? []);
            set => WriteVertexData(ref indices, value, value.Length, false);
        }

        public int VertexCount => vertices?.Length ?? 0;
        public int IndexCount => indices?.Length ?? 0;

        public bool HasColors => (colors?.Length ?? 0) > 0;
        public bool HasNormals => (normals?.Length ?? 0) > 0;

        public Mesh() { }

        private T ReadVertexData<T>(T value)
        {
            if (!IsReadable)
            {
                throw new Exception("Mesh is not readable");
            }

            return value;
        }

        private void WriteVertexData<T>(ref T target, T value, int length, bool mustMatchLength = true)
        {
            if (!IsWriteable)
            {
                throw new Exception("Mesh is not writeable");
            }

            if (value == null || length <= 0 || length != (vertices?.Length ?? 0) && mustMatchLength)
            {
                throw new Exception("Array length should match vertices length");
            }

            target = value;
        }
    }

    public class MeshLoader
    {
        public static Mesh Load(string path)
        {
            // Load mesh using assimp 
            var context = new AssimpContext();

            var scene = context.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.GenerateSmoothNormals);

            if (scene == null || scene.SceneFlags == SceneFlags.Incomplete)
            {
                throw new Exception("Failed to load mesh");
            }

            var vertices = new List<Vector3>();
            var indices = new List<uint>();
            var normals = new List<Vector3>();

            foreach (var mesh in scene.Meshes)
            {
                foreach (var vertex in mesh.Vertices)
                {
                    vertices.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
                }

                foreach (var face in mesh.Faces)
                {
                    foreach (var index in face.Indices)
                    {
                        indices.Add((uint)index);
                    }
                }

                foreach (var normal in mesh.Normals)
                {
                    normals.Add(new Vector3(normal.X, normal.Y, normal.Z));
                }
            }

            return new Mesh
            {
                Vertices = vertices.ToArray(),
                Indices = indices.ToArray(),
                Normals = normals.ToArray()
            };
        }
    }

    public class TerrainMesh : Mesh
    {
        public TerrainMesh(int width, int height)
        {
            var vertices = new Vector3[width * height];
            var indices = new uint[(width - 1) * (height - 1) * 6];
            var normals = new Vector3[width * height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    vertices[i * width + j] = new Vector3(i, 0, j);
                    normals[i * width + j] = new Vector3(0, 1, 0);
                }
            }

            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    indices[(i * (width - 1) + j) * 6] = (uint)(i * width + j);
                    indices[(i * (width - 1) + j) * 6 + 1] = (uint)(i * width + j + 1);
                    indices[(i * (width - 1) + j) * 6 + 2] = (uint)((i + 1) * width + j + 1);
                    indices[(i * (width - 1) + j) * 6 + 3] = (uint)(i * width + j);
                    indices[(i * (width - 1) + j) * 6 + 4] = (uint)((i + 1) * width + j + 1);
                    indices[(i * (width - 1) + j) * 6 + 5] = (uint)((i + 1) * width + j);
                }
            }

            Vertices = vertices;
            Indices = indices;
            Normals = normals;
        }
    }
}
