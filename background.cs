using UnityEngine;

// 지정된 위치로 이동한 배경을 재배치
public class BackgroundLoop : MonoBehaviour {
    private float width; 

    // 위치를 리셋 시켜주는 메서드
    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }

    private void Awake() {
        // 가로의 길이를 측정한다
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;

    }

    private void Update() {
        
        if (transform.position.x <= -width) // 왼쪽으로 width 이상 이동시 위치를 재배치 시켜준다
        {
            Reposition();
        }
    }
    
}