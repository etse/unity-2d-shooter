using UnityEngine;

public interface IEnemy
{
    void setBehaviour(INPCBehaviour behaviour);

    void takeDamage(int damage);
    int health { get; set; }
}