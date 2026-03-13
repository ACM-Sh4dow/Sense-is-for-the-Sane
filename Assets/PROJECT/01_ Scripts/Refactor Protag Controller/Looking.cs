using UnityEngine;

public class Looking : Behaviour
{
    public static Vector2 input;

    private float targetYaw;
    private float targetPitch;

    private static float TopClamp = 90;
    private static float BottomClamp = 270;
    
    private float sensitivity = 30f;
    
    public static void SyncInput(Vector2 Input)
    {
        input = Input;
    }
    
    public static float ClampAngle(float Angle)
    {
        var clampSplit = TopClamp + ((BottomClamp - TopClamp) / 2);

        while (Angle > 360)
        {
            Angle -= 360;
        }
        while (Angle < 0)
        {
            Angle += 360;
        }
        
        if (Angle >= TopClamp && Angle <= clampSplit) 
        {
            Angle = TopClamp;
        }
        else if (Angle <= BottomClamp && Angle > clampSplit) 
        {
            Angle = BottomClamp;
        }
        
        return Angle;
    }
    
    public void Begin()
    {
        targetYaw = JackBehaviour.Instance.CameraHolder.transform.rotation.eulerAngles.y;
        targetPitch = JackBehaviour.Instance.CameraHolder.transform.rotation.eulerAngles.x;
    }

    public void Run()
    {
        targetYaw += input.x * sensitivity * Time.deltaTime;
        targetPitch -= input.y * sensitivity * Time.deltaTime;
        
        targetPitch = ClampAngle(targetPitch);
        
        JackBehaviour.Instance.CameraHolder.transform.rotation = Quaternion.Euler(
            targetPitch,
            targetYaw,
            0f);
    }

    public void End()
    {
    }
}
