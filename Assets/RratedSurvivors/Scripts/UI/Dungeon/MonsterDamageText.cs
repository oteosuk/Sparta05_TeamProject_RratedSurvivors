using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterDamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageTex;
    private Vector3 movePos = Vector3.zero;
    public void SetDamage(int damage)
    {
        damageTex.text = damage.ToString();
    }

    public void TextAnimation()
    {
        movePos.Set(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        StartCoroutine(TextAnim(movePos));

        //damageTex.transform.DOMoveY(transform.position.y + 0.5f, 0.5f).OnComplete(() =>
        //{
        //    Managers.Resource.Destroy(this.gameObject);
        //});
    }

    private IEnumerator TextAnim(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            float step = 1.5f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            yield return null;
        }
        Managers.Resource.Destroy(this.gameObject);
    }
}
