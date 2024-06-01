using System.Numerics;

namespace TerraWorldEnginePrototype.Core
{
    public class Camera
    {
        public Matrix4x4 ProjectionMatrix { get; set; }
        public Matrix4x4 ViewMatrix { get; set; }

        public Camera()
        {
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4, 3840 / 1600, 0.1f, 100.0f);
            ViewMatrix = Matrix4x4.CreateLookAt(new Vector3(-5, -5, -5), Vector3.Zero, Vector3.UnitY);
        }
    }
}
