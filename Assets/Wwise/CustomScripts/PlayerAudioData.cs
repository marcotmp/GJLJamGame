using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Player", fileName = "PlayerAudio")]

public class PlayerAudioData : ScriptableObject
{
    [Header("Wheels")]

    public AK.Wwise.Event wheelsMoveStart = null;

    public AK.Wwise.Event wheelsMoveStop = null;

    [Header("Legs")]

    public AK.Wwise.Event legsGetIn = null;

    public AK.Wwise.Event legsGetOut = null;

    public AK.Wwise.Event legsFootstep = null;

    public AK.Wwise.Event legsJump = null;

    public AK.Wwise.Event legsLand = null;

    [Header("Tank")]

    public AK.Wwise.Event tankGetIn = null;

    public AK.Wwise.Event tankGetOut = null;

    public AK.Wwise.Event tankMoveStart = null;

    public AK.Wwise.Event tankMoveStop = null;

    public AK.Wwise.Event tankShot = null;

    public AK.Wwise.Event tankShotExplosion = null;

    [Header("Helicopter")]

    public AK.Wwise.Event heliGetIn = null;

    public AK.Wwise.Event heliGetOut = null;

    public AK.Wwise.Event heliMoveStart = null;

    public AK.Wwise.Event heliMoveStop = null;

    public AK.Wwise.Event heliGrabObj = null;

    public AK.Wwise.Event heliReleaseObj = null;

}
