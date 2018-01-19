using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton
    public static EquipmentManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public Equipment[] defaultItems;

    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start() {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    // Equip a new item
    public void Equip(Equipment newItem) {
        // Find out what slot the item fits in
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        // An item has been equipped -> trigger callback
        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        setEquipmentBlendShapes(newItem, 100);

        // Insert the item into the slot
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    // Unequip an item with a particular index
    public Equipment Unequip(int slotIndex) {
        // If item is there, do this
        if (currentEquipment[slotIndex] != null) {
            if (currentMeshes[slotIndex] != null) {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            // Add the item to the inventory
            Equipment oldItem = currentEquipment[slotIndex];
            setEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);

            // Remove item form equipment array
            currentEquipment[slotIndex] = null;

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }

        return null;
    }

    // Unequip all items
    public void UnequipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    void setEquipmentBlendShapes(Equipment item, int weight) {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions) {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems() {
        foreach (Equipment item in defaultItems) {
            Equip(item);
        }
    }

    void Update() {
        // If "U" is pressed, Unequip all items
        if (Input.GetKeyDown(KeyCode.U)) {
            UnequipAll();
        }
    }
}