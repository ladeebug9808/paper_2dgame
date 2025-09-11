using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SFX))]
public class SFX_Gui : Editor
{
    private bool showRandomOptions = true;
    private bool showFadeOptions = true;

    public override void OnInspectorGUI()
    {
        SFX sfx = (SFX)target;

        EditorGUI.BeginChangeCheck(); // Track changes

        // --- Play On Awake ---
        sfx.playOnAwake = EditorGUILayout.Toggle("Play On Awake", sfx.playOnAwake);

        EditorGUILayout.Space();

        // --- Randomization Options ---
        showRandomOptions = EditorGUILayout.Foldout(showRandomOptions, "Randomization Options", true);
        if (showRandomOptions)
        {
            EditorGUILayout.BeginVertical("box");

            // Random Pitch
            sfx.randomPitch = GUILayout.Toggle(sfx.randomPitch, "Random Pitch", "Button");
            if (sfx.randomPitch)
            {
                float minPitch = sfx.pitchRange.x;
                float maxPitch = sfx.pitchRange.y;
                EditorGUILayout.MinMaxSlider("Pitch Range", ref minPitch, ref maxPitch, 0.1f, 3f);

                EditorGUILayout.BeginHorizontal();
                minPitch = EditorGUILayout.FloatField("Min", minPitch);
                maxPitch = EditorGUILayout.FloatField("Max", maxPitch);
                EditorGUILayout.EndHorizontal();

                sfx.pitchRange = new Vector2(Mathf.Min(minPitch, maxPitch), Mathf.Max(minPitch, maxPitch));
            }

            EditorGUILayout.Space();

            // Random Volume
            sfx.randomVolume = GUILayout.Toggle(sfx.randomVolume, "Random Volume", "Button");
            if (sfx.randomVolume)
            {
                float minVol = sfx.volumeRange.x;
                float maxVol = sfx.volumeRange.y;
                EditorGUILayout.MinMaxSlider("Volume Range", ref minVol, ref maxVol, 0f, 1f);

                EditorGUILayout.BeginHorizontal();
                minVol = EditorGUILayout.FloatField("Min", minVol);
                maxVol = EditorGUILayout.FloatField("Max", maxVol);
                EditorGUILayout.EndHorizontal();

                sfx.volumeRange = new Vector2(Mathf.Min(minVol, maxVol), Mathf.Max(minVol, maxVol));
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        // --- Fade Options ---
        showFadeOptions = EditorGUILayout.Foldout(showFadeOptions, "Fade Options", true);
        if (showFadeOptions)
        {
            EditorGUILayout.BeginVertical("box");

            sfx.useFade = GUILayout.Toggle(sfx.useFade, "Enable Fading", "Button");
            if (sfx.useFade)
            {
                sfx.fadeInDuration = EditorGUILayout.FloatField("Fade In Duration", sfx.fadeInDuration);
                sfx.fadeOutDuration = EditorGUILayout.FloatField("Fade Out Duration", sfx.fadeOutDuration);
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        // --- Preview SFX Button ---
        

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(sfx);
        }
    }
}
