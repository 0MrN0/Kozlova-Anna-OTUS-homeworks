using System;
using Code.Services.Views;
using UnityEngine;

namespace Code.Components
{
    [Serializable]
    public struct TransformView
    {
        public Transform Value;
    }

    [Serializable]
    public struct RendererView
    {
        public Renderer Value;
    }

    [Serializable]
    public struct TriggerListenerView
    {
        public TriggerListener Value;
    }

    [Serializable]
    public struct AnimatorView
    {
        public Animator Value;
    }
}