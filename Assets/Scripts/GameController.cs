using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private Vector2 wigth;
    [SerializeField] private Vector2 height;
    private List<Apple> spawnedApple;
    private List<Vector3> spawnPos;
    private GameObject setApple;
    [SerializeField] private GameObject AppleGO;
    [SerializeField] private GameObject AppleParent;
    [SerializeField] private int speed;
    [SerializeField] private HeadMoveController head;

    private void Awake() {
        spawnedApple = new List<Apple>();
        spawnPos =  new List<Vector3>();
        spawnPos = fillSpawnPos();
        setApple = Instantiate(AppleGO, spawnPos[Random.Range(0,spawnPos.Count-1)], AppleGO.gameObject.transform.rotation, AppleParent.transform);
        spawnedApple.Add(setApple.GetComponent<Apple>());
        spawnedApple[spawnedApple.Count-1].eatApple.AddListener(resetApple);
        head.Init(speed);
    }

    private void resetApple(){
        setApple.transform.position = spawnPos[Random.Range(0,spawnPos.Count-1)];
    }

    private List<Vector3> fillSpawnPos(){
        List<Vector3> tempList = new List<Vector3>();
        for(float i= wigth.x; i <= wigth.y; i+=1)
            for(float j= height.x; j <= height.y; j+=1)
                tempList.Add(new Vector2(i,j));
        return tempList;
    }

    private void scenRestart(){
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
