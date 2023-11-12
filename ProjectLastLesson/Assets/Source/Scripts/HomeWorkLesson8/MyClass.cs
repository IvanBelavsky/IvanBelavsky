using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    public int a;

    private void Start()
    {
        MyClass myClass1 = new();
        myClass1.a = 10;
        MyClass myClass2 = new();
        myClass2.a = 20;
    }

    class Value
    {
        void Foo(ref MyClass a)
        {
            MyClass myClass = new();
            myClass.a = 40;
        }
    }
}
