using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float _moveSpeed = 8f;
    /// <summary>ジャンプ速度</summary>
    [SerializeField] float _jumpSpeed = 5f;
    /// <summary>ジャンプ中にジャンプボタンを離した時の上昇速度減衰率</summary>
    [SerializeField] float _gravityDrag = .2f;
    /// <summary>発射する弾丸をPrefabで指定する。</summary>
    [SerializeField] GameObject _bullet;
    /// <summary>プレイヤー指定</summary>
    [SerializeField] GameObject _player;
    /// <summary>ソウル状態変更時のSE</summary>
    [SerializeField] AudioClip _changeSound;

    float h;
    float v;

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

    /// <summary>アイテム取得等によるバフの状態をビットフラグで判定。</summary>
    Itemtype _soulState;

    public float rote;

    /// <summary>ソウルの状態を指定。外部から変更することによってソウル状態が変わる。
    /// また、エリア上にSoulModeChangerを設置することによってフィールド上でもソウル状態を変えれる。</summary>
    [Tooltip("ソウルの状態を設定できる。外部からでも変更可能。")]
    public SoulMode soul;
        
    Rigidbody2D _rb = default;
    SpriteRenderer _sprite = default;
    /// <summary>接地フラグ</summary>
    bool _isGrounded = false;
    Vector3 _initialPosition = default;
    AudioSource _audioSource;
    //Animator _anim = default;
    /// <summary>持っているアイテムのリスト</summary>
    List<ItemBase> _itemList = new List<ItemBase>();
    // Start is called before the first frame update


    private int soulMode { get; set; }
    void Start()
    {
        _player = GameObject.Find("Player");
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
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
        if(soul == SoulMode.blue) 
        {
            _sprite.color = new Color(0, 0, 1);
            _moveSpeed = 8f;

            _rb.gravityScale = 3;
            float h = Input.GetAxis("Horizontal");
            if (h > 0)
            {
                rote = 0;
            }
            else if (h < 0)
            {
                rote = 180;
            }
            Vector2 velocity = _rb.velocity;   // この変数 velocity に速度を計算して、最後に Rigidbody2D.velocity に戻す

            if (h != 0)
            {
                velocity.x = h * _moveSpeed * highSpeed;
            }

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
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

        if(soul == SoulMode.red)
        {
            _sprite.color = new Color(1, 0, 0);
            _rb.gravityScale = 0;
            if (Input.GetKey(KeyCode.X) == true)
            {
                _moveSpeed = 2.5f;
            }
            else
            {
                _moveSpeed = 7f;
            }
            // 水平方向の入力を検出する
            h = Input.GetAxisRaw("Horizontal");
            //Debug.Log(h);
            v = Input.GetAxisRaw("Vertical");
            //Debug.Log(v);
            // 入力に応じてパドルを水平方向に動かす
            _rb.velocity = new Vector3(h * _moveSpeed, v * _moveSpeed);

            if (transform.position.x <= -11.95f)
            {
                transform.position = new Vector2(-11.95f, this.transform.position.y);
            }
            if (transform.position.x >= 11.95f)
            {
                transform.position = new Vector2(11.95f, this.transform.position.y);
            }
            if (transform.position.y <= -6.5f)
            {
                transform.position = new Vector2(this.transform.position.x, -6.5f);
            }
            if (transform.position.y >= 6.5f)
            {
                transform.position = new Vector2(this.transform.position.x, 6.5f);
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            if(!_isGrounded)
            {
                _isGrounded = true;
            }
        }

        if (collision.tag == "SoulChanger")
        {
            GameObject _soulChangerObject = collision.gameObject;
            SoulModeChanger _soulModeChanger = _soulChangerObject.GetComponent<SoulModeChanger>();
            if (_soulModeChanger._changeMode == soul)
            {

            }
            else
            {
                _audioSource.PlayOneShot(_changeSound);
            }
            soul = _soulModeChanger._changeMode;


            
        }
    }

    public enum SoulMode
    {
        red,
        blue,
    }

    public enum Itemtype
    {
        None = 0b0000,
        Highspeed = 0b0001,
        Levitation = 0b0010,
        Highjump = 0b0100,
        Invincible = 0b1000,
    }
}
