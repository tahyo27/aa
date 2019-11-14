using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -20); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        platforms = new GameObject[count];

        //카운트 루프하면서 발판 생성
        for (int i = 0;i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity); //플랫폰 프리팹 원본으로 새 발판 풀포지션 위치 복제 생성후 플랫폼 배열에 할당
        }
        lastSpawnTime = 0f; //마지막 배치 시점 초기화
        timeBetSpawn = 0f; //다음번 배치까지 시간 간격 0으로 초기화
    }

    void Update() {
        
        if (GameManager.instance.isGameover)
        {
            return; //게임오버 상태에서는 동작x
        }
        
        if (Time.time >= lastSpawnTime + timeBetSpawn) //마지막 배치시점에서 timeBetspawn 이상 시간 흘렀으면
        {
            lastSpawnTime = Time.time;  //기록된 마지막 배치 시점 현재 시점으로 갱신
         
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); //다음 배치까지의 시간 간격을 min ~ max 에서 결정

            float yPos = Random.Range(yMin, yMax); //배치할 위치 결정
            platforms[currentIndex].SetActive(false);  //비활성화 하고 즉시 활성화
            platforms[currentIndex].SetActive(true);   //플랫폼의 OnEnable 매서드 실행됨

            platforms[currentIndex].transform.position = new Vector2(xPos, yPos); //현재 순번의 발판을 화면 오른쪽에 재배치

            currentIndex++; //순번 넘기기

            if (currentIndex >= count) // 마지막 순번에 도달하면 리셋
            {
                currentIndex = 0;
            }
        }
    }
}