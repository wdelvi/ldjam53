// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.Audio;

namespace Sonity.Internal {

    public class EditorTextSoundEvent {

        public static string soundEventTooltip = 
        $"{nameof(SoundEvent)}s are what you play in Sonity." + "\n" +
        "\n" +
        $"They contain {nameof(SoundContainer)} and options of how the sound should be played." + "\n" +
        "\n" +
        $"All {nameof(SoundEvent)}s are multi-object editable.";

        // Reset
        public static string resetSettingsLabel = $"Reset Options";
        public static string resetSettingsTooltip = $"Resets all but the {nameof(SoundContainer)}s.";

        public static string resetAllLabel = $"Reset All";
        public static string resetAllTooltip = $"Resets everything.";

        // Find References
        public static string findReferencesLabel = $"Find References";
        public static string findReferencesTooltip = $"Finds all the references to the {nameof(SoundEvent)}.";

        public static string findReferencesSelectAllLabel = $"Select All";
        public static string findReferencesSelectAllTooltip = $"Selects all the assets with references to the {nameof(SoundEvent)}.";

        public static string findReferencesClearLabel = $"Clear";
        public static string findReferencesClearTooltip = $"Removes all the found references.";

        // Warnings
        public static string soundContainerWarningEmpty = $"Empty {nameof(SoundContainer)}";
        public static string soundContainerWarningNull = $"Null {nameof(SoundContainer)}";

        public static string soundTagWarningEmpty = $"Empty {nameof(SoundTag)}";

        public static string triggerOtherSoundEventsWarningEmpty = $"Empty {nameof(SoundEvent)}";
        public static string triggerOtherSoundEventsWarningNull = $"Null {nameof(SoundEvent)}";

        public static string triggerOnTailWarningNoAudioClipFound = $"No {nameof(AudioClip)} found on the first {nameof(SoundContainer)}";
        public static string triggerOnTailWarningTailLengthIsTooLong = $"Tail Length is longer than the shortest {nameof(AudioClip)} on the first {nameof(SoundContainer)}";
        public static string triggerOnTailWarningLengthWarning = $"The shortest {nameof(AudioClip)} length is";

        // Mute, Solo, Disable
        public static string muteEnableLabel = $"Mute";
        public static string muteEnableTooltip = 
            $"Mutes the {nameof(SoundEvent)}. " + "\n" +
            $"Only affects the Unity Editor.";

        public static string soloEnableLabel = $"Solo";
        public static string soloEnableTooltip = 
            $"Mutes all other {nameof(SoundEvent)}s who don't have solo enabled." + "\n" +
            "\n" +
            $"The Solo property is not serialized (e.g. will be reset on start)." + "\n" +
            "\n" +
            $"This is to prevent leaving a {nameof(SoundEvent)} soloed by mistake which would make nothing else sound when the game runs." + "\n" +
            "\n" +
            $"Only affects the Unity Editor.";

        public static string disableEnableLabel = $"Disable";
        public static string disableEnableTooltip =
            $"Disables the playing of the {nameof(SoundEvent)}." + "\n" +
            "\n" +
            $"It is also disabled when building the project.";

        public static string findSoundContainersLabel = "Find SoundContainers";
        public static string findSoundContainersTooltip =
            $"Automatically finds all {nameof(SoundContainer)}s containing the same name as this {nameof(SoundEvent)} (disregarding _SE, numbers)." + "\n" +
            "\n" +
            $"Can be used on multiple items at once (tip: search \"t:soundEvent\")." + "\n" +
            "\n" +
            $"If no matching {nameof(SoundContainer)}s are found, it will try and remove one character at the end of the name at a time until it finds a hit.";

        // Timeline
        public static string timelineExpandLabel = "Timeline";
        public static string timelineExpandTooltip =
            $"Zoom: Ctrl + mouse wheel scroll." + "\n" +
            "\n" +
            $"Pan: Mouse wheel hold and drag (or left mouse button hold and drag on the background)." + "\n" +
            "\n" +
            $"Volume: Hold and drag top of item up/down or click on the volume to write the decibel value." + "\n" +
            "\n" +
            $"Move item: Hold and drag on item left/right." + "\n" +
            "\n" +
            $"Focus on items: F";

        public static string timelineResetLabel = "Reset";
        public static string timelineResetTooltip = "Reset all the Timeline settings.";

        public static string timelineFocusLabel = "Focus";
        public static string timelineFocusTooltip = 
            $"Focus on the {nameof(SoundContainer)}s in the timeline." + "\n" +
            "\n" +
            $"Shortcut key: F.";

        // Settings
        public static string polyphonyModeLabel = "Polyphony Mode";
        public static string polyphonyModeTooltip =
            $"Limited Per Owner:" + "\n" +
            "\n" +
            $"Useful if you want to limit polyphony e.g per player." + "\n" +
            "\n" +
            $"You can use e.g {nameof(SoundEvent.PlayAtPosition)}(); to play a {nameof(SoundEvent)} at one position with another owner." + "\n" +
            "\n" +
            $"Limited Globally:" + "\n" +
            "\n" +
            $"Useful if you want to limit the polyphony globally e.g for bullet impacts." + "\n" +
            "\n" +
            $"This setting will change the old owner to the new position and set the new owner as the {nameof(Transform)} of the {nameof(SoundManager)}.Instance." + "\n" +
            "\n" +
            $"Tip: If you want to limit the polyphony per owner and globally at the same time, you can use {nameof(SoundPolyGroup)}s.";

        public static string audioMixerGroupLabel = "AudioMixerGroup";
        public static string audioMixerGroupTooltip =
            $"The {nameof(AudioMixerGroup)} you want to output to." + "\n" +
            "\n" +
            $"The {nameof(SoundEvent)}s {nameof(AudioMixerGroup)} overrides the {nameof(SoundContainer)}s {nameof(AudioMixerGroup)}." + "\n" +
            "\n" +
            $"Changing {nameof(AudioMixerGroup)} for the Voice often takes a lot of performance." + "\n" +
            "\n" +
            $"Use {nameof(AudioMixerGroup)} when you want effects per group or e.g. ducking." + "\n" +
            "\n" +
            $"If you just want to control volume hierarchically look at {nameof(SoundMix)} assets for a high performance solution.";

        public static string soundMixLabel = $"{nameof(SoundMix)}";
        public static string soundMixTooltip = $"{nameof(SoundMix)} enables hierarchical control of for example volume.";

        public static string soundPolyGroupLabel = $"{nameof(SoundPolyGroup)}";
        public static string soundPolyGroupTooltip = $"{nameof(SoundPolyGroup)} gives polyphony control grouped over different {nameof(SoundEvent)} types.";

        public static string soundPolyGroupPriorityLabel = $"Priority";
        public static string soundPolyGroupPriorityTooltip = 
            $"Lower priority {nameof(SoundEvent)}s will be stolen first. " + "\n" +
            "\n" +
            $"If \"Skip Lower Priority\" is enabled on the {nameof(SoundPolyGroup)} this will determine if this {nameof(SoundEvent)} will play or not when the Polyphony Limit is reached.";

        public static string cooldownTimeLabel = $"Cooldown Time";
        public static string cooldownTimeTooltip = 
            $"How quick this {nameof(SoundEvent)} can be retriggered in seconds." + "\n" +
            "\n" +
            $"Calculated using Time.realtimeSinceStartup.";

        public static string probabilityLabel = $"Probability %";
        public static string probabilityTooltip = $"The probability that this {nameof(SoundEvent)} should play.";

        // Intensity Scaling
        public static string intensityFoldoutLabel = "Intensity";
        public static string intensityFoldoutTooltip =
            $"Settings for how any used {nameof(SoundParameterIntensity)} is scaled before it is applied to any enabled intensity options e.g Volume, Pitch etc.";

        public static string intensityAddLabel = "Add";
        public static string intensityAddTooltip = $"Adds to the {nameof(SoundParameterIntensity)}.";

        public static string intensityMultiplierLabel = "Multiplier";
        public static string intensityMultiplierTooltip = $"Multiplier of the {nameof(SoundParameterIntensity)}.";

        public static string intensityRolloffLabel = "Rolloff";
        public static string intensityRolloffTooltip = $"The power of the rolloff.\n\n0 is linear.";

        public static string intensitySeekTimeLabel = "Seek Time";
        public static string intensitySeekTimeTooltip =
            $"The seek time of the {nameof(SoundParameterIntensity)} in seconds ." + "\n" +
            "\n" +
            $"Uses Time.realtimeSinceStartup.";

        public static string intensityCurveLabel = "Curve";
        public static string intensityCurveTooltip = "Curve of the intensity.\n\nFrom 0 (soft) to 1 (hard).";

        public static string intensityThresholdEnableLabel = "Enable Threshold";
        public static string intensityThresholdEnableTooltip =
            $"If this {nameof(SoundEvent)} is played with a {nameof(SoundParameterIntensity)} and it is under the threshold when starting it won't be played.";

        public static string intensityThresholdLabel = "Threshold";
        public static string intensityThresholdTooltip = "The threshold limit after scaling the intensity value.";

        public static string intensityDebugLabel = "Intensity Record";
        public static string intensityDebugTooltip =
            $"If enabled it will record all {nameof(SoundParameterIntensity)} used when playing this {nameof(SoundEvent)}.";

        public static string intensityDebugScaleValuesLabel = "Scale Values to 0 to 1 Range";
        public static string intensityDebugScaleValuesTooltip =
            "First sets Intensity Add so the lowest value is 0." + "\n" +
            "\n" +
            "Then sets Intensity Multiply so that the highest value is 1.";

        public static string intensityDebugResolutionLabel = "Debug Resolution";
        public static string intensityDebugResolutionTooltip = "The resolution of the displayed values.";

        // Intensity Settings Warnings
        public static string intensityDebugRecordLabel = "Record available in Play Mode";
        public static string intensityDebugRecordTooltip = "To record intensity debug data, play the game with Intensity Record enabled.";

        public static string intensityValuesRecordedLabel = "Values Recorded";
        public static string intensityValuesRecordedTooltip =
            "If too many values are recorded, drawing the debug values might be slow." + "\n" +
            "\n" +
            "If so, clear the logged values to speed it up.";

        // TriggerOn
        public static string triggerOnWhichToPlayLabel = $"Which to Play";
        public static string triggerOnWhichToPlayTooltip =
            $"If Play All is selected, then all assigned {nameof(SoundEvent)}s will be played." + "\n" +
            "\n" +
            $"If One Random is selected, then one random of the assigned {nameof(SoundEvent)}s will be played." + "\n" +
            "\n" +
            $"The randomizer uses a pseudo random function remembering which {nameof(SoundEvent)}s it last played to avoid repetition.";

        public static string triggerOnPlayLabel = $"Trigger On Play";
        public static string triggerOnPlayTooltip =
            $"Triggers another {nameof(SoundEvent)} when this {nameof(SoundEvent)} is played.";

        public static string triggerOnStopLabel = $"Trigger On Stop";
        public static string triggerOnStopTooltip =
            $"Triggers another {nameof(SoundEvent)} when this {nameof(SoundEvent)} is stopped.";

        public static string triggerOnTailLabel = $"Trigger On Tail";
        public static string triggerOnTailTooltip = 
            $"Triggers another {nameof(SoundEvent)} \"Tail Length\" before the end." + "\n" +
            "\n" +
            $"It looks at the time of the last played voice on the first {nameof(SoundContainer)}." + "\n" + 
            "\n" +
            $"Useful for music, e.g. if you have an intro with a 2 second tail and a loop you want to play on the tail of the intro." + "\n" +
            "\n" +
            $"If you play with {nameof(SoundManager.PlayMusic)}, you can stop the next {nameof(SoundEvent)} with {nameof(SoundManager.StopAllMusic)} without a reference." + "\n" +
            "\n" +
            $"If you want it to trigger itself, make sure to set the \"Settings\" polyphony to 2." + "\n" +
            "\n" +
            $"If the trigger timing is not tight enough, try setting the AudioClip \"Compression Format\" to PCM or ADPCM (Vorbis is less accurate).";

        public static string triggerOnTailTailLengthLabel = $"Tail Length";
        public static string triggerOnTailTailLengthTooltip =
            $"How long in seconds before the end of the {nameof(SoundEvent)} to trigger the next {nameof(SoundEvent)}." + "\n" +
            "\n" +
            $"It looks at the time of the last played voice on the first {nameof(SoundContainer)}." + "\n" + 
            "\n" +
            $"It takes into account pitch when calculating time." + "\n" +
            "\n" +
            $"For example, if the tail length is 2 seconds and you pitch it +12 semitones (2x speed) the internal tail length will be 1 seconds." + "\n" +
            "\n" +
            $"This is because double the speed with half the duration and vice versa.";

        public static string triggerOnTailSetTailLengthFromBpmLabel = $"Set Tail Length from BPM & Beats";
        public static string triggerOnTailSetTailLengthFromBpmTooltip = $"Calculates the Tail Length from the BPM and the Beats settings.";

        public static string triggerOnTailBpmLabel = $"BPM";
        public static string triggerOnTailBpmTooltip = $"Beats per minute.";

        public static string triggerOnTailTailLengthInBeatsLabel = $"Beats";
        public static string triggerOnTailTailLengthInBeatsTooltip = $"How long the tail is in beats.";

        // SoundTag
        public static string soundTagEnableLabel = $"{nameof(SoundTag)}";
        public static string soundTagEnableTooltip = 
            $"Uses {nameof(SoundTag)} to play other {nameof(SoundEvent)}s and/or change {nameof(SoundEventModifier)}." + "\n" +
            "\n" +
            $"The {nameof(SoundTag)} won't be passed to the {nameof(SoundEvent)}s of the {nameof(SoundTag)} in order to avoid infinite repetitions.";

        public static string soundTagModeLabel = $"Mode";
        public static string soundTagModeTooltip =
            $"If local {nameof(SoundTag)} is selected you need to pass an {nameof(SoundTag)} when playing the {nameof(SoundEvent)}." + "\n" +
            "\n" +
            $"If global {nameof(SoundTag)} is selected you need to set the {nameof(SoundTag)} on the {nameof(SoundManager)}.";

        public static string soundTagLabel = $"{nameof(SoundTag)}";
        public static string soundTagTooltip = $"If this {nameof(SoundTag)} is selected the {nameof(SoundEvent)}s below will be played.";
    }
}
#endif