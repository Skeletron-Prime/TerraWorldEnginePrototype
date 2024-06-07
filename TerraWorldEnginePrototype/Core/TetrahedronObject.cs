using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;

namespace TerraWorldEnginePrototype.Core
{
    public class TetrahedronObject : GameObject
    {
        public static Mesh SMesh { get; } = new Mesh
        {
            Vertices =
            [
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.942809f, -0.333333f),
                new Vector3(-0.816497f, -0.471405f, -0.333333f),
                new Vector3(0.816497f, -0.471405f, -0.333333f)
            ],

            Colors =
            [
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow
            ],

            Indices =
            [
                0, 1, 2,
                0, 2, 3,
                0, 3, 1,
                1, 3, 2
            ]
        };

        public TetrahedronObject(Transform transform) : base(SMesh)
        {
            Transform = transform;
        }
    }
}
