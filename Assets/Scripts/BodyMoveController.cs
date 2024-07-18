using Cysharp.Threading.Tasks;
using UnityEngine;

public class BodyMoveController : BodyPart
{
    public void SetDirection (Vector3 point, Vector2 _direction){
        _ = SetDirectionWait(point, _direction);
    }
    public async UniTaskVoid SetDirectionWait(Vector3 point, Vector2 _direction){
        if(direction == Vector2.zero){
            direction = (point-transform.position).normalized; //_direction;
            Debug.Log("Body "+direction);
        }
        while(transform.position!= point)
            await UniTask.Delay(1,cancellationToken: this.GetCancellationTokenOnDestroy());
        direction = _direction;
        nextBody?.SetDirectionWait(point,_direction);
    }
}
