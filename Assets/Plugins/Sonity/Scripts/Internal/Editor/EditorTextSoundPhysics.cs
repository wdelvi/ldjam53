// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

using UnityEngine;

namespace Sonity.Internal {

    public class EditorTextSoundPhysics {

        public static string soundPhysicsTooltip =
            $"{nameof(SoundPhysics)} is a component used for easily playing {nameof(SoundEvent)}s on physics collisions and friction" + "\n" +
            "\n" +
            $"Use intensity record in the {nameof(SoundEvent)} for easy scaling of the velocity into a 0 to 1 range." + "\n" +
            "\n" +
            $"All {nameof(SoundPhysics)} components are multi-object editable.";

        public static string physicsDimensionLabel = $"Collision Type";
        public static string physicsDimensionTooltip = 
            $"If 3D is selected a {nameof(Rigidbody)} and  {nameof(Collider)} is required." + "\n" +
            "\n" +
            $"If 2D is selected a {nameof(Rigidbody2D)} and  {nameof(Collider2D)} is required.";

        public static string warningRigidbody3D = $"{nameof(Rigidbody)} required";
        public static string warningCollider3D = $"{nameof(Collider)} required";

        public static string warningRigidbody2D = $"{nameof(Rigidbody2D)} required";
        public static string warningCollider2D = $"{nameof(Collider2D)} required";

        public static string impactHeaderLabel = $"Impact";
        public static string impactHeaderTooltip = $"Impacts are played per contact point.";

        public static string impactSoundEventLabel = $"{nameof(SoundEvent)}";
        public static string impactSoundEventTooltip = $"The {nameof(SoundEvent)} which is played on impact.";

        public static string impactCollisionTagLabel = $"Collision Tag";
        public static string impactCollisionTagTooltip = $"If enabled the {nameof(SoundEvent)}s will only play on matching tags.";

        public static string frictionHeaderLabel = $"Friction";
        public static string frictionHeaderTooltip = $"Friction is played when touching another object.";

        public static string frictionSoundEventLabel = $"{nameof(SoundEvent)}";
        public static string frictionSoundEventTooltip = $"The {nameof(SoundEvent)} which is played on friction.";

        public static string frictionCollisionTagLabel = $"Collision Tag";
        public static string frictionCollisionTagTooltip = $"If enabled the {nameof(SoundEvent)}s will only play on matching tags.";
    }
}
#endif