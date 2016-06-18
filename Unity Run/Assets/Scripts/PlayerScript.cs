using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    // ここから
    public float speed = 5.0f;
    public float slideSpeed = 2.0f;
    public float tmp_speed = 5.0f;
    public Camera camera;
    public Text danger;
    // ここまでの間に変数を書いてね

    Animator animator;
    UIScript uiscript;

    // ゲームが始まった時に一回だけ呼ばれる
    void Start() {
        animator = GetComponent<Animator>();
        uiscript = GameObject.Find("Canvas").GetComponent<UIScript>();
        danger.text = "";

    }

    // 1フレームごとに呼ばれる
    void Update() {
        //ここから
        transform.position += Vector3.forward * speed * Time.deltaTime;
        float pos_x = transform.position.x;

        if (Input.GetKey (KeyCode.LeftArrow))
        {
            if (pos_x > -1.9f)
            {
                transform.position += Vector3.left * slideSpeed * Time.deltaTime;
            }
        }
        if(Input.GetKey (KeyCode.RightArrow))
        {
            if (pos_x < 1.9f)
            {
                transform.position += Vector3.right* slideSpeed * Time.deltaTime;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            tmp_speed = speed; //現在の速度を保存
            speed = 0;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            speed = tmp_speed; //速度をもとに戻す処理
            
        }

        //ここまでの間に書こう！

        //アニメーションについて(いじらない)
        if (Input.GetKey (KeyCode.UpArrow)) {
			animator.SetBool ("JUMP", true);
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			animator.SetBool ("JUMP", false);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			animator.SetBool ("SLIDE", true);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			animator.SetBool ("SLIDE", false);
		}

        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("wait", true);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("wait", false);
        }

        //Ray実装
        Seancer ();

    }
		
	// Triggerである障害物にぶつかったとき
	void OnTriggerEnter (Collider colider){
		
		var stateInfo = animator.GetCurrentAnimatorStateInfo (0);
		bool isJump = stateInfo.IsName("Base Layer.JUMP00");
		bool isSlide = stateInfo.IsName("Base Layer.SLIDE00");
		bool isRun = stateInfo.IsName("Base Layer.RUN00_F");

		bool isHigh = colider.CompareTag("High");
		bool isLow = colider.CompareTag("Low");
		bool isBarrier = colider.CompareTag ("barrier");
		bool isGoal = colider.CompareTag ("goal");

		// 障害物に当たったとき
		if( (isHigh == true && isSlide == false) ||
			(isLow == true && isJump == false) ||
		    (isBarrier == true)){
            //この下に書こう
            speed = 0;
			animator.SetBool ("DEAD", true);
			// UI
			uiscript.Gameover();
            
        }

       

        //ゴールした時
        if (isGoal == true){
            //この下に書こう
            speed = 0;
			animator.SetBool ("WIN", true);
            
			// UI
			uiscript.Goal();
		}
	}
    void Seancer()
    {
        int distance = 4; //Reyの距離
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = camera.ScreenPointToRay(center); //Reyを飛ばす位置

        RaycastHit hit; //Rayが当たったときの情報を格納する変数宣言
        if (Physics.SphereCast(ray, 1, out hit, distance)) //太さを持ったRayを使用(2はRayの太さ)
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red); //デバッグ用

            danger.text = "あぶない！"; //テキスト表示
        }
        else
        {
            danger.text = "";
        }

        //if (hit.collider.tag == "High" || hit.collider.tag == "Low" || hit.collider.tag == "barrier")
        
       
    }
}
