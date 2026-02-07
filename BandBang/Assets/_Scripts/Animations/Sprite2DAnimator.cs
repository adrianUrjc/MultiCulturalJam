using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public enum SpriteAnim
{
    Idle,
    Speak,
    Saltar,
    Nod,
    SayNo
}
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator2D : MonoBehaviour
{
    [Header("Current Animation")]
    [SerializeField]
    private LTDescr currentTween; // Tween activo
    [SerializeField]
    private SpriteAnim currentAnim = SpriteAnim.Speak;
    [Header("Idle Animation")]

    public float idleEscalaFactor = 1.05f; // cuánto se expande y contrae
    public float idleRotacionFactor = 3f;  // ligera rotación en grados
    public float idleDuracion = 1.5f;      // velocidad de “respirar”
    [Header("Speaking Animation")]
    // Configuraciones opcionales
    public float SpeakEscalaY = 1.2f;
    public float SpeakDuracion = 0.3f;

    [Header("Jump Animation")]
    public float saltoAltura = 2f;
    public float saltoDuracion = 0.5f;
    [Header("Nod Animation")]

    public float nodAngulo = 30f;
    public float nodDuracion = 0.3f;


    [Header("Shake Head Animation")]
    public float noOffsetX = 0.5f; // cuánto se mueve a los lados
    public float noDuracion = 0.2f; // tiempo por movimiento
    [Header("Logic")]
    [SerializeField] bool ignoreOriginalPosition = false;
    private SpriteRenderer spriteRenderer;
    // Guardar escala y rotación originales
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    private void Awake()
    {
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayAnimation();
    }
    public void SetOriginalPosition(Vector3 newPosition)
    {
        originalPosition = newPosition;
    }
    public void SetOriginalRotation(Quaternion newRotation)
    {
        originalRotation = newRotation;
    }

    public void SetAnimation(SpriteAnim anim)
    {
        currentAnim = anim;
        PlayAnimation();
    }
    public void SetAnimationIdx(int idx)
    {
        if (idx < 0 || idx >= Enum.GetValues(typeof(SpriteAnim)).Length)
        {
            Debug.LogError("Índice de animación fuera de rango: " + idx);
            return;
        }
        SetAnimation((SpriteAnim)idx);
    }

    [ContextMenu("Test Animacion")]

    public void PlayAnimation()
    {
        StopAnimation(false);



        switch (currentAnim)
        {
            case SpriteAnim.Idle:
                // Escalar ligeramente y rotar suavemente
                currentTween = LeanTween.scaleY(gameObject, originalScale.y * idleEscalaFactor, idleDuracion)
                    .setEaseInOutSine()
                    .setLoopPingPong(-1);

                // Opcional: si quieres también rotación
                LeanTween.rotateZ(gameObject, idleRotacionFactor, idleDuracion).setOnComplete(() =>
                 LeanTween.rotateZ(gameObject, -idleRotacionFactor, idleDuracion))
                    .setEaseInOutSine()
                    .setLoopPingPong(-1);
                break;

            case SpriteAnim.Speak:
                currentTween = LeanTween.scaleY(gameObject, originalScale.y * SpeakEscalaY, SpeakDuracion)
                    .setLoopPingPong(-1)
                    .setEaseInOutSine();
                break;

            case SpriteAnim.Saltar:
                currentTween = LeanTween.moveY(gameObject, originalPosition.y + saltoAltura, saltoDuracion)
                    .setLoopPingPong(-1)
                    .setEaseInOutSine();
                break;

            case SpriteAnim.Nod:
                currentTween = LeanTween.rotateZ(gameObject, nodAngulo, nodDuracion)
                    .setLoopPingPong(-1)
                    .setEaseInOutSine();
                break;
            case SpriteAnim.SayNo:
                currentTween = LeanTween.moveX(gameObject, originalPosition.x - noOffsetX, noDuracion)
       .setOnStart(() => spriteRenderer.flipX = false) // mirar hacia la izquierda
       .setOnComplete(() =>
       {
           LeanTween.moveX(gameObject, originalPosition.x + noOffsetX, noDuracion)
               .setOnStart(() => spriteRenderer.flipX = true) // mirar hacia la derecha
               .setOnComplete(() =>
               {
                   LeanTween.moveX(gameObject, originalPosition.x, noDuracion)
                       .setOnStart(() => spriteRenderer.flipX = false)
                       .setOnComplete(() =>
                       {
                           currentTween = null;
                           currentAnim = SpriteAnim.Idle;
                           PlayAnimation();
                       });
               });
       });
                break;

        }
    }

    public void StopAnimation(bool IdleAfter = true)
    {
        if (currentTween != null)
        {
            LeanTween.cancel(currentTween.id);
            LeanTween.cancel(gameObject); // Asegura cancelar cualquier tween activo en este objeto
            transform.localScale = originalScale;
            transform.rotation = originalRotation;
            if (!ignoreOriginalPosition) transform.position = originalPosition;
            currentTween = null;
            if (IdleAfter)
                currentAnim = SpriteAnim.Idle;
        }
    }
    void OnDisable()
    {
        StopAnimation();
    }
}
