using System.Diagnostics;
using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.Graphics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.Core
{
    public class EngineWindow : NativeWindow
    {
        EngineObject obj = new CubeObject(new Transform { Position = Vector3.Zero, Rotation = new Quaternion(0, 0, 0, 1), Scale = Vector3.One });

        EngineObject obj2 = new CubeObject(new Transform { Position = new Vector3(2, 0, 0), Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(90f)), Scale = Vector3.One });

        EngineObject obj3 = new CubeObject(new Transform { Position = new Vector3(0, 2, 0), Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(180f)), Scale = Vector3.One });

        EngineObject obj4 = new CubeObject(new Transform { Position = new Vector3(0, 0, 2), Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(270f)), Scale = Vector3.One });

        public EngineWindow(WindowSettings windowSettings, Input input) : base(windowSettings, input)
        {
            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), windowSettings.Size.X / windowSettings.Size.Y, 0.1f, 100.0f);

            Graphics.Graphics.Renderer.Projection(projection);
        }

        protected override void OnLoad()
        {
            GL.Enable(EnableCap.DepthTest);

            base.OnLoad();
        }

        protected override void OnRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(10, 10, 10), new Vector3(0, 0, 0), Vector3.UnitY);

            Graphics.Graphics.Renderer.View(view);

            Graphics.Graphics.Renderer.Draw(obj);

            Graphics.Graphics.Renderer.Draw(obj2);

            Graphics.Graphics.Renderer.Draw(obj3);

            Graphics.Graphics.Renderer.Draw(obj4);

            base.OnRender();
        }

        protected void OnResize(int width, int height)
        {
            if (height == 0)
                height = 1;

            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), width / height, 0.1f, 100.0f);

            Graphics.Graphics.Renderer.Projection(projection);
        }
    }

    public class Camera : Actor
    {

    }
}
