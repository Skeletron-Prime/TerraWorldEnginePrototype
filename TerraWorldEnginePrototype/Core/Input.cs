namespace TerraWorldEnginePrototype.Core
{
    public abstract class Input
    {
        public virtual void OnKeyDown(KeyCode key) { }
        public virtual void OnKeyUp(KeyCode key) { }

        public virtual void OnMouseMove(int x, int y) { }

        public virtual void OnMouseButtonDown(int button) { }
        public virtual void OnMouseButtonUp(int button) { }
        public virtual void OnMouseWheel(int delta) { }

        public virtual void OnTextInput(string text) { }
    }

    public class EngineInput : Input
    {
    }

    [Flags]
    public enum KeyModifiers
    {
        None = 0,
        Shift = 1,
        Control = 2,
        Alt = 4,
        Super = 8
    }
}
