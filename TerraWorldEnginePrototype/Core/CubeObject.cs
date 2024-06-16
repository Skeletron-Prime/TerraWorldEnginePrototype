using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;

namespace TerraWorldEnginePrototype.Core
{
    public class CubeObject : GameObject
    {
        public static Mesh SMesh { get; } = new Mesh
        {
            Vertices =
            [
                new Vector3(-0.5f, -0.5f, -0.5f), // 0
                new Vector3(0.5f, -0.5f, -0.5f), // 1
                new Vector3(0.5f, 0.5f, -0.5f), // 2
                new Vector3(-0.5f, 0.5f, -0.5f), // 3
                new Vector3(-0.5f, -0.5f, 0.5f), // 4
                new Vector3(0.5f, -0.5f, 0.5f), // 5
                new Vector3(0.5f, 0.5f, 0.5f), // 6
                new Vector3(-0.5f, 0.5f, 0.5f) // 7
            ],

            Colors =
            [
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Purple,
                Color.Cyan,
                Color.White,
                Color.Black
            ],

            Normals =
            [
                new Vector3(-0.577f, -0.577f, -0.577f), // 0
                new Vector3(0.577f, -0.577f, -0.577f), // 1
                new Vector3(0.577f, 0.577f, -0.577f), // 2
                new Vector3(-0.577f, 0.577f, -0.577f), // 3
                new Vector3(-0.577f, -0.577f, 0.577f), // 4
                new Vector3(0.577f, -0.577f, 0.577f), // 5
                new Vector3(0.577f, 0.577f, 0.577f), // 6
                new Vector3(-0.577f, 0.577f, 0.577f) // 7
            ],

            Indices =
            [
                0, 1, 2, 2, 3, 0,
                1, 5, 6, 6, 2, 1,
                5, 4, 7, 7, 6, 5,
                4, 0, 3, 3, 7, 4,
                3, 2, 6, 6, 7, 3,
                4, 5, 1, 1, 0, 4
            ]
        };

        public CubeObject(Transform transform) : base(SMesh)
        {
            Transform = transform;
        }
    }
}
