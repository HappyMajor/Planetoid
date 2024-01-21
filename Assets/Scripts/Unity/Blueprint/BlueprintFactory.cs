using UnityEngine;

public class BlueprintFactory
{
    private BuildingStore buildingStore;
    public static readonly string BP_SHELTER_ID = "id_bp_shelter";
    public static readonly string BP_HQ_ID = "id_bp_hq";
    public BlueprintFactory(BuildingStore buildingStore)
    {
        this.buildingStore = buildingStore;
    }

    public Blueprint CreateBlueprint(string id)
    {
        if (id == BP_SHELTER_ID)
        {
            Construction construction = new Construction();
            construction.BuildsTo = Buildings.SHELTER_ID;
            Blueprint blueprint = new Blueprint(id, "shelter", "A roof, a bed and heating.", Buildings.SHELTER_ID, construction);
            blueprint.BuildsTo = Buildings.SHELTER_ID;
            return blueprint;
        }

        if (id == BP_HQ_ID)
        {
            Construction construction = new Construction();
            construction.BuildsTo = Buildings.HQ_ID;
            Blueprint blueprint = new Blueprint(id, "Hq", "The headquarter.", Buildings.HQ_ID, construction);
            blueprint.BuildsTo = Buildings.HQ_ID;
            return blueprint;
        }

        return null;
    }

    public static Blueprint GetBlueprint(string id)
    {
        if (id == BP_SHELTER_ID)
        {
            Construction construction = new Construction();
            construction.BuildsTo = Buildings.SHELTER_ID;
            Blueprint blueprint = new Blueprint(id, "shelter", "A roof, a bed and heating.", Buildings.SHELTER_ID, construction);
            blueprint.BuildsTo = Buildings.SHELTER_ID;
            return blueprint;
        }

        if (id == BP_HQ_ID)
        {
            Construction construction = new Construction();
            construction.BuildsTo = Buildings.HQ_ID;
            Blueprint blueprint = new Blueprint(id, "Hq", "The headquarter.", Buildings.HQ_ID, construction);
            blueprint.BuildsTo = Buildings.HQ_ID;
            return blueprint;
        }

        throw new System.Exception("No blueprint with id " + id + " does not exist");
    }
}