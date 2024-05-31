using System.Numerics;

namespace TerraWorldEnginePrototype.Core
{
    public struct Transform
    {
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Scale { get; set; } = Vector3.One;
        public Quaternion Rotation { get; set; } = Quaternion.Identity;

        public Transform() { }

        public Matrix4x4 GetModelMatrix()
        {
            return Matrix4x4.CreateFromQuaternion(Rotation) * Matrix4x4.CreateTranslation(Position) * Matrix4x4.CreateScale(Scale);
        }
    }
}
