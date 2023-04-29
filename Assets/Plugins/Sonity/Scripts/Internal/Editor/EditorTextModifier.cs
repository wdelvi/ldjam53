﻿// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

namespace Sonity.Internal {

    public class EditorTextModifier {

        public static string modifiersLabel = "Modifiers";
        public static string modifiersTooltip = $"Modifiers are used to control how {nameof(SoundEvent)}s are played (e.g. volume, polyphony, fade in length etc).";

        public static string addRemoveLabel = "Add/Remove";
        public static string addRemoveTooltip = $"Adds or removes a modifier.";

        public static string resetLabel = "Reset";
        public static string resetTooltip = $"Resets all the values of the modifiers.";

        public static string clearLabel = "Clear";
        public static string clearTooltip = $"Clears all the added modifiers.";

        public static string fadeShapeExponential = "Fade shape is exponential";
        public static string fadeShapeLogarithmic = "Fade shape is logarithmic";
        public static string fadeShapeLinear = "Fade shape is linear";

        private static string priority =
            "\n" + "\n" +
            $"The modifier with the highest priority will determine the value." + "\n" +
            $"1st: SoundParameter" + "\n" +
            $"2nd: {nameof(SoundMix)}" + "\n" +
            $"3rd: {nameof(SoundTrigger)}/{nameof(SoundPicker)}" + "\n" +
            $"4th: {nameof(SoundTag)}" + "\n" +
            $"5th: {nameof(SoundEvent)}";

        public static string volumeLabel = "Volume dB";
        public static string volumeTooltip = "Volume offset in decibel.";

        public static string pitchLabel = "Pitch st";
        public static string pitchTooltip = $"Pitch offset in semitones.";

        public static string delayLabel = "Delay";
        public static string delayTooltip = $"Increase the delay in seconds.";

        public static string startPositionLabel = "Start Position";
        public static string startPositionTooltip = $"Sets the start position, 0 is the start, 1 is the end." + priority;

        public static string reverseEnabledLabel = "Reverse";
        public static string reverseEnabledTooltip = 
            $"If enabled the AudioClip will be played backwards." + "\n" +
            "\n" +
            $"Make sure to set the start position to the end." + "\n" +
            "\n" +
            $"Reverse is only supported for AudioClips which are stored in an uncompressed format or will be decompressed at load time."
            + priority;

        public static string distanceScaleLabel = "Distance Scale";
        public static string distanceScaleTooltip =
            $"Distance scale multiplier (how far it will be heard)." + "\n" +
            "\n" +
            $"It is multiplied by the Distance Scale of the {nameof(SoundManager)}.";

        public static string distanceScaleWarning = $"Distance Scale is 0. The {nameof(SoundEvent)} will not be heard";
        public static string distanceScaleNotEnabledText = "Distance is not enabled";
        public static string distanceScaleNotEnabledTooltip = $"Distance is not enabled on the {nameof(SoundContainer)} of the {nameof(SoundEvent)}.";

        public static string reverbZoneMixDecibelLabel = "Reverb Zone Mix dB";
        public static string reverbZoneMixDecibelTooltip = $"Reverb Zone Mix volume offset in decibel.";

        public static string fadeInLengthLabel = "Fade In Length";
        public static string fadeInLengthTooltip = 
            $"The length of the fade in." + "\n" +
            "\n" +
            $"Uses Time.realtimeSinceStartup." 
            + priority;

        public static string fadeInShapeLabel = "Fade In Shape";
        public static string fadeInShapeTooltip = 
            $"Shape of the fade in." + "\n" +
            "\n" +
            $"Negative is exponential, 0 is linear, Positive is logarithmic."
            + priority;

        public static string fadeOutLengthLabel = "Fade Out Length";
        public static string fadeOutLengthTooltip =
            $"The length of the fade out." + "\n" +
            "\n" +
            $"Uses Time.realtimeSinceStartup."
            + priority;

        public static string fadeOutShapeLabel = "Fade Out Shape";
        public static string fadeOutShapeTooltip =
            $"Shape of the fade out." + "\n" +
            "\n" +
            $"Negative is exponential, 0 is linear, Positive is logarithmic."
            + priority;

        public static string increase2DLabel = "Increase 2D";
        public static string increase2DTooltip = 
            $"Makes the {nameof(SoundEvent)} more 2D (less spatialized)." + "\n" +
            "\n" +
            $"Useful for first person sounds.";

        public static string stereoPanLabel = "Stereo Pan";
        public static string stereoPanTooltip = 
            $"Stereo pan offset." + "\n" +
            "\n" +
            $"-1 is left, 0 is centered, +1 is right.";

        public static string intensityLabel = "Intensity";
        public static string intensityTooltip = $"Multiplier of any used {nameof(SoundParameterIntensity)} parameter.";

        public static string intensityNotEnabledText = "Intensity is not used";
        public static string intensityNotEnabledTooltip = $"Intensity is not enabled on the {nameof(SoundContainer)} of the {nameof(SoundEvent)}.";

        public static string distortionIncreaseLabel = "Distortion Increase";
        public static string distortionIncreaseTooltip = 
            $"Increases the distortion." + "\n" +
            "\n" +
            $"Distortion needs to be enabled on the {nameof(SoundContainer)} for this to have any effect.";

        public static string distortionIncreaseWarning = "Distortion Increase is 1. Watch out for high volume/distortion";
        public static string distortionNotEnabledText = "Distortion is not enabled";
        public static string distortionNotEnabledTooltip = $"Distortion is not enabled on the {nameof(SoundContainer)} of the {nameof(SoundEvent)}.";

        public static string polyphonyLabel = "Polyphony";
        public static string polyphonyTooltip = 
            $"How many instances of the {nameof(SoundEvent)} that can exist at the same transform."
            + priority;

        public static string followPositionLabel = "Follow Position";
        public static string followPositionTooltip = 
            $"If the {nameof(SoundEvent)} should follow the given Transform position." 
            + priority;

        public static string bypassReverbZonesLabel = "Bypass Reverb Zones";
        public static string bypassReverbZonesTooltip = 
            $"If enabled all reverb zones will be bypassed." 
            + priority;

        public static string bypassVoiceEffectsLabel = "Bypass Voice Effects";
        public static string bypassVoiceEffectsTooltip = 
            $"If enabled all voice effects (lowpass/highpass/distortion) will be bypassed." 
            + priority;

        public static string bypassListenerEffectsLabel = "Bypass Listener Effects";
        public static string bypassListenerEffectsTooltip = 
            $"If enabled all listener effects will be bypassed." 
            + priority;
    }
}
#endif