using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int damage = 100;
    
    public int getDamage()
    {
        return damage; //returns damage
    }

    public void Hit()
    {
        Destroy(gameObject); //when hit, destroys the gameobject
    }
}
