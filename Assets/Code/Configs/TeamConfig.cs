using System;
using Code.Data;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "TeamConfig", menuName = "Configs / Team Config")]
    public sealed class TeamConfig : ScriptableObject
    {
        [Serializable]
        public struct TeamView
        {
            public TeamType Team;
            public Material Material;
        }

        [SerializeField] private TeamView[] _teams;

        public Material GetMaterial(TeamType team)
        {
            foreach (var view in _teams)
                if (view.Team == team)
                    return view.Material;

            Debug.LogError($"No material for team {team}", this);
            return null;
        }
    }
}