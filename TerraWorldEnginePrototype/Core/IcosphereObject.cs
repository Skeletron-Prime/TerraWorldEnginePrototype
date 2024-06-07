using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;

namespace TerraWorldEnginePrototype.Core
{
    public class IcosphereObject : GameObject
    {
        public static Mesh SMesh { get; } = new Mesh
        {
            Vertices =
            [
                new Vector3(-0.525731112119133606f, 0.0f, 0.850650808352039932f),
                new Vector3(0.525731112119133606f, 0.0f, 0.850650808352039932f),
                new Vector3(-0.525731112119133606f, 0.0f, -0.850650808352039932f),
                new Vector3(0.525731112119133606f, 0.0f, -0.850650808352039932f),
                new Vector3(0.0f, 0.850650808352039932f, 0.525731112119133606f),
                new Vector3(0.0f, 0.850650808352039932f, -0.525731112119133606f),
                new Vector3(0.0f, -0.850650808352039932f, 0.525731112119133606f),
                new Vector3(0.0f, -0.850650808352039932f, -0.525731112119133606f),
                new Vector3(0.850650808352039932f, 0.525731112119133606f, 0.0f),
                new Vector3(-0.850650808352039932f, 0.525731112119133606f, 0.0f),
                new Vector3(0.850650808352039932f, -0.525731112119133606f, 0.0f),
                new Vector3(-0.850650808352039932f, -0.525731112119133606f, 0.0f)
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
                Color.Black,
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow
            ],

            Indices =
            [
                0, 1, 4,
                0, 4, 9,
                9, 4, 5,
                4, 8, 5,
                4, 1, 8,
                8, 1, 10,
                8, 10, 3,
                5, 8, 3,
                5, 3, 2,
                2, 3, 7,
                7, 3, 10,
                7, 10, 6,
                7, 6, 11,
                11, 6, 0,
                0, 6, 1,
                6, 10, 1,
                9, 11, 0,
                9, 2, 11,
                9, 5, 2,
                7, 11, 2
            ]
        };

        public IcosphereObject(Transform transform) : base(SMesh)
        {
            Transform = transform;
        }
    }
}
