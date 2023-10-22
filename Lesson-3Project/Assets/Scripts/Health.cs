using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float _health;
     float _damag;

    void Start()
    {
        Debug.Log("I'm Hero! Hello!");
        Debug.Log("My health: " + _health);
    }


    public void TakeDamag(int damag)
    {
        

        for(int i = 0; i <= _health; i++)
        {
            _health -= damag;
            if (_health < 0)
            {
                _health = 0;
            }
            Debug.Log("Вам нанесли урон:" + damag+ "! У вас осталось здоровья: " + _health);
            if(_health==0)
                Debug.Log("Game over");

        }
       
    }
    
    
}
