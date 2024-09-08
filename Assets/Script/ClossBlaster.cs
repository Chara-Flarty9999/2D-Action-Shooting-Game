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
    /// �\���u���X�^�[���쓮�����郁�\�b�h�B�����ʒu��ړ����x�Ȃǂ͎��O�ɕϐ��Ɋi�[���Ă����悤�ɁB
    /// </summary>
    /// <param name="startInfo">��������X���W�AY���W�A�p�x��ݒ肷��B�p�x�͓x���@��OK�B���W��Vector�ł�OK�B</param>
    /// <param name="moveInfo">�r�[�����ˌ�Ƀu���X�^�[�{�̂�X�AY�ɑ΂��āA�܂��p�x����b�ɂǂꂾ������������ݒ肷��B</param>
    /// <param name="size">�u���X�^�[�̃T�C�Y��ݒ肷��B��{��1��OK�B</param>
    /// <param name="waittime">�u���X�^�[�������甭�˂܂ł̒x�����Ԃ�ݒ肷��B</param>
    /// <param name="looplong">�r�[�����˂̏���������J��Ԃ����A�܂艽�b�ԃr�[���𔭎˂��邩�ݒ肷��B</param>
    /// <param name="beamcolor">�������I�����W��ݒ肷��B</param>
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
