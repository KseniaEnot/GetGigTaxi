using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    public BodyMoveController nextBody;
    protected int speed;

    [SerializeField]
    protected float cellSize;
    protected Vector2 direction;

    public virtual void Init(int _speed)
    {
        direction = Vector2.zero;
        speed = _speed;
        _ = playerMove();
    }
    protected async UniTaskVoid playerMove(){
        while(true){
            Vector2 localDirection = direction;
            for(int i=1;i<=speed;i++){ 
                transform.position += new Vector3(localDirection.x,localDirection.y,0)*cellSize/speed;
                await UniTask.Delay(1,cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            if(localDirection!=direction)
                nextBody?.SetDirectionWait(transform.position, direction);
        }
    }
}
