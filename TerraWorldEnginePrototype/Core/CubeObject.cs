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
