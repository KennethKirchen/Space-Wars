using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickup
{
    void BeginEffect(Collider2D collision);
    void CancelEffect();
}
