using UnityEngine;

public class BlueprintFactory
{
    public static string ID_SHELTER = "bp_id_shelter";
    public static string ID_HQ = "bp_id_hq";

    private BuildingStore buildingStore;

    public BlueprintFactory(BuildingStore buildingStore)
    {
        this.buildingStore = buildingStore;
    }

    public Blueprint CreateBlueprint(string id)
    {
        if (id == ID_SHELTER)
        {
            Construction construction = (Construction) this.buildingStore.CreateBuilding(BuildingType.CONSTRUCTION);
            construction.BuildsTo = ID_SHELTER;
            Blueprint blueprint = new Blueprint(id, "shelter", "A roof, a bed and heating.", ID_SHELTER, construction);
            blueprint.BuildsTo = ID_SHELTER;
            return blueprint;
        }

        if (id == ID_HQ)
        {
            Construction construction = (Construction)this.buildingStore.CreateBuilding(BuildingType.CONSTRUCTION);
            construction.BuildsTo = ID_HQ;
            Blueprint blueprint = new Blueprint(id, "Hq", "The headquarter.", ID_HQ, construction);
            blueprint.BuildsTo = ID_HQ;
            return blueprint;
        }

        return null;
    }

    public static Blueprint GetBlueprint(string id)
    {
        if(id == ID_SHELTER)
        {
            Shelter shelter = new Shelter();
            Construction construction = new Construction();
            Blueprint blueprint = new Blueprint(id, "shelter", "A roof, a bed and heating.", shelter, construction);
            return blueprint;
        } 

        if(id == ID_HQ)
        {
            HQ hq = new HQ();
            Construction construction = new Construction();
            Blueprint blueprint = new Blueprint(id, "Hq", "The headquarter.", hq, construction);
            return blueprint;
        }

        throw new System.Exception("Id does not exist");
    }
}