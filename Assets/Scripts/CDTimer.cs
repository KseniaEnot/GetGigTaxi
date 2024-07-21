using UnityEngine;

public class CDTimer
{
    public float cooldown;
    private float _nextTime;

    public bool IsTimer => Time.time >= _nextTime;
    public void StartCooldown()
    {
        _nextTime = Time.time + cooldown;
    }
}
