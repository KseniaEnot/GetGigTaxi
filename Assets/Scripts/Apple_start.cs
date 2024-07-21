using UnityEngine.Events;
using UnityEngine;

public class Apple_start : Apple
{
    public UnityEvent eatApple;
    public Apple_goal apple_Goal;
    private void Awake()
    {
        eatApple.AddListener(() => { gameObject.SetActive(false); });
    }
}
