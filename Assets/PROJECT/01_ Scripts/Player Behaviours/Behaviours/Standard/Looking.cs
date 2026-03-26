using UnityEngine;

public class Looking : Behaviour
{
    #region Variables
    public static Vector2 input;
    private Vector2 staleInput;

    private float targetYaw;
    private float targetPitch;

    private static float TopClamp = 90;
    private static float BottomClamp = 270;
    
    private float sensitivity = 0.2f;
    #endregion
    
    public static void SyncInput(Vector2 Input)
    {
        input = Input;
    }
    
    public static float ClampAngle(float Angle)
    {
        #region Angle 0-360
        while (Angle > 360)
        {
            Angle -= 360;
        }
        while (Angle < 0)
        {
            Angle += 360;
        }
        #endregion
        #region Clamp Angle
        var clampSplit = TopClamp + ((BottomClamp - TopClamp) / 2);
        if (Angle >= TopClamp && Angle <= clampSplit) 
        {
            Angle = TopClamp;
        }
        else if (Angle <= BottomClamp && Angle > clampSplit) 
        {
            Angle = BottomClamp;
        }
        #endregion
        
        return Angle;
    }
    
    public void Begin()
    {
        #region Initial Yaw/Pitch
        targetYaw = PlayerBehaviour.Instance.CameraHolder.transform.eulerAngles.y;
        targetPitch = PlayerBehaviour.Instance.CameraHolder.transform.eulerAngles.x;
        #endregion
    }

    public void Run()
    {
        #region Clear Stale Inputs
        if (input == staleInput)
        {
            PlayerBehaviour.Instance.End<Looking>();
            return;
        }
        staleInput = input;
        #endregion
        #region Get Targets
        targetYaw += input.x * sensitivity;
        targetPitch -= input.y * sensitivity;
        targetPitch = ClampAngle(targetPitch);
        #endregion
        
        #region Apply Rotation
        PlayerBehaviour.Instance.CameraHolder.transform.localEulerAngles = new Vector3(
            targetPitch,
            0f,
            0f);
        PlayerBehaviour.Instance.transform.eulerAngles = new Vector3(
            0f,
            targetYaw,
            0f);
        #endregion
    }

    public void End()
    {
        input = Vector2.zero;
    }
}
