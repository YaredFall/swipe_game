using System;
using GeneralEnums;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ParticleSystem))]
public class ColorizedArrow : Arrow
{
    #region Public Variables

    public ColorName ColorName { get; private set; }

    #endregion

    #region Serialized Private Variables
    [SerializeField] private Sprite _sprite = null;

    [Range(0, 30)] [SerializeField] private float _speed = 15f;

    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods
    
    private void Start()
    {
        Init();
    }

    #endregion

    #region Public Methods
    public void Init()
    {
        SetRandomColor();
        SetType(ArrowType.Filled);
        SetSprite();

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
    public void MoveAway(bool arrowType)
    {
        _desiredVector *= arrowType ? 1 : -1;
        GetComponent<Rigidbody2D>().velocity = _desiredVector * _speed;
        GetComponent<Animator>().Play(_fadeOut);

        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (!arrowType)
        {
            var shape = ps.shape;
            shape.rotation = new Vector3(0, 90, 0);
            shape.position = new Vector3(1f, 0, 0);
        }
        ps.Play();

    }

    public override void Kaboom()
    {
        Destroy(gameObject);
    }
    #endregion

    #region Private Methods
    new private void SetRandomDir()
    {
        int rndm = Random.Range(0, 2) == 0 ? 0 : 2;
        Direction = (Direction)rndm;
        transform.localScale = Direction == Direction.Right ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    private void SetRandomColor()
    {
        ColorName = (ColorName)Random.Range(0, Enum.GetNames(typeof(ColorName)).Length);
        GetComponent<SpriteRenderer>().color = Colors.GetColor(ColorName);
        
    }

    private void SetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _sprite;
    }

    private void SetParticleSystem()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = Colors.GetColor(ColorName);
        var shape = ps.shape;
        ps.textureSheetAnimation.SetSprite(0, _sprite);
        shape.rotation = new Vector3(0, -90, 0);
        shape.position = new Vector3(-1f, 0, 0);
    }
    #endregion
}
