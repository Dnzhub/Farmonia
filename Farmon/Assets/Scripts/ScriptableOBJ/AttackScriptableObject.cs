using UnityEngine;


[CreateAssetMenu(fileName = "AttackSO", menuName = "ScriptableObjects/AttackAttributes")]
public class AttackScriptableObject : ScriptableObject
{     
    public float attackRadius = 1f;
    public int Damage = 2;
}

