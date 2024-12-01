using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    //[RequireComponent(typeof(Canvas))]
    public sealed class HeroListView : MonoBehaviour
    {
        private const int FORWARD_LAYER = 10;
        private const int BACK_LAYER = 0;

        public event Action<HeroView> OnHeroClicked;

        [SerializeField]
        //private List<HeroView> views;
        private HeroView[] views;

        private Canvas canvas;

        private void Awake()
        {
            this.canvas = GetComponentInParent<Canvas>();
            //this.canvas = this.GetComponent<Canvas>();
        }

        private void OnEnable()
        {
            foreach (var view in this.views)
            {
                view.OnClicked += () => this.OnHeroClicked?.Invoke(view);
            }
        }

        private void OnDisable()
        {
            Action<HeroView> @event = this.OnHeroClicked;
            if (@event == null)
            {
                return;
            }

            foreach (var @delegate in @event.GetInvocationList())
            {
                this.OnHeroClicked -= (Action<HeroView>) @delegate;
            }
        }

        public IReadOnlyList<HeroView> GetViews()
        {
            return this.views;
        }

        public HeroView GetView(int index)
        {
            return this.views[index];
        }

        public void SetActive(bool isActive)
        {
            this.canvas.sortingOrder = isActive ? FORWARD_LAYER : BACK_LAYER;
        }
    }
}