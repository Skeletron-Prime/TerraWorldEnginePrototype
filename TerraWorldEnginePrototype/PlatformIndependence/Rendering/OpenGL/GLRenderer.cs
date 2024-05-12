using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLRenderer : Renderer
    {
        public override void Draw(Mesh mesh)
        {
            UploadMeshData(mesh);


        }

        private void UploadMeshData(Mesh mesh)
        {
            GLBuffer<Vector3> vertexBuffer = new GLBuffer<Vector3>();
            vertexBuffer.BufferData(mesh.Vertices, BufferUsage.StaticDraw);

            GLBuffer<Vector3> normalBuffer = new GLBuffer<Vector3>();
            normalBuffer.BufferData(mesh.Normals, BufferUsage.StaticDraw);

            GLBuffer<Vector2> uvBuffer = new GLBuffer<Vector2>();
            uvBuffer.BufferData(mesh.UVs, BufferUsage.StaticDraw);

            GLBuffer<uint> indexBuffer = new GLBuffer<uint>();
            indexBuffer.BufferData(mesh.Indices, BufferUsage.StaticDraw);

            GLBuffer<Color> colorBuffer = new GLBuffer<Color>();
            colorBuffer.BufferData(mesh.Colors, BufferUsage.StaticDraw);
        }
    }
}