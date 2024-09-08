using UnityEngine;
using DG.Tweening;

public class ClossBlaster : MonoBehaviour
{
    SpriteRenderer spriterenderer;
    AudioSource audioSource;
    MoveInfo moveInfo;
    StartInfo startInfo;
    [SerializeField] AudioClip blasterSpawn = default;
    [SerializeField] AudioClip blasterbeam = default;


    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spriterenderer.color -= new Color(0f, 0f, 0f, 255f);
        new MoveInfo(transform.position, 5);
    }

    // Update is called once per frame
    void Update()
    {
        moveInfo = new MoveInfo(1, 2, 90);
        startInfo = new StartInfo(1, 2, 0);

        ClossBlasterCharge(startInfo,moveInfo,1,5,7,BlasterColor.blue);
    }

    /// <summary>
    /// 十字ブラスターを作動させるメソッド。初期位置や移動速度などは事前に変数に格納しておくように。
    /// </summary>
    /// <param name="startInfo">召喚時のX座標、Y座標、角度を設定する。角度は度数法でOK。座標はVectorでもOK。</param>
    /// <param name="moveInfo">ビーム発射後にブラスター本体がX、Yに対して、また角度を一秒にどれだけ動かすかを設定する。</param>
    /// <param name="size">ブラスターのサイズを設定する。基本は1でOK。</param>
    /// <param name="waittime">ブラスター召喚から発射までの遅延時間を設定する。</param>
    /// <param name="looplong">ビーム発射の処理を何回繰り返すか、つまり何秒間ビームを発射するか設定する。</param>
    /// <param name="beamcolor">白か青かオレンジを設定する。</param>
    public void ClossBlasterCharge(StartInfo startInfo, MoveInfo moveInfo, float size, int waittime, int looplong, BlasterColor beamcolor)
    {
        this.transform.localScale = new Vector3 (size,size);
        this.transform.position = startInfo.vector2;
        this.transform.eulerAngles = new Vector3(0,0, startInfo.di);
        audioSource.PlayOneShot(blasterSpawn);
        this.spriterenderer.material.DOFade(255f, 1f);


        


    }

    public struct MoveInfo
    {
        public float x;
        public float y;
        public Vector2 vector2;
        public int di;

        public MoveInfo(Vector2 vector2, int di)
        {
            this.vector2 = vector2;
            this.di = di;
            this.x = vector2.x;
            this.y = vector2.y;
        }
        public MoveInfo(float x, float y, int di)
        {
            this.x = x;
            this.y = y;
            this.di = di;
            this.vector2 = new Vector2(x, y);
        }
    }

    public struct StartInfo
    {
        public float x;
        public float y;
        public Vector2 vector2;
        public int di;

        public StartInfo(Vector2 vector2, int di)
        {
            this.vector2 = vector2;
            this.di = di;
            this.x = vector2.x;
            this.y = vector2.y;
        }

        public StartInfo(float x, float y, int di)
        {
            this.x = x;
            this.y = y;
            this.di = di;
            this.vector2 = new Vector2(x, y);
        }
    }
    public enum BlasterColor
    {
        white,
        orange,
        blue

    }
}
