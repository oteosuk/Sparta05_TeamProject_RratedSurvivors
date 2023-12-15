using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRange;
    [SerializeField] private LayerMask _targetLayer;
    private RaycastHit2D[] _targets;
    private Transform _nearestTarget;
    private Transform _randomTarget;

    public Transform NearestTarget { get { return _nearestTarget;} }
    public Transform RandomTarget { get { return _randomTarget;} }

    private void FixedUpdate() 
    {
        _targets = Physics2D.CircleCastAll(transform.position, _scanRange, Vector2.zero, 0, _targetLayer);
        _nearestTarget = GetNearest();
        _randomTarget = GetRandom();
    }

    private Transform GetNearest()
    {
        Transform result = null;

        float diff = 1000000;

        foreach(RaycastHit2D target in _targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }

    private Transform GetRandom()
    {
        Transform result = null;

        if(_targets.Length == 0)
            return null;

        int targetIdx = Random.Range(0, _targets.Length);

        result = _targets[targetIdx].transform;

        return result;
    }

}
