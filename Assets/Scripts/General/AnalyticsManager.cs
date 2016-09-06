using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnalyticsManager : MonoBehaviour {
    List<EntityControlScript> EntityList = new List<EntityControlScript>();
    static int totalDamageDeltToVehicle;
    static int totalDamageDeltByZombies;
    static int totalDamageDeltByRocks;
    static int totalDamageDeltByFreddys;
    
   /*
    * TODO: FOR TUDOR create a dictionary where stats damage updates are handled
    * throught Key = <Damager.type, Damagable.type> value = updateDamageFunction (Damager, Damageable)
    */
 //   Dictionary<>


    public void updateMe(EntityControlScript entity) {
        // print(EnityList.Find(x => entity).name);
        int idx = EntityList.IndexOf(entity);
        print(EntityList[idx].name);
    }
    public void AddToEnityList(EntityControlScript entitty) {
        if (!EntityList.Contains(entitty)){
            EntityList.Add(entitty);
            print(entitty.name);
            updateMe(entitty);
        }

    }

    public void UpdateTotalDamageDeltToVehicle(int damage) {

    }

    public void UpdateTotalGlobalDamage() {

    }

    // public void UpdateTotalDam

    public void UpdateTotalDamageDeltByFreddys(int damage) {

    }

    public void UpdateTotalDamageDeltByRocks(int damage) {
         
    }

    public void CalculateMVPFreddys() {

    }


}
