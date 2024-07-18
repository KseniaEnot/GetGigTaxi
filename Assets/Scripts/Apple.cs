using UnityEngine.Events;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public UnityEvent eatApple;
    public Transform GetTransform(){
        return gameObject.transform;
    }
}
