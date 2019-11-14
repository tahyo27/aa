using UnityEngine;

// 게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour {
    public float speed = 9f; // 이동 속도

    private void Update() {
        if (!GameManager.instance.isGameover) //게임오버 상태가 아니라면
        {
            // 게임 오브젝트를 초당 speed의 속도로 평행 이동시킨다
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}