using System.Collections;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField, Tooltip("Rotation speed")] 
    private float _speed = 100f;
    private float _spinTime = 3.5f;

    private bool _isSpining = false;
    public bool IsSpinning
    {
        get { return _isSpining; }
        private set { _isSpining = value; }
    }


    #region MonoBehaviour methods
    private void Update()
    {
        if (IsSpinning)
        {
            transform.Rotate(-_speed * Time.deltaTime, 0, 0);
        }
    }
    #endregion

    private void RoundRotation()
    {
        int tmp, number = (int)transform.rotation.eulerAngles.x;
        if ((tmp = number % 30) != 0) // 360 грудсов делить на 12 граней = 30
        {
            number += number > -1 ? (30 - tmp) : -tmp;
        }
        transform.rotation = Quaternion.Euler(number, 0, 0);
    }

    public void StartSpin()
    {
        IsSpinning = true;
        StopAllCoroutines();
        StartCoroutine("StopSpin");
    }

    private IEnumerator StopSpin()
    {
        yield return new WaitForSeconds(_spinTime);
        RoundRotation();
        IsSpinning = false;
    }

}
