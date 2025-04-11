using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Plant")] public class Plant : organism
{
   public int plantGrowStage = 0;
   public GameObject plantGrowStagePrefab2;
   public GameObject plantGrowStagePrefab3;
   public override void OrganismBehavior()
   {
      
   }
}
