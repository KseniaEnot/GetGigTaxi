using System;
using UnityEngine;
using UnityEngine.Events;

public class HeadMoveController : BodyPart
{
    [HideInInspector]
    public UnityEvent loseGame;
    public GameController gameController;
    private PlayerInputs inputs;
    private void switchDirection(Vector2 _direction)
    {
        if (((localDirection.x != -_direction.x) && (localDirection.y != -_direction.y)) || (direction == Vector2.zero))
        {
            canMove = true;
            direction = _direction;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Apple_start ifApple = other.gameObject.GetComponent<Apple_start>();
        if (ifApple != null)
        {
            gameController.returnToFree(ifApple.GetTransformPos());
            ifApple.apple_Goal.SetActive(true);
            ifApple.eatApple.Invoke();
            return;
        }
        Apple_goal ifAppleGoal = other.gameObject.GetComponent<Apple_goal>();
        if (ifAppleGoal != null)
        {
            gameController.returnToFree(ifAppleGoal.GetTransformPos());
            gameController.addScore(ifAppleGoal.Score);
            ifAppleGoal.getGoal.Invoke();
            return;
        }
        loseGame.Invoke();
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
