// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

namespace Sonity.Internal {

    public static class ShouldDebug {

        public static bool Warnings() {
#if DEBUG
            return SoundManager.Instance == null || SoundManager.Instance.Internals.settings.GetShouldDebugWarnings();
#else
            return false;
#endif
        }

#if UNITY_EDITOR
        public static bool GuiWarnings() {
            return SoundManager.Instance == null || SoundManager.Instance.Internals.settings.GetShouldDebugGuiWarnings();
        }
#endif
    }
}