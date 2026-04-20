using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [Header("移动设置")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 150f; // 坦克式旋转速度
    public float gravity = 9.81f;

    private float verticalVelocity;
    private Animator animator;
    public bool isOver;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    //吃金币
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("碰到障碍物了+"+other.name);
        if (other.gameObject.tag=="Score")
        {
            GameManger.Instance.AddScore();
            GameManger.Instance.audioSource.PlayOneShot(GameManger.Instance.rightAudioClip);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag== "ZhangAi")
        {

            GameManger.Instance.SubHealth();
            GameManger.Instance.audioSource.PlayOneShot(GameManger.Instance.hurtAudioClip);
        }

        if (other.gameObject.tag == "Win")
        {
            GameManger.Instance.WinGame();
        }

    }

   

    void Update()
    {
        if (isOver)
        {
            return;
        }

        // 1. 获取输入
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S

        //动画
        if (vertical != 0)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        // --- 处理旋转 (基于自身 Y 轴) ---
        // A/D 控制角色原地左右转头
        transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime);

        // --- 处理移动 (基于自身正前方) ---
        // transform.forward 会自动根据角色当前的旋转指向“前方”
        Vector3 move = transform.forward * vertical * moveSpeed;

        // 2. 处理重力
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // 结合水平移动和垂直重力
        move.y = verticalVelocity;

        // 3. 执行最终位移
        controller.Move(move * Time.deltaTime);
    }
}