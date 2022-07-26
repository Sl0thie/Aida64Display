// https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/effects/touch-tracking
// https://github.com/xamarin/xamarin-forms-samples/tree/main/Effects/TouchTrackingEffect/TouchTrackingEffect/TouchTrackingEffect
using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("XamarinDocs")]
[assembly: ExportEffect(typeof(TouchTracking.Droid.TouchEffect), "TouchEffect")]

namespace TouchTracking.Droid
{
    /// <summary>
    /// TouchEffect class.
    /// </summary>
    public class TouchEffect : PlatformEffect
    {
        private static readonly Dictionary<Android.Views.View, TouchEffect> ViewDictionary = new Dictionary<Android.Views.View, TouchEffect>();
        private static readonly Dictionary<int, TouchEffect> IdToEffectDictionary = new Dictionary<int, TouchEffect>();
        private readonly int[] twoIntArray = new int[2];
        private Android.Views.View view;
        private Element formsElement;
        private TouchTracking.TouchEffect libTouchEffect;
        private bool capture;
        private Func<double, double> fromPixels;

        /// <summary>
        /// OnAttached method is called after the effect is attached and made valid.
        /// </summary>
        protected override void OnAttached()
        {
            // Get the Android View corresponding to the Element that the effect is attached to.
            view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library.
            TouchTracking.TouchEffect touchEffect =
                (TouchTracking.TouchEffect)Element.Effects.
                    FirstOrDefault(e => e is TouchTracking.TouchEffect);

            if (touchEffect != null && view != null)
            {
                ViewDictionary.Add(view, this);

                formsElement = Element;

                libTouchEffect = touchEffect;

                // Save fromPixels function.
                fromPixels = view.Context.FromPixels;

                // Set event handler on View.
                view.Touch += OnTouch;
            }
        }

        /// <summary>
        /// OnDetached method is called after the effect is detached and made invalidated.
        /// </summary>
        protected override void OnDetached()
        {
            if (ViewDictionary.ContainsKey(view))
            {
                _ = ViewDictionary.Remove(view);
                view.Touch -= OnTouch;
            }
        }

        /// <summary>
        /// OnTouch method.
        /// </summary>
        /// <param name="sender">The object where the event originated.</param>
        /// <param name="args">The arguements parameter.</param>
        private void OnTouch(object sender, Android.Views.View.TouchEventArgs args)
        {
            // Two object common to all the events
            Android.Views.View senderView = sender as Android.Views.View;
            MotionEvent motionEvent = args.Event;

            // Get the pointer index
            int pointerIndex = motionEvent.ActionIndex;

            // Get the id that identifies a finger over the course of its progress
            int id = motionEvent.GetPointerId(pointerIndex);

            senderView.GetLocationOnScreen(twoIntArray);
            Point screenPointerCoords = new Point(twoIntArray[0] + motionEvent.GetX(pointerIndex), twoIntArray[1] + motionEvent.GetY(pointerIndex));

            // Use ActionMasked here rather than Action to reduce the number of possibilities
            switch (args.Event.ActionMasked)
            {
                case MotionEventActions.Down:
                case MotionEventActions.PointerDown:
                    FireEvent(this, id, TouchActionType.Pressed, screenPointerCoords, true);

                    IdToEffectDictionary.Add(id, this);

                    capture = libTouchEffect.Capture;
                    break;

                case MotionEventActions.Move:
                    // Multiple Move events are bundled, so handle them in a loop
                    for (pointerIndex = 0; pointerIndex < motionEvent.PointerCount; pointerIndex++)
                    {
                        id = motionEvent.GetPointerId(pointerIndex);

                        if (capture)
                        {
                            senderView.GetLocationOnScreen(twoIntArray);

                            screenPointerCoords = new Point(twoIntArray[0] + motionEvent.GetX(pointerIndex), twoIntArray[1] + motionEvent.GetY(pointerIndex));

                            FireEvent(this, id, TouchActionType.Moved, screenPointerCoords, true);
                        }
                        else
                        {
                            CheckForBoundaryHop(id, screenPointerCoords);

                            if (IdToEffectDictionary[id] != null)
                            {
                                FireEvent(IdToEffectDictionary[id], id, TouchActionType.Moved, screenPointerCoords, true);
                            }
                        }
                    }

                    break;

                case MotionEventActions.Up:
                case MotionEventActions.Pointer1Up:
                    if (capture)
                    {
                        FireEvent(this, id, TouchActionType.Released, screenPointerCoords, false);
                    }
                    else
                    {
                        CheckForBoundaryHop(id, screenPointerCoords);

                        if (IdToEffectDictionary[id] != null)
                        {
                            FireEvent(IdToEffectDictionary[id], id, TouchActionType.Released, screenPointerCoords, false);
                        }
                    }

                    _ = IdToEffectDictionary.Remove(id);
                    break;

                case MotionEventActions.Cancel:
                    if (capture)
                    {
                        FireEvent(this, id, TouchActionType.Cancelled, screenPointerCoords, false);
                    }
                    else
                    {
                        if (IdToEffectDictionary[id] != null)
                        {
                            FireEvent(IdToEffectDictionary[id], id, TouchActionType.Cancelled, screenPointerCoords, false);
                        }
                    }

                    _ = IdToEffectDictionary.Remove(id);
                    break;
            }
        }

        private void CheckForBoundaryHop(int id, Point pointerLocation)
        {
            TouchEffect touchEffectHit = null;

            foreach (Android.Views.View view in ViewDictionary.Keys)
            {
                // Get the view rectangle
                try
                {
                    view.GetLocationOnScreen(twoIntArray);
                }
                catch
                {
                    // System.ObjectDisposedException: Cannot access a disposed object.
                    continue;
                }

                Rectangle viewRect = new Rectangle(twoIntArray[0], twoIntArray[1], view.Width, view.Height);

                if (viewRect.Contains(pointerLocation))
                {
                    touchEffectHit = ViewDictionary[view];
                }
            }

            if (touchEffectHit != IdToEffectDictionary[id])
            {
                if (IdToEffectDictionary[id] != null)
                {
                    FireEvent(IdToEffectDictionary[id], id, TouchActionType.Exited, pointerLocation, true);
                }

                if (touchEffectHit != null)
                {
                    FireEvent(touchEffectHit, id, TouchActionType.Entered, pointerLocation, true);
                }

                IdToEffectDictionary[id] = touchEffectHit;
            }
        }

        private void FireEvent(TouchEffect touchEffect, int id, TouchActionType actionType, Point pointerLocation, bool isInContact)
        {
            // Get the method to call for firing events.
            Action<Element, TouchActionEventArgs> onTouchAction = touchEffect.libTouchEffect.OnTouchAction;

            // Get the location of the pointer within the view.
            touchEffect.view.GetLocationOnScreen(twoIntArray);
            double x = pointerLocation.X - twoIntArray[0];
            double y = pointerLocation.Y - twoIntArray[1];
            Point point = new Point(fromPixels(x), fromPixels(y));

            // Call the method.
            onTouchAction(touchEffect.formsElement, new TouchActionEventArgs(id, actionType, point, isInContact));
        }
    }
}