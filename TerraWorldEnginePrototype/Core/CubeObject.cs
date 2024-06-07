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
                new Vector3(-1f, -1f, -1f),
                new Vector3(1f, -1f, -1f),
                new Vector3(1f, 1f, -1f),
                new Vector3(-1f, 1f, -1f),
                new Vector3(-1f, -1f, 1f),
                new Vector3(1f, -1f, 1f),
                new Vector3(1f, 1f, 1f),
                new Vector3(-1f, 1f, 1f)
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
