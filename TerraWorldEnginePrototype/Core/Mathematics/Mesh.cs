using System.Numerics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering;

namespace TerraWorldEnginePrototype.Core.Mathematics
{
    public class Mesh
    {
        public Vector3[]? Vertices { get; set; }
        public Vector3[]? Normals { get; set; }
        public Vector2[]? UVs { get; set; }
        public uint[]? Indices { get; set; }
        public Color[]? Colors { get; set; }

        public GraphicsBuffer? VertexBuffer { get; set; }
        public GraphicsBuffer? IndexBuffer { get; set; }

        public Mesh() { }

        public void Upload()
        {
            // Upload the mesh to the GPU

        }
    }
}
