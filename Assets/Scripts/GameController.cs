using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameUI gameUI;
    [SerializeField] private Vector2 wigth;
    [SerializeField] private Vector2 height;
    [SerializeField] private float cellSize;
    [SerializeField] private int maxAppleCopunt;
    private List<Apple_start> spawnedApple;
    private List<Apple_goal> spawnedGoal;
    private List<Vector3> spawnPos_free;
    private List<Vector3> spawnPos_full;
    [SerializeField] private CDTimer timerSpawnApple;
    [SerializeField] private int score;
    [SerializeField] private GameObject AppleGO;
    [SerializeField] private GameObject AppleGoalGO;
    [SerializeField] private GameObject AppleParent;
    [SerializeField] private int speed;
    [SerializeField] private HeadMoveController head;
    [SerializeField] private GameObject BodyObject;

    private void Awake()
    {
        score = 0;
        gameUI.SetScore(score);
        spawnedApple = new List<Apple_start>();
        spawnedGoal = new List<Apple_goal>();
        spawnPos_free = fillSpawnPos();
        spawnPos_full = new List<Vector3>();
        FillAppleAndGoal();
        head.Init(speed, cellSize, BodyObject);

        timerSpawnApple = new CDTimer();
        spawnApple();
        timerSpawnApple.cooldown = 4f;
        timerSpawnApple.StartCooldown();
    }

    private void FixedUpdate()
    {
        if (timerSpawnApple.IsTimer)
        {
            spawnApple();
            timerSpawnApple.StartCooldown();
        }
    }

    private List<Vector3> fillSpawnPos()
    {
        List<Vector3> tempList = new List<Vector3>();
        for (float i = wigth.x; i <= wigth.y; i += cellSize)
            for (float j = height.x; j <= height.y; j += cellSize)
                tempList.Add(new Vector2(i, j));
        return tempList;
    }

    private void spawnApple()
    {
        (Vector3, Vector3) pos = getFreePos();
        for (int i = 0; i < spawnedApple.Count; i++)
        {
            if ((spawnedApple[i].isActiv() == false) && (spawnedGoal[i].isActiv() == false))
            {
                spawnedApple[i].SetTransformPosition(pos.Item1);
                spawnedApple[i].SetActive(true);
                spawnedGoal[i].SetTransformPosition(pos.Item2);
                spawnedGoal[i].Score = 100;
                return;
            }
        }
    }

    private (Vector3, Vector3) getFreePos()
    {
        int pos = Random.Range(0, spawnPos_free.Count - 1);
        Vector3 v1 = spawnPos_free[pos];
        spawnPos_free.Remove(v1);
        pos = Random.Range(0, spawnPos_free.Count - 1);
        Vector3 v2 = spawnPos_free[pos];
        spawnPos_free.Remove(v2);
        spawnPos_full.Add(v1);
        spawnPos_full.Add(v2);
        return (v1, v2);
    }

    public void addScore(int _score)
    {
        score += _score;
        gameUI.SetScore(score);
    }

    public void returnToFree(Vector3 pos)
    {
        spawnPos_full.Remove(pos);
        spawnPos_free.Add(pos);
    }

    private void FillAppleAndGoal()
    {
        GameObject go1, go2;
        for (int i = 0; i < maxAppleCopunt; i++)
        {
            go1 = Instantiate(AppleGO, Vector3.zero, Quaternion.identity, AppleParent.transform);
            go2 = Instantiate(AppleGoalGO, Vector3.zero, Quaternion.identity, AppleParent.transform);
            go1.SetActive(false);
            go2.SetActive(false);
            spawnedGoal.Add(go2.GetComponent<Apple_goal>());
            spawnedGoal.Last().getGoal.AddListener(() =>
                        {
                            head.spawnNext();
                        });
            spawnedApple.Add(go1.GetComponent<Apple_start>());
            spawnedApple.Last().apple_Goal = spawnedGoal.Last();
            spawnedApple.Last().eatApple.AddListener(() =>
                        {

                        });
        }
    }

    private void scenRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        head.loseGame.AddListener(scenRestart);
    }

    private void OnDisable()
    {
        head.loseGame.RemoveListener(scenRestart);
    }
}
