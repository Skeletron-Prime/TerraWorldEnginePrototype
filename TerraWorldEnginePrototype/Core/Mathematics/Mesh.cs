using System.Numerics;
namespace TerraWorldEnginePrototype.Core.Mathematics
{
    public class Mesh
    {
        public Vector3[] Vertices;

        public Mesh(Vector3[] vertices)
        {
            Vertices = vertices;
        }
    }
}
