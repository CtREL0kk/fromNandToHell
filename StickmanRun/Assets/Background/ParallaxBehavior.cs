using UnityEngine;
using UnityEngine.Serialization;

public class ParallaxBehavior : MonoBehaviour
{
    [FormerlySerializedAs("followingTarget")] [SerializeField] private Transform _followingTarget;

    [FormerlySerializedAs("parallaxStrength")] [SerializeField, Range(0f, 1f)] private float _parallaxStrength = 0.1f;

    [FormerlySerializedAs("disableVerticalParallax")] [SerializeField] private bool _disableVerticalParallax;

    private Vector3 targetPreviousPosition;
    
    void Start()
    {
        if (!_followingTarget)
            _followingTarget = Camera.main.transform;

        targetPreviousPosition = _followingTarget.position;
    }

    
    void Update()
    {
        var delta = _followingTarget.position - targetPreviousPosition;

        if (_disableVerticalParallax)
            delta.y = 0;

        targetPreviousPosition = _followingTarget.position;
        transform.position += delta * _parallaxStrength;
    }
}