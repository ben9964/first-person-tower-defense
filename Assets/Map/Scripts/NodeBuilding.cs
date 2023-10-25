using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuilding : MonoBehaviour
{
    public Color Hover_Colour;

    private Renderer render;
    private Color Node_Colour;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        Node_Colour = render.material.color;  // Save the original color
    }

    // Called when the mouse enters the GameObject's collider
    void OnMouseEnter()
    {
        Node_Cursor_On();
    }

    // Called when the mouse exits the GameObject's collider
    void OnMouseExit()
    {
        Node_Cursor_Off();
    }

    void Node_Cursor_On()
    {
        render.material.color = Hover_Colour;
    }

    void Node_Cursor_Off()
    {
        render.material.color = Node_Colour;
    }
}
