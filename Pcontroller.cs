using UnityEngine;

// PController는 플레이어 캐릭터로서 플레이어 캐릭터를 제어한다
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 플레이어가 사망시에 재생하는 오디오 클립
   public float jumpForce = 600f; // 점프의 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   }

   private void Update() {
       // 사용자 입력을 감지하고 처리
       if(isDead)
        {
            //사망하면 종료 처리
            return;
        }
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero; // 점프전 속도 0
            playerRigidbody.AddForce(new Vector2(0, jumpForce)); //몸 위쪽으로 힘줘서 떨어지는 속도 늦추기
            playerAudio.Play(); // 설정한 오디오 재생
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;   //터치후 yy값 양수면 속도 줄이기
        }
        animator.SetBool("Grounded", isGrounded); //Grounded -> isGrounded로 갱신
   }

   private void Die() {
        // 사망 처리
        animator.SetTrigger("Die");  //die 트리거 셋
        playerAudio.clip = deathClip; //오디오 클립을 변경
        playerAudio.Play(); // 사망시 오디오클립을 재생
        playerRigidbody.velocity = Vector2.zero; // 속도 0으로 변경

        isDead = true; //isDead값 참으로

        GameManager.instance.OnPlayerDead(); // 게임 매니저에서 게임오버 처리 실행

   }

   private void OnTriggerEnter2D(Collider2D other) {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
       if (other.tag == "Dead" && !isDead)
        {
            Die(); // 충돌한 태그가 Dead면 죽음
        }
   }

   private void OnCollisionEnter2D(Collision2D collision) {
       // 바닥에 닿았음을 감지하는 처리
       if(collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;   //닿았으면 isGround 값 참 점프 카운트 0으로
        }
   }

   private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
   }
}