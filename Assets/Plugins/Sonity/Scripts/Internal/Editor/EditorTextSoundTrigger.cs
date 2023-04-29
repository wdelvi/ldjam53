// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

using UnityEngine;

namespace Sonity.Internal {

    public class EditorTextSoundTrigger {

        public static string soundTriggerTooltip =
            $"{nameof(SoundTrigger)} is a component used for easily playing/stopping {nameof(SoundEvent)}s on callbacks built into Unity like Enable, Disable, OnCollisionEnter etc." + "\n" +
            "\n" +
            $"They contain {nameof(SoundEvent)}s with modifiers and triggers which decide when it should play or stop." + "\n" +
            "\n" +
            $"{nameof(SoundTrigger)}s also have a radius handle, which is visually editable in the scene viewport for easy adjustment of how far {nameof(SoundEvent)}s should be heard." + "\n" +
            "\n" +
            $"All {nameof(SoundTrigger)} components are multi-object editable.";

        public static string distanceRadiusLabel = "Distance Radius";
        public static string distanceRadiusTooltip = $"Distance of the {nameof(SoundEvent)} (how far it should be heard).";

        public static string radiusHandleWarningNoDistance = $"No {nameof(SoundContainer)} has distance enabled";
        public static string radiusHandleWarningNoSoundEvents = $"No {nameof(SoundEvent)}s";

        public static string soundEventLabel = $"{nameof(SoundEvent)}";
        public static string soundEventTooltip = $"The {nameof(SoundEvent)} which is played";

        // On Basic
        public static string onBasicLabel = $"Basic";
        public static string onBasicTooltip = $"";

        public static string onEnableLabel = $"On Enable";
        public static string onEnableTooltip = $"";

        public static string onDisableLabel = $"On Disable";
        public static string onDisableTooltip = $"";

        public static string onStartLabel = $"On Start";
        public static string onStartTooltip = $"";

        public static string onDestroyLabel = $"On Destroy";
        public static string onDestroyTooltip = $"";

        // On Trigger
        public static string onTriggerLabel = $"Trigger";
        public static string onTriggerTooltip = $"";

        public static string onTriggerEnterLabel = $"On Trigger Enter";
        public static string onTriggerEnterTooltip = $"";

        public static string onTriggerExitLabel = $"On Trigger Exit";
        public static string onTriggerExitTooltip = $"";

        public static string onTriggerEnter2DLabel = $"On Trigger Enter 2D";
        public static string onTriggerEnter2DTooltip = $"";

        public static string onTriggerExit2DLabel = $"On Trigger Exit 2D";
        public static string onTriggerExit2DTooltip = $"";

        public static string triggerTagLabel = $"Tag";
        public static string triggerTagTooltip = $"If enabled, the {nameof(SoundEvent)} will only play if the triggering object has a tag matching the selected tags.";

        // On Collision
        public static string onCollisionLabel = $"Collision";
        public static string onCollisionTooltip = $"";

        public static string onCollisionEnterLabel = $"On Collision Enter";
        public static string onCollisionEnterTooltip = $"";

        public static string onCollisionExitLabel = $"On Collision Exit";
        public static string onCollisionExitTooltip = $"";

        public static string onCollisionEnter2DLabel = $"On Collision Enter 2D";
        public static string onCollisionEnter2DTooltip = $"";

        public static string onCollisionExit2DLabel = $"On Collision Exit 2D";
        public static string onCollisionExit2DTooltip = $"";

        public static string velocityToIntensityLabel = $"Velocity to Intensity";
        public static string velocityToIntensityTooltip = $"If enabled, the velocity magnitude will be passed as an intensity parameter.";

        public static string collisionTagLabel = $"Tag";
        public static string collisionTagTooltip = $"If enabled, the {nameof(SoundEvent)} will only play if the collision object has a tag matching the selected tags.";

        // On Mouse
        public static string onMouseLabel = $"Mouse";
        public static string onMouseTooltip = $"";

        public static string onMouseEnterLabel = $"On Mouse Enter";
        public static string onMouseEnterTooltip = $"";

        public static string onMouseExitLabel = $"On Mouse Exit";
        public static string onMouseExitTooltip = $"";

        public static string onMouseDownLabel = $"On Mouse Down";
        public static string onMouseDownTooltip = $"";

        public static string onMouseUpLabel = $"On Mouse Up";
        public static string onMouseUpTooltip = $"";

        // Warning
        public static string warningRigidbody3D = $"{nameof(Rigidbody)} is required by the Triggers";
        public static string warningCollider3D = $"{nameof(Collider)} is required by the Triggers";

        public static string warningRigidbody2D = $"{nameof(Rigidbody2D)} is required by the Triggers";
        public static string warningCollider2D = $"{nameof(Collider2D)} is required by the Triggers";
    }
}
#endif