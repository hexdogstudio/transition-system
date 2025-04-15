using System.IO;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEditor;

namespace Contrast.Editor
{
    public static class BackgroundRecompiler
    {
        public static bool Enabled = true;
        public static bool DebugMode = false;
        public static bool LogWhenBackgroundCompiled = true;
        private static bool shouldRecompile = false;

        private static FileSystemWatcher watcher;

        private static MethodInfo CanReloadAssembliesMethod;
        private static bool IsAssemblyLocked
        {
            get
            {
                if (CanReloadAssembliesMethod == null)
                {
                    CanReloadAssembliesMethod = typeof(EditorApplication).GetMethod("CanReloadAssemblies", BindingFlags.NonPublic | BindingFlags.Static);

                    if (CanReloadAssembliesMethod == null)
                    {
                        Debug.LogError("Can't find CanReloadAssemblies method. It might have been renamed or removed.");
                    }
                }

                return !(bool)CanReloadAssembliesMethod.Invoke(null, null);
            }
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            LoadPrefs();

            SetActive(Enabled);
        }

        private static void OnUpdate()
        {
            if (!shouldRecompile) return;
            if (EditorApplication.isCompiling) return;
            if (EditorApplication.isUpdating) return;


            if (!Enabled)
            {
                shouldRecompile = false;
                return;
            }

            if (IsAssemblyLocked)
            {
                shouldRecompile = false;
                return;
            }


            AssetDatabase.Refresh();
            shouldRecompile = false;
        }


        public static void SetActive(bool newState)
        {
            Enabled = newState;

            if (newState)
            {
                var timer1 = System.Diagnostics.Stopwatch.StartNew();

                if (watcher != null)
                    watcher.Dispose();
                var dataPath = Application.dataPath;
                var thread = new Thread(
                    () =>
                    {
                        var timer2 = System.Diagnostics.Stopwatch.StartNew();
                        watcher = new FileSystemWatcher(dataPath, "*.cs")
                        {
                            NotifyFilter =
                                NotifyFilters.LastAccess |
                                NotifyFilters.LastWrite |
                                NotifyFilters.FileName |
                                NotifyFilters.DirectoryName,
                            IncludeSubdirectories = true,
                            EnableRaisingEvents = true,
                        };

                        watcher.Changed += OnScriptFileChange;
                        watcher.Created += OnScriptFileChange;
                        watcher.Deleted += OnScriptFileChange;
                        watcher.Renamed += OnScriptFileChange;

                        watcher.Disposed += (sender, args) =>
                        {
                            watcher.Changed -= OnScriptFileChange;
                            watcher.Created -= OnScriptFileChange;
                            watcher.Deleted -= OnScriptFileChange;
                            watcher.Renamed -= OnScriptFileChange;
                        };

                        timer2.Stop();
                    }
                );
                thread.Start();


                EditorApplication.update += OnUpdate;
                timer1.Stop();
            }
            else
            {
                if (watcher != null)
                    watcher.Dispose();
                EditorApplication.update -= OnUpdate;
            }
        }

        private static void OnScriptFileChange(object sender, FileSystemEventArgs e)
        {
            shouldRecompile = true;
        }



        #region Preferences
        private const string PREFS_KEY = "BACKGROUND_RECOMPILER_";
        private const string PREFS_KEY_ENABLED = PREFS_KEY + "ENABLED";
        private const string PREFS_KEY_DEBUG_MODE = PREFS_KEY + "DEBUG_MODE";
        private const string PREFS_KEY_LOG_COMPILES = PREFS_KEY + "LOG_COMPILES";

        public static void SavePrefs()
        {
            EditorPrefs.SetBool(PREFS_KEY_ENABLED, Enabled);
            EditorPrefs.SetBool(PREFS_KEY_DEBUG_MODE, DebugMode);
            EditorPrefs.SetBool(PREFS_KEY_LOG_COMPILES, LogWhenBackgroundCompiled);
        }
        public static void LoadPrefs()
        {
            Enabled = EditorPrefs.GetBool(PREFS_KEY_ENABLED, Enabled);
            DebugMode = EditorPrefs.GetBool(PREFS_KEY_DEBUG_MODE, DebugMode);
            LogWhenBackgroundCompiled = EditorPrefs.GetBool(PREFS_KEY_LOG_COMPILES, LogWhenBackgroundCompiled);
        }
        #endregion Preferences


        // Preferences Window Entry
        internal class PluginPrefsProvider : SettingsProvider
        {
            private const string PreferencePath = "Plugins/Background Recompiler";


            private static PluginPrefsProvider provider;


            private PluginPrefsProvider(string path, SettingsScope scope)
                : base(path, scope) { }


            public override void OnGUI(string searchContext)
            {
                EditorGUI.BeginChangeCheck();

                GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                EditorGUIUtility.labelWidth += 100;
                EditorGUILayout.BeginVertical();


                // draw pref fields
                var enabled = EditorGUILayout.Toggle("Enabled", Enabled);
                if (enabled != Enabled)
                {
                    SetActive(enabled);
                }
                var debugMode = EditorGUILayout.Toggle("Debug Mode", DebugMode);
                if (debugMode != DebugMode)
                {
                    DebugMode = debugMode;
                }

                LogWhenBackgroundCompiled = EditorGUILayout.Toggle("Log When Background Compiled", LogWhenBackgroundCompiled);



                EditorGUILayout.EndVertical();
                EditorGUIUtility.labelWidth -= 100;
                GUILayout.Space(10);
                EditorGUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.Space(5);

                if (EditorGUI.EndChangeCheck())
                {
                    SavePrefs();
                }
            }


            [SettingsProvider]
            private static SettingsProvider GetSettingsProvider()
            {
                if (provider == null)
                {
                    provider = new PluginPrefsProvider(PreferencePath, SettingsScope.User);
                }

                return provider;
            }
        }
    }
}