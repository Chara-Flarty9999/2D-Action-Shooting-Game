using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float _moveSpeed = 3f;
    /// <summary>ジャンプ速度</summary>
    [SerializeField] float _jumpSpeed = 5f;
    /// <summary>ジャンプ中にジャンプボタンを離した時の上昇速度減衰率</summary>
    [SerializeField] float _gravityDrag = .2f;
    /// <summary>発射する弾丸をPrefabで指定する。</summary>
    [SerializeField] GameObject _bullet;
    /// <summary>プレイヤー指定</summary>
    [SerializeField] GameObject _player;

    /// <summary>
    /// [外部変更用]ジャンプ力上昇のレート。倍率で設定する。
    /// </summary>
    public static float highJumpRate = 1f;
    /// <summary>
    /// [外部変更用]低速落下用のレート。基本的に3か1でOK。
    /// </summary>
    public static float levitation = 3;
    /// <summary>
    /// [外部変更用]移動速度のレート。倍率で設定。
    /// </summary>
    public static float highSpeed = 1;

    public float Rote;
        
    Rigidbody2D _rb = default;
    /// <summary>接地フラグ</summary>
    bool _isGrounded = false;
    Vector3 _initialPosition = default;
    //Animator _anim = default;
    /// <summary>持っているアイテムのリスト</summary>
    List<ItemBase> _itemList = new List<ItemBase>();
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _rb = GetComponent<Rigidbody2D>();
        _initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        // アイテムを使う
        if (Input.GetButtonDown("Fire1"))
        {
            if (_itemList.Count > 0)
            {
                // リストの先頭にあるアイテムを使って、破棄する
                ItemBase item = _itemList[0];
                _itemList.RemoveAt(0);
                item.Activate();
                Destroy(item.gameObject);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(_bullet, _player.transform.position,Quaternion.identity);
        }

        // 画面外に落ちたら初期位置に戻す
        if (this.transform.position.y < -15)
        {
            this.transform.position = _initialPosition;
        }
    }

    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }

    void Movement()
    {
        _rb.gravityScale = 3;
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            Rote = 0;
        }
        else if (h < 0)
        {
            Rote = 180;
        }
        Vector2 velocity = _rb.velocity;   // この変数 velocity に速度を計算して、最後に Rigidbody2D.velocity に戻す

        if (h != 0)
        {
            velocity.x = h * _moveSpeed * highSpeed;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isGrounded = false;
            velocity.y = _jumpSpeed * highJumpRate;
        }
        else if (!Input.GetButton("Jump") && velocity.y > 0)
        {
            // 上昇中にジャンプボタンを離したら上昇を減速する
            velocity.y *= _gravityDrag;
        }

        if (velocity.y < -1)
        {
            _rb.gravityScale = levitation;
        }

        _rb.velocity = velocity;
        //Debug.Log(_rb.velocity);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
}
