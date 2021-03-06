using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVehicle
{
    void MountPlayer(PlayerController player);
    void UnmountPlayer();
    void Deactivate();
    Transform transform {get;}
}
