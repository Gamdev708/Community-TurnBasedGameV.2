using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTags
{
    public const string MOVEMENT = "Movement";

    public const string PUNCH1_TRIGGER = "Punch1";
    public const string PUNCH2_TRIGGER = "Punch2";
    public const string PUNCH3_TRIGGER = "Punch3";

    public const string KICK1_TRIGGER = "Kick1";
    public const string KICK2_TRIGGER = "Kick2";

    public const string ATTACK1_TRIGGER = "Attack1";
    public const string ATTACK2_TRIGGER = "Attack2";
    public const string ATTACK3_TRIGGER = "Attack3";

    public const string IDLE = "Idle";

    public const string KNOCKDOWN_TRIGGER = "Knockdown";
    public const string STANDUP_TRIGGER = "Standup";
    public const string HIT_TRIGGER = "HIt";
    public const string DEATH_TRIGGER = "Death";


}

public class Axis
{
    public const string HORIZONTAL_AXIS="Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
}

public class Tags
{
    public const string GROUND_TAG = "Ground";
    public const string PLAYER_TAG = "Player";
    public const string PLAYERTEAM_TAG = "Team";
    public const string ENEMY_TAG = "Enemy";

    public const string LEFTARM_TAG = "LeftArm";
    public const string LEFTLEG_TAG = "LeftLeg";
    public const string RIGHTARM_TAG = "RightArm";
    public const string UNTAGGED_TAG = "Untagged";
    public const string MAIN_CAM_TAG = "MainCamera";
    public const string HEALTH_UI = "HealthUI";
}
