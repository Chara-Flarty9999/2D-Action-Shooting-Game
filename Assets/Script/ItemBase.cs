using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    /// <summary>アイテムを取った時に鳴る効果音</summary>
    [Tooltip("アイテムを取った時に鳴らす効果音")]
    [SerializeField] AudioClip _sound = default;
    /// <summary>アイテムの効果をいつ発揮するか</summary>
    [Tooltip("Get を選ぶと、取った時に効果が発動する。Use を選ぶと、アイテムを使った時に発動する")]
    [SerializeField] ActivateTiming _whenActivated = ActivateTiming.Get;

    
    public abstract void Activate();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (_sound)
            {
                AudioSource.PlayClipAtPoint(_sound, Camera.main.transform.position);
            }

            // アイテム発動タイミングによって処理を分ける
            if (_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
            else if (_whenActivated == ActivateTiming.Use)
            {
                // 見えない所に移動する
                this.transform.position = Camera.main.transform.position;
                // コライダーを無効にする
                GetComponent<Collider2D>().enabled = false;
                // プレイヤーにアイテムを渡す
                collision.gameObject.GetComponent<PlayerController>().GetItem(this);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    enum ActivateTiming
    {
        /// <summary>取った時にすぐ使う</summary>
        Get,
        /// <summary>「使う」コマンドで使う</summary>
        Use,
    }
}
