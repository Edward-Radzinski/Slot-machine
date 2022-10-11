using System.Collections;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField, Tooltip("Rotation speed")] 
    private float _speed = 100f;
    private float _spinTime = 3.5f;

    public float SpinTime
    {
        get { return _spinTime; }
        private set { _spinTime = value; }
    }
    public bool IsSpinning { get;  private set; }

    private void Update()
    {
        if (IsSpinning)
        {
            transform.Rotate(-_speed * Time.deltaTime, 0, 0);
        }
    }

    private void RoundRotation()
    {
        int tmp, angle = (int)transform.rotation.eulerAngles.x;
        if ((tmp = angle % 30) != 0) // 360 divided by 12 = 30
        {
            angle += angle > -1 ? (30 - tmp) : -tmp;
        }
        transform.rotation = Quaternion.Euler(angle, 0, 0);
    }

    public void StartSpin()
    {
        StopAllCoroutines();
        StartCoroutine("StopSpin");
    }

    private IEnumerator StopSpin()
    {
        IsSpinning = true;
        yield return new WaitForSeconds(SpinTime);
        RoundRotation();
        IsSpinning = false;
    }

}
