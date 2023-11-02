// This script manages the behavior of nodes where towers can be built.
// It handles mouse interactions, such as hovering and right-clicking, 
// to provide feedback to the user and facilitate tower placement.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuilding : MonoBehaviour
{
    // Public variable to set the color when the cursor hovers over the node.
    public Color Hover_Colour;

    // Public Variable that offsets the postion of the tower.
    public Vector3 positionOffset;

    // Variable to reference the tower object if one is built on this node.
    private GameObject towerObj;

    // Renderer component for changing the node's color.
    private Renderer render;

    // Variable to store the original color of the node.
    private Color Node_Colour;

    // Initialization method called once at the start.
    void Start()
    {
        // Get the Renderer component from this object.
        render = GetComponent<Renderer>();
        
        // Store the original color of the node for later use.
        Node_Colour = render.material.color;
    }

    // Method called when the mouse cursor enters the node's collider area.
    void OnMouseEnter()
    {
        Node_Cursor_On();
    }

    // Method called continuously while the mouse cursor hovers over the node.
    void OnMouseOver()
    {
        // If the right mouse button is clicked, initiate tower placement.
        if (Input.GetMouseButtonDown(1))
        {
            OnRightMouseClick();
        }
    }

    // Handle actions to be performed on right mouse click.
    void OnRightMouseClick()
    {
        // Check if a tower already exists on this node.
        if (towerObj != null)
        {
            Debug.Log("Cannot build tower on occupied node");
            return;
        }

        // Fetch the tower to be built and instantiate it on this node.
        GameObject BuildTower = TowerBuildingManager.instance.GetBuildTower();
        TowerController tower = BuildTower.GetComponent<TowerController>();
        if (Global.HasPlayer())
        {
            AbstractPlayer player = GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>();
        
            if (!player.HasMoney(tower.GetCost())) return;
            player.RemoveMoney(tower.GetCost());
        }
        
        towerObj = Instantiate(BuildTower, transform.position + positionOffset, transform.rotation);
    }

    // Method called when the mouse cursor exits the node's collider area.
    void OnMouseExit()
    {
        Node_Cursor_Off();
    }

    // Change the node's color to indicate a hover state.
    void Node_Cursor_On()
    {
        render.material.color = Hover_Colour;
    }

    // Revert the node's color to its original state.
    void Node_Cursor_Off()
    {
        render.material.color = Node_Colour;
    }
}
