using UnityEngine;
using Random = UnityEngine.Random;
using GeneralEnums;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ParticleSystem))]
public class BaseishArrow : Arrow
{


    #region Public Variables
    #endregion

    #region Serialized Private Variables
    [SerializeField] private Sprite _filledSprite = null;
    [SerializeField] private Sprite _blankSprite = null;

    [Range(0, 30)] [SerializeField] private float _speed = 15f;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods

    #endregion

    #region Public Methods
    public void Init(ArrowMode arrowMode)
    {
        switch (arrowMode)
        {
            case ArrowMode.FilledOnly:
                SetType(ArrowType.Filled);
                break;
            case ArrowMode.BlankOnly:
                SetType(ArrowType.Blank);
                break;
            case ArrowMode.Base:
                SetRandomType();
                break;
            default:
                break;
        }
        SetSprite();
        SetColor();
        SetParticleSystem();
        SetRandomDir();
        SetDesiredVector();
        GetComponent<Animator>().Play(_fadeIn);
    }

    public override void MoveAway()
    {
        GetComponent<Rigidbody2D>().velocity = _desiredVector * _speed;
        GetComponent<Animator>().Play(_fadeOut);
        GetComponent<ParticleSystem>().Play();
    }

    public override void Kaboom()
    {
        Destroy(gameObject);
    }
    #endregion

    #region Private Methods
    private void SetRandomType()
    {
        Type = (ArrowType)Random.Range(0, 2);
    }

    private void SetColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, 0);
    }

    private void SetParticleSystem()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var shape = ps.shape;
        if (Type == ArrowType.Filled)
        {
            ps.textureSheetAnimation.SetSprite(0, _filledSprite);
            shape.rotation = new Vector3(0, -90, 0);
            shape.position = new Vector3(-1f, 0, 0);
        }
        else
        {
            ps.textureSheetAnimation.SetSprite(0, _blankSprite);
            shape.rotation = new Vector3(180, -90, 0);
            shape.position = new Vector3(0.9f, 0, 0);
        }
    }
    private void SetSprite()
    {
        if (_filledSprite != null && _blankSprite != null)
            GetComponent<SpriteRenderer>().sprite = Type == ArrowType.Filled ? _filledSprite : _blankSprite;
        else
            Debug.LogError("Sprites are not set!");
    }
    #endregion
}
