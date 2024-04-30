using System.Numerics;

namespace TerraWorldEnginePrototype.Core
{
    public class Transform : Component
    {
        public Vector3 Location { get; set; }
        public Vector3 Scale { get; set; }
        public Quaternion Rotation { get; set; }
    }
}
