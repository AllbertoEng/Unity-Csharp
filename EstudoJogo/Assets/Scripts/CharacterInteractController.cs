using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    CharacterControler2D characterController;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f,
                        sizeOfInteractableArea = 0.2f;
    Character character;
    [SerializeField] HighlightController highlightController;

    private void Awake()
    {
        characterController = GetComponent<CharacterControler2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        Check();

        if (Input.GetMouseButton(1))
        {
            Interact();
        }
    }

    private void Check()
    {
        Vector2 position = rgbd2d.position + characterController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }

        highlightController.Hide();
    }

    private void Interact()
    {
        Vector2 position = rgbd2d.position + characterController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    }
}
