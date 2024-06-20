using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.Graphics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.Core
{
    public class EngineWindow : NativeWindow
    {
        public EngineWindow(WindowSettings windowSettings, Input input) : base(windowSettings, input)
        {
        }

        protected override void OnLoad()
        {
            CubeObject cube = new CubeObject(new Transform()
            {
                Position = new Vector3(-2, -2, -2),
                Rotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(45f)))
            });

            Scene.AddObject(cube);

            CubeObject cube2 = new CubeObject(new Transform()
            {
                Position = new Vector3(7, 7, 7),
                Rotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(45f)))
            });

            Scene.AddObject(cube2);

            GameObject terrain = new GameObject(new TerrainMesh(1028, 1028))
            {
                Transform = new Transform()
                {
                    Position = new Vector3(-64, 10, -64)
                }
            };

            Scene.AddObject(terrain);

            //TetrahedronObject tetrahedronObject = new TetrahedronObject(new Transform()
            //{
            //    Position = new Vector3(0, 2, 0),
            //    Rotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(25f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(25f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(25f)))
            //});

            //Scene.AddObject(tetrahedronObject);

            Mesh mesh = MeshLoader.Load("E:\\Blender Models\\Monkey.obj");
            GameObject monkey = new GameObject(mesh);

            Scene.AddObject(monkey);

            //DirectionalLight light = new DirectionalLight(Color.White, -Vector3.UnitY, new Vector3(0, 1, 0));

            //Scene.AddObject(light);

            PointLight pointLight = new PointLight(new Vector3(0, 20, 0), Color.White, 2.5f);
            Scene.AddObject(pointLight);

            base.OnLoad();
        }
    }
}
