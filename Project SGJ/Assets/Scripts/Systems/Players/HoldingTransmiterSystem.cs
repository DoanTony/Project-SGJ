using UnityEngine;
using System.Collections;
using Unity.Entities;
using System;

public class HoldingTransporterSystem : ComponentSystem
{
    struct Characters
    {
        public readonly int Length;
        public ComponentArray<CharacterComponent> CharacterComponent;
    }

    [Inject] Characters _Characters;

    protected override void OnUpdate()
    {
        for (int i = 0; i < _Characters.Length; i++)
        {
            ToggleHeadTransporter(_Characters.CharacterComponent[i]);
        }   
    }

    private void ToggleHeadTransporter(CharacterComponent _characters)
    {
        _characters.transporter.enabled = _characters.hasTransporter;
    }
}
