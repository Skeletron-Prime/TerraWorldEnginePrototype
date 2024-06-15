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
            //CubeObject cube = new CubeObject(new Transform()
            //{
            //    Position = new Vector3(0, -2, 0),
            //    Rotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(45f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(45f)))
            //});

            //Scene.AddObject(cube);

            //TetrahedronObject tetrahedronObject = new TetrahedronObject(new Transform()
            //{
            //    Position = new Vector3(0, 2, 0),
            //    Rotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(25f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(25f)) * Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(25f)))
            //});

            //Scene.AddObject(tetrahedronObject);

            Mesh mesh = MeshLoader.Load("E:\\Blender Models\\Monkey.obj");
            GameObject monkey = new GameObject(mesh);

            Scene.AddObject(monkey);

            base.OnLoad();
        }
    }
}
