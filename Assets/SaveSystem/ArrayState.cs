using System;

[Serializable]
public class ArrayState<T>
{
    public T[] Array;

    public ArrayState(T[] array)
    {
        Array = array;
    }
}