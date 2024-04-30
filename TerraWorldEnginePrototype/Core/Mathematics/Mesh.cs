using System.Numerics;

namespace TerraWorldEnginePrototype.Core.Mathematics
{
    public struct Mesh
    {
        public List<Vector3> Vertices;
        public List<Vector3> Normals;
        public List<Vector2> UVs;
        public List<int> Indices;
    }
}
