using Random = UnityEngine.Random;
using GeneralEnums;
using UnityEngine;

public abstract class Arrow : MonoBehaviour
{
    #region Public Variables
    public enum ArrowType
    {
        Blank,
        Filled
    }

    public Direction Direction { get; protected set; }
    public ArrowType Type { get; protected set; }
    #endregion

    #region Serialized Private Variables

    #endregion

    #region Private Variables
    protected const string _fadeIn = "ArrowFadeIn";
    protected const string _fadeOut = "ArrowFadeOut";

    protected Vector2 _desiredVector;
    #endregion

    #region Unity Methods

    #endregion

    #region Public Methods

    public abstract void MoveAway();

    public abstract void Kaboom();
    #endregion

    #region Private Methods


    protected void SetRandomDir()
    {
        Direction = (Direction)Random.Range(0, 4);
        transform.rotation = Quaternion.Euler(0f, 0f, 90f * (int)Direction);
    }

    protected void SetDesiredVector()
    {
        switch (Direction)
        {
            case Direction.Right:
                _desiredVector = Type == ArrowType.Filled ? Vector2.right : Vector2.left;
                break;
            case Direction.Up:
                _desiredVector = Type == ArrowType.Filled ? Vector2.up : Vector2.down;
                break;
            case Direction.Left:
                _desiredVector = Type == ArrowType.Filled ? Vector2.left : Vector2.right;
                break;
            case Direction.Down:
                _desiredVector = Type == ArrowType.Filled ? Vector2.down : Vector2.up;
                break;
            default:
                _desiredVector = Vector2.zero;
                break;
        }
    }

    protected void SetType(ArrowType type)
    {
        Type = type;
    }


    #endregion
}
