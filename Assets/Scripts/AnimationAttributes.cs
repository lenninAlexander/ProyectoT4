using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationAttributes", menuName = "Scripts/AnimationAttributes", order = 2)]

public class AnimationAttributes : ScriptableObject
{
    public bool isIdle;
    public bool isWalk;
    public bool isJump;
}
