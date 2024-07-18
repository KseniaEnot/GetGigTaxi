using System;
using UnityEngine;
using UnityEngine.Events;

public class HeadMoveController : BodyPart
{
    [HideInInspector]
    public UnityEvent loseGame;
    private PlayerInputs inputs;
    override public void Init(int _speed)
    {
        base.Init(_speed);
        nextBody?.Init(_speed);
    }
    private void switchDirection(Vector2 _direction){
        if(direction == Vector2.zero){
            nextBody?.SetDirection(transform.position, _direction);
            direction = _direction;
        }
        if((direction.x != -_direction.x)&&(direction.y != -_direction.y)){
            direction = _direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Apple ifApple = other.gameObject.GetComponent<Apple>();
        if (ifApple != null){
            ifApple.eatApple.Invoke();
        } else {
            loseGame.Invoke();
        }
    }

    private void OnEnable()
    {
        inputs = new PlayerInputs();
        inputs.Enable();
        inputs.Player.Move.performed += ctx => switchDirection(ctx.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputs.Player.Move.performed -= ctx => switchDirection(ctx.ReadValue<Vector2>());
        inputs.Disable();
    }
}
