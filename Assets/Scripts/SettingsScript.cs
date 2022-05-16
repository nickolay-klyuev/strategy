public class SettingsScript
{
    public enum ControlType
    {
        Touch,
        KeyboardMouse
    }

    private static ControlType s_controlType = ControlType.Touch;
    public static ControlType GetCurrentControlType()
    {
        return s_controlType;
    }

    public static bool IsTouch()
    {
        return s_controlType == ControlType.Touch;
    }

    public static bool IsKeyboardMouse()
    {
        return s_controlType == ControlType.KeyboardMouse;
    }
}
