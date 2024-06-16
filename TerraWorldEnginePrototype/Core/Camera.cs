using System.Numerics;

namespace TerraWorldEnginePrototype.Core
{
    public class Camera
    {
        public Transform Transform { get; set; } = new Transform()
        {
            Position = new Vector3(0, 0, 5),
            Rotation = Quaternion.Identity
        };
        public Matrix4x4 ProjectionMatrix { get; set; }
        public Matrix4x4 ViewMatrix { get; set; }

        public Camera()
        {
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4, 3840 / 1600, 0.1f, 100.0f);
            ViewMatrix = Matrix4x4.CreateLookAt(Transform.Position, Vector3.Zero, Vector3.UnitY);
        }
    }
}
