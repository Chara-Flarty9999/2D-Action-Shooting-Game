using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    /// <summary>初期動作を設定できる。</summary>
    [SerializeField] liftmode liftmoving = default;
    int _liftmode;
    [SerializeField] bool playOnCollision = default;
    Animator m_anim = default;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        _liftmode = (int)liftmoving;
        Debug.Log(_liftmode);
        m_anim.SetInteger("Liftmoving", _liftmode);
    }

    // Update is called once per frame
    void Update()
    {
        m_anim.SetBool("PlayOnCollision", playOnCollision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playOnCollision == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                playOnCollision = false;
            }
        }
    }

    enum liftmode
    {
        /// <summary>
        /// 上に動く。
        /// </summary>
        Up, 
        /// <summary>
        /// 下に動く。
        /// </summary>
        Down, 
        /// <summary>
        /// 左に動く。
        /// </summary>
        Left, 
        /// <summary>
        /// 右に動く。
        /// </summary>
        Right,
    }
}
