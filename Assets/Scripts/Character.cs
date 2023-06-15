using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    // 캐릭터 체력
    public int hp;

    // 캐릭터 공격력
    public int damage;

    private float jumpForce = 20;
    private float jumpGauge = 0;
    private bool pressJump = false;

    [HideInInspector]
    public bool isGround = false;

    [SerializeField]
    private Slider jumpSlider;

    [SerializeField]
    private Button defenseButton;

    [SerializeField]
    private GameObject timeObject;

    [SerializeField]
    private Text timeText;

    private float defenseCooltime;

    private bool isJump { get { return animator.GetBool("Jump"); } }

    private Animator animator;
    private new Rigidbody2D rigidbody;
    private GameObject target;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Jump();            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Defense();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Attack();
        }


        if(defenseCooltime > 0)
        {
            defenseCooltime -= Time.deltaTime;

            timeText.text = string.Format("{0:0.0}", defenseCooltime);

            if (defenseCooltime < 0)
            {
                defenseCooltime = 0;
                defenseButton.interactable = true;
                timeText.transform.parent.gameObject.SetActive(false);
            }
        }

        if (pressJump)
        {
            jumpGauge += Time.deltaTime;

            if (jumpGauge > 1)
                jumpGauge = 1;
        }

        jumpSlider.value = jumpGauge;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;

            if (isGround && isJump)
            {
                animator.SetBool("Jump", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    public void Initialize()
    {
        hp = 3;
        damage = 1;
        timeText.transform.parent.gameObject.SetActive(false);
    }

    public void Jump()
    {
        if (!GameMgr.instance.gameStart)
            return;

        rigidbody.AddForce(new Vector2(0, jumpForce * jumpGauge), ForceMode2D.Impulse);
        animator.SetBool("Jump", true);
    }

    public void Defense()
    {
        if (!GameMgr.instance.gameStart)
            return;

        animator.SetTrigger("Defense");

        defenseButton.interactable = false;
        timeText.transform.parent.gameObject.SetActive(true);
        defenseCooltime = 1.2f;
    }

    public void Attack()
    {
        if (!GameMgr.instance.gameStart)
            return;

        animator.SetTrigger("Attack");
    }    

    public void OnPressDownJump()
    {
        if (!GameMgr.instance.gameStart)
            return;

        pressJump = true;
        jumpGauge = 0;
    }
    public void OnPressUpJump()
    {
        if (!GameMgr.instance.gameStart)
            return;

        Jump();

        pressJump = false;
        jumpGauge = 0;
    }
}
