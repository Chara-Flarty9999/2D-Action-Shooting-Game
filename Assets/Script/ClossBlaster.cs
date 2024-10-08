using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEditorInternal;

public class ClossBlaster : MonoBehaviour
{
    SpriteRenderer spriterenderer;
    AudioSource audioSource;

    MoveInfo _moveInfo;
    StartInfo _startInfo;
    public float _blasterSize;
    int _beamWait;
    int _beamLoop;
    BlasterColor _blasterColor;

    [SerializeField] GameObject _BeamPrefab;
    [SerializeField] Sprite whileBeam;
    [SerializeField] Sprite endBeam;
    [SerializeField] AudioClip blasterSpawn = default;
    [SerializeField] AudioClip blasterbeam = default;

    [SerializeField] float testX;
    [SerializeField] float testY;
    [SerializeField] int testDi;

    [SerializeField] float testX2;
    [SerializeField] float testY2;
    [SerializeField] int testDi2;



    void Start()
    {
        GameObject spawner = GameObject.Find("SpawnArea");
        KnifeSpawn spawndata = spawner.GetComponent<KnifeSpawn>();
        spriterenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spriterenderer.color -= new Color(0f, 0f, 0f, 0f);
        spriterenderer.material.color = new Color(1f, 1f, 1f, 0f);
        new MoveInfo(transform.position, 5);

        _startInfo = spawndata.f_place;
        _moveInfo = spawndata.m_place;
        _blasterSize = spawndata.blasterSize;
        _beamWait = spawndata.beamWait;
        _beamLoop = spawndata.beamTime;
        _blasterColor = spawndata.blasterColor;


        ClossBlasterCharge(_startInfo, _moveInfo, _blasterSize, _beamWait, _beamLoop, _blasterColor);
    }

    // Update is called once per frame
    void Update()
    {
        


        
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
        this.transform.position = startInfo.vector3;
        this.transform.eulerAngles = new Vector3(0,0, startInfo.di);
        audioSource.PlayOneShot(blasterSpawn);
        this.spriterenderer.material.DOFade(1f, 1.5f);

        Debug.Log("処理したぞ");

        StartCoroutine(ClossBlasterFire(moveInfo, waittime, looplong, beamcolor));
    }

    IEnumerator ClossBlasterFire(MoveInfo moveinfo, int wait, int looplong,BlasterColor beamcolor)
    {
        yield return new WaitForSeconds(wait);
        spriterenderer.sprite = whileBeam;
        audioSource.PlayOneShot(blasterbeam);
        Debug.Log("スプライトまでは変えました");
        //Instantiateを追加する
        var parent = this.transform;
        Instantiate(_BeamPrefab, Vector3.zero, Quaternion.identity, parent);
        for (int i = 0; i < (looplong + 2) * 50; i++) 
        {
            Debug.Log("処理中");
            transform.position += moveinfo.vector3;
            transform.eulerAngles += new Vector3(0, 0, moveinfo.di);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("ループしたよ");
        spriterenderer.sprite = endBeam;
        GameObject gameObject = GameObject.Find("ClossBeam(Clone)");
        ClossBlasterBeam clossBlasterBeam = gameObject.GetComponent<ClossBlasterBeam>();
        clossBlasterBeam.ClossBlasterBeamExit();
        this.spriterenderer.material.DOFade(0f, 0.5f);
    }


    /// <summary>
    /// ブラスターの移動情報を入力する
    /// </summary>
    public struct MoveInfo
    {
        public float x;
        public float y;
        public Vector3 vector3;
        public int di;

        public MoveInfo(Vector3 vector3, int di)
        {
            this.vector3 = vector3;
            this.di = di;
            this.x = vector3.x;
            this.y = vector3.y;
        }
        public MoveInfo(float x, float y, int di)
        {
            this.x = x;
            this.y = y;
            this.di = di;
            this.vector3 = new Vector3(x, y);
        }
    }
    /// <summary>
    /// ブラスターの初期位置を入力する
    /// </summary>
    public struct StartInfo
    {
        public float x;
        public float y;
        public Vector3 vector3;
        public int di;

        public StartInfo(Vector3 vector3, int di)
        {
            this.vector3 = vector3;
            this.di = di;
            this.x = vector3.x;
            this.y = vector3.y;
        }

        public StartInfo(float x, float y, int di)
        {
            this.x = x;
            this.y = y;
            this.di = di;
            this.vector3 = new Vector3(x, y);
        }
    }
    public enum BlasterColor
    {
        white,
        orange,
        blue

    }
}
