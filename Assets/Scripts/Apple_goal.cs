using UnityEngine.Events;
using UnityEngine;

public class Apple_goal : Apple
{
    public UnityEvent getGoal;
    public int Score;
    private void Awake()
    {
        getGoal.AddListener(() => { gameObject.SetActive(false); });
    }

}
