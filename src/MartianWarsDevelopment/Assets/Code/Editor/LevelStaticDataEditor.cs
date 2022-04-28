using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string PlayerInitialPoint = "PlayerInitialPoint";
    private const string ButtonName = "Collect";

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData) target;

      if (GUILayout.Button(ButtonName))
        levelData.PlayerInitialPosition = GameObject.FindWithTag(PlayerInitialPoint).transform.position;
    }
    
  }
}