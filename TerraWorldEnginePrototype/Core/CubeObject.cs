using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;

namespace TerraWorldEnginePrototype.Core
{
    public class CubeObject : GameObject
    {
        public static Mesh SMesh { get; } = new Mesh
        {
            Vertices = [
                // Front face
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f,  1.0f),

                // Back face
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),

                // Left face
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),

                // Right face
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),

                // Top face
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),

                // Bottom face
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f,  1.0f)
            ],

            Colors =
            [
                // Front face
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,

                // Back face
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,

                // Left face
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,

                // Right face
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,

                // Top face
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,

                // Bottom face
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan
            ]
        };

        public CubeObject(Transform transform) : base(SMesh)
        {
            Transform = transform;
        }
    }
}
