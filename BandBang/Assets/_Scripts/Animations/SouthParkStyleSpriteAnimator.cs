using UnityEngine;

public class SouthParkStyleSpriteAnimator : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    public SpriteRenderer sprite; // ASIGNA el child sprite

    public float moveThreshold = 0.01f;
    public float bobScale = 0.05f;
    public float tiltAngle = 3f;
    public float scaleSpeed = 0.15f;
    public float tiltSpeed = 0.15f;

    private Vector3 baseScale;
    private Vector2 lastPos;
    private bool animating;

    void Awake()
    {
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();
        baseScale = sprite.transform.localScale;
    }

    void Update()
    {
        Vector2 velocity = inputHandler.GetMoveInput();

        // Flip sprite
        if (velocity.x > 0.001f) sprite.flipX = true;
        else if (velocity.x < -0.001f) sprite.flipX = false;

        bool moving = velocity.magnitude > moveThreshold;

        if (moving && !animating) StartMoveAnim();

        else if (!moving && animating) StopMoveAnim();
    }

    void StartMoveAnim()
    {
        animating = true;
        //LeanTween.cancel(sprite.gameObject);

        // Bob scale
        LeanTween.scale(sprite.gameObject, baseScale + new Vector3(0, bobScale, 0), scaleSpeed)
            .setLoopPingPong()
            .setEase(LeanTweenType.easeInOutSine);

        // Tilt rotation
        if (!sprite.flipX)

            LeanTween.rotateZ(sprite.gameObject, tiltAngle, scaleSpeed)
         .setOnComplete(() => LeanTween.rotateZ(sprite.gameObject, -tiltAngle, scaleSpeed)).setLoopPingPong(-1);
        else

            LeanTween.rotateZ(sprite.gameObject, -tiltAngle, scaleSpeed)
         .setOnComplete(() => LeanTween.rotateZ(sprite.gameObject, tiltAngle, scaleSpeed)).setLoopPingPong(-1);

    }

    void StopMoveAnim()
    {
        animating = false;
        LeanTween.cancel(sprite.gameObject);
        LeanTween.scale(sprite.gameObject, baseScale, 0.1f);
        gameObject.transform.rotation = Quaternion.identity;
        GetComponent<SpriteAnimator2D>().SetAnimation(SpriteAnim.Idle);
    }
}
