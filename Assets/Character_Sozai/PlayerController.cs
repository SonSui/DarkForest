using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadSceneを使うために必要

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;  //歩行アニメーションの再生速度追加プログラム（アニメーションクリップを作成した時にコメントアウトする）
    float jumpForce = 400.0f;   //ゲームバランスで各個人で調整
    float walkForce = 30.0f;    //ゲームバランスで各個人で調整
    float maxWalkSpeed = 5.0f;  //ゲームバランスで各個人で調整

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
        this.animator = GetComponent<Animator>();  //歩行アニメーションの再生速度追加プログラム（アニメーションクリップを作成した時にコメントアウトする）※AnimatorのComponentを取得
        lastPos = transform.position;
    }

    void Update()
    {
        
        //ジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)//GetKeyDownメソッドを使ってスペースキーが押されたかを調べる。かつ、Y方向の移動が0の時
        {
            //this.animator.SetTrigger("JumpTrigger");//歩行アニメーションの再生速度追加プログラム（Jumpのアニメーションクリップを作成した時にコメントアウトする）※ジャンプアニメーションの切り替え追加プログラム
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
        //左右に移動する
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

        //動く方向に応じて反転
        if(key !=0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
        
		//歩行アニメーションの再生速度追加プログラム52行から55行まで（Jumpのアニメーションクリップを作成した時にコメントアウトする）※ジャンプアニメーションの切り替え追加プログラム
		//プレイヤーの速度に応じてアニメーション速度を変える
		//if (this.rigid2D.velocity.y == 0)
		//{ this.animator.speed = speedx / 2.0f; }
		//else
		//{ this.animator.speed = 1.0f; }

		//画面外に落下し出た場合は最初から（処理を入れたい場合に、58～61行をコメントアウトする）
		//if (transform.position.y < -30)//ｙの値で落下内容を指定（マップをつくる時に座標に注意。この場合、ｙ＝-30にキャラクターが移動するとゲームオーバー）
		//{
		//	SceneManager.LoadScene("GameScene");//やり直さなく、ゲームオーバーにする場合は、GameOverSceneを作成させ、遷移させる
		//}

		//プレイヤーの速度に応じてアニメーション速度を変える
		//this.animator.speed = speedx / 2.0f;　//歩行アニメーションの再生速度追加プログラム（アニメーションクリップを作成した時にコメントアウトする）
	}

    //ゴールに到着
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Finish"))SceneManager.LoadScene("ClearScene"); // ClearSceneに移動する。
        if(other.gameObject.CompareTag("Coin"))//コインの収集
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
