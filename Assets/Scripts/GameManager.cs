using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Spin[] _cylinders;
    private Animator _animator;
    private int _currentCylinder = 0;
    private bool _isSpinned = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_currentCylinder < _cylinders.Length && CheckSpinCylinder(_currentCylinder) == false && _isSpinned)
        {
            _cylinders[_currentCylinder].StartSpin();
            StartCoroutine("NextCylinder");
        }
        else if(_currentCylinder >= _cylinders.Length - 1 && CheckSpinCylinder(_cylinders.Length - 1) == false)
        {
            _isSpinned = false;
        }
    }

    private void OnMouseDown()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ActiveSpin") == false && _isSpinned == false)
        {
            _animator.SetTrigger("Spin");
            _currentCylinder = 0;
            _isSpinned = true;
        }
    }

    private bool CheckSpinCylinder(int numb)
    {
        if (_cylinders[numb].IsSpinning)
        {
            return true;
        }
        return false;
    }

    private IEnumerator NextCylinder()
    {
        yield return new WaitForSeconds(_cylinders[_currentCylinder].SpinTime);
        _currentCylinder++;
    }
}
