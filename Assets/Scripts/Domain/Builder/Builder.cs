using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Builder
{
    private IBuildingContext buildingContext;
    private List<Storage> storages = new List<Storage>();
    private List<string> availableBlueprints = new List<string>();
    private Dictionary<string, List<Blueprint>> standardBlueprints = new Dictionary<string, List<Blueprint>>();
    private Dictionary<string, List<Blueprint>> spaceBlueprints = new Dictionary<string, List<Blueprint>>();
    private Dictionary<string, List<Blueprint>> geoEngineeringBlueprints = new Dictionary<string, List<Blueprint>>(); 
    private Dictionary<string, List<Blueprint>> experimentalBlueprints = new Dictionary<string, List<Blueprint>>();
    private BlueprintRepository blueprintRepository;
    private BuildingStore buildingStore;

    public Dictionary<string, List<Blueprint>> StandardBlueprints { get => standardBlueprints; set => standardBlueprints = value; }
    public Dictionary<string, List<Blueprint>> SpaceBlueprints { get => spaceBlueprints; set => spaceBlueprints = value; }
    public Dictionary<string, List<Blueprint>> GeoEngineeringBlueprints { get => geoEngineeringBlueprints; set => geoEngineeringBlueprints = value; }
    public Dictionary<string, List<Blueprint>> ExperimentalBlueprints { get => experimentalBlueprints; set => experimentalBlueprints = value; }
    public List<string> AvailableBlueprints { get => availableBlueprints; set => availableBlueprints = value; }
    public List<Storage> Storages { get => storages; set => storages = value; }
    public IBuildingContext BuildingContext { get => buildingContext; set => buildingContext = value; }
    public BlueprintRepository BlueprintRepository { get => blueprintRepository; set => blueprintRepository = value; }

    public Builder(IBuildingContext buildingContext, BlueprintRepository blueprintRepository, BuildingStore buildingStore)
    {
        this.blueprintRepository = blueprintRepository;
        this.buildingContext = buildingContext;
        this.buildingStore = buildingStore;
    }

    public Builder() { }

    public void Build(string blueprintId)
    {
        Debug.Log("blueprintId: " + blueprintId);
        string foundAvailableBlueprintID = this.availableBlueprints.Find((bp) => bp == blueprintId);
        Blueprint foundAvailableBP = blueprintRepository.GetBlueprintById(foundAvailableBlueprintID);
        if(foundAvailableBlueprintID != null)
        {
            if(this.HasEnoughMaterials(foundAvailableBP.RequiredMaterialsToBuild))
            {
                this.RemoveMaterialsFromStorage(foundAvailableBP.RequiredMaterialsToBuild);
                this.buildingContext.BuildSomewhere(foundAvailableBP.Construction);
            } else
            {
                Debug.Log("Not enough materials!");
            }
        } else
        {
            Debug.Log("You do not have this blueprint.");
        }
    }

    public void BuildAt(string blueprintId, float x, float y)
    {
        Debug.Log("blueprintId: " + blueprintId);
        string foundAvailableBlueprintID = this.availableBlueprints.Find((bp) => bp == blueprintId);
        Blueprint foundAvailableBP = BlueprintFactory.GetBlueprint(foundAvailableBlueprintID);
        if (foundAvailableBlueprintID != null)
        {
            if (this.HasEnoughMaterials(foundAvailableBP.RequiredMaterialsToBuild))
            {
                this.RemoveMaterialsFromStorage(foundAvailableBP.RequiredMaterialsToBuild);
                Construction construction = foundAvailableBP.Construction;
                //construction.BuildingContext = buildingContext;
                this.buildingContext.Build(construction, x,y);
            }
            else
            {
                Debug.Log("Not enough materials!");
            }
        }
        else
        {
            Debug.Log("You do not have this blueprint.");
        }
    }

    private void RemoveMaterialsFromStorage(List<IMaterialStack> requiredMats)
    {
        foreach(IMaterialStack required in requiredMats)
        {
            //remove mats from each storage
            foreach(Storage storage in this.storages)
            {
                //Go through each Materialstack in storage and remove some of it (as required)
                foreach(IMaterialStack inStock in storage.StoredMaterials)
                {
                    if(inStock.MaterialId == required.MaterialId)
                    {
                        if(inStock.Amount < required.Amount)
                        {
                            required.Amount-=inStock.Amount;
                            inStock.Amount = 0;
                        } else
                        {
                            inStock.Amount -= required.Amount;
                            required.Amount = 0;
                        }
                    }
                }
            }
        }
    }
    private bool HasEnoughMaterials(List<IMaterialStack> requiredMats)
    {
        Dictionary<int, IMaterialStack> idMaterialStack = new Dictionary<int, IMaterialStack>();
        Dictionary<int, bool> idMaterialFullfilled = new Dictionary<int, bool>();

        foreach(IMaterialStack required in requiredMats)
        {
            foreach(Storage storage in this.storages)
            {
                //ADD AMOUNT IN STOCK HERE
                int amountInStock = storage.HasAmountInStock(required.MaterialId);
                if(amountInStock > 0)
                {
                    if(idMaterialStack.ContainsKey(required.MaterialId))
                    {
                        idMaterialStack[required.MaterialId].Amount += amountInStock;
                    } else
                    {
                        idMaterialStack[required.MaterialId] = new MaterialStack(required.MaterialId, required.Name, amountInStock);
                    }
                }

                //SEE IF ALREADY IS ENOUGH IN STOCK
                if(idMaterialStack.ContainsKey(required.MaterialId))
                {
                    if (idMaterialStack[required.MaterialId].Amount >= required.Amount)
                    {
                        //ADD TO FULLFILLED
                        idMaterialFullfilled[required.MaterialId] = true;
                    }
                }
            }
        }
        if (idMaterialFullfilled.Count == requiredMats.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
