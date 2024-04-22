﻿using UnityEngine;

namespace _Scripts.Components
{
    public class RenderSorting : MonoBehaviour
    {
        public static readonly int SortingDiscretion = 6;

        public void SetSortingLayer()
        {
            var sprites = FindObjectsOfType<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                if (sprite.sortingLayerName != "Grass")
                    sprite.sortingOrder = -(int)(sprite.transform.position.y * SortingDiscretion);
            }
        }
    }
}