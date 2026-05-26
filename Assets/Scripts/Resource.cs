using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Resource
{
    public static int _currentResource { get; private set; }
    public void ResetResource()
    {
        _currentResource = 0;
    }
    public int GetCurrentResource()
    {
        return _currentResource;
    }
    public virtual void AddResource(int amount)
    {
        _currentResource += (int)MathF.Abs(amount);
    }
}
