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
    [SerializeField] float _gravityDrag = .8f;

    public static float highJump = 1;
    Rigidbody2D _rb = default;
    /// <summary>接地フラグ</summary>
    bool _isGrounded = false;
    Vector3 _initialPosition = default;
    Animator _anim = default;
    /// <summary>持っているアイテムのリスト</summary>
    List<ItemBase> _itemList = new List<ItemBase>();
    // Start is called before the first frame update
    void Start()
    {
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
        float h = Input.GetAxis("Horizontal");
        Vector2 velocity = _rb.velocity;   // この変数 velocity に速度を計算して、最後に Rigidbody2D.velocity に戻す

        if (h != 0)
        {
            velocity.x = h * _moveSpeed;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isGrounded = false;
            velocity.y = _jumpSpeed * highJump;
        }
        else if (!Input.GetButton("Jump") && velocity.y > 0)
        {
            // 上昇中にジャンプボタンを離したら上昇を減速する
            velocity.y *= _gravityDrag;
        }

        _rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }
}
