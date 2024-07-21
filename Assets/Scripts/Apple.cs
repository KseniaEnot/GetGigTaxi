using UnityEngine;

public class Apple : MonoBehaviour
{
    public bool isActiv() => gameObject.activeSelf;
    public Vector3 GetTransformPos() => gameObject.transform.position;
    public void SetActive(bool _state) => gameObject.SetActive(_state);
    public void SetTransformPosition(Vector3 transformAple) => gameObject.transform.position = transformAple;
}
