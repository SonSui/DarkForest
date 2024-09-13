using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene���g�����߂ɕK�v

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;  //���s�A�j���[�V�����̍Đ����x�ǉ��v���O�����i�A�j���[�V�����N���b�v���쐬�������ɃR�����g�A�E�g����j
    float jumpForce = 400.0f;   //�Q�[���o�����X�Ŋe�l�Œ���
    float walkForce = 30.0f;    //�Q�[���o�����X�Ŋe�l�Œ���
    float maxWalkSpeed = 5.0f;  //�Q�[���o�����X�Ŋe�l�Œ���

    public GameManager manager;


    private Vector3 lastPos;
    private bool isJumping = false;
    private bool Jumped = false;
    private bool isFalling = false;
    private float preVY = 0;
    private float JumpTime = 0;
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();  //���s�A�j���[�V�����̍Đ����x�ǉ��v���O�����i�A�j���[�V�����N���b�v���쐬�������ɃR�����g�A�E�g����j��Animator��Component���擾
        lastPos = transform.position;
    }

    void Update()
    {
        
        //�W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)//GetKeyDown���\�b�h���g���ăX�y�[�X�L�[�������ꂽ���𒲂ׂ�B���AY�����̈ړ���0�̎�
        {
            //this.animator.SetTrigger("JumpTrigger");//���s�A�j���[�V�����̍Đ����x�ǉ��v���O�����iJump�̃A�j���[�V�����N���b�v���쐬�������ɃR�����g�A�E�g����j���W�����v�A�j���[�V�����̐؂�ւ��ǉ��v���O����
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            SetJumpAnime();
            Jumped = true;
            isJumping = true;
        }
        if(Jumped)
        {
            JumpTime += Time.deltaTime;
            isJumping=true;
            SetJumpAnime();
            if(JumpTime > 0.5) 
            {
                Jumped = false;
                JumpTime = 0;
            }
        }
        AnimeUpdate();
        //���E�Ɉړ�����
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rigid2D.velocity.x < maxWalkSpeed)
            {
                Vector2 newSpead = new Vector2(rigid2D.velocity.x + walkForce * Time.deltaTime, rigid2D.velocity.y);
                rigid2D.velocity = newSpead;
            }
        }
        else if(!Input.GetKey(KeyCode.RightArrow)&& rigid2D.velocity.x>0)
        {
            Vector2 newSpead = new Vector2(rigid2D.velocity.x - walkForce * Time.deltaTime*0.3f, rigid2D.velocity.y);
            if(newSpead.x < 0)newSpead.x = 0;
            rigid2D.velocity = newSpead;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        { 
            if (rigid2D.velocity.x > -maxWalkSpeed)
            {
                Vector2 newSpead = new Vector2(rigid2D.velocity.x - walkForce * Time.deltaTime, rigid2D.velocity.y);
                rigid2D.velocity = newSpead;
            }
        }
        else if(!Input.GetKey(KeyCode.LeftArrow) && rigid2D.velocity.x < 0)
        {
            Vector2 newSpead = new Vector2(rigid2D.velocity.x + walkForce * Time.deltaTime * 0.3f, rigid2D.velocity.y);
            if (newSpead.x > 0) newSpead.x = 0;
            rigid2D.velocity = newSpead;
        }

		if(rigid2D.velocity.x>0.1)
        {
            key = 1;
        }
        else if(rigid2D.velocity.x<-0.1)
        {
            key = -1;
        }

        //���������ɉ����Ĕ��]
        if(key !=0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
        
		//���s�A�j���[�V�����̍Đ����x�ǉ��v���O����52�s����55�s�܂ŁiJump�̃A�j���[�V�����N���b�v���쐬�������ɃR�����g�A�E�g����j���W�����v�A�j���[�V�����̐؂�ւ��ǉ��v���O����
		//�v���C���[�̑��x�ɉ����ăA�j���[�V�������x��ς���
		//if (this.rigid2D.velocity.y == 0)
		//{ this.animator.speed = speedx / 2.0f; }
		//else
		//{ this.animator.speed = 1.0f; }

		//��ʊO�ɗ������o���ꍇ�͍ŏ�����i��������ꂽ���ꍇ�ɁA58�`61�s���R�����g�A�E�g����j
		//if (transform.position.y < -30)//���̒l�ŗ������e���w��i�}�b�v�����鎞�ɍ��W�ɒ��ӁB���̏ꍇ�A����-30�ɃL�����N�^�[���ړ�����ƃQ�[���I�[�o�[�j
		//{
		//	SceneManager.LoadScene("GameScene");//��蒼���Ȃ��A�Q�[���I�[�o�[�ɂ���ꍇ�́AGameOverScene���쐬�����A�J�ڂ�����
		//}

		//�v���C���[�̑��x�ɉ����ăA�j���[�V�������x��ς���
		//this.animator.speed = speedx / 2.0f;�@//���s�A�j���[�V�����̍Đ����x�ǉ��v���O�����i�A�j���[�V�����N���b�v���쐬�������ɃR�����g�A�E�g����j
	}

    //�S�[���ɓ���
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Finish"))SceneManager.LoadScene("ClearScene"); // ClearScene�Ɉړ�����B
        if(other.gameObject.CompareTag("Coin"))//�R�C���̎��W
        {
            Debug.Log("GetCoin");
            manager.GetCoin();
            Destroy(other.gameObject);
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("OnGround");
            SetIdleAnime();
            isJumping = false;
        }
    }
    void AnimeUpdate()
    {
        if(animator.GetBool("isJumping")&&rigid2D.velocity.y<0.5)
        {
            SetFallAnime();
        }
        if(!animator.GetBool("isJumping")&&!animator.GetBool("isFalling"))
        {
            
            if(Mathf.Abs(rigid2D.velocity.x)>0.8)
                SetRunAnime();
        }
        if(!animator.GetBool("isFalling")&& rigid2D.velocity.y < -2)
        {
            SetFallAnime();
            isJumping = true;
        }
    }
    void SetJumpAnime()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isIdle", false);
    }
    void SetFallAnime()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", true);
        animator.SetBool("isIdle", false);
    }
    void SetRunAnime()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", true);
        animator.SetBool("isFalling", false);
        animator.SetBool("isIdle", false);
    }
    void SetIdleAnime()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isIdle", true);
    }
}
