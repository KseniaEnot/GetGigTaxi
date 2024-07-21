using UnityEngine;

public class BodyPart : MonoBehaviour
{
    protected static bool canMove = false;
    [SerializeField]
    public BodyMoveController nextBody;
    protected int speed;
    protected float cellSize;
    protected Vector2 direction;
    protected Vector2 localDirection;
    private GameObject nextBodyGO;
    private bool needSpawn;

    protected Vector3 nextDot;
    protected Vector3 lasttDot;

    private void Awake()
    {
        needSpawn = false;
        direction = Vector2.zero;
        localDirection = Vector2.zero;
        nextDot = transform.position;
        lasttDot = transform.position;
    }

    public virtual void Init(int _speed, float _cellSize, GameObject _nextBodyGO)
    {
        speed = _speed;
        cellSize = _cellSize;
        nextBodyGO = _nextBodyGO;
        nextBody?.Init(_speed, _cellSize, _nextBodyGO);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if ((transform.position == nextDot) && (canMove))
        {
            /*Debug.Log("localDirection " + localDirection);
            Debug.Log("direction " + direction);
            Debug.Log("lasttDot " + lasttDot);
            Debug.Log("nextDot " + nextDot);*/
            if (needSpawn)
            {
                GameObject _nextbodyGO = Instantiate(nextBodyGO, lasttDot, gameObject.transform.rotation);
                nextBody = _nextbodyGO.GetComponent<BodyMoveController>();
                nextBody.Init(speed, cellSize, nextBodyGO);
                //nextBody.setDirectionForNewBody(direction);
                needSpawn = false;
            }
            lasttDot = nextDot;
            nextDot = transform.position + new Vector3(direction.x, direction.y, 0) * cellSize;
            localDirection = direction;
            if (direction != Vector2.zero)
                nextBody?.SetDirection(lasttDot);
        }
        else
        {
            transform.position += new Vector3(localDirection.x, localDirection.y, 0) * cellSize / speed;
        }
    }

    public void spawnNext()
    {
        if (nextBody != null)
            nextBody.spawnNext();
        else
            needSpawn = true;
    }
}
